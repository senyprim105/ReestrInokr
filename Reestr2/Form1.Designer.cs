namespace Reestr2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveReestr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMo = new System.Windows.Forms.ComboBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DLGFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PaketGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NChet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DChet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SummChet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PacketBS = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaketGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PacketBS)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveReestr);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbMo);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 45);
            this.panel1.TabIndex = 0;
            // 
            // btnSaveReestr
            // 
            this.btnSaveReestr.Location = new System.Drawing.Point(191, 3);
            this.btnSaveReestr.Name = "btnSaveReestr";
            this.btnSaveReestr.Size = new System.Drawing.Size(100, 36);
            this.btnSaveReestr.TabIndex = 8;
            this.btnSaveReestr.Text = "Сформировать реестр";
            this.btnSaveReestr.UseVisualStyleBackColor = true;
            this.btnSaveReestr.Click += new System.EventHandler(this.btnCreateReestr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(441, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Месяц";
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь",
            "Все месяцы"});
            this.cmbMonth.Location = new System.Drawing.Point(497, 12);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(89, 21);
            this.cmbMonth.TabIndex = 6;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ЛПУ";
            // 
            // cmbMo
            // 
            this.cmbMo.FormattingEnabled = true;
            this.cmbMo.Items.AddRange(new object[] {
            "250426",
            "250433",
            "250443",
            "250452",
            "250458",
            "250473",
            "250477",
            "250700",
            "250718",
            "250755",
            "250777",
            "Все"});
            this.cmbMo.Location = new System.Drawing.Point(334, 12);
            this.cmbMo.Name = "cmbMo";
            this.cmbMo.Size = new System.Drawing.Size(89, 21);
            this.cmbMo.TabIndex = 4;
            this.cmbMo.SelectedIndexChanged += new System.EventHandler(this.cmbMo_SelectedIndexChanged);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(96, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(88, 36);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "Удалить пакет";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Загрузить пакет";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PaketGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(599, 458);
            this.panel2.TabIndex = 1;
            // 
            // PaketGridView
            // 
            this.PaketGridView.AllowDrop = true;
            this.PaketGridView.AllowUserToAddRows = false;
            this.PaketGridView.AllowUserToOrderColumns = true;
            this.PaketGridView.AutoGenerateColumns = false;
            this.PaketGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PaketGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.Type,
            this.Num,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.NChet,
            this.DChet,
            this.SummChet,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn7});
            this.PaketGridView.DataSource = this.PacketBS;
            this.PaketGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaketGridView.Location = new System.Drawing.Point(0, 0);
            this.PaketGridView.Name = "PaketGridView";
            this.PaketGridView.ReadOnly = true;
            this.PaketGridView.RowHeadersWidth = 5;
            this.PaketGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PaketGridView.Size = new System.Drawing.Size(599, 458);
            this.PaketGridView.TabIndex = 2;
            this.PaketGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.PacketsGrid_RowPrePaint);
            this.PaketGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView2_DragDrop);
            this.PaketGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView2_DragEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PacketId";
            this.dataGridViewTextBoxColumn1.HeaderText = "PacketId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Mo";
            this.dataGridViewTextBoxColumn2.HeaderText = "Код МО";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Month";
            this.dataGridViewTextBoxColumn5.HeaderText = "Месяц";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Тип пакета";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Num
            // 
            this.Num.DataPropertyName = "Num";
            this.Num.HeaderText = "№счета Барс";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Count";
            this.dataGridViewTextBoxColumn8.HeaderText = "Кол-во записей";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "MnAll";
            this.dataGridViewTextBoxColumn9.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // NChet
            // 
            this.NChet.DataPropertyName = "NChet";
            this.NChet.HeaderText = "№счета";
            this.NChet.Name = "NChet";
            this.NChet.ReadOnly = true;
            // 
            // DChet
            // 
            this.DChet.DataPropertyName = "DChet";
            this.DChet.HeaderText = "Дата счета";
            this.DChet.Name = "DChet";
            this.DChet.ReadOnly = true;
            // 
            // SummChet
            // 
            this.SummChet.DataPropertyName = "SummChet";
            this.SummChet.HeaderText = "Сумма счета";
            this.SummChet.Name = "SummChet";
            this.SummChet.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "LoadDate";
            this.dataGridViewTextBoxColumn6.HeaderText = "Дата загрузки";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "MoName";
            this.dataGridViewTextBoxColumn3.HeaderText = "Имя МО";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn4.HeaderText = "Год";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn7.HeaderText = "Имя пакета";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // PacketBS
            // 
            this.PacketBS.DataMember = "DataSource";
            this.PacketBS.DataSource = typeof(Reestr2.VisualModel.PacketsLines);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 503);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PaketGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PacketBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.FolderBrowserDialog DLGFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idReestrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lpuDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.BindingSource PacketBS;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loadDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mnAllDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnSaveReestr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView PaketGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn NChet;
        private System.Windows.Forms.DataGridViewTextBoxColumn DChet;
        private System.Windows.Forms.DataGridViewTextBoxColumn SummChet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}

