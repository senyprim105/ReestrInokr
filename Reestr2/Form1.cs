using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Reestr2.Model;
using System.IO.Compression;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using Reestr2.VisualModel;
using Reestr2.VisualModel;

namespace Reestr2
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }
        //документ в номерами счетов (может в репозитарий переместить?)
        public XDocument chets = null;
        //Католог из каторого запустили программу
        public string curentDir = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        //Класс с данными о загруженных пакетах для грида
        public List<PacketsLine> VisualGrid = new List<PacketsLine>();
        //фильт по МО выставленный в комбобоксе 
        public int FilterMo { get; set; }
        //фильтр по месяцу выставленный в комбобоксе
        public int CurentMonth { get; set; }
        //ID Выбеленныx пакетов в гриде
        public List<int> SelectedPacketsId
        {
            get
            {
                List<int> result = new List<int>();
                for (int i = 0; i < PaketGridView.SelectedRows.Count; i++)
                {
                    result.Add((PaketGridView.SelectedRows[i].DataBoundItem as PacketsLine).PacketId);
                }
                return result;

            }
            private set { }
        }
        //Текущий ID выделенный в гриде
        public int CurentPacketId { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Устанавливаем при запуске фильтры
            cmbMonth.SelectedItem = "Все месяцы";
            cmbMo.SelectedItem = "Все";
            //Обновляем грид
            UpdateGrid();
            //Грузим хмл со счетами если файл есть
            var chetfile = Path.Combine(curentDir, "Data", "Chets2019.xml");
            if (File.Exists(chetfile))
                chets = XDocument.Load(Path.Combine(curentDir, "Data", "Chets2019.xml"));
            
        }

        //Обновляем сетку
        public void UpdateGrid()
        {
            
            PrintHeader();
            //Перечитываем данные
            VisualGrid = PacketsLines.GetPacketsLines(FilterMo, CurentMonth);
            //Переустанавливаем датасурс биндинга - так как данные меняются полность то только так получилось отображать изменения
            PacketBS.DataSource = VisualGrid;

        }

        //Какие то действия при обновлени
        public void PrintHeader()
        {
            this.Text = $"Текущий реестр с Id={SelectedPacketsId}  Фильтр Месяц={CurentMonth} Mo={FilterMo} ";

        }

        //Загружает пакет по полному пути - выводит список ошибок закрузки
        public List<string> AddPacket(string filename)
        {
            List<string> error = new List<string>();
            int AddPacketId = -1;
            using (reestr db = reestr.GetContext(null))
            {
                if (Repo.ContainsPacket(filename, db)) error.Add($"Пакет {Path.GetFileNameWithoutExtension(filename)} не залит так уже существует");
                else
                {
                    var packet = new Packet(new FileInfo(filename));
                    if (packet.errors.Any()) error.Add($"Пакет {Path.GetFileNameWithoutExtension(filename)} не залит - {string.Join("\n", packet.errors)}");
                    else
                    {
                        db.Packets.Add(packet);
                        db.SaveChanges();
                        AddPacketId = packet.PacketId;
                    }
                }
            }
            if (AddPacketId >= 0) Repo.Validate(chets, AddPacketId);
            UpdateGrid();
            return error;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    errors.AddRange(AddPacket(openFileDialog1.FileName));
                }
                catch (FieldAccessException ex)
                {
                    MessageBox.Show($"Пакет не залит. Ошибка - {ex.Message}");
                }
                catch (DbUpdateException ex)
                {
                    var ie = ex.InnerException;
                    List<string> mess = new List<string>();
                    while (ie.InnerException != null)
                        mess.Add((ie = ie.InnerException).Message);
                    MessageBox.Show($"Ошибка SQL сервера - {string.Join("\n", mess)}");
                }
            MessageBox.Show(string.Join("\n", errors));
        }

        //Удаление пакета по списку ID - возвращает список ошибок
        public List<string> DelPacket(List<int> PacketIdList, reestr db = null)
        {
            List<string> errors = new List<string>();
            using (var dbc = reestr.GetContext(db))
            {
                foreach (var PacketId in PacketIdList)
                {
                    try
                    {
                        if (!Repo.ContainsPacket(PacketId, dbc)) errors.Add($"Невозможно удалить пакте с Id={PacketId} так как его не существует");
                        else
                        {
                            dbc.Packets.Remove(dbc.Packets.FirstOrDefault(a => a.PacketId == PacketId));
                            dbc.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        errors.Add($"Невозможно удалить пакте с Id={PacketId} из-за ошибки - {e.Message}");
                    }
                }
            }
            return errors;
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (SelectedPacketsId.Count > 0) errors = DelPacket(SelectedPacketsId);
            if (errors.Count > 0) MessageBox.Show(string.Join("\n", errors));
            UpdateGrid();
        }

        private void cmbMo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            if (int.TryParse((string)combo.SelectedItem.ToString(), out int _)) FilterMo = int.Parse((string)combo.SelectedItem.ToString());
            else FilterMo = 0;
            UpdateGrid();
        }
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            CurentMonth = combo.SelectedIndex + 1;
            if (CurentMonth > 12) CurentMonth = 0;
            UpdateGrid();
        }
        private void PacketBS_CurrentChanged(object sender, EventArgs e)
        {
            CurentPacketId = ((sender as BindingSource).Current as PacketsLine)?.PacketId ?? -1;
            PrintHeader();
        }

        //Сохраняет выделенные реестры в выбранное место
        private void btnCreateReestr_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (DLGFolder.ShowDialog() == DialogResult.OK)
            {
                foreach (var PacketId in SelectedPacketsId)
                {
                    var reestr = new VisualReestr(PacketId);
                    errors.AddRange(SaveReestrXls.SaveReestr(reestr,
                        new FileInfo(Path.Combine(curentDir, "Shablon", "Shablon.xls")),
                        Path.Combine(DLGFolder.SelectedPath, SaveReestrXls.GetNameReestr(reestr)))
                        );
                    Application.DoEvents();

                }
            }
            if (errors.Count>0) MessageBox.Show(string.Join("\n", errors));
        }

        //Происходит, когда пользователь отпускает мышь над мишенью 
        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            List<string> errors = new List<string>();
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            foreach (var file in files)
            {
                try
                {
                    errors.AddRange(AddPacket(file));
                }
                catch (Exception exc)
                {
                    errors.Add($"Не удалось залить реестр {Path.GetFileNameWithoutExtension(file)} - {exc.Message}");
                }
            }
            if (errors.Count > 0) MessageBox.Show(string.Join("\n", errors));
        }
        //Это событие происходит, когда пользователь перетаскивает элемент управления перетаскиванием мышью во время операции перетаскивания. 
        private void dataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            bool result = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                result = files.All(a => Packet.GetErrPacket(Path.GetFileName(a)).Count == 0);
                e.Effect = result ? DragDropEffects.All : DragDropEffects.None;
            }
        }

        //Подкрашивает Пакеты которые нашлись в списке счетов
        private void PacketsGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < PaketGridView.RowCount)
            {
                if (!string.IsNullOrEmpty(((DataGridView)sender).Rows[e.RowIndex].Cells[7].Value?.ToString()))
                {
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkGreen;

                }

            }
        }

    }
}
