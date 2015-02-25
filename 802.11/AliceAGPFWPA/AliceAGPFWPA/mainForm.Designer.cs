namespace AliceAGPFWPA
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.essid_input = new System.Windows.Forms.TextBox();
            this.psk_button = new System.Windows.Forms.Button();
            this.mac_input = new System.Windows.Forms.TextBox();
            this.psk_output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // essid_input
            // 
            this.essid_input.Location = new System.Drawing.Point(12, 12);
            this.essid_input.MaxLength = 14;
            this.essid_input.Name = "essid_input";
            this.essid_input.Size = new System.Drawing.Size(100, 20);
            this.essid_input.TabIndex = 0;
            this.essid_input.Text = "Alice-53385145";
            this.essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.essid_input.WordWrap = false;
            this.essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.essid_input_KeyDown);
            // 
            // psk_button
            // 
            this.psk_button.Location = new System.Drawing.Point(118, 10);
            this.psk_button.Name = "psk_button";
            this.psk_button.Size = new System.Drawing.Size(38, 23);
            this.psk_button.TabIndex = 2;
            this.psk_button.Text = "psk";
            this.psk_button.UseVisualStyleBackColor = true;
            this.psk_button.Click += new System.EventHandler(this.psk_button_Click);
            // 
            // mac_input
            // 
            this.mac_input.Location = new System.Drawing.Point(162, 12);
            this.mac_input.MaxLength = 17;
            this.mac_input.Name = "mac_input";
            this.mac_input.Size = new System.Drawing.Size(100, 20);
            this.mac_input.TabIndex = 1;
            this.mac_input.Text = "00:25:53:09:5F:3C";
            this.mac_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mac_input.WordWrap = false;
            this.mac_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mac_input_KeyDown);
            // 
            // psk_output
            // 
            this.psk_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.psk_output.Location = new System.Drawing.Point(12, 39);
            this.psk_output.MaxLength = 24;
            this.psk_output.Name = "psk_output";
            this.psk_output.ReadOnly = true;
            this.psk_output.Size = new System.Drawing.Size(250, 20);
            this.psk_output.TabIndex = 3;
            this.psk_output.Text = "input essid or click to update config.txt";
            this.psk_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.psk_output.WordWrap = false;
            this.psk_output.Click += new System.EventHandler(this.psk_output_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 72);
            this.Controls.Add(this.psk_output);
            this.Controls.Add(this.mac_input);
            this.Controls.Add(this.psk_button);
            this.Controls.Add(this.essid_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alice AGPF WPA-PSK Recovery Tool";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox essid_input;
        private System.Windows.Forms.Button psk_button;
        private System.Windows.Forms.TextBox mac_input;
        private System.Windows.Forms.TextBox psk_output;

    }
}

