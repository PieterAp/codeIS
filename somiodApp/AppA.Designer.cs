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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameModule = new System.Windows.Forms.TextBox();
            this.btnAddModule = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtEndpoint = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEvent = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbModules = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddSubs = new System.Windows.Forms.Button();
            this.txtSubscriptions = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbApp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNameModule);
            this.groupBox2.Controls.Add(this.btnAddModule);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(352, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(327, 310);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modules";
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Location = new System.Drawing.Point(80, 76);
            this.cbApp.Margin = new System.Windows.Forms.Padding(2);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(207, 21);
            this.cbApp.TabIndex = 5;
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
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(695, 42);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(327, 310);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Subscriptions";
            // 
            // txtEndpoint
            // 
            this.txtEndpoint.Location = new System.Drawing.Point(76, 123);
            this.txtEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.txtEndpoint.Name = "txtEndpoint";
            this.txtEndpoint.Size = new System.Drawing.Size(207, 20);
            this.txtEndpoint.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 127);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Endpoint";
            // 
            // txtEvent
            // 
            this.txtEvent.Location = new System.Drawing.Point(76, 82);
            this.txtEvent.Margin = new System.Windows.Forms.Padding(2);
            this.txtEvent.Name = "txtEvent";
            this.txtEvent.Size = new System.Drawing.Size(207, 20);
            this.txtEvent.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 84);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Event";
            // 
            // cbModules
            // 
            this.cbModules.FormattingEnabled = true;
            this.cbModules.Location = new System.Drawing.Point(76, 164);
            this.cbModules.Margin = new System.Windows.Forms.Padding(2);
            this.cbModules.Name = "cbModules";
            this.cbModules.Size = new System.Drawing.Size(207, 21);
            this.cbModules.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 169);
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
            // 
            // txtSubscriptions
            // 
            this.txtSubscriptions.Location = new System.Drawing.Point(76, 40);
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
            // lblData
            // 
            this.lblData.FormattingEnabled = true;
            this.lblData.Location = new System.Drawing.Point(28, 396);
            this.lblData.Margin = new System.Windows.Forms.Padding(2);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(281, 121);
            this.lblData.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 370);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Data";
            // 
            // AppA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 539);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AppA";
            this.Text = "AppA";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApplicationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameApp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNameModule;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lblData;
        private System.Windows.Forms.Label label8;
    }
}

