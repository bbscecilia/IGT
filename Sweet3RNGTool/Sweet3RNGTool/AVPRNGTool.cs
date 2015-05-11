using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using IGT.TestFramework.EGM;
using System.Reflection;
using IGT.PA.Framework.EgmClients.Interfaces.Enumerations;
using IGT.PA.Framework.EgmClients.Avp;
using System.IO;
using System.Configuration;
using Microsoft.Office.Interop.Excel;


namespace Sweet3RNGTool
{
    public partial class AVPRNGTool : Form
    {
        public AVPRNGTool()
        {
            InitializeComponent();
            GetPaytableDefinedSections();
        }

        AvpEgm avpegm = new AvpEgm();
        public LocalEGM egmConnection;
        public bool connected;
        int doorFlag = 0;
        private Thread scriptSendThread;
        private delegate void ReadCOMPortControlsHandler(int actionCode, List<object> dataList);
        public delegate string WriteCOMAndWaitHandler(string command, int timeout);

        public List<string> P1_Reel0 = new List<string>();
        public List<string> P1_Reel1 = new List<string>();
        public List<string> P1_Reel2 = new List<string>();
        public List<string> P1_Reel3 = new List<string>();
        public List<string> P1_Reel4 = new List<string>();
        public List<string> P1_WinCat = new List<string>();

        public List<string> P1_Reel0_cp = new List<string>();
        public List<string> P1_Reel1_cp = new List<string>();
        public List<string> P1_Reel2_cp = new List<string>();
        public List<string> P1_Reel3_cp = new List<string>();
        public List<string> P1_Reel4_cp = new List<string>();

        public string ThemeName = Convert.ToString(ConfigurationSettings.AppSettings["ThemeName"]);
        public string featurelist = Convert.ToString(ConfigurationSettings.AppSettings["PaytableConfig_FeatureList"]);
        public string gamelist = Convert.ToString(ConfigurationSettings.AppSettings["PaytableConfig_GameList"]);
        public string ip = Convert.ToString(ConfigurationSettings.AppSettings["IP"]);
        public int ReelCount = int.Parse(ConfigurationSettings.AppSettings["PaytableConfig_ReelCount"]);
        public List<string> LoadedRNG = new List<string>();
        public string LoadedPaytableName = string.Empty;
        
        //configurations
        private void GetPaytableDefinedSections()
        {
            this.Text = ThemeName + " RNG Tool";
            IP.Text = ip;
            l_ThemeName.Text = ThemeName;

            foreach (var game in gamelist.Split(';'))
            {
                P1_Select.Items.Add(game);
            }
            foreach (var feature in featurelist.Split(';'))
            {
                ddl_bonus.Items.Add(feature);
            }

            l_Note.Text = "NOTE: Please load .paytable file of " + ThemeName;

        }

        private void LoadPaytable_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "paytable files (*.paytable)|*.paytable";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string a = openFileDialog1.FileName;

                    LoadedPaytableName = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1);

                    Helpers.ReadTxt(a);
                    Helpers.LoadSSTable(a);
                    P1_Select.SelectedItem = P1_Select.Items[0];
                    GetReelstops("BASE_GAME");
                    GetWinCats();
                    //CheckReel5();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void GetReelstops(string gamename)
        {
            P1_Reel0.Clear();
            P1_Reel1.Clear();
            P1_Reel2.Clear();
            P1_Reel3.Clear();
            P1_Reel4.Clear();

            #region populate reel stops
            foreach (var a in Helpers.stops)
            {
                if (a.ToString().Contains(gamename))
                {
                    if (a.ToString().Contains("reel0"))
                    {
                        P1_Reel0.Add(a.Substring(a.Length - 2));
                    }
                    else if (a.ToString().Contains("reel1"))
                    {
                        P1_Reel1.Add(a.Substring(a.Length - 2));
                    }
                    else if (a.ToString().Contains("reel2"))
                    {
                        P1_Reel2.Add(a.Substring(a.Length - 2));
                    }
                    else if (a.ToString().Contains("reel3"))
                    {
                        P1_Reel3.Add(a.Substring(a.Length - 2));
                    }
                    else if (a.ToString().Contains("reel4"))
                    {
                        P1_Reel4.Add(a.Substring(a.Length - 2));
                    }
                }
            }

            #endregion
            lb_R1.DataSource = null;
            lb_R2.DataSource = null;
            lb_R3.DataSource = null;
            lb_R4.DataSource = null;
            lb_R5.DataSource = null;

            lb_R1.DataSource = P1_Reel0;
            lb_R2.DataSource = P1_Reel1;
            lb_R3.DataSource = P1_Reel2;
            lb_R4.DataSource = P1_Reel3;
            lb_R5.DataSource = P1_Reel4;
        }
        private void GetWinCats()
        {

            P1_WinCat.Clear();
            lb_WinCat.DataSource = null;
            foreach (var a in Helpers.WinCats)
            {
                P1_WinCat.Add(a);
            }
            lb_WinCat.DataSource = P1_WinCat;
        }
        private List<string> ReplaceWinCat(List<string> lstReplace, string strA, string strB)
        {
            int index;
            while ((index = lstReplace.FindIndex(o => o == strA)) >= 0)
            {
                lstReplace[index] = strB;
            }
            return lstReplace;
        }


