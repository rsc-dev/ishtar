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
            this.gbLogs = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAssembly = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lbAssemblies = new System.Windows.Forms.ListBox();
            this.gbLoader.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLoader
            // 
            this.gbLoader.Controls.Add(this.lbAssemblies);
            this.gbLoader.Controls.Add(this.btnLoad);
            this.gbLoader.Controls.Add(this.btnOpen);
            this.gbLoader.Controls.Add(this.tbAssembly);
            this.gbLoader.Controls.Add(this.label1);
            this.gbLoader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLoader.Location = new System.Drawing.Point(0, 0);
            this.gbLoader.Name = "gbLoader";
            this.gbLoader.Size = new System.Drawing.Size(840, 354);
            this.gbLoader.TabIndex = 0;
            this.gbLoader.TabStop = false;
            this.gbLoader.Text = "Load assembly";
            // 
            // gbLogs
            // 
            this.gbLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbLogs.Location = new System.Drawing.Point(0, 360);
            this.gbLogs.Name = "gbLogs";
            this.gbLogs.Size = new System.Drawing.Size(840, 92);
            this.gbLogs.TabIndex = 1;
            this.gbLogs.TabStop = false;
            this.gbLogs.Text = "Logs";
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
            // tbAssembly
            // 
            this.tbAssembly.Location = new System.Drawing.Point(44, 25);
            this.tbAssembly.Name = "tbAssembly";
            this.tbAssembly.ReadOnly = true;
            this.tbAssembly.Size = new System.Drawing.Size(327, 20);
            this.tbAssembly.TabIndex = 1;
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
            // lbAssemblies
            // 
            this.lbAssemblies.FormattingEnabled = true;
            this.lbAssemblies.Location = new System.Drawing.Point(15, 97);
            this.lbAssemblies.Name = "lbAssemblies";
            this.lbAssemblies.Size = new System.Drawing.Size(440, 251);
            this.lbAssemblies.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 452);
            this.Controls.Add(this.gbLogs);
            this.Controls.Add(this.gbLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "[Ishtar] Managed Loader";
            this.gbLoader.ResumeLayout(false);
            this.gbLoader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLoader;
        private System.Windows.Forms.GroupBox gbLogs;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbAssembly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbAssemblies;
    }
}

