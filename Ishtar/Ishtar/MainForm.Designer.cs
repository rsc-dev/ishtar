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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPid = new System.Windows.Forms.Label();
            this.tpAssemblies = new System.Windows.Forms.TabPage();
            this.btnAssembliesRefresh = new System.Windows.Forms.Button();
            this.tvAssemblies = new System.Windows.Forms.TreeView();
            this.tpPython = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbPyCode = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbPyConsole = new System.Windows.Forms.TextBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.cbMultiline = new System.Windows.Forms.CheckBox();
            this.tpInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpAssemblies.SuspendLayout();
            this.tpPython.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.groupBox1);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Size = new System.Drawing.Size(689, 413);
            this.tpInfo.TabIndex = 5;
            this.tpInfo.Text = "Info";
            this.tpInfo.UseVisualStyleBackColor = true;
            this.tpInfo.Enter += new System.EventHandler(this.tpInfo_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblPid);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 181);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "PID:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(49, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // lblPid
            // 
            this.lblPid.AutoSize = true;
            this.lblPid.Location = new System.Drawing.Point(50, 26);
            this.lblPid.Name = "lblPid";
            this.lblPid.Size = new System.Drawing.Size(28, 13);
            this.lblPid.TabIndex = 0;
            this.lblPid.Text = "PID:";
            // 
            // tpAssemblies
            // 
            this.tpAssemblies.Controls.Add(this.btnAssembliesRefresh);
            this.tpAssemblies.Controls.Add(this.tvAssemblies);
            this.tpAssemblies.Location = new System.Drawing.Point(4, 22);
            this.tpAssemblies.Name = "tpAssemblies";
            this.tpAssemblies.Padding = new System.Windows.Forms.Padding(3);
            this.tpAssemblies.Size = new System.Drawing.Size(689, 413);
            this.tpAssemblies.TabIndex = 4;
            this.tpAssemblies.Text = "Assemblies";
            this.tpAssemblies.UseVisualStyleBackColor = true;
            // 
            // btnAssembliesRefresh
            // 
            this.btnAssembliesRefresh.Location = new System.Drawing.Point(608, 3);
            this.btnAssembliesRefresh.Name = "btnAssembliesRefresh";
            this.btnAssembliesRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnAssembliesRefresh.TabIndex = 1;
            this.btnAssembliesRefresh.Text = "Refresh";
            this.btnAssembliesRefresh.UseVisualStyleBackColor = true;
            this.btnAssembliesRefresh.Click += new System.EventHandler(this.btnAssembliesRefresh_Click);
            // 
            // tvAssemblies
            // 
            this.tvAssemblies.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvAssemblies.Location = new System.Drawing.Point(3, 3);
            this.tvAssemblies.Name = "tvAssemblies";
            this.tvAssemblies.Size = new System.Drawing.Size(599, 407);
            this.tvAssemblies.TabIndex = 0;
            this.tvAssemblies.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAssemblies_NodeMouseClick);
            // 
            // tpPython
            // 
            this.tpPython.Controls.Add(this.panel1);
            this.tpPython.Location = new System.Drawing.Point(4, 22);
            this.tpPython.Name = "tpPython";
            this.tpPython.Padding = new System.Windows.Forms.Padding(3);
            this.tpPython.Size = new System.Drawing.Size(689, 413);
            this.tpPython.TabIndex = 0;
            this.tpPython.Text = "Python";
            this.tpPython.UseVisualStyleBackColor = true;
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
            this.panel2.Controls.Add(this.cbMultiline);
            this.panel2.Controls.Add(this.tbPyCode);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 321);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(683, 86);
            this.panel2.TabIndex = 3;
            // 
            // tbPyCode
            // 
            this.tbPyCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbPyCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbPyCode.Location = new System.Drawing.Point(3, 5);
            this.tbPyCode.Name = "tbPyCode";
            this.tbPyCode.Size = new System.Drawing.Size(473, 20);
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
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpPython);
            this.tcMain.Controls.Add(this.tpAssemblies);
            this.tcMain.Controls.Add(this.tpInfo);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(697, 439);
            this.tcMain.TabIndex = 0;
            // 
            // cbMultiline
            // 
            this.cbMultiline.AutoSize = true;
            this.cbMultiline.Location = new System.Drawing.Point(482, 32);
            this.cbMultiline.Name = "cbMultiline";
            this.cbMultiline.Size = new System.Drawing.Size(64, 17);
            this.cbMultiline.TabIndex = 2;
            this.cbMultiline.Text = "Multiline";
            this.cbMultiline.UseVisualStyleBackColor = true;
            this.cbMultiline.CheckedChanged += new System.EventHandler(this.cbMultiline_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(697, 439);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Ishtar";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tpInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpAssemblies.ResumeLayout(false);
            this.tpPython.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TabPage tpAssemblies;
        private System.Windows.Forms.Button btnAssembliesRefresh;
        private System.Windows.Forms.TreeView tvAssemblies;
        private System.Windows.Forms.TabPage tpPython;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbPyCode;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbPyConsole;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPid;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbMultiline;
    }
}