        List<string> RNG = new List<string>();

        #region EGM
        private void ScriptSendThreadMethod(object data)
        {
            object[] parameters = new object[2];

            List<string> script = (List<string>)data;

            try
            {
                string lineStr;

                for (int j = 0; j < script.Count; j++)
                {
                    lineStr = script[j];

                    if (lineStr.Trim() != "")
                    {
                        // Generate the CommandSent event
                        Form_CommandSent(this, new ScriptEventArgs(lineStr, false));
                    }
                    //Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Form_CommandSent(object sender, ScriptEventArgs sea)
        {
            if (sea.WaitForResponse)
            {
                //WriteCOMAndWaitHandler handler = new WriteCOMAndWaitHandler(WriteCOMAndWait);
                //handler.BeginInvoke(sea.PADCommand, 4000, new AsyncCallback(WriteCOMAndWaitAsyncCallback), handler);


                WriteCOMAndWaitHandler handler = new WriteCOMAndWaitHandler(egmConnection.Send);
                handler.BeginInvoke(sea.PADCommand, 4000,
                    new AsyncCallback(WriteCOMAndWaitAsyncCallback), handler);
            }
            else
            {
                //WriteCOM(sea.PADCommand);
                egmConnection.Send(sea.PADCommand);
            }
        }

        public void WriteCOMAndWaitAsyncCallback(IAsyncResult ar)
        {
            WriteCOMAndWaitHandler handler = (WriteCOMAndWaitHandler)ar.AsyncState;

            string rxMsg = handler.EndInvoke(ar);
            object[] parameters = new object[2];
            List<object> dataList = new List<object>();
        }

        private string inBuffer;
        void egmConnection_EGMDataReceived(object sender, EGMDataEventArgs e)
        {
            ReadCOMPortControlsHandler uh = new ReadCOMPortControlsHandler(ReadCOMPortUpdateHandler);
            object[] parameters = new object[2];
            List<object> updateDataList;

            inBuffer += e.Data;

            //  Add the line of text from the COM port to the terminal I/O window (action 0)
            parameters[0] = 0;
            updateDataList = new List<object>();
            updateDataList.Add(inBuffer.Substring(0, inBuffer.Length - 2));
            parameters[1] = updateDataList;
            this.Invoke(uh, parameters);

            if (inBuffer.StartsWith("INVALID PAD BOARD COMMAND"))
            {
                parameters[0] = 2;
                parameters[1] = null;
                this.Invoke(uh, parameters);
            }

            inBuffer = "";

        }

        private void ReadCOMPortUpdateHandler(int actionCode, List<object> dataList)
        {

            if (actionCode == 0)
            {

            }
            else if (actionCode == 1)
            {

            }
            else if (actionCode == 2) // Remove the last two lines in the TerminalIOTextBox (to clear any garbage characters when first connecting).
            {

            }
        }

        public void egmConnection_EGMDisconnected(object sender, EGMDisconnectArgs e)
        {
            if (connected)
            {
                MessageBox.Show("EGM connection has been lost, please reconnect.");
            }
        }
        #endregion

        #region buttons
        private void Connect_Click(object sender, EventArgs e)
        {
            string EGMIP = IP.Text.ToString();

            if (connected != true)
            {
                try
                {
                    egmConnection = new LocalEGM();
                    egmConnection.EGMDataReceived +=
                    new EventHandler<EGMDataEventArgs>(egmConnection_EGMDataReceived);

                    if (EGMIP == "")
                    {
                        if (COM.SelectedItem != null)
                        {
                            connected = egmConnection.Connect(COM.SelectedItem.ToString());

                            if (!connected)
                                MessageBox.Show("Connection Failed!!\n Could not connect to the game through the COM port you selected.\n The COM port may be in use by another application or may not exist on this machine.");
                        }
                        else
                        {
                            connected = egmConnection.Connect("COM1");

                            if (!connected)
                            {
                                MessageBox.Show("Connection Failed!!\n Could not connect to the game through the COM1.\n The COM port may be in use by another application or may not exist on this machine.");
                            }
                            else COM.SelectedItem = COM.Items[0];
                        }
                    }
                    else
                    {                 
                        connected = egmConnection.ConnectIp(EGMIP);
                        COM.SelectedItem = null;
                        if (!connected)
                        { MessageBox.Show("Connection Failed!!\n Please a valid IP address"); }
                        egmConnection.EGMDisconnected +=
                        new EventHandler<EGMDisconnectArgs>(egmConnection_EGMDisconnected);
                    }

                    if (connected == true && checkBox1.Checked == false)
                    {
                        checkBox1.Checked = true;
                    }
                }
                catch
                {
                    connected = false;
                }
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                checkBox1.Checked = false;
                egmConnection.Disconnect();
                connected = false;
                COM.SelectedItem = null;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (connected == true && checkBox1.Checked == false)
            {
                egmConnection.SetRealRNG();
            }
            else if (connected == true && checkBox1.Checked == true)
            {
                egmConnection.SetTestRNG();
            }
        }

        private void Doors_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                if (doorFlag % 2 == 0)
                {
                    egmConnection.OpenDoor(IGT.TestFramework.EGM.Doors.MainDoor);
                    egmConnection.OpenDoor(IGT.TestFramework.EGM.Doors.CardCageDoor);
                }
                if (doorFlag % 2 == 1)
                {
                    egmConnection.CloseDoor(IGT.TestFramework.EGM.Doors.MainDoor);
                    egmConnection.CloseDoor(IGT.TestFramework.EGM.Doors.CardCageDoor);
                }
                doorFlag++;
            }
        }

        private void Key_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                egmConnection.TurnJackpotResetKey();
            }
        }

