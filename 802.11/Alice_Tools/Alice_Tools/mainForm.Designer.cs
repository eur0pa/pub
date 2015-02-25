namespace Alice_Tools
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.psk_tab = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.psk_update_button = new System.Windows.Forms.Button();
            this.psk_bssid_input = new System.Windows.Forms.TextBox();
            this.psk_psk_button = new System.Windows.Forms.Button();
            this.psk_essid_input = new System.Windows.Forms.TextBox();
            this.kq_tab = new System.Windows.Forms.TabPage();
            this.kq_export_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kq_open_button = new System.Windows.Forms.Button();
            this.kq_button = new System.Windows.Forms.Button();
            this.kq_serial_input = new System.Windows.Forms.TextBox();
            this.kq_essid_input = new System.Windows.Forms.TextBox();
            this.brute_serialmac_tab = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.brute_snmac_threads = new System.Windows.Forms.NumericUpDown();
            this.brute_snmac_quick = new System.Windows.Forms.CheckBox();
            this.brute_snmac_button = new System.Windows.Forms.Button();
            this.brute_snmac_psk_input = new System.Windows.Forms.TextBox();
            this.brute_snmac_essid_input = new System.Windows.Forms.TextBox();
            this.brute_psk_tab = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.brute_psk_threads = new System.Windows.Forms.NumericUpDown();
            this.brute_psk_quick = new System.Windows.Forms.CheckBox();
            this.brute_psk_button = new System.Windows.Forms.Button();
            this.brute_psk_essid_input = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.kq_openfile_dialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.psk_tab.SuspendLayout();
            this.kq_tab.SuspendLayout();
            this.brute_serialmac_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brute_snmac_threads)).BeginInit();
            this.brute_psk_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brute_psk_threads)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.psk_tab);
            this.tabControl1.Controls.Add(this.kq_tab);
            this.tabControl1.Controls.Add(this.brute_serialmac_tab);
            this.tabControl1.Controls.Add(this.brute_psk_tab);
            this.tabControl1.Location = new System.Drawing.Point(-1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(261, 104);
            this.tabControl1.TabIndex = 0;
            // 
            // psk_tab
            // 
            this.psk_tab.Controls.Add(this.label9);
            this.psk_tab.Controls.Add(this.label1);
            this.psk_tab.Controls.Add(this.psk_update_button);
            this.psk_tab.Controls.Add(this.psk_bssid_input);
            this.psk_tab.Controls.Add(this.psk_psk_button);
            this.psk_tab.Controls.Add(this.psk_essid_input);
            this.psk_tab.Location = new System.Drawing.Point(4, 22);
            this.psk_tab.Name = "psk_tab";
            this.psk_tab.Padding = new System.Windows.Forms.Padding(3);
            this.psk_tab.Size = new System.Drawing.Size(253, 78);
            this.psk_tab.TabIndex = 0;
            this.psk_tab.Text = "PSK";
            this.psk_tab.ToolTipText = "generate psk";
            this.psk_tab.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(123, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "bssid (opt)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(35, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "essid";
            // 
            // psk_update_button
            // 
            this.psk_update_button.Location = new System.Drawing.Point(230, 20);
            this.psk_update_button.Name = "psk_update_button";
            this.psk_update_button.Size = new System.Drawing.Size(19, 23);
            this.psk_update_button.TabIndex = 2;
            this.psk_update_button.Text = "U";
            this.psk_update_button.UseVisualStyleBackColor = true;
            this.psk_update_button.Click += new System.EventHandler(this.psk_update_button_Click);
            // 
            // psk_bssid_input
            // 
            this.psk_bssid_input.Location = new System.Drawing.Point(100, 21);
            this.psk_bssid_input.MaxLength = 17;
            this.psk_bssid_input.Name = "psk_bssid_input";
            this.psk_bssid_input.Size = new System.Drawing.Size(100, 20);
            this.psk_bssid_input.TabIndex = 1;
            this.psk_bssid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.psk_bssid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psk_bssid_input_KeyDown);
            // 
            // psk_psk_button
            // 
            this.psk_psk_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.psk_psk_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.psk_psk_button.Location = new System.Drawing.Point(201, 20);
            this.psk_psk_button.Name = "psk_psk_button";
            this.psk_psk_button.Size = new System.Drawing.Size(27, 23);
            this.psk_psk_button.TabIndex = 2;
            this.psk_psk_button.Text = "ok";
            this.psk_psk_button.UseVisualStyleBackColor = true;
            this.psk_psk_button.Click += new System.EventHandler(this.psk_psk_button_Click);
            // 
            // psk_essid_input
            // 
            this.psk_essid_input.Location = new System.Drawing.Point(4, 21);
            this.psk_essid_input.MaxLength = 14;
            this.psk_essid_input.Name = "psk_essid_input";
            this.psk_essid_input.Size = new System.Drawing.Size(92, 20);
            this.psk_essid_input.TabIndex = 0;
            this.psk_essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.psk_essid_input.WordWrap = false;
            this.psk_essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psk_essid_input_KeyDown);
            // 
            // kq_tab
            // 
            this.kq_tab.Controls.Add(this.kq_export_button);
            this.kq_tab.Controls.Add(this.label3);
            this.kq_tab.Controls.Add(this.label2);
            this.kq_tab.Controls.Add(this.kq_open_button);
            this.kq_tab.Controls.Add(this.kq_button);
            this.kq_tab.Controls.Add(this.kq_serial_input);
            this.kq_tab.Controls.Add(this.kq_essid_input);
            this.kq_tab.Location = new System.Drawing.Point(4, 22);
            this.kq_tab.Name = "kq_tab";
            this.kq_tab.Padding = new System.Windows.Forms.Padding(3);
            this.kq_tab.Size = new System.Drawing.Size(253, 78);
            this.kq_tab.TabIndex = 1;
            this.kq_tab.Text = "k && Q";
            this.kq_tab.ToolTipText = "compute k and Q";
            this.kq_tab.UseVisualStyleBackColor = true;
            // 
            // kq_export_button
            // 
            this.kq_export_button.Location = new System.Drawing.Point(194, 47);
            this.kq_export_button.Name = "kq_export_button";
            this.kq_export_button.Size = new System.Drawing.Size(53, 23);
            this.kq_export_button.TabIndex = 8;
            this.kq_export_button.Text = "export";
            this.kq_export_button.UseVisualStyleBackColor = true;
            this.kq_export_button.Click += new System.EventHandler(this.kq_export_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(130, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "serial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(35, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "essid";
            // 
            // kq_open_button
            // 
            this.kq_open_button.Location = new System.Drawing.Point(222, 20);
            this.kq_open_button.Name = "kq_open_button";
            this.kq_open_button.Size = new System.Drawing.Size(25, 23);
            this.kq_open_button.TabIndex = 5;
            this.kq_open_button.Text = "...";
            this.kq_open_button.UseVisualStyleBackColor = true;
            this.kq_open_button.Click += new System.EventHandler(this.kq_open_button_Click);
            // 
            // kq_button
            // 
            this.kq_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kq_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kq_button.Location = new System.Drawing.Point(194, 20);
            this.kq_button.Name = "kq_button";
            this.kq_button.Size = new System.Drawing.Size(27, 23);
            this.kq_button.TabIndex = 3;
            this.kq_button.Text = "ok";
            this.kq_button.UseVisualStyleBackColor = true;
            this.kq_button.Click += new System.EventHandler(this.kq_button_Click);
            // 
            // kq_serial_input
            // 
            this.kq_serial_input.Location = new System.Drawing.Point(100, 21);
            this.kq_serial_input.MaxLength = 13;
            this.kq_serial_input.Name = "kq_serial_input";
            this.kq_serial_input.Size = new System.Drawing.Size(91, 20);
            this.kq_serial_input.TabIndex = 1;
            this.kq_serial_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.kq_serial_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kq_serial_input_KeyDown);
            // 
            // kq_essid_input
            // 
            this.kq_essid_input.Location = new System.Drawing.Point(4, 21);
            this.kq_essid_input.MaxLength = 14;
            this.kq_essid_input.Name = "kq_essid_input";
            this.kq_essid_input.Size = new System.Drawing.Size(92, 20);
            this.kq_essid_input.TabIndex = 0;
            this.kq_essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.kq_essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kq_essid_input_KeyDown);
            // 
            // brute_serialmac_tab
            // 
            this.brute_serialmac_tab.Controls.Add(this.label8);
            this.brute_serialmac_tab.Controls.Add(this.label7);
            this.brute_serialmac_tab.Controls.Add(this.label5);
            this.brute_serialmac_tab.Controls.Add(this.brute_snmac_threads);
            this.brute_serialmac_tab.Controls.Add(this.brute_snmac_quick);
            this.brute_serialmac_tab.Controls.Add(this.brute_snmac_button);
            this.brute_serialmac_tab.Controls.Add(this.brute_snmac_psk_input);
            this.brute_serialmac_tab.Controls.Add(this.brute_snmac_essid_input);
            this.brute_serialmac_tab.Location = new System.Drawing.Point(4, 22);
            this.brute_serialmac_tab.Name = "brute_serialmac_tab";
            this.brute_serialmac_tab.Padding = new System.Windows.Forms.Padding(3);
            this.brute_serialmac_tab.Size = new System.Drawing.Size(253, 78);
            this.brute_serialmac_tab.TabIndex = 2;
            this.brute_serialmac_tab.Text = "brute sn/mac";
            this.brute_serialmac_tab.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(162, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "psk";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(35, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "essid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(52, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "threads";
            // 
            // brute_snmac_threads
            // 
            this.brute_snmac_threads.Enabled = false;
            this.brute_snmac_threads.Location = new System.Drawing.Point(11, 48);
            this.brute_snmac_threads.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.brute_snmac_threads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.brute_snmac_threads.Name = "brute_snmac_threads";
            this.brute_snmac_threads.ReadOnly = true;
            this.brute_snmac_threads.Size = new System.Drawing.Size(35, 20);
            this.brute_snmac_threads.TabIndex = 7;
            this.brute_snmac_threads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.brute_snmac_threads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // brute_snmac_quick
            // 
            this.brute_snmac_quick.AutoSize = true;
            this.brute_snmac_quick.Checked = true;
            this.brute_snmac_quick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.brute_snmac_quick.ForeColor = System.Drawing.SystemColors.ControlText;
            this.brute_snmac_quick.Location = new System.Drawing.Point(150, 50);
            this.brute_snmac_quick.Name = "brute_snmac_quick";
            this.brute_snmac_quick.Size = new System.Drawing.Size(52, 17);
            this.brute_snmac_quick.TabIndex = 6;
            this.brute_snmac_quick.Text = "quick";
            this.brute_snmac_quick.UseVisualStyleBackColor = true;
            // 
            // brute_snmac_button
            // 
            this.brute_snmac_button.Location = new System.Drawing.Point(100, 47);
            this.brute_snmac_button.Name = "brute_snmac_button";
            this.brute_snmac_button.Size = new System.Drawing.Size(44, 23);
            this.brute_snmac_button.TabIndex = 3;
            this.brute_snmac_button.Text = "brute";
            this.brute_snmac_button.UseVisualStyleBackColor = true;
            this.brute_snmac_button.Click += new System.EventHandler(this.brute_snmac_button_Click);
            // 
            // brute_snmac_psk_input
            // 
            this.brute_snmac_psk_input.Location = new System.Drawing.Point(100, 21);
            this.brute_snmac_psk_input.MaxLength = 24;
            this.brute_snmac_psk_input.Name = "brute_snmac_psk_input";
            this.brute_snmac_psk_input.Size = new System.Drawing.Size(148, 20);
            this.brute_snmac_psk_input.TabIndex = 2;
            this.brute_snmac_psk_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.brute_snmac_psk_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brute_snmac_psk_input_KeyDown);
            // 
            // brute_snmac_essid_input
            // 
            this.brute_snmac_essid_input.Location = new System.Drawing.Point(4, 21);
            this.brute_snmac_essid_input.MaxLength = 14;
            this.brute_snmac_essid_input.Name = "brute_snmac_essid_input";
            this.brute_snmac_essid_input.Size = new System.Drawing.Size(92, 20);
            this.brute_snmac_essid_input.TabIndex = 0;
            this.brute_snmac_essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.brute_snmac_essid_input.WordWrap = false;
            this.brute_snmac_essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brute_snmac_essid_input_KeyDown);
            // 
            // brute_psk_tab
            // 
            this.brute_psk_tab.Controls.Add(this.label4);
            this.brute_psk_tab.Controls.Add(this.label6);
            this.brute_psk_tab.Controls.Add(this.brute_psk_threads);
            this.brute_psk_tab.Controls.Add(this.brute_psk_quick);
            this.brute_psk_tab.Controls.Add(this.brute_psk_button);
            this.brute_psk_tab.Controls.Add(this.brute_psk_essid_input);
            this.brute_psk_tab.Location = new System.Drawing.Point(4, 22);
            this.brute_psk_tab.Name = "brute_psk_tab";
            this.brute_psk_tab.Padding = new System.Windows.Forms.Padding(3);
            this.brute_psk_tab.Size = new System.Drawing.Size(253, 78);
            this.brute_psk_tab.TabIndex = 4;
            this.brute_psk_tab.Text = "brute psk";
            this.brute_psk_tab.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(108, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "threads";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(39, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "essid";
            // 
            // brute_psk_threads
            // 
            this.brute_psk_threads.Location = new System.Drawing.Point(112, 21);
            this.brute_psk_threads.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.brute_psk_threads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.brute_psk_threads.Name = "brute_psk_threads";
            this.brute_psk_threads.Size = new System.Drawing.Size(35, 20);
            this.brute_psk_threads.TabIndex = 9;
            this.brute_psk_threads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.brute_psk_threads.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // brute_psk_quick
            // 
            this.brute_psk_quick.AutoSize = true;
            this.brute_psk_quick.Checked = true;
            this.brute_psk_quick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.brute_psk_quick.Location = new System.Drawing.Point(203, 24);
            this.brute_psk_quick.Name = "brute_psk_quick";
            this.brute_psk_quick.Size = new System.Drawing.Size(52, 17);
            this.brute_psk_quick.TabIndex = 8;
            this.brute_psk_quick.Text = "quick";
            this.brute_psk_quick.UseVisualStyleBackColor = true;
            // 
            // brute_psk_button
            // 
            this.brute_psk_button.Location = new System.Drawing.Point(154, 20);
            this.brute_psk_button.Name = "brute_psk_button";
            this.brute_psk_button.Size = new System.Drawing.Size(44, 23);
            this.brute_psk_button.TabIndex = 3;
            this.brute_psk_button.Text = "brute";
            this.brute_psk_button.UseVisualStyleBackColor = true;
            this.brute_psk_button.Click += new System.EventHandler(this.brute_psk_button_Click);
            // 
            // brute_psk_essid_input
            // 
            this.brute_psk_essid_input.Location = new System.Drawing.Point(4, 21);
            this.brute_psk_essid_input.MaxLength = 14;
            this.brute_psk_essid_input.Name = "brute_psk_essid_input";
            this.brute_psk_essid_input.Size = new System.Drawing.Size(101, 20);
            this.brute_psk_essid_input.TabIndex = 0;
            this.brute_psk_essid_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.brute_psk_essid_input.WordWrap = false;
            this.brute_psk_essid_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brute_psk_mac_input_KeyDown);
            // 
            // output
            // 
            this.output.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(-3, 101);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(261, 101);
            this.output.TabIndex = 1;
            this.output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // kq_openfile_dialog
            // 
            this.kq_openfile_dialog.FileName = "list.txt";
            this.kq_openfile_dialog.Title = "gimme a list ;)";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(256, 200);
            this.Controls.Add(this.output);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alice WPA-PSK SAT";
            this.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.psk_tab.ResumeLayout(false);
            this.psk_tab.PerformLayout();
            this.kq_tab.ResumeLayout(false);
            this.kq_tab.PerformLayout();
            this.brute_serialmac_tab.ResumeLayout(false);
            this.brute_serialmac_tab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brute_snmac_threads)).EndInit();
            this.brute_psk_tab.ResumeLayout(false);
            this.brute_psk_tab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brute_psk_threads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage psk_tab;
        private System.Windows.Forms.TabPage kq_tab;
        private System.Windows.Forms.Button psk_psk_button;
        private System.Windows.Forms.TextBox psk_essid_input;
        private System.Windows.Forms.TextBox psk_bssid_input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button psk_update_button;
        private System.Windows.Forms.Button kq_button;
        private System.Windows.Forms.TextBox kq_serial_input;
        private System.Windows.Forms.TextBox kq_essid_input;
        private System.Windows.Forms.Button kq_open_button;
        private System.Windows.Forms.OpenFileDialog kq_openfile_dialog;
        private System.Windows.Forms.TabPage brute_serialmac_tab;
        private System.Windows.Forms.TextBox brute_snmac_essid_input;
        private System.Windows.Forms.TextBox brute_snmac_psk_input;
        private System.Windows.Forms.Button brute_snmac_button;
        private System.Windows.Forms.TabPage brute_psk_tab;
        private System.Windows.Forms.Button brute_psk_button;
        private System.Windows.Forms.TextBox brute_psk_essid_input;
        private System.Windows.Forms.CheckBox brute_snmac_quick;
        private System.Windows.Forms.CheckBox brute_psk_quick;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown brute_snmac_threads;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown brute_psk_threads;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button kq_export_button;
    }
}

