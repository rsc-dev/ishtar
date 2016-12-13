namespace Ishtar
{
    partial class MainForm
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gbHeap = new System.Windows.Forms.GroupBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnHeapManually = new System.Windows.Forms.Button();
            this.lbManagedHeapItems = new System.Windows.Forms.ListBox();
            this.tbVMMapCsv = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVMMap = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbTestAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbTestMt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTestType = new System.Windows.Forms.TextBox();
            this.lbTest = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbPyCode = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbPyConsole = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnScanAssemblies = new System.Windows.Forms.Button();
            this.tvAssemblies = new System.Windows.Forms.TreeView();
            this.tcMain.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.gbHeap.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPage4);
            this.tcMain.Controls.Add(this.tabPage3);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(697, 439);
            this.tcMain.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gbHeap);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(689, 413);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Workspace";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gbHeap
            // 
            this.gbHeap.Controls.Add(this.btnOpenFile);
            this.gbHeap.Controls.Add(this.btnHeapManually);
            this.gbHeap.Controls.Add(this.lbManagedHeapItems);
            this.gbHeap.Controls.Add(this.tbVMMapCsv);
            this.gbHeap.Controls.Add(this.label4);
            this.gbHeap.Controls.Add(this.btnVMMap);
            this.gbHeap.Location = new System.Drawing.Point(8, 3);
            this.gbHeap.Name = "gbHeap";
            this.gbHeap.Size = new System.Drawing.Size(599, 256);
            this.gbHeap.TabIndex = 3;
            this.gbHeap.TabStop = false;
            this.gbHeap.Text = "Managed Heap";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(476, 17);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(117, 23);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "Open...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnHeapManually
            // 
            this.btnHeapManually.Location = new System.Drawing.Point(259, 120);
            this.btnHeapManually.Name = "btnHeapManually";
            this.btnHeapManually.Size = new System.Drawing.Size(334, 23);
            this.btnHeapManually.TabIndex = 4;
            this.btnHeapManually.Text = "Add manually";
            this.btnHeapManually.UseVisualStyleBackColor = true;
            this.btnHeapManually.Click += new System.EventHandler(this.btnHeapManually_Click);
            // 
            // lbManagedHeapItems
            // 
            this.lbManagedHeapItems.FormattingEnabled = true;
            this.lbManagedHeapItems.Location = new System.Drawing.Point(12, 19);
            this.lbManagedHeapItems.Name = "lbManagedHeapItems";
            this.lbManagedHeapItems.Size = new System.Drawing.Size(174, 225);
            this.lbManagedHeapItems.TabIndex = 3;
            // 
            // tbVMMapCsv
            // 
            this.tbVMMapCsv.Location = new System.Drawing.Point(259, 19);
            this.tbVMMapCsv.Name = "tbVMMapCsv";
            this.tbVMMapCsv.Size = new System.Drawing.Size(211, 20);
            this.tbVMMapCsv.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "VMMap csv:";
            // 
            // btnVMMap
            // 
            this.btnVMMap.Location = new System.Drawing.Point(259, 45);
            this.btnVMMap.Name = "btnVMMap";
            this.btnVMMap.Size = new System.Drawing.Size(334, 23);
            this.btnVMMap.TabIndex = 0;
            this.btnVMMap.Text = "Parse";
            this.btnVMMap.UseVisualStyleBackColor = true;
            this.btnVMMap.Click += new System.EventHandler(this.btnVMMap_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbTestAddress);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.tbTestMt);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.tbTestType);
            this.tabPage3.Controls.Add(this.lbTest);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(689, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Target";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbTestAddress
            // 
            this.tbTestAddress.Location = new System.Drawing.Point(47, 29);
            this.tbTestAddress.Name = "tbTestAddress";
            this.tbTestAddress.Size = new System.Drawing.Size(100, 20);
            this.tbTestAddress.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Address";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(153, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Find instances";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tbTestMt
            // 
            this.tbTestMt.Location = new System.Drawing.Point(47, 55);
            this.tbTestMt.Name = "tbTestMt";
            this.tbTestMt.Size = new System.Drawing.Size(100, 20);
            this.tbTestMt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "MT:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Find MT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type:";
            // 
            // tbTestType
            // 
            this.tbTestType.Location = new System.Drawing.Point(47, 3);
            this.tbTestType.Name = "tbTestType";
            this.tbTestType.Size = new System.Drawing.Size(100, 20);
            this.tbTestType.TabIndex = 1;
            // 
            // lbTest
            // 
            this.lbTest.FormattingEnabled = true;
            this.lbTest.Location = new System.Drawing.Point(8, 117);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(177, 290);
            this.lbTest.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(689, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Python";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tbPyConsole);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 407);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnTest);
            this.panel2.Controls.Add(this.tbPyCode);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 321);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(683, 86);
            this.panel2.TabIndex = 3;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(580, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbPyCode
            // 
            this.tbPyCode.Location = new System.Drawing.Point(3, 5);
            this.tbPyCode.Multiline = true;
            this.tbPyCode.Name = "tbPyCode";
            this.tbPyCode.Size = new System.Drawing.Size(473, 78);
            this.tbPyCode.TabIndex = 0;
            this.tbPyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPyCode_KeyDown);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(482, 3);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(92, 23);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tbPyConsole
            // 
            this.tbPyConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPyConsole.Location = new System.Drawing.Point(3, 7);
            this.tbPyConsole.Multiline = true;
            this.tbPyConsole.Name = "tbPyConsole";
            this.tbPyConsole.ReadOnly = true;
            this.tbPyConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPyConsole.Size = new System.Drawing.Size(675, 313);
            this.tbPyConsole.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnScanAssemblies);
            this.tabPage2.Controls.Add(this.tvAssemblies);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(689, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnScanAssemblies
            // 
            this.btnScanAssemblies.Location = new System.Drawing.Point(228, 6);
            this.btnScanAssemblies.Name = "btnScanAssemblies";
            this.btnScanAssemblies.Size = new System.Drawing.Size(75, 23);
            this.btnScanAssemblies.TabIndex = 1;
            this.btnScanAssemblies.Text = "Scan";
            this.btnScanAssemblies.UseVisualStyleBackColor = true;
            // 
            // tvAssemblies
            // 
            this.tvAssemblies.Location = new System.Drawing.Point(8, 6);
            this.tvAssemblies.Name = "tvAssemblies";
            this.tvAssemblies.Size = new System.Drawing.Size(214, 399);
            this.tvAssemblies.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(697, 439);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MainForm";
            this.Text = "Ishtar";
            this.tcMain.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.gbHeap.ResumeLayout(false);
            this.gbHeap.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbPyConsole;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbPyCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tvAssemblies;
        private System.Windows.Forms.Button btnScanAssemblies;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbTestMt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTestType;
        private System.Windows.Forms.ListBox lbTest;
        private System.Windows.Forms.TextBox tbTestAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbVMMapCsv;
        private System.Windows.Forms.Button btnVMMap;
        private System.Windows.Forms.GroupBox gbHeap;
        private System.Windows.Forms.Button btnHeapManually;
        private System.Windows.Forms.ListBox lbManagedHeapItems;
        private System.Windows.Forms.Button btnOpenFile;
    }
}

