namespace somiodApp
{
    partial class AppA
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
            this.btnApplicationName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNameApp = new System.Windows.Forms.TextBox();
            this.gbModules = new System.Windows.Forms.GroupBox();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameModule = new System.Windows.Forms.TextBox();
            this.btnAddModule = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gbSubscriptions = new System.Windows.Forms.GroupBox();
            this.txtEndpoint = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbModules = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddSubs = new System.Windows.Forms.Button();
            this.txtSubscriptions = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDataData = new System.Windows.Forms.ListBox();
            this.pbon = new System.Windows.Forms.PictureBox();
            this.pboff = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.gbModules.SuspendLayout();
            this.gbSubscriptions.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboff)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplicationName
            // 
            this.btnApplicationName.Location = new System.Drawing.Point(101, 250);
            this.btnApplicationName.Margin = new System.Windows.Forms.Padding(2);
            this.btnApplicationName.Name = "btnApplicationName";
            this.btnApplicationName.Size = new System.Drawing.Size(129, 46);
            this.btnApplicationName.TabIndex = 0;
            this.btnApplicationName.Text = "Add";
            this.btnApplicationName.UseVisualStyleBackColor = true;
            this.btnApplicationName.Click += new System.EventHandler(this.btnApplicationName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNameApp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnApplicationName);
            this.groupBox1.Location = new System.Drawing.Point(8, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(327, 316);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // txtNameApp
            // 
            this.txtNameApp.Location = new System.Drawing.Point(67, 40);
            this.txtNameApp.Margin = new System.Windows.Forms.Padding(2);
            this.txtNameApp.Name = "txtNameApp";
            this.txtNameApp.Size = new System.Drawing.Size(177, 20);
            this.txtNameApp.TabIndex = 3;
            // 
            // gbModules
            // 
            this.gbModules.Controls.Add(this.cbApp);
            this.gbModules.Controls.Add(this.label3);
            this.gbModules.Controls.Add(this.txtNameModule);
            this.gbModules.Controls.Add(this.btnAddModule);
            this.gbModules.Controls.Add(this.label2);
            this.gbModules.Enabled = false;
            this.gbModules.Location = new System.Drawing.Point(352, 42);
            this.gbModules.Margin = new System.Windows.Forms.Padding(2);
            this.gbModules.Name = "gbModules";
            this.gbModules.Padding = new System.Windows.Forms.Padding(2);
            this.gbModules.Size = new System.Drawing.Size(327, 310);
            this.gbModules.TabIndex = 4;
            this.gbModules.TabStop = false;
            this.gbModules.Text = "Modules";
            // 
            // cbApp
            // 
            this.cbApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Location = new System.Drawing.Point(80, 76);
            this.cbApp.Margin = new System.Windows.Forms.Padding(2);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(207, 21);
            this.cbApp.TabIndex = 5;
            this.cbApp.SelectedIndexChanged += new System.EventHandler(this.cbApp_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Application";
            // 
            // txtNameModule
            // 
            this.txtNameModule.Location = new System.Drawing.Point(80, 36);
            this.txtNameModule.Margin = new System.Windows.Forms.Padding(2);
            this.txtNameModule.Name = "txtNameModule";
            this.txtNameModule.Size = new System.Drawing.Size(207, 20);
            this.txtNameModule.TabIndex = 3;
            // 
            // btnAddModule
            // 
            this.btnAddModule.Location = new System.Drawing.Point(95, 244);
            this.btnAddModule.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddModule.Name = "btnAddModule";
            this.btnAddModule.Size = new System.Drawing.Size(129, 46);
            this.btnAddModule.TabIndex = 0;
            this.btnAddModule.Text = "Add";
            this.btnAddModule.UseVisualStyleBackColor = true;
            this.btnAddModule.Click += new System.EventHandler(this.btnAddModule_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // gbSubscriptions
            // 
            this.gbSubscriptions.Controls.Add(this.txtEndpoint);
            this.gbSubscriptions.Controls.Add(this.label7);
            this.gbSubscriptions.Controls.Add(this.cbModules);
            this.gbSubscriptions.Controls.Add(this.label4);
            this.gbSubscriptions.Controls.Add(this.btnAddSubs);
            this.gbSubscriptions.Controls.Add(this.txtSubscriptions);
            this.gbSubscriptions.Controls.Add(this.label5);
            this.gbSubscriptions.Enabled = false;
            this.gbSubscriptions.Location = new System.Drawing.Point(695, 42);
            this.gbSubscriptions.Margin = new System.Windows.Forms.Padding(2);
            this.gbSubscriptions.Name = "gbSubscriptions";
            this.gbSubscriptions.Padding = new System.Windows.Forms.Padding(2);
            this.gbSubscriptions.Size = new System.Drawing.Size(327, 310);
            this.gbSubscriptions.TabIndex = 5;
            this.gbSubscriptions.TabStop = false;
            this.gbSubscriptions.Text = "Subscriptions";
            // 
            // txtEndpoint
            // 
            this.txtEndpoint.Location = new System.Drawing.Point(76, 75);
            this.txtEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.txtEndpoint.Name = "txtEndpoint";
            this.txtEndpoint.Size = new System.Drawing.Size(207, 20);
            this.txtEndpoint.TabIndex = 9;
            this.txtEndpoint.Text = "mqtt://127.0.0.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 84);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Endpoint";
            // 
            // cbModules
            // 
            this.cbModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModules.FormattingEnabled = true;
            this.cbModules.Location = new System.Drawing.Point(76, 118);
            this.cbModules.Margin = new System.Windows.Forms.Padding(2);
            this.cbModules.Name = "cbModules";
            this.cbModules.Size = new System.Drawing.Size(207, 21);
            this.cbModules.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Modules";
            // 
            // btnAddSubs
            // 
            this.btnAddSubs.Location = new System.Drawing.Point(95, 244);
            this.btnAddSubs.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddSubs.Name = "btnAddSubs";
            this.btnAddSubs.Size = new System.Drawing.Size(129, 46);
            this.btnAddSubs.TabIndex = 0;
            this.btnAddSubs.Text = "Add";
            this.btnAddSubs.UseVisualStyleBackColor = true;
            this.btnAddSubs.Click += new System.EventHandler(this.btnAddSubs_Click);
            // 
            // txtSubscriptions
            // 
            this.txtSubscriptions.Location = new System.Drawing.Point(76, 37);
            this.txtSubscriptions.Margin = new System.Windows.Forms.Padding(2);
            this.txtSubscriptions.Name = "txtSubscriptions";
            this.txtSubscriptions.Size = new System.Drawing.Size(207, 20);
            this.txtSubscriptions.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.lblDataData);
            this.groupBox4.Location = new System.Drawing.Point(170, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(406, 334);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(26, -23);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(207, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-33, -20);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Modules";
            // 
            // lblDataData
            // 
            this.lblDataData.FormattingEnabled = true;
            this.lblDataData.Location = new System.Drawing.Point(17, 26);
            this.lblDataData.Margin = new System.Windows.Forms.Padding(2);
            this.lblDataData.Name = "lblDataData";
            this.lblDataData.Size = new System.Drawing.Size(375, 277);
            this.lblDataData.TabIndex = 10;
            // 
            // pbon
            // 
            this.pbon.Image = global::somiodApp.Properties.Resources.bulbon2;
            this.pbon.Location = new System.Drawing.Point(761, 452);
            this.pbon.Name = "pbon";
            this.pbon.Size = new System.Drawing.Size(122, 148);
            this.pbon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbon.TabIndex = 12;
            this.pbon.TabStop = false;
            // 
            // pboff
            // 
            this.pboff.Image = global::somiodApp.Properties.Resources.bulbon;
            this.pboff.Location = new System.Drawing.Point(761, 452);
            this.pboff.Name = "pboff";
            this.pboff.Size = new System.Drawing.Size(122, 148);
            this.pboff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboff.TabIndex = 13;
            this.pboff.TabStop = false;
            // 
            // AppA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 730);
            this.Controls.Add(this.pboff);
            this.Controls.Add(this.pbon);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbSubscriptions);
            this.Controls.Add(this.gbModules);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AppA";
            this.Text = "AppA";
            this.Load += new System.EventHandler(this.AppA_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbModules.ResumeLayout(false);
            this.gbModules.PerformLayout();
            this.gbSubscriptions.ResumeLayout(false);
            this.gbSubscriptions.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApplicationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameApp;
        private System.Windows.Forms.GroupBox gbModules;
        private System.Windows.Forms.TextBox txtNameModule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddModule;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbSubscriptions;
        private System.Windows.Forms.TextBox txtEndpoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbModules;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddSubs;
        private System.Windows.Forms.TextBox txtSubscriptions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lblDataData;
        private System.Windows.Forms.PictureBox pbon;
        private System.Windows.Forms.PictureBox pboff;
    }
}

