namespace spLogListener
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.selectSpLogsBtn = new System.Windows.Forms.Button();
            this.readSpLogsBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.allCashInBtn = new System.Windows.Forms.Button();
            this.clearTextBoxesBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchKeywordBtn = new System.Windows.Forms.Button();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listPhyDevBtn = new System.Windows.Forms.Button();
            this.findErrorCodesBtn = new System.Windows.Forms.Button();
            this.iStoreMoneyandiRetractBtn = new System.Windows.Forms.Button();
            this.idcErrorsBtn = new System.Windows.Forms.Button();
            this.eventMsgBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(320, 31);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(210, 438);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox2.Location = new System.Drawing.Point(536, 31);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(234, 438);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            //this.richTextBox2.ContextMenuStripChanged += new System.EventHandler(this.richTextBox2_ContextMenuStripChanged);
            this.richTextBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox2_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.copyToolStripMenuItem_MouseDown);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // selectSpLogsBtn
            // 
            this.selectSpLogsBtn.Location = new System.Drawing.Point(9, 70);
            this.selectSpLogsBtn.Name = "selectSpLogsBtn";
            this.selectSpLogsBtn.Size = new System.Drawing.Size(103, 23);
            this.selectSpLogsBtn.TabIndex = 7;
            this.selectSpLogsBtn.Text = "Select SP Logs";
            this.selectSpLogsBtn.UseVisualStyleBackColor = true;
            this.selectSpLogsBtn.Click += new System.EventHandler(this.button5_Click);
            // 
            // readSpLogsBtn
            // 
            this.readSpLogsBtn.Location = new System.Drawing.Point(9, 227);
            this.readSpLogsBtn.Name = "readSpLogsBtn";
            this.readSpLogsBtn.Size = new System.Drawing.Size(103, 27);
            this.readSpLogsBtn.TabIndex = 8;
            this.readSpLogsBtn.Text = "Read SP Logs";
            this.readSpLogsBtn.UseVisualStyleBackColor = true;
            this.readSpLogsBtn.Click += new System.EventHandler(this.readSpLogsBtn_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(9, 11);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(103, 55);
            this.button5.TabIndex = 9;
            this.button5.Text = "Find End of The Day Operations";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // allCashInBtn
            // 
            this.allCashInBtn.Location = new System.Drawing.Point(9, 144);
            this.allCashInBtn.Name = "allCashInBtn";
            this.allCashInBtn.Size = new System.Drawing.Size(103, 36);
            this.allCashInBtn.TabIndex = 10;
            this.allCashInBtn.Text = "All CashIn Transactions";
            this.allCashInBtn.UseVisualStyleBackColor = true;
            this.allCashInBtn.Click += new System.EventHandler(this.allCashInBtn_Click);
            // 
            // clearTextBoxesBtn
            // 
            this.clearTextBoxesBtn.Location = new System.Drawing.Point(9, 186);
            this.clearTextBoxesBtn.Name = "clearTextBoxesBtn";
            this.clearTextBoxesBtn.Size = new System.Drawing.Size(103, 36);
            this.clearTextBoxesBtn.TabIndex = 12;
            this.clearTextBoxesBtn.Text = "Clear TextBoxes";
            this.clearTextBoxesBtn.UseVisualStyleBackColor = true;
            this.clearTextBoxesBtn.Click += new System.EventHandler(this.clearTextBoxesBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(118, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(196, 458);
            this.dataGridView1.TabIndex = 13;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Physical Devices";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 188;
            // 
            // searchKeywordBtn
            // 
            this.searchKeywordBtn.Location = new System.Drawing.Point(9, 349);
            this.searchKeywordBtn.Name = "searchKeywordBtn";
            this.searchKeywordBtn.Size = new System.Drawing.Size(103, 23);
            this.searchKeywordBtn.TabIndex = 14;
            this.searchKeywordBtn.Text = "Search";
            this.searchKeywordBtn.UseVisualStyleBackColor = true;
            this.searchKeywordBtn.Click += new System.EventHandler(this.searchKeywordBtn_Click_1);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(779, 29);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(249, 253);
            this.richTextBox3.TabIndex = 15;
            this.richTextBox3.Text = "";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Location = new System.Drawing.Point(776, 330);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(249, 139);
            this.richTextBox4.TabIndex = 16;
            this.richTextBox4.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "All CashIn Transactions";
            //this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Comparison Correct Amount";
            //this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(776, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Comparison Wrong Amount";
            //this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(776, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "SP log/logs Total Deposit";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 306);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(103, 20);
            this.textBox1.TabIndex = 21;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 330);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Exclude";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // listPhyDevBtn
            // 
            this.listPhyDevBtn.Location = new System.Drawing.Point(9, 99);
            this.listPhyDevBtn.Name = "listPhyDevBtn";
            this.listPhyDevBtn.Size = new System.Drawing.Size(103, 39);
            this.listPhyDevBtn.TabIndex = 23;
            this.listPhyDevBtn.Text = "Physical Devs";
            this.listPhyDevBtn.UseVisualStyleBackColor = true;
            this.listPhyDevBtn.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // findErrorCodesBtn
            // 
            this.findErrorCodesBtn.Location = new System.Drawing.Point(9, 376);
            this.findErrorCodesBtn.Name = "findErrorCodesBtn";
            this.findErrorCodesBtn.Size = new System.Drawing.Size(103, 35);
            this.findErrorCodesBtn.TabIndex = 24;
            this.findErrorCodesBtn.Text = "Find Error Codes";
            this.findErrorCodesBtn.UseVisualStyleBackColor = true;
            this.findErrorCodesBtn.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // iStoreMoneyandiRetractBtn
            // 
            this.iStoreMoneyandiRetractBtn.Location = new System.Drawing.Point(9, 258);
            this.iStoreMoneyandiRetractBtn.Name = "iStoreMoneyandiRetractBtn";
            this.iStoreMoneyandiRetractBtn.Size = new System.Drawing.Size(103, 43);
            this.iStoreMoneyandiRetractBtn.TabIndex = 25;
            this.iStoreMoneyandiRetractBtn.Text = "iStoreMoney + iRetract";
            this.iStoreMoneyandiRetractBtn.UseVisualStyleBackColor = true;
            this.iStoreMoneyandiRetractBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // idcErrorsBtn
            // 
            this.idcErrorsBtn.Location = new System.Drawing.Point(9, 416);
            this.idcErrorsBtn.Name = "idcErrorsBtn";
            this.idcErrorsBtn.Size = new System.Drawing.Size(103, 23);
            this.idcErrorsBtn.TabIndex = 27;
            this.idcErrorsBtn.Text = "IDC Errors";
            this.idcErrorsBtn.UseVisualStyleBackColor = true;
            this.idcErrorsBtn.Click += new System.EventHandler(this.button6_Click);
            // 
            // eventMsgBtn
            // 
            this.eventMsgBtn.Location = new System.Drawing.Point(9, 446);
            this.eventMsgBtn.Name = "eventMsgBtn";
            this.eventMsgBtn.Size = new System.Drawing.Size(103, 23);
            this.eventMsgBtn.TabIndex = 28;
            this.eventMsgBtn.Text = "Event Msg";
            this.eventMsgBtn.UseVisualStyleBackColor = true;
            this.eventMsgBtn.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 481);
            this.Controls.Add(this.eventMsgBtn);
            this.Controls.Add(this.idcErrorsBtn);
            this.Controls.Add(this.iStoreMoneyandiRetractBtn);
            this.Controls.Add(this.findErrorCodesBtn);
            this.Controls.Add(this.listPhyDevBtn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.searchKeywordBtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.clearTextBoxesBtn);
            this.Controls.Add(this.allCashInBtn);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.readSpLogsBtn);
            this.Controls.Add(this.selectSpLogsBtn);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SP Log Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button selectSpLogsBtn;
        private System.Windows.Forms.Button readSpLogsBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button allCashInBtn;
        private System.Windows.Forms.Button clearTextBoxesBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchKeywordBtn;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button listPhyDevBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button findErrorCodesBtn;
        private System.Windows.Forms.Button iStoreMoneyandiRetractBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button idcErrorsBtn;
        private System.Windows.Forms.Button eventMsgBtn;
    }
}

