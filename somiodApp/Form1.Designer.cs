namespace somiodApp
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
            this.btnApplicationName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNameApp = new System.Windows.Forms.TextBox();
            this.lblApps = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNameModule = new System.Windows.Forms.TextBox();
            this.lblModules = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddModule = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbModules = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSubscriptions = new System.Windows.Forms.TextBox();
            this.lblSubs = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddSubs = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEvent = new System.Windows.Forms.TextBox();
            this.txtEndpoint = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApplicationName
            // 
            this.btnApplicationName.Location = new System.Drawing.Point(152, 385);
            this.btnApplicationName.Name = "btnApplicationName";
            this.btnApplicationName.Size = new System.Drawing.Size(194, 71);
            this.btnApplicationName.TabIndex = 0;
            this.btnApplicationName.Text = "Add";
            this.btnApplicationName.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNameApp);
            this.groupBox1.Controls.Add(this.lblApps);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnApplicationName);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 638);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // txtNameApp
            // 
            this.txtNameApp.Location = new System.Drawing.Point(101, 61);
            this.txtNameApp.Name = "txtNameApp";
            this.txtNameApp.Size = new System.Drawing.Size(264, 26);
            this.txtNameApp.TabIndex = 3;
            // 
            // lblApps
            // 
            this.lblApps.FormattingEnabled = true;
            this.lblApps.ItemHeight = 20;
            this.lblApps.Location = new System.Drawing.Point(30, 465);
            this.lblApps.Name = "lblApps";
            this.lblApps.Size = new System.Drawing.Size(420, 144);
            this.lblApps.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbApp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNameModule);
            this.groupBox2.Controls.Add(this.lblModules);
            this.groupBox2.Controls.Add(this.btnAddModule);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(528, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 638);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modules";
            // 
            // txtNameModule
            // 
            this.txtNameModule.Location = new System.Drawing.Point(120, 56);
            this.txtNameModule.Name = "txtNameModule";
            this.txtNameModule.Size = new System.Drawing.Size(308, 26);
            this.txtNameModule.TabIndex = 3;
            // 
            // lblModules
            // 
            this.lblModules.FormattingEnabled = true;
            this.lblModules.ItemHeight = 20;
            this.lblModules.Location = new System.Drawing.Point(30, 456);
            this.lblModules.Name = "lblModules";
            this.lblModules.Size = new System.Drawing.Size(420, 144);
            this.lblModules.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // btnAddModule
            // 
            this.btnAddModule.Location = new System.Drawing.Point(143, 376);
            this.btnAddModule.Name = "btnAddModule";
            this.btnAddModule.Size = new System.Drawing.Size(194, 71);
            this.btnAddModule.TabIndex = 0;
            this.btnAddModule.Text = "Add";
            this.btnAddModule.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Application";
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Location = new System.Drawing.Point(120, 117);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(308, 28);
            this.cbApp.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtEndpoint);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtEvent);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbModules);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnAddSubs);
            this.groupBox3.Controls.Add(this.txtSubscriptions);
            this.groupBox3.Controls.Add(this.lblSubs);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(1042, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(491, 638);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Subscriptions";
            // 
            // cbModules
            // 
            this.cbModules.FormattingEnabled = true;
            this.cbModules.Location = new System.Drawing.Point(114, 252);
            this.cbModules.Name = "cbModules";
            this.cbModules.Size = new System.Drawing.Size(308, 28);
            this.cbModules.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Modules";
            // 
            // txtSubscriptions
            // 
            this.txtSubscriptions.Location = new System.Drawing.Point(114, 62);
            this.txtSubscriptions.Name = "txtSubscriptions";
            this.txtSubscriptions.Size = new System.Drawing.Size(308, 26);
            this.txtSubscriptions.TabIndex = 3;
            // 
            // lblSubs
            // 
            this.lblSubs.FormattingEnabled = true;
            this.lblSubs.ItemHeight = 20;
            this.lblSubs.Location = new System.Drawing.Point(30, 456);
            this.lblSubs.Name = "lblSubs";
            this.lblSubs.Size = new System.Drawing.Size(420, 144);
            this.lblSubs.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            // 
            // btnAddSubs
            // 
            this.btnAddSubs.Location = new System.Drawing.Point(142, 376);
            this.btnAddSubs.Name = "btnAddSubs";
            this.btnAddSubs.Size = new System.Drawing.Size(194, 71);
            this.btnAddSubs.TabIndex = 0;
            this.btnAddSubs.Text = "Add";
            this.btnAddSubs.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Event";
            // 
            // txtEvent
            // 
            this.txtEvent.Location = new System.Drawing.Point(114, 126);
            this.txtEvent.Name = "txtEvent";
            this.txtEvent.Size = new System.Drawing.Size(308, 26);
            this.txtEvent.TabIndex = 7;
            // 
            // txtEndpoint
            // 
            this.txtEndpoint.Location = new System.Drawing.Point(114, 190);
            this.txtEndpoint.Name = "txtEndpoint";
            this.txtEndpoint.Size = new System.Drawing.Size(308, 26);
            this.txtEndpoint.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Endpoint";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1586, 818);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApplicationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameApp;
        private System.Windows.Forms.ListBox lblApps;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNameModule;
        private System.Windows.Forms.ListBox lblModules;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddModule;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtEndpoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEvent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbModules;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddSubs;
        private System.Windows.Forms.TextBox txtSubscriptions;
        private System.Windows.Forms.ListBox lblSubs;
        private System.Windows.Forms.Label label5;
    }
}

