namespace AliceAGPF_kQ
{
    partial class KQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KQ));
            this.essid_input = new System.Windows.Forms.TextBox();
            this.serial_input = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kq_output = new System.Windows.Forms.TextBox();
            this.kq_ok = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ql_output = new System.Windows.Forms.TextBox();
            this.q2_input = new System.Windows.Forms.TextBox();
            this.serial2_input = new System.Windows.Forms.TextBox();
            this.essid2_input = new System.Windows.Forms.TextBox();
            this.q1_input = new System.Windows.Forms.TextBox();
            this.serial1_input = new System.Windows.Forms.TextBox();
            this.essid1_input = new System.Windows.Forms.TextBox();
            this.qlimit_ok = new System.Windows.Forms.Button();
            this.openfile_dialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // essid_input
            // 
            this.essid_input.Location = new System.Drawing.Point(6, 19);
            this.essid_input.MaxLength = 14;
            this.essid_input.Name = "essid_input";
            this.essid_input.Size = new System.Drawing.Size(100, 20);
            this.essid_input.TabIndex = 0;
            this.essid_input.Text = "Alice-94390829";
            this.essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.essid_input.WordWrap = false;
            this.essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.essid_input_KeyDown);
            // 
            // serial_input
            // 
            this.serial_input.Location = new System.Drawing.Point(112, 19);
            this.serial_input.MaxLength = 13;
            this.serial_input.Name = "serial_input";
            this.serial_input.Size = new System.Drawing.Size(100, 20);
            this.serial_input.TabIndex = 1;
            this.serial_input.Text = "67901X0116705";
            this.serial_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serial_input.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kq_output);
            this.groupBox1.Controls.Add(this.kq_ok);
            this.groupBox1.Controls.Add(this.essid_input);
            this.groupBox1.Controls.Add(this.serial_input);
            this.groupBox1.Location = new System.Drawing.Point(5, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "k && Q finder";
            // 
            // kq_output
            // 
            this.kq_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kq_output.Location = new System.Drawing.Point(8, 45);
            this.kq_output.Name = "kq_output";
            this.kq_output.ReadOnly = true;
            this.kq_output.Size = new System.Drawing.Size(237, 20);
            this.kq_output.TabIndex = 4;
            this.kq_output.Text = "load a list (essid serieXserial format)";
            this.kq_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.kq_output.MouseClick += new System.Windows.Forms.MouseEventHandler(this.kq_output_MouseClick);
            // 
            // kq_ok
            // 
            this.kq_ok.Location = new System.Drawing.Point(218, 19);
            this.kq_ok.Name = "kq_ok";
            this.kq_ok.Size = new System.Drawing.Size(30, 20);
            this.kq_ok.TabIndex = 3;
            this.kq_ok.Text = "ok";
            this.kq_ok.UseVisualStyleBackColor = true;
            this.kq_ok.Click += new System.EventHandler(this.kq_ok_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ql_output);
            this.groupBox2.Controls.Add(this.q2_input);
            this.groupBox2.Controls.Add(this.serial2_input);
            this.groupBox2.Controls.Add(this.essid2_input);
            this.groupBox2.Controls.Add(this.q1_input);
            this.groupBox2.Controls.Add(this.serial1_input);
            this.groupBox2.Controls.Add(this.essid1_input);
            this.groupBox2.Location = new System.Drawing.Point(5, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 125);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Q limits finder";
            // 
            // ql_output
            // 
            this.ql_output.Location = new System.Drawing.Point(9, 97);
            this.ql_output.Name = "ql_output";
            this.ql_output.Size = new System.Drawing.Size(206, 20);
            this.ql_output.TabIndex = 7;
            // 
            // q2_input
            // 
            this.q2_input.Location = new System.Drawing.Point(115, 71);
            this.q2_input.Name = "q2_input";
            this.q2_input.Size = new System.Drawing.Size(100, 20);
            this.q2_input.TabIndex = 5;
            // 
            // serial2_input
            // 
            this.serial2_input.Location = new System.Drawing.Point(9, 71);
            this.serial2_input.Name = "serial2_input";
            this.serial2_input.Size = new System.Drawing.Size(100, 20);
            this.serial2_input.TabIndex = 4;
            // 
            // essid2_input
            // 
            this.essid2_input.Location = new System.Drawing.Point(115, 45);
            this.essid2_input.Name = "essid2_input";
            this.essid2_input.Size = new System.Drawing.Size(100, 20);
            this.essid2_input.TabIndex = 3;
            // 
            // q1_input
            // 
            this.q1_input.Location = new System.Drawing.Point(9, 45);
            this.q1_input.Name = "q1_input";
            this.q1_input.Size = new System.Drawing.Size(100, 20);
            this.q1_input.TabIndex = 2;
            // 
            // serial1_input
            // 
            this.serial1_input.Location = new System.Drawing.Point(115, 19);
            this.serial1_input.Name = "serial1_input";
            this.serial1_input.Size = new System.Drawing.Size(100, 20);
            this.serial1_input.TabIndex = 1;
            // 
            // essid1_input
            // 
            this.essid1_input.Location = new System.Drawing.Point(9, 19);
            this.essid1_input.Name = "essid1_input";
            this.essid1_input.Size = new System.Drawing.Size(100, 20);
            this.essid1_input.TabIndex = 0;
            // 
            // qlimit_ok
            // 
            this.qlimit_ok.Location = new System.Drawing.Point(226, 140);
            this.qlimit_ok.Name = "qlimit_ok";
            this.qlimit_ok.Size = new System.Drawing.Size(27, 21);
            this.qlimit_ok.TabIndex = 6;
            this.qlimit_ok.Text = "ok";
            this.qlimit_ok.UseVisualStyleBackColor = true;
            // 
            // openfile_dialog
            // 
            this.openfile_dialog.FileName = "list.txt";
            // 
            // KQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 213);
            this.Controls.Add(this.qlimit_ok);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KQ";
            this.Text = "Alice AGPF k & Q Finder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox essid_input;
        private System.Windows.Forms.TextBox serial_input;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox kq_output;
        private System.Windows.Forms.Button kq_ok;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ql_output;
        private System.Windows.Forms.TextBox q2_input;
        private System.Windows.Forms.TextBox serial2_input;
        private System.Windows.Forms.TextBox essid2_input;
        private System.Windows.Forms.TextBox q1_input;
        private System.Windows.Forms.TextBox serial1_input;
        private System.Windows.Forms.TextBox essid1_input;
        private System.Windows.Forms.Button qlimit_ok;
        private System.Windows.Forms.OpenFileDialog openfile_dialog;
    }
}

