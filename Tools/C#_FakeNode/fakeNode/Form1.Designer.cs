namespace fakeNode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDevEUI = new System.Windows.Forms.TextBox();
            this.txtAppEUI = new System.Windows.Forms.TextBox();
            this.txtAppKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDevAddr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNwkSKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAppSKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DevEUI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "AppEUI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "AppKey";
            // 
            // txtDevEUI
            // 
            this.txtDevEUI.Location = new System.Drawing.Point(62, 56);
            this.txtDevEUI.Name = "txtDevEUI";
            this.txtDevEUI.Size = new System.Drawing.Size(264, 20);
            this.txtDevEUI.TabIndex = 3;
            this.txtDevEUI.Text = "0102030405060708";
            // 
            // txtAppEUI
            // 
            this.txtAppEUI.Location = new System.Drawing.Point(62, 82);
            this.txtAppEUI.Name = "txtAppEUI";
            this.txtAppEUI.Size = new System.Drawing.Size(264, 20);
            this.txtAppEUI.TabIndex = 4;
            this.txtAppEUI.Text = "70B3D57ED0000D2C";
            // 
            // txtAppKey
            // 
            this.txtAppKey.Location = new System.Drawing.Point(62, 108);
            this.txtAppKey.Name = "txtAppKey";
            this.txtAppKey.Size = new System.Drawing.Size(264, 20);
            this.txtAppKey.TabIndex = 5;
            this.txtAppKey.Text = "B549EB2D0CFF76F7069E5D7E0287ABD0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(371, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "DevAddr";
            // 
            // txtDevAddr
            // 
            this.txtDevAddr.Enabled = false;
            this.txtDevAddr.Location = new System.Drawing.Point(426, 56);
            this.txtDevAddr.Name = "txtDevAddr";
            this.txtDevAddr.ReadOnly = true;
            this.txtDevAddr.Size = new System.Drawing.Size(252, 20);
            this.txtDevAddr.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "NwkSKey";
            // 
            // txtNwkSKey
            // 
            this.txtNwkSKey.Enabled = false;
            this.txtNwkSKey.Location = new System.Drawing.Point(426, 82);
            this.txtNwkSKey.Name = "txtNwkSKey";
            this.txtNwkSKey.ReadOnly = true;
            this.txtNwkSKey.Size = new System.Drawing.Size(252, 20);
            this.txtNwkSKey.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "AppSKey";
            // 
            // txtAppSKey
            // 
            this.txtAppSKey.Enabled = false;
            this.txtAppSKey.Location = new System.Drawing.Point(426, 108);
            this.txtAppSKey.Name = "txtAppSKey";
            this.txtAppSKey.ReadOnly = true;
            this.txtAppSKey.Size = new System.Drawing.Size(252, 20);
            this.txtAppSKey.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Data";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(62, 169);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(616, 20);
            this.txtData.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Send Message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(62, 227);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(616, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 262);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAppSKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNwkSKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDevAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAppKey);
            this.Controls.Add(this.txtAppEUI);
            this.Controls.Add(this.txtDevEUI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FakeNode";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDevEUI;
        private System.Windows.Forms.TextBox txtAppEUI;
        private System.Windows.Forms.TextBox txtAppKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDevAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNwkSKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAppSKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