        private void Cashout_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                egmConnection.Cashout();
            }
        }

        private void Billin_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                egmConnection.BillIn2(50, 0);
            }
        }

        private void GenerateRNG_Click(object sender, EventArgs e)
        {
            //Add your logic here...

            //int weight = 0;
            int count = 5;
            r1_search.Text = "";
            r2_search.Text = "";
            r3_search.Text = "";
            r4_search.Text = "";
            r5_search.Text = "";

            RNG.Clear();
            //if (P1_Select.SelectedItem != null)
            //{
            //    if (P1_Select.SelectedItem.ToString() == "[base game]")
            //    {
            //        if (ddl_bonus.SelectedItem != null)
            //        {
            //            if (ddl_bonus.SelectedItem.ToString() != "")
            //            {
            //                count++;
            //                if (ddl_bonus.SelectedItem.ToString() == "[WolfRun 5 FS]")
            //                {
            //                    weight = 250;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[WolfRun 10 FS]")
            //                {
            //                    weight = 480;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[SiberianStorm 8 FS]")
            //                {
            //                    weight = 700;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[SiberianStorm 16 FS]")
            //                {
            //                    weight = 305;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[KittyGlitter 15 FS]")
            //                {
            //                    weight = 400;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[KittyGlitter 20 FS]")
            //                {
            //                    weight = 623;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[StinkinRich 10 FS]")
            //                {
            //                    weight = 550;
            //                }
            //                else if (ddl_bonus.SelectedItem.ToString() == "[StinkinRich 15 FS]")
            //                {
            //                    weight = 825;
            //                }
            //            }
            //        }
            //    }
            //    else 
            //    { 
            //        cb_bonus.Checked = false;
            //        ddl_bonus.SelectedItem = null;
            //    }
            //}
            //RNG.Add("clear rng vals");
            //RNG.Add("rng vals");
            //RNG.Add("1");

            //RNG.Add(count.ToString());
            //for (int i = 0; i < count; i++)
            //{
            //    RNG.Add("1");
            //}
            RNG.Add(lb_R1.SelectedIndex.ToString());
            RNG.Add(lb_R2.SelectedIndex.ToString());
            RNG.Add(lb_R3.SelectedIndex.ToString());
            RNG.Add(lb_R4.SelectedIndex.ToString());
            RNG.Add(lb_R5.SelectedIndex.ToString());
            //if (count == 6)
            //{
            //    RNG.Add(weight.ToString());
            //}
            //RNG.Add("SPIN");

            Result.Text = "";
            foreach (var a in RNG)
            {
                Result.AppendText(a);
                Result.AppendText("\n");
            }
        }

        private void SendToEGM_Click(object sender, EventArgs e)
        {
            List<string> RNGforSend = new List<string>();

            if (connected == true)
            {
                if (Result.Text != "")
                {
                    RNGforSend.Clear();

                    string[] split = Result.Text.Split(new Char[] { '\n' });
                    foreach (var a in split)
                    {
                        if (a != "")
                        {
                            RNGforSend.Add(a);
                        }
                    }

                    scriptSendThread = new Thread(ScriptSendThreadMethod);
                    scriptSendThread.Priority = ThreadPriority.Normal;
                    scriptSendThread.IsBackground = true;
                    scriptSendThread.Start(RNGforSend);
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Result.Clear();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                egmConnection.Disconnect();
                connected = false;
            }
            System.Windows.Forms.Application.Exit();
        }
        #endregion

        #region UI works
        private void P1_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReelstops(P1_Select.SelectedItem.ToString());

            if (P1_Select.SelectedItem.ToString() != "[base game]")
            { ddl_bonus.SelectedItem = null; }

            Result.Clear();
        }

   

        private void cb_bonus_CheckedChanged(object sender, EventArgs e)
        {
            r1_search.Text = "";
            r2_search.Text = "";
            r3_search.Text = "";
            r4_search.Text = "";
            r5_search.Text = "";
            if (P1_Select.SelectedItem!=null)
            {
                if (P1_Select.SelectedItem.ToString() == "[base game]" && cb_bonus.Checked == true)
                {
                    //Add your own logic here...

                    //lb_R2.SelectedItem = lb_R2.Items[lb_R2.Items.IndexOf("SC")];
                    //lb_R3.SelectedItem = lb_R3.Items[lb_R3.Items.IndexOf("SC")];
                    //lb_R4.SelectedItem = lb_R4.Items[lb_R4.Items.IndexOf("SC")];
                }
            }
            if (cb_bonus.Checked == false)
            {
                ddl_bonus.SelectedItem = ddl_bonus.Items[0];
            }
        }

     
        private void r1_search_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int index = 0;
                List<int> dup = new List<int>();
                foreach (var a in lb_R1.Items)
                {
                    if (a.ToString() != "" && a.ToString().Equals(r1_search.Text.ToString().ToUpper()))
                    {
                        dup.Add(index);
                    }
                    index++;
                }
                foreach (var b in dup)
                {
                    if (lb_R1.SelectedIndex < b)
                    {
                        lb_R1.SelectedIndex = b;
                        break;
                    }
                }
            }
        }
        private void r2_search_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int index = 0;
                List<int> dup = new List<int>();
                foreach (var a in lb_R2.Items)
                {
                    if (a.ToString() != "" && a.ToString().Equals(r2_search.Text.ToString().ToUpper()))
                    {
                        dup.Add(index);
                    }
                    index++;
                }
                foreach (var b in dup)
                {
                    if (lb_R2.SelectedIndex < b)
                    {
                        lb_R2.SelectedIndex = b;
                        break;
                    }
                }
            }
        }
        private void r3_search_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int index = 0;
                List<int> dup = new List<int>();
                foreach (var a in lb_R3.Items)
                {
                    if (a.ToString() != "" && a.ToString().Equals(r3_search.Text.ToString().ToUpper()))
                    {
                        dup.Add(index);
                    }
                    index++;
                }
                foreach (var b in dup)
                {
                    if (lb_R3.SelectedIndex < b)
                    {
                        lb_R3.SelectedIndex = b;
                        break;
                    }
                }
            }
        }
        private void r4_search_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int index = 0;
                List<int> dup = new List<int>();
                foreach (var a in lb_R4.Items)
                {
                    if (a.ToString() != "" && a.ToString().Equals(r4_search.Text.ToString().ToUpper()))
                    {
                        dup.Add(index);
                    }
                    index++;
                }
                foreach (var b in dup)
                {
                    if (lb_R4.SelectedIndex < b)
                    {
                        lb_R4.SelectedIndex = b;
                        break;
                    }
                }
            }
        }
        private void r5_search_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int index = 0;
                List<int> dup = new List<int>();
                foreach (var a in lb_R5.Items)
                {
                    if (a.ToString() != "" && a.ToString().Equals(r5_search.Text.ToString().ToUpper()))
                    {
                        dup.Add(index);
                    }
                    index++;
                }
                foreach (var b in dup)
                {
                    if (lb_R5.SelectedIndex < b)
                    {
                        lb_R5.SelectedIndex = b;
                        break;
                    }
                }
            }
        }

        private void r1_search_TextChanged(object sender, EventArgs e)
        {
            foreach (var a in lb_R1.Items)
            {
                if (a.ToString() != "" && a.ToString().Equals(r1_search.Text.ToString().ToUpper()))
                {
                    lb_R1.SelectedItem = a;
                    break;
                }
            }
        }

        private void r2_search_TextChanged(object sender, EventArgs e)
        {
            foreach (var a in lb_R2.Items)
            {
                if (a.ToString() != "" && a.ToString().Equals(r2_search.Text.ToString().ToUpper()))
                {
                    lb_R2.SelectedItem = a;
                    break;
                }
            }
        }

        private void r3_search_TextChanged(object sender, EventArgs e)
        {
            foreach (var a in lb_R3.Items)
            {
                if (a.ToString() != "" && a.ToString().Equals(r3_search.Text.ToString().ToUpper()))
                {
                    lb_R3.SelectedItem = a;
                    break;
                }
            }
        }

        private void r4_search_TextChanged(object sender, EventArgs e)
        {
            foreach (var a in lb_R4.Items)
            {
                if (a.ToString() != "" && a.ToString().Equals(r4_search.Text.ToString().ToUpper()))
                {
                    lb_R4.SelectedItem = a;
                    break;
                }
            }
        }

        private void r5_search_TextChanged(object sender, EventArgs e)
        {
            foreach (var a in lb_R5.Items)
            {
                if (a.ToString() != "" && a.ToString().Equals(r5_search.Text.ToString().ToUpper()))
                {
                    lb_R5.SelectedItem = a;

                    break;
                }
            }
        }
        #endregion

        private List<int> GetRandomRNG()
        {
            List<int> RNG = new List<int>();
            int r1 = RandomNumber(0, lb_R1.Items.Count - 1);
            int r2 = RandomNumber(0, lb_R2.Items.Count - 1);
            int r3 = RandomNumber(0, lb_R3.Items.Count - 1);
            int r4 = RandomNumber(0, lb_R4.Items.Count - 1);
            RNG.Add(r1);
            RNG.Add(r2);
            RNG.Add(r2);
            RNG.Add(r3);
            return RNG;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void btn_LoadRNG_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*|pscript files (*.pscript)|*.pscript|txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string a = openFileDialog1.FileName;
                    LoadedRNG.Clear();
                    LoadedRNG = Helpers.LoadRNG(a);

                    if (LoadedRNG != null)
                    {
                        Result.Clear();
                        foreach (var line in LoadedRNG)
                        {
                            Result.AppendText(line);
                            Result.AppendText("\n"); 
                        }
                    }
                                       
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btn_SaveRNG_Click(object sender, EventArgs e)
        {
            //Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All files (*.*)|*.*|pscript files (*.pscript)|*.pscript";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();//输出文件
 
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        if (Result.Text != "")
                        {
                            string[] split = Result.Text.Split(new Char[] { '\n' });
                            foreach (var a in split)
                            {
                                writer.WriteLine(a);
                            }
                        }                  
                    }
                    fs.Close();
            }
        }

        private void btn_GetReelStrip_Click(object sender, EventArgs e)
        {
            if (P1_Reel0.Count > 0)
            {
                //etc etc
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook book1 = excel.Workbooks.Add(Type.Missing);

                int sheetCount = 1;
                //etc etc      
                try
                {
                    foreach (var game in gamelist.Split(';'))
                    {
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = (Worksheet)book1.Sheets[sheetCount];
                        string[] a = LoadedPaytableName.Split('.');
                        worksheet.Name = a[0];

                        string gameName = game.Substring(1, game.Length - 2);

                        worksheet.Name = a[0] + "_" + gameName;
                        worksheet.Cells[1, 1] = gameName;
                        worksheet.Cells[2, 1] = "Reel1";
                        worksheet.Cells[2, 2] = "Reel2";
                        worksheet.Cells[2, 3] = "Reel3";
                        worksheet.Cells[2, 4] = "Reel4";
                        worksheet.Cells[2, 5] = "Reel5";

                        GetReelstops(game);
                        for (int row = 0; row < P1_Reel0.Count; ++row)
                        { worksheet.Cells[row + 3, 1] = P1_Reel0[row]; }

                        for (int row = 0; row < P1_Reel1.Count; ++row)
                        { worksheet.Cells[row + 3, 2] = P1_Reel1[row]; }

                        for (int row = 0; row < P1_Reel2.Count; ++row)
                        { worksheet.Cells[row + 3, 3] = P1_Reel2[row]; }

                        for (int row = 0; row < P1_Reel3.Count; ++row)
                        { worksheet.Cells[row + 3, 4] = P1_Reel3[row]; }

                        for (int row = 0; row < P1_Reel4.Count; ++row)
                        { worksheet.Cells[row + 3, 5] = P1_Reel4[row]; }

                        sheetCount++;
                    }
                    string fileName = string.Format(@"C:\" + ThemeName + "_" + LoadedPaytableName + "_ReelStrip.xlsx");

                    //Save the data to the file
                    //worksheet.SaveAs(fileName);
                    book1.SaveAs(fileName);
                    excel.Quit();

                }
                catch (Exception filenotfound)
                {
                    //Etc etc
                }
                finally
                {
                    //Etc etc
                }
            }
            else { MessageBox.Show("Please load a paytable first"); }

        }

        private void btnReplaceST_Click(object sender, EventArgs e)
        {
            lb_R1.DataSource = null;
            lb_R2.DataSource = null;
            lb_R3.DataSource = null;
            lb_R4.DataSource = null;
            lb_R5.DataSource = null;

            string a = lb_WinCat.SelectedItem.ToString();
            string[] split = a.ToString().Split('_');
            P1_Reel0_cp.Clear();
            P1_Reel1_cp.Clear();
            P1_Reel2_cp.Clear();
            P1_Reel3_cp.Clear();
            P1_Reel4_cp.Clear();

            P1_Reel0.ForEach(i => P1_Reel0_cp.Add(i));
            P1_Reel1.ForEach(i => P1_Reel1_cp.Add(i));
            P1_Reel2.ForEach(i => P1_Reel2_cp.Add(i));
            P1_Reel3.ForEach(i => P1_Reel3_cp.Add(i));
            P1_Reel4.ForEach(i => P1_Reel4_cp.Add(i));

            lb_R1.DataSource = ReplaceWinCat(P1_Reel0_cp,"AS",split[0]);
            lb_R2.DataSource = ReplaceWinCat(P1_Reel1_cp, "AS", split[1]);
            lb_R3.DataSource = ReplaceWinCat(P1_Reel2_cp, "AS", split[2]);
            lb_R4.DataSource = ReplaceWinCat(P1_Reel3_cp, "AS", split[3]);
            lb_R5.DataSource = ReplaceWinCat(P1_Reel4_cp, "AS", split[4]);
        }

        private void lb_WinCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_R1.DataSource = null;
            lb_R2.DataSource = null;
            lb_R3.DataSource = null;
            lb_R4.DataSource = null;
            lb_R5.DataSource = null;

            string a = lb_WinCat.SelectedItem.ToString();
            string[] split = a.ToString().Split('_');
            P1_Reel0_cp.Clear();
            P1_Reel1_cp.Clear();
            P1_Reel2_cp.Clear();
            P1_Reel3_cp.Clear();
            P1_Reel4_cp.Clear();

            P1_Reel0.ForEach(i => P1_Reel0_cp.Add(i));
            P1_Reel1.ForEach(i => P1_Reel1_cp.Add(i));
            P1_Reel2.ForEach(i => P1_Reel2_cp.Add(i));
            P1_Reel3.ForEach(i => P1_Reel3_cp.Add(i));
            P1_Reel4.ForEach(i => P1_Reel4_cp.Add(i));

            lb_R1.DataSource = ReplaceWinCat(P1_Reel0_cp, "AS", split[0]);
            lb_R2.DataSource = ReplaceWinCat(P1_Reel1_cp, "AS", split[1]);
            lb_R3.DataSource = ReplaceWinCat(P1_Reel2_cp, "AS", split[2]);
            lb_R4.DataSource = ReplaceWinCat(P1_Reel3_cp, "AS", split[3]);
            lb_R5.DataSource = ReplaceWinCat(P1_Reel4_cp, "AS", split[4]);

        }






    } 
}
