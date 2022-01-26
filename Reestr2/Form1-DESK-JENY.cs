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

namespace Reestr2
{
    public partial class Form1 : Form
    {
        string packet = "";
        int PacketId = 0;
        int Pmonth = 0;
        int K_lpu = 0;
        string TypePacket = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            UpdateList();
        }
        private void UpdateList()
        {
            var curent = new { packetId = ((PacketsLine)packetsLineBindingSource.Current)?.packetId,
                packetsLineBindingSource.Position };
            packetsLineBindingSource.Clear();
            Repo.GetPacket( (a)=>(K_lpu>0?a.k_lpu==K_lpu:true) && (Pmonth>0?a.month==Pmonth:true)).ForEach(a => { packetsLineBindingSource.Add(a); });
            if (packetsLineBindingSource.Count > 0)
            {
                int? newcurent = curent.packetId == null ? 0 : packetsLineBindingSource.List.Cast<PacketsLine>().ToArray().Select((a, b) => new { ind = b, val = a.packetId }).FirstOrDefault(a => a.val == curent.packetId)?.ind;
                newcurent = newcurent == null ? (packetsLineBindingSource.Count > curent.Position) ? curent.Position : packetsLineBindingSource.Count - 1 : newcurent;
                packetsLineBindingSource.Position = (int)newcurent;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> times = new List<string>();
            Stopwatch sw = new Stopwatch();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                packet = openFileDialog1.FileName;
                using (reestr db = new reestr())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    sw.Start();
                    Packet pac = new Packet(new FileInfo(packet));
                    if (pac.errors != null) MessageBox.Show(string.Join("/n/r", pac.errors));
                    sw.Stop();
                    times.Add($"Десириализация заняла {sw.ElapsedMilliseconds / 1000} секунд");
                    sw.Reset();
                    sw.Start();
                    db.Packets.Add(pac);
                    sw.Stop();
                    times.Add($"Копирование в объескты EF заняла {sw.ElapsedMilliseconds / 1000} секунд");
                    sw.Reset();
                    sw.Start();
                    db.SaveChanges();
                    sw.Stop();
                    times.Add($"Копирование в базу заняло {sw.ElapsedMilliseconds / 1000} секунд");
                    
                }
                MessageBox.Show(string.Join("/n", times));
            }
            UpdateList();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string zip = openFileDialog1.FileName;
                using (ZipArchive archive = ZipFile.OpenRead(zip))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        using (reestr db = new reestr())
                        {
                            db.Packets.First(a => a.PacketId == PacketId).AddAr(entry);
                            db.SaveChanges();
                        }
                    }
                }
                UpdateList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            if (packetsLineBindingSource.Current != null)
            {
                var packet = (PacketsLine) packetsLineBindingSource.Current;
                using (reestr db = new reestr()) {
                    db.Packets.Remove(db.Packets.Where(a => a.PacketId == packet.packetId).FirstOrDefault());
                    db.SaveChanges();
                }
                UpdateList();
            }
        }

        private void packetsLineBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            int? temp= (packetsLineBindingSource.Current as PacketsLine)?.packetId;
            if (temp != PacketId) PacketId = temp ?? 0;
           // using (reestr db = new reestr()) MessageBox.Show(db.Packets.Include("XmlReestrs").FirstOrDefault(a => a.PacketId == PacketId).XmlReestrs.Count.ToString());
            this.Text = PacketId.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                var folder = folderBrowserDialog1.SelectedPath;
                int id = 0;
                using (reestr db = new reestr())
                    foreach (var reestr in db.Packets.Where(a => a.PacketId == PacketId).First().XmlReestrs)
                    {
                        if (reestr.Type == "V") continue;
                        var rr = XmlReestr.GetReestr(reestr.ReestrId);
                        var filename = rr.FileName;
                        var settings = new XmlWriterSettings{Encoding = Encoding.GetEncoding(1251),Indent = true};
                        using (var writer = XmlWriter.Create(Path.Combine(folder, filename), settings))
                        {
                            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                            ns.Add("", "");
                            XmlSerializer ser = rr.Type[0] == 'H' ? new XmlSerializer(typeof(HCode)) :
                                                rr.Type[0] == 'P' ? new XmlSerializer(typeof(PCode)) :
                                                rr.Type[0] == 'C' ? new XmlSerializer(typeof(CCode)) :
                                                rr.Type[0] == 'L' ? new XmlSerializer(typeof(LCode)) : null;

                            ser.Serialize(writer, rr, ns);
                            ser = null;
                        }
                    }
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            if (int.TryParse((string)combo.SelectedItem, out int _)) Pmonth = int.Parse((string)combo.SelectedItem);
            else Pmonth = 0;
            this.Text = $" IDRST={PacketId.ToString()} PMONTH={Pmonth} ЛПУ={K_lpu}";
            UpdateList();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            if (int.TryParse((string)combo.Items[combo.SelectedIndex], out int _)) K_lpu = int.Parse((string)combo.Items[combo.SelectedIndex]);
            else K_lpu = 0;
            this.Text = $" IDRST={PacketId.ToString()} PMONTH={Pmonth} ЛПУ={K_lpu}";
            UpdateList();
        }
    }
}
