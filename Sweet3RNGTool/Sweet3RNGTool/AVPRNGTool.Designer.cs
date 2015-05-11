namespace Sweet3RNGTool
{
    partial class AVPRNGTool
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
            this.LoadPaytable = new System.Windows.Forms.Button();
            this.P1_Select = new System.Windows.Forms.ComboBox();
            this.l_ThemeName = new System.Windows.Forms.Label();
            this.Result = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GenerateRNG = new System.Windows.Forms.Button();
            this.SendToEGM = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.IP = new System.Windows.Forms.TextBox();
            this.Disconnect = new System.Windows.Forms.Button();
            this.COM = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Doors = new System.Windows.Forms.Button();
            this.Key = new System.Windows.Forms.Button();
            this.Cashout = new System.Windows.Forms.Button();
            this.Billin = new System.Windows.Forms.Button();
            this.EGMIP = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.l_Note = new System.Windows.Forms.Label();
            this.ddl_bonus = new System.Windows.Forms.ComboBox();
            this.cb_bonus = new System.Windows.Forms.CheckBox();
            this.lb_R1 = new System.Windows.Forms.ListBox();
            this.lb_R2 = new System.Windows.Forms.ListBox();
            this.lb_R3 = new System.Windows.Forms.ListBox();
            this.lb_R4 = new System.Windows.Forms.ListBox();
            this.lb_R5 = new System.Windows.Forms.ListBox();
            this.r1_search = new System.Windows.Forms.TextBox();
            this.r2_search = new System.Windows.Forms.TextBox();
            this.r3_search = new System.Windows.Forms.TextBox();
            this.r4_search = new System.Windows.Forms.TextBox();
            this.r5_search = new System.Windows.Forms.TextBox();
            this.btn_LoadRNG = new System.Windows.Forms.Button();
            this.btn_SaveRNG = new System.Windows.Forms.Button();
            this.btn_GetReelStrip = new System.Windows.Forms.Button();
            this.lb_WinCat = new System.Windows.Forms.ListBox();
            this.btnReplaceST = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoadPaytable
            // 
            this.LoadPaytable.Location = new System.Drawing.Point(12, 12);
            this.LoadPaytable.Name = "LoadPaytable";
            this.LoadPaytable.Size = new System.Drawing.Size(94, 28);
            this.LoadPaytable.TabIndex = 0;
            this.LoadPaytable.Text = "Load Paytable";
            this.LoadPaytable.UseVisualStyleBackColor = true;
            this.LoadPaytable.Click += new System.EventHandler(this.LoadPaytable_Click);
            // 
            // P1_Select
            // 
            this.P1_Select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.P1_Select.FormattingEnabled = true;
            this.P1_Select.Location = new System.Drawing.Point(12, 123);
            this.P1_Select.Name = "P1_Select";
            this.P1_Select.Size = new System.Drawing.Size(121, 21);
            this.P1_Select.TabIndex = 1;
            this.P1_Select.SelectedIndexChanged += new System.EventHandler(this.P1_Select_SelectedIndexChanged);
            // 
            // l_ThemeName
            // 
            this.l_ThemeName.AutoSize = true;
            this.l_ThemeName.Location = new System.Drawing.Point(12, 107);
            this.l_ThemeName.Name = "l_ThemeName";
            this.l_ThemeName.Size = new System.Drawing.Size(0, 13);
            this.l_ThemeName.TabIndex = 4;
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(12, 364);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(276, 210);
            this.Result.TabIndex = 22;
            this.Result.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "RNG:";
            // 
            // GenerateRNG
            // 
            this.GenerateRNG.Location = new System.Drawing.Point(305, 364);
            this.GenerateRNG.Name = "GenerateRNG";
            this.GenerateRNG.Size = new System.Drawing.Size(98, 35);
            this.GenerateRNG.TabIndex = 24;
            this.GenerateRNG.Text = "Generate RNG";
            this.GenerateRNG.UseVisualStyleBackColor = true;
            this.GenerateRNG.Click += new System.EventHandler(this.GenerateRNG_Click);
            // 
            // SendToEGM
            // 
            this.SendToEGM.Location = new System.Drawing.Point(305, 405);
            this.SendToEGM.Name = "SendToEGM";
            this.SendToEGM.Size = new System.Drawing.Size(98, 35);
            this.SendToEGM.TabIndex = 25;
            this.SendToEGM.Text = "Send To EGM";
            this.SendToEGM.UseVisualStyleBackColor = true;
            this.SendToEGM.Click += new System.EventHandler(this.SendToEGM_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(305, 539);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(98, 35);
            this.Clear.TabIndex = 26;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(625, 15);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 27;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(519, 17);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(100, 20);
            this.IP.TabIndex = 28;
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(625, 44);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 29;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // COM
            // 
            this.COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM.FormattingEnabled = true;
            this.COM.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.COM.Location = new System.Drawing.Point(519, 44);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(100, 21);
            this.COM.TabIndex = 30;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(626, 73);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 17);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "TestRNG";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Doors
            // 
            this.Doors.Location = new System.Drawing.Point(625, 93);
            this.Doors.Name = "Doors";
            this.Doors.Size = new System.Drawing.Size(75, 23);
            this.Doors.TabIndex = 32;
            this.Doors.Text = "C, M door";
            this.Doors.UseVisualStyleBackColor = true;
            this.Doors.Click += new System.EventHandler(this.Doors_Click);
            // 
            // Key
            // 
            this.Key.Location = new System.Drawing.Point(625, 121);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(75, 23);
            this.Key.TabIndex = 33;
            this.Key.Text = "JackpotKey";
            this.Key.UseVisualStyleBackColor = true;
            this.Key.Click += new System.EventHandler(this.Key_Click);
            // 
            // Cashout
            // 
            this.Cashout.Location = new System.Drawing.Point(625, 149);
            this.Cashout.Name = "Cashout";
            this.Cashout.Size = new System.Drawing.Size(75, 23);
            this.Cashout.TabIndex = 34;
            this.Cashout.Text = "Cashout";
            this.Cashout.UseVisualStyleBackColor = true;
            this.Cashout.Click += new System.EventHandler(this.Cashout_Click);
            // 
            // Billin
            // 
            this.Billin.Location = new System.Drawing.Point(625, 178);
            this.Billin.Name = "Billin";
            this.Billin.Size = new System.Drawing.Size(75, 23);
            this.Billin.TabIndex = 35;
            this.Billin.Text = "Bill in";
            this.Billin.UseVisualStyleBackColor = true;
            this.Billin.Click += new System.EventHandler(this.Billin_Click);
            // 
            // EGMIP
            // 
            this.EGMIP.AutoSize = true;
            this.EGMIP.Location = new System.Drawing.Point(472, 20);
            this.EGMIP.Name = "EGMIP";
            this.EGMIP.Size = new System.Drawing.Size(44, 13);
            this.EGMIP.TabIndex = 36;
            this.EGMIP.Text = "EGM IP";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(473, 48);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(31, 13);
            this.Port.TabIndex = 37;
            this.Port.Text = "COM";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(605, 539);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(95, 35);
            this.exit.TabIndex = 38;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // l_Note
            // 
            this.l_Note.AutoSize = true;
            this.l_Note.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_Note.Location = new System.Drawing.Point(12, 46);
            this.l_Note.Name = "l_Note";
            this.l_Note.Size = new System.Drawing.Size(211, 13);
            this.l_Note.TabIndex = 39;
            this.l_Note.Text = "NOTE: Please load .paytable file of ";
            // 
            // ddl_bonus
            // 
            this.ddl_bonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_bonus.FormattingEnabled = true;
            this.ddl_bonus.Location = new System.Drawing.Point(167, 123);
            this.ddl_bonus.Name = "ddl_bonus";
            this.ddl_bonus.Size = new System.Drawing.Size(121, 21);
            this.ddl_bonus.TabIndex = 46;
            // 
            // cb_bonus
            // 
            this.cb_bonus.AutoSize = true;
            this.cb_bonus.Location = new System.Drawing.Point(167, 106);
            this.cb_bonus.Name = "cb_bonus";
            this.cb_bonus.Size = new System.Drawing.Size(62, 17);
            this.cb_bonus.TabIndex = 48;
            this.cb_bonus.Text = "Bonus?";
            this.cb_bonus.UseVisualStyleBackColor = true;
            this.cb_bonus.CheckedChanged += new System.EventHandler(this.cb_bonus_CheckedChanged);
            // 
            // lb_R1
            // 
            this.lb_R1.FormattingEnabled = true;
            this.lb_R1.Location = new System.Drawing.Point(12, 179);
            this.lb_R1.Name = "lb_R1";
            this.lb_R1.Size = new System.Drawing.Size(50, 147);
            this.lb_R1.TabIndex = 50;
            // 
            // lb_R2
            // 
            this.lb_R2.FormattingEnabled = true;
            this.lb_R2.Location = new System.Drawing.Point(68, 179);
            this.lb_R2.Name = "lb_R2";
            this.lb_R2.Size = new System.Drawing.Size(50, 147);
            this.lb_R2.TabIndex = 51;
            // 
            // lb_R3
            // 
            this.lb_R3.FormattingEnabled = true;
            this.lb_R3.Location = new System.Drawing.Point(126, 179);
            this.lb_R3.Name = "lb_R3";
            this.lb_R3.Size = new System.Drawing.Size(50, 147);
            this.lb_R3.TabIndex = 52;
            // 
            // lb_R4
            // 
            this.lb_R4.FormattingEnabled = true;
            this.lb_R4.Location = new System.Drawing.Point(182, 179);
            this.lb_R4.Name = "lb_R4";
            this.lb_R4.Size = new System.Drawing.Size(50, 147);
            this.lb_R4.TabIndex = 53;
            // 
            // lb_R5
            // 
            this.lb_R5.FormattingEnabled = true;
            this.lb_R5.Location = new System.Drawing.Point(238, 179);
            this.lb_R5.Name = "lb_R5";
            this.lb_R5.Size = new System.Drawing.Size(50, 147);
            this.lb_R5.TabIndex = 54;
            // 
            // r1_search
            // 
            this.r1_search.Location = new System.Drawing.Point(12, 153);
            this.r1_search.Name = "r1_search";
            this.r1_search.Size = new System.Drawing.Size(50, 20);
            this.r1_search.TabIndex = 55;
            this.r1_search.TextChanged += new System.EventHandler(this.r1_search_TextChanged);
            this.r1_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.r1_search_KeyPress);
            // 
            // r2_search
            // 
            this.r2_search.Location = new System.Drawing.Point(68, 153);
            this.r2_search.Name = "r2_search";
            this.r2_search.Size = new System.Drawing.Size(50, 20);
            this.r2_search.TabIndex = 56;
            this.r2_search.TextChanged += new System.EventHandler(this.r2_search_TextChanged);
            this.r2_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.r2_search_KeyPress);
            // 
            // r3_search
            // 
            this.r3_search.Location = new System.Drawing.Point(126, 153);
            this.r3_search.Name = "r3_search";
            this.r3_search.Size = new System.Drawing.Size(50, 20);
            this.r3_search.TabIndex = 57;
            this.r3_search.TextChanged += new System.EventHandler(this.r3_search_TextChanged);
            this.r3_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.r3_search_KeyPress);
            // 
            // r4_search
            // 
            this.r4_search.Location = new System.Drawing.Point(182, 153);
            this.r4_search.Name = "r4_search";
            this.r4_search.Size = new System.Drawing.Size(50, 20);
            this.r4_search.TabIndex = 58;
            this.r4_search.TextChanged += new System.EventHandler(this.r4_search_TextChanged);
            this.r4_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.r4_search_KeyPress);
            // 
            // r5_search
            // 
            this.r5_search.Location = new System.Drawing.Point(238, 153);
            this.r5_search.Name = "r5_search";
            this.r5_search.Size = new System.Drawing.Size(50, 20);
            this.r5_search.TabIndex = 59;
            this.r5_search.TextChanged += new System.EventHandler(this.r5_search_TextChanged);
            this.r5_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.r5_search_KeyPress);
            // 
            // btn_LoadRNG
            // 
            this.btn_LoadRNG.Location = new System.Drawing.Point(305, 447);
            this.btn_LoadRNG.Name = "btn_LoadRNG";
            this.btn_LoadRNG.Size = new System.Drawing.Size(98, 33);
            this.btn_LoadRNG.TabIndex = 64;
            this.btn_LoadRNG.Text = "Load RNG";
            this.btn_LoadRNG.UseVisualStyleBackColor = true;
            this.btn_LoadRNG.Click += new System.EventHandler(this.btn_LoadRNG_Click);
            // 
            // btn_SaveRNG
            // 
            this.btn_SaveRNG.Location = new System.Drawing.Point(305, 486);
            this.btn_SaveRNG.Name = "btn_SaveRNG";
            this.btn_SaveRNG.Size = new System.Drawing.Size(98, 34);
            this.btn_SaveRNG.TabIndex = 65;
            this.btn_SaveRNG.Text = "Save RNG";
            this.btn_SaveRNG.UseVisualStyleBackColor = true;
            this.btn_SaveRNG.Click += new System.EventHandler(this.btn_SaveRNG_Click);
            // 
            // btn_GetReelStrip
            // 
            this.btn_GetReelStrip.Location = new System.Drawing.Point(605, 411);
            this.btn_GetReelStrip.Name = "btn_GetReelStrip";
            this.btn_GetReelStrip.Size = new System.Drawing.Size(92, 29);
            this.btn_GetReelStrip.TabIndex = 66;
            this.btn_GetReelStrip.Text = "Get Reel Strip";
            this.btn_GetReelStrip.UseVisualStyleBackColor = true;
            this.btn_GetReelStrip.Click += new System.EventHandler(this.btn_GetReelStrip_Click);
            // 
            // lb_WinCat
            // 
            this.lb_WinCat.FormattingEnabled = true;
            this.lb_WinCat.Location = new System.Drawing.Point(354, 179);
            this.lb_WinCat.Name = "lb_WinCat";
            this.lb_WinCat.Size = new System.Drawing.Size(187, 147);
            this.lb_WinCat.TabIndex = 67;
            this.lb_WinCat.SelectedIndexChanged += new System.EventHandler(this.lb_WinCat_SelectedIndexChanged);
            // 
            // btnReplaceST
            // 
            this.btnReplaceST.Location = new System.Drawing.Point(354, 121);
            this.btnReplaceST.Name = "btnReplaceST";
            this.btnReplaceST.Size = new System.Drawing.Size(94, 28);
            this.btnReplaceST.TabIndex = 68;
            this.btnReplaceST.Text = "Replace ST";
            this.btnReplaceST.UseVisualStyleBackColor = true;
            this.btnReplaceST.Click += new System.EventHandler(this.btnReplaceST_Click);
            // 
            // AVPRNGTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 586);
            this.ControlBox = false;
            this.Controls.Add(this.btnReplaceST);
            this.Controls.Add(this.lb_WinCat);
            this.Controls.Add(this.btn_GetReelStrip);
            this.Controls.Add(this.btn_SaveRNG);
            this.Controls.Add(this.btn_LoadRNG);
            this.Controls.Add(this.r5_search);
            this.Controls.Add(this.r4_search);
            this.Controls.Add(this.r3_search);
            this.Controls.Add(this.r2_search);
            this.Controls.Add(this.r1_search);
            this.Controls.Add(this.lb_R5);
            this.Controls.Add(this.lb_R4);
            this.Controls.Add(this.lb_R3);
            this.Controls.Add(this.lb_R2);
            this.Controls.Add(this.lb_R1);
            this.Controls.Add(this.cb_bonus);
            this.Controls.Add(this.ddl_bonus);
            this.Controls.Add(this.l_Note);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.EGMIP);
            this.Controls.Add(this.Billin);
            this.Controls.Add(this.Cashout);
            this.Controls.Add(this.Key);
            this.Controls.Add(this.Doors);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.COM);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.SendToEGM);
            this.Controls.Add(this.GenerateRNG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.l_ThemeName);
            this.Controls.Add(this.P1_Select);
            this.Controls.Add(this.LoadPaytable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AVPRNGTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadPaytable;
        private System.Windows.Forms.ComboBox P1_Select;
        private System.Windows.Forms.Label l_ThemeName;
        private System.Windows.Forms.RichTextBox Result;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GenerateRNG;
        private System.Windows.Forms.Button SendToEGM;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.ComboBox COM;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Doors;
        private System.Windows.Forms.Button Key;
        private System.Windows.Forms.Button Cashout;
        private System.Windows.Forms.Button Billin;
        private System.Windows.Forms.Label EGMIP;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label l_Note;
        private System.Windows.Forms.ComboBox ddl_bonus;
        private System.Windows.Forms.CheckBox cb_bonus;
        private System.Windows.Forms.ListBox lb_R1;
        private System.Windows.Forms.ListBox lb_R2;
        private System.Windows.Forms.ListBox lb_R3;
        private System.Windows.Forms.ListBox lb_R4;
        private System.Windows.Forms.ListBox lb_R5;
        private System.Windows.Forms.TextBox r1_search;
        private System.Windows.Forms.TextBox r2_search;
        private System.Windows.Forms.TextBox r3_search;
        private System.Windows.Forms.TextBox r4_search;
        private System.Windows.Forms.TextBox r5_search;
        private System.Windows.Forms.Button btn_LoadRNG;
        private System.Windows.Forms.Button btn_SaveRNG;
        private System.Windows.Forms.Button btn_GetReelStrip;
        private System.Windows.Forms.ListBox lb_WinCat;
        private System.Windows.Forms.Button btnReplaceST;
    }
}

