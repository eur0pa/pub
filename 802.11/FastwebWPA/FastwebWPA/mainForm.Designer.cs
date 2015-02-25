namespace FastwebWPA
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PSK_button = new System.Windows.Forms.Button();
            this.PSK_output = new System.Windows.Forms.TextBox();
            this.ESSID_input = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PSK_button);
            this.groupBox1.Controls.Add(this.PSK_output);
            this.groupBox1.Controls.Add(this.ESSID_input);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "pirelli / telsey essid";
            // 
            // PSK_button
            // 
            this.PSK_button.Location = new System.Drawing.Point(6, 42);
            this.PSK_button.Name = "PSK_button";
            this.PSK_button.Size = new System.Drawing.Size(50, 22);
            this.PSK_button.TabIndex = 1;
            this.PSK_button.Text = "psk";
            this.PSK_button.UseVisualStyleBackColor = true;
            this.PSK_button.Click += new System.EventHandler(this.PSK_button_Click);
            // 
            // PSK_output
            // 
            this.PSK_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PSK_output.ForeColor = System.Drawing.Color.Firebrick;
            this.PSK_output.Location = new System.Drawing.Point(62, 43);
            this.PSK_output.Name = "PSK_output";
            this.PSK_output.ReadOnly = true;
            this.PSK_output.Size = new System.Drawing.Size(100, 20);
            this.PSK_output.TabIndex = 2;
            this.PSK_output.Text = "insert an essid";
            this.PSK_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ESSID_input
            // 
            this.ESSID_input.Location = new System.Drawing.Point(6, 19);
            this.ESSID_input.MaxLength = 22;
            this.ESSID_input.Name = "ESSID_input";
            this.ESSID_input.Size = new System.Drawing.Size(156, 20);
            this.ESSID_input.TabIndex = 0;
            this.ESSID_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ESSID_input.WordWrap = false;
            this.ESSID_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ESSID_input_KeyDown);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(197, 97);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FastWeb Pirelli & Telsey PSK Tool";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ESSID_input;
        private System.Windows.Forms.Button PSK_button;
        private System.Windows.Forms.TextBox PSK_output;
    }
}

