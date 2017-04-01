namespace ManagedLoader
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
            this.gbLoader = new System.Windows.Forms.GroupBox();
            this.lbAssemblies = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbAssembly = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.processInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbExecute = new System.Windows.Forms.GroupBox();
            this.lblAssembly = new System.Windows.Forms.Label();
            this.tbExecuteAssembly = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cbMethods = new System.Windows.Forms.ComboBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.gbLoader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbExecute.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLoader
            // 
            this.gbLoader.Controls.Add(this.lbAssemblies);
            this.gbLoader.Controls.Add(this.btnLoad);
            this.gbLoader.Controls.Add(this.btnOpen);
            this.gbLoader.Controls.Add(this.tbAssembly);
            this.gbLoader.Controls.Add(this.label1);
            this.gbLoader.Location = new System.Drawing.Point(12, 11);
            this.gbLoader.Name = "gbLoader";
            this.gbLoader.Size = new System.Drawing.Size(476, 343);
            this.gbLoader.TabIndex = 0;
            this.gbLoader.TabStop = false;
            this.gbLoader.Text = "Process assemblies";
            // 
            // lbAssemblies
            // 
            this.lbAssemblies.FormattingEnabled = true;
            this.lbAssemblies.Location = new System.Drawing.Point(15, 81);
            this.lbAssemblies.Name = "lbAssemblies";
            this.lbAssemblies.Size = new System.Drawing.Size(440, 251);
            this.lbAssemblies.TabIndex = 4;
            this.lbAssemblies.SelectedIndexChanged += new System.EventHandler(this.lbAssemblies_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(377, 52);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(78, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(377, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(78, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbAssembly
            // 
            this.tbAssembly.Location = new System.Drawing.Point(44, 25);
            this.tbAssembly.Name = "tbAssembly";
            this.tbAssembly.ReadOnly = true;
            this.tbAssembly.Size = new System.Drawing.Size(327, 20);
            this.tbAssembly.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processInfoLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 375);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // processInfoLabel
            // 
            this.processInfoLabel.Name = "processInfoLabel";
            this.processInfoLabel.Size = new System.Drawing.Size(96, 17);
            this.processInfoLabel.Text = "processInfoLabel";
            // 
            // gbExecute
            // 
            this.gbExecute.Controls.Add(this.btnExecute);
            this.gbExecute.Controls.Add(this.cbMethods);
            this.gbExecute.Controls.Add(this.lblMethod);
            this.gbExecute.Controls.Add(this.cbTypes);
            this.gbExecute.Controls.Add(this.lblType);
            this.gbExecute.Controls.Add(this.tbExecuteAssembly);
            this.gbExecute.Controls.Add(this.lblAssembly);
            this.gbExecute.Location = new System.Drawing.Point(494, 12);
            this.gbExecute.Name = "gbExecute";
            this.gbExecute.Size = new System.Drawing.Size(349, 342);
            this.gbExecute.TabIndex = 2;
            this.gbExecute.TabStop = false;
            this.gbExecute.Text = "Execute code";
            // 
            // lblAssembly
            // 
            this.lblAssembly.AutoSize = true;
            this.lblAssembly.Location = new System.Drawing.Point(17, 37);
            this.lblAssembly.Name = "lblAssembly";
            this.lblAssembly.Size = new System.Drawing.Size(57, 13);
            this.lblAssembly.TabIndex = 0;
            this.lblAssembly.Text = "Assembly: ";
            // 
            // tbExecuteAssembly
            // 
            this.tbExecuteAssembly.Location = new System.Drawing.Point(80, 34);
            this.tbExecuteAssembly.Name = "tbExecuteAssembly";
            this.tbExecuteAssembly.ReadOnly = true;
            this.tbExecuteAssembly.Size = new System.Drawing.Size(263, 20);
            this.tbExecuteAssembly.TabIndex = 1;
            this.tbExecuteAssembly.TextChanged += new System.EventHandler(this.tbExecuteAssembly_TextChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(40, 63);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // cbTypes
            // 
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(80, 60);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(263, 21);
            this.cbTypes.TabIndex = 4;
            this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged);
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(28, 90);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(46, 13);
            this.lblMethod.TabIndex = 5;
            this.lblMethod.Text = "Method:";
            // 
            // cbMethods
            // 
            this.cbMethods.FormattingEnabled = true;
            this.cbMethods.Location = new System.Drawing.Point(80, 87);
            this.cbMethods.Name = "cbMethods";
            this.cbMethods.Size = new System.Drawing.Size(263, 21);
            this.cbMethods.TabIndex = 6;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(80, 114);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(263, 23);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 397);
            this.Controls.Add(this.gbExecute);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "[Ishtar] Managed Loader";
            this.gbLoader.ResumeLayout(false);
            this.gbLoader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbExecute.ResumeLayout(false);
            this.gbExecute.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLoader;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbAssembly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbAssemblies;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel processInfoLabel;
        private System.Windows.Forms.GroupBox gbExecute;
        private System.Windows.Forms.TextBox tbExecuteAssembly;
        private System.Windows.Forms.Label lblAssembly;
        private System.Windows.Forms.ComboBox cbTypes;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbMethods;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Button btnExecute;
    }
}

