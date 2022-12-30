namespace somiodApp
{
    partial class AppB
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
            this.gbModules = new System.Windows.Forms.GroupBox();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameModule = new System.Windows.Forms.TextBox();
            this.btnAddModule = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbModules = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbmessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSend = new System.Windows.Forms.Button();
            this.gbModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbModules
            // 
            this.gbModules.Controls.Add(this.cbApp);
            this.gbModules.Controls.Add(this.label3);
            this.gbModules.Controls.Add(this.txtNameModule);
            this.gbModules.Controls.Add(this.btnAddModule);
            this.gbModules.Controls.Add(this.label2);
            this.gbModules.Enabled = false;
            this.gbModules.Location = new System.Drawing.Point(11, 11);
            this.gbModules.Margin = new System.Windows.Forms.Padding(2);
            this.gbModules.Name = "gbModules";
            this.gbModules.Padding = new System.Windows.Forms.Padding(2);
            this.gbModules.Size = new System.Drawing.Size(327, 237);
            this.gbModules.TabIndex = 5;
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
            this.btnAddModule.Location = new System.Drawing.Point(114, 148);
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
            // cbModules
            // 
            this.cbModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModules.FormattingEnabled = true;
            this.cbModules.Location = new System.Drawing.Point(442, 85);
            this.cbModules.Margin = new System.Windows.Forms.Padding(2);
            this.cbModules.Name = "cbModules";
            this.cbModules.Size = new System.Drawing.Size(207, 21);
            this.cbModules.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Modules";
            // 
            // tbmessage
            // 
            this.tbmessage.Location = new System.Drawing.Point(442, 173);
            this.tbmessage.Name = "tbmessage";
            this.tbmessage.Size = new System.Drawing.Size(207, 20);
            this.tbmessage.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 176);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Data";
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(442, 225);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(207, 23);
            this.btSend.TabIndex = 10;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // AppB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 281);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbmessage);
            this.Controls.Add(this.cbModules);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbModules);
            this.Name = "AppB";
            this.Text = "AppB";
            this.gbModules.ResumeLayout(false);
            this.gbModules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbModules;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameModule;
        private System.Windows.Forms.Button btnAddModule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbModules;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbmessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSend;
    }
}