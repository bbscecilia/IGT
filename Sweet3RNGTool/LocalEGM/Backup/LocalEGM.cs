using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Threading;
using IGT.TestFramework.RemoteSwitch;

namespace IGT.TestFramework.EGM
{
    public class LocalEGM : ILocalEGM, ICompactEGM
    {
        public bool Connect(string connectionStr)
        {
            return Connect(connectionStr, 0);
        }

        public bool Connect(string connectionStr, int interCommandDelay)
        {
            InterCommandDelay = interCommandDelay;
            if (Regex.IsMatch(connectionStr, @"^COM[1-9][0-9]?$", RegexOptions.IgnoreCase))
            {
                try
                {
                    serialPort = new SerialPort(connectionStr.ToUpper(), 19200, Parity.None, 8, StopBits.One);
                    serialPort.DiscardNull = false;
                    serialPort.DtrEnable = true;
                    serialPort.ParityReplace = 63;
                    serialPort.ReadBufferSize = 4096;
                    serialPort.ReadTimeout = -1;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = false;
                    serialPort.WriteBufferSize = 4096;
                    serialPort.WriteTimeout = -1;
                }
                catch
                {
                    serialPort = null;
                }
            }
            else
                serialPort = null;
            
            if (serialPort != null)
            {
                try
                {
                    serialPort.Open();
                    serialPortReadThread = new Thread(SerialPortReadThreadMethod);
                    serialPortReadThread.Priority = ThreadPriority.Normal;
                    serialPortReadThread.IsBackground = true;
                    serialPortReadThread.Start();
                    platform = StringToPlatforms(GetPlatformName());
                }
                catch
                {
                    if (serialPort != null)
                        serialPort.Close();
                    serialPort = null;
                    serialPortReadThread = null;
                }
            }
            else
            {
                serialPort = null;
                serialPortReadThread = null;
            }

            return (serialPort != null);
        }

        public void Disconnect()
        {
            if (serialPort != null)
                serialPort.Close();
        }

        public bool IsConnected
        {
            get { return (serialPort != null) && serialPort.IsOpen; }
        }

        private Thread serialPortReadThread;
        private StringBuilder inBuffer;
        private string response = "";
        private bool waitingForReply = false;

        private SerialPort serialPort;
        public string Port
        {
            get { return serialPort.PortName; }
        }

        private Platforms platform;
        public Platforms Platform
        {
            get { return platform; }
        }

        public int InterCommandDelay { get; set; }
        public event EventHandler<EGMDataEventArgs> EGMDataReceived;

        public void BillIn(DomesticBillDenominations billDenomination, int countryCode)
        {
            List<string> commands = new List<string>() { "bill in",
                ((int)billDenomination).ToString() + "00" };
            if ((this.Platform == Platforms.Stepper_960) || (this.Platform == Platforms.Video_960))
                commands.Add("0");
            else
                commands.Add(countryCode.ToString());
            WriteCOM(commands.ToArray());
        }

        public void BillIn2(int billDenomination, int countryCode)
        {
            if (Enum.GetValues(typeof(DomesticBillDenominations)).Cast<int>().Any(d => d == billDenomination))
            {
                List<string> commands = new List<string>() { "bill in",
                    billDenomination.ToString() + "00" };
                if ((this.Platform == Platforms.Stepper_960) || (this.Platform == Platforms.Video_960))
                    commands.Add("0");
                else
                    commands.Add(countryCode.ToString());
                WriteCOM(commands.ToArray());
            }
        }

        public void CoinIn(int numberOfCoins)
        {
            string[] commands = new string[] { "coin in" };
            for (int i = 0; i < numberOfCoins; i++)
                WriteCOM(commands);
        }

        public void CardIn(int trackNumber, string trackData)
        {
            if (trackNumber < 1)
                trackNumber = 1;
            else if (trackNumber < 4)
                trackNumber = 4;
            if (trackData.Trim() == "")
                trackData = "0";
            string[] commands = new string[] { "insert card",
                trackNumber.ToString(), trackData };
            WriteCOM(commands);
        }

        public void CardOut()
        {
            string[] commands = new string[] { "remove card" };
            WriteCOM(commands);
        }

        public void PlayGame()
        {
            List<string> commands = new List<string>();
            if (this.Platform == Platforms.Video_960)
                commands.Add("deal");
            else
                commands.Add("spin");
            WriteCOM(commands.ToArray());
        }

        public void OpenDoor(Doors door)
        {
            string[] commands = null;
            switch (door)
            {
                case Doors.MainDoor:
                    commands = new string[] { "mn dr op" };
                    break;

                case Doors.DropDoor:
                    commands = new string[] { "dr dr op" };
                    break;

                case Doors.CashboxDoor:
                    commands = new string[] { "bi dr op" };
                    break;

                case Doors.LogicDoor:
                case Doors.CardCageDoor:
                    commands = new string[] { "logic acc" };
                    break;
            }

            if (commands != null)
            {
                WriteCOM(commands);
            }
        }

        public void OpenDoor2(int door)
        {
            if (Enum.GetValues(typeof(Doors)).Cast<int>().Any(d => d == door))
            {
                string[] commands = null;
                switch (door)
                {
                    case 1: // main door
                        commands = new string[] { "mn dr op" };
                        break;

                    case 2: //  drop door
                        commands = new string[] { "dr dr op" };
                        break;

                    case 3: // cashbox door
                        commands = new string[] { "bi dr op" };
                        break;

                    case 4: // logic door
                    case 5: // card cage door
                        commands = new string[] { "logic acc" };
                        break;
                }

                if (commands != null)
                {
                    WriteCOM(commands);
                }
            }            
        }

        public void CloseDoor(Doors door)
        {
            string[] commands = null;
            switch (door)
            {
                case Doors.MainDoor:
                    commands = new string[] { "mn dr cl" };
                    break;

                case Doors.DropDoor:
                    commands = new string[] { "dr dr cl" };
                    break;

                case Doors.CashboxDoor:
                    commands = new string[] { "bi dr cl" };
                    break;

                case Doors.LogicDoor:
                case Doors.CardCageDoor:
                    commands = new string[] { "logic cl" };
                    break;
            }

            if (commands != null)
            {
                WriteCOM(commands);
            }
        }

        public void CloseDoor2(int door)
        {
            if (Enum.GetValues(typeof(Doors)).Cast<int>().Any(d => d == door))
            {
                string[] commands = null;
                switch (door)
                {
                    case 1: // main door
                        commands = new string[] { "mn dr cl" };
                        break;

                    case 2: //  drop door
                        commands = new string[] { "dr dr cl" };
                        break;

                    case 3: // cashbox door
                        commands = new string[] { "bi dr cl" };
                        break;

                    case 4: // logic door
                    case 5: // card cage door
                        commands = new string[] { "logic cl" };
                        break;
                }

                if (commands != null)
                {
                    WriteCOM(commands);
                }
            }
        }

        public void Bet1()
        {
            WriteCOM(new string[] { "bet1" });
        }

        public void BetMax()
        {
            WriteCOM(new string[] { "bet max" });
        }

        public void Play1()
        {
            WriteCOM(new string[] { "play1" });
        }

        public void Play2()
        {
            WriteCOM(new string[] { "play2" });
        }

        public void Play3()
        {
            WriteCOM(new string[] { "play3" });
        }

        public void Play4()
        {
            WriteCOM(new string[] { "play4" });
        }

        public void Play5()
        {
            WriteCOM(new string[] { "play5" });
        }

        public void Hold1()
        {
            WriteCOM(new string[] { "hold1" });
        }

        public void Hold2()
        {
            WriteCOM(new string[] { "hold2" });
        }

        public void Hold3()
        {
            WriteCOM(new string[] { "hold3" });
        }

        public void Hold4()
        {
            WriteCOM(new string[] { "hold4" });
        }

        public void Hold5()
        {
            WriteCOM(new string[] { "hold5" });
        }

        public void Cashout()
        {
            WriteCOM(new string[] { "cashout" });
        }

        public void TurnJackpotResetKey()
        {
            WriteCOM(new string[] { "jkpt res pressed", "jkpt res released" });
        }

        public void SetTestRNG()
        {
            WriteCOM(new string[] { "test rng" });
        }

        public void SetRealRNG()
        {
            WriteCOM(new string[] { "real rng" });
        }

        public void SendAllRNGValues(int[] rngVals)
        {
            List<string> commands = new List<string>();
            commands.Add("test rng");
            commands.Add("rng vals");
            commands.AddRange(rngVals.Select(rng => rng.ToString()));
            WriteCOM(commands.ToArray());
        }

        public void SendReelStopRNGValues(int[] reelStops)
        {
            if ((reelStops != null) && (reelStops.Length >= 3))
            {
                List<string> commands = new List<string>() { "test rng", "rng vals" };
                commands.Add("1");
                commands.Add(reelStops.Length.ToString());
                for (int i = 1; i <= reelStops.Length; i++)
                    commands.Add("1");
                commands.AddRange(reelStops.Select(rng => rng.ToString()));
                WriteCOM(commands.ToArray());
            }
        }

        public void Send(string data)
        {
            SendPADCommands(TokenizeDataToSend(data));
        }

        public string Send(string data, int timeout)
        {
            return SendPADCommandsAndWait(TokenizeDataToSend(data), timeout);
        }

        public void SendPADCommands(string[] commands)
        {
            WriteCOM(commands);
        }

        public string SendPADCommandsAndWait(string[] commands, int timeout)
        {
            return WriteCOMAndWait(commands, timeout);
        }

        public int GetWinAmount()
        {
            string rxStr = WriteCOMAndWait(new string[] { "get win amount" }, 2000);
            int winAmount;
            return int.TryParse(rxStr, out winAmount) ? winAmount : -1;
        }

        public string GetCurrentState()
        {
            return WriteCOMAndWait(new string[] { "get game state" }, 2000);
        }

        public string[] GetMeters()
        {
            string[] commands = new string[] { "TX:get meters" };
            string rxStr = "";
            int tryCount = 0;
            string[] meters;

            do
            {
                rxStr = WriteCOMAndWait(commands, 2000);
                tryCount++;
                if (rxStr == "")
                    Thread.Sleep(500);
            }
            while ((rxStr == "") && (tryCount < 3));

            if (rxStr.StartsWith("RX:"))
                rxStr = rxStr.Substring(3).Trim();
            meters = (from meter in rxStr.Split(new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries)
                      let nameValuePair = meter.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries)
                      select (nameValuePair[0].Trim() + " = " + nameValuePair[1].Trim())).ToArray();
            return meters;
        }

        public string GetBaseProgramName()
        {
            string[] commands = new string[] { "TX:get base program" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
            }

            return rxStr;
        }

        public string GetGameProgramName()
        {
            string[] commands = new string[] { "TX:get game program" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
            }

            return rxStr;
        }

        public int GetPlayerDenomination()
        {
            string[] commands = new string[] { "TX:get player denom" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
                else if (rxStr == "")
                    rxStr = "-1";
            }
            else
                rxStr = "-1";

            return int.Parse(rxStr);           
        }

        public int GetAccountingDenomination()
        {
            string[] commands = new string[] { "TX:get acc denom" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
                else if (rxStr == "")
                    rxStr = "-1";
            }
            else
                rxStr = "-1";

            return int.Parse(rxStr);           
        }

        public int GetCoinDenomination()
        {
            string[] commands = new string[] { "TX:get coin denom" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
                else if (rxStr == "")
                    rxStr = "-1";
            }
            else
                rxStr = "-1";
            return int.Parse(rxStr);           
        }

        public int GetCredits()
        {
            string[] commands = (this.Platform == Platforms.AVP) ?
                new string[] { "credits" } : new string[] { "TX:credits" };
            string rxStr = "";
            int tryCount = 0;

            do
            {
                rxStr = WriteCOMAndWait(commands, 2000);
                tryCount++;
                if (rxStr == "")
                    Thread.Sleep(500);
            }
            while ((rxStr == "") && (tryCount < 3));

            if (rxStr.StartsWith("RX:"))
                rxStr = rxStr.Substring(3).Trim();
            else if (rxStr == "")
                rxStr = "-1";
            return int.Parse(rxStr);           
        }

        public long GetAssetNumber()
        {
            string[] commands = new string[] { "get asset number" };
            string rxStr = "";
            int tryCount = 0;

            if (this.Platform != Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                    if (rxStr == "")
                        Thread.Sleep(500);
                }
                while ((rxStr == "") && (tryCount < 3));

                if (rxStr.StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
                else if (rxStr == "")
                    rxStr = "-1";
            }
            else
                rxStr = "-1";
            return long.Parse(rxStr);
        }

        public string GetEGMID()
        {
            string[] commands = new string[] { "TX:get machine id" };
            int tryCount = 0;
            string rxStr = "";

            if (this.Platform == Platforms.AVP)
            {
                do
                {
                    rxStr = WriteCOMAndWait(commands, 2000);
                    tryCount++;
                }
                while ((rxStr.Trim() == "") && (tryCount < 3));

                if (rxStr.Trim().StartsWith("RX:"))
                    rxStr = rxStr.Substring(3).Trim();
            }

            return rxStr;
        }

        public RemoteSwitchResponse TurnPowerOn(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword)
        {
            RemoteSwitch.RemoteSwitch remoteSwitch = new IGT.TestFramework.RemoteSwitch.RemoteSwitch(powerSwitchIP,
                powerSwitchPort, powerSwitchPassword);
            return remoteSwitch.PowerOn();
        }

        public RemoteSwitchResponse TurnPowerOff(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword)
        {
            RemoteSwitch.RemoteSwitch remoteSwitch = new IGT.TestFramework.RemoteSwitch.RemoteSwitch(powerSwitchIP,
                powerSwitchPort, powerSwitchPassword);
            return remoteSwitch.PowerOff();
        }

        public RemoteSwitchResponse CyclePower(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword)
        {
            RemoteSwitch.RemoteSwitch remoteSwitch = new IGT.TestFramework.RemoteSwitch.RemoteSwitch(powerSwitchIP,
                powerSwitchPort, powerSwitchPassword);
            return remoteSwitch.CyclePower();
        }

        public RemoteSwitchResponse GetCurrentPowerState(string powerSwitchIP, int powerSwitchPort,
            string powerSwitchPassword)
        {
            RemoteSwitch.RemoteSwitch remoteSwitch = new IGT.TestFramework.RemoteSwitch.RemoteSwitch(powerSwitchIP,
                powerSwitchPort, powerSwitchPassword);
            return remoteSwitch.QuerySwitch();
        }

        private string[] TokenizeDataToSend(string data)
        {
            return Regex.Split(data, @";|,|\r?\n|\t|\|").Select(cmd =>
                cmd.Trim()).Where(cmd => cmd != "").ToArray();
        }

        private void WriteCOM(string[] commands)
        {
            try
            {
                Array.ForEach(commands/*.Where(cmd => cmd.Trim() != "").ToArray()*/, cmd =>
                    {
                        serialPort.Write((cmd + "\r\n").ToCharArray(), 0, cmd.Length + 1);
                        if (InterCommandDelay > 0)
                            Thread.Sleep(InterCommandDelay);
                    });
            }
            catch
            {
                // Do nothing if an error occurs, just return silently.
                // Only writing to an uninitialized or closed serial port
                // should trigger this error.
            }
        }

        private string WriteCOMAndWait(string[] commands, int timeout)
        {
            //commands = commands.Where(cmd => cmd.Trim() != "").ToArray();
            for (int i = 0; i < commands.Length; i++)
            {
                try
                {
                    if (i == commands.Length - 1)
                    {
                        lock (this)
                        {
                            waitingForReply = true;
                            response = "";
                            serialPort.Write((commands[i] + "\r\n").ToCharArray(), 0, commands[i].Length + 1);
                            Monitor.Wait(this, timeout);
                        }
                    }
                    else
                        serialPort.Write((commands[i] + "\r\n").ToCharArray(), 0, commands[i].Length + 1);
                    if (InterCommandDelay > 0)
                        Thread.Sleep(InterCommandDelay);
                }
                catch
                {
                    // If a serial port error occurs, return a blank response.
                    response = "";
                    break;
                }
            }

            return response;
        }

        private string GetPlatformName()
        {
            string[] commands = new string[] { "TX:get platform" };
            string rxStr = "";
            int tryCount = 0;

            do
            {
                rxStr = WriteCOMAndWait(commands, 500);
                tryCount++;
                if (rxStr.Trim() == "")
                    Thread.Sleep(500);
            }
            while ((rxStr.Trim() == "") && (tryCount < 3));

            if (rxStr.StartsWith("RX:"))
                rxStr = rxStr.Substring(3).Trim();
            return rxStr;
        }

        private static Platforms StringToPlatforms(string platform)
        {
            Platforms platformID;

            switch (platform.Trim().ToUpper())
            {
                case "960 VIDEO":
                    platformID = Platforms.Video_960;
                    break;

                case "960 STEPPER":
                    platformID = Platforms.Stepper_960;
                    break;

                case "AVP":
                    platformID = Platforms.AVP;
                    break;

                default:
                    platformID = Platforms.Unknown;
                    break;
            }

            return platformID;
        }

        private void SerialPortReadThreadMethod()
        {
            string oneCharStr;
            try
            {
                inBuffer = new StringBuilder();
                while (true)
                {
                    oneCharStr = ((char)serialPort.ReadChar()).ToString();
                    inBuffer.Append(oneCharStr);

                    if (inBuffer.ToString().EndsWith("\r\n") || inBuffer.ToString().EndsWith("\n\r"))
                    {
                        if (waitingForReply)
                        {
                            if (inBuffer.ToString().Trim().StartsWith("RX:"))
                            {
                                lock (this)
                                {
                                    response = inBuffer.Remove(0, 3).ToString().TrimEnd(null);
                                    waitingForReply = false;
                                    Monitor.Pulse(this);
                                }
                            }
                        }
                        else
                        {
                            response = "";
                            EGMDataReceived(this, new EGMDataEventArgs(inBuffer.ToString()));
                        }

                        inBuffer = new StringBuilder();
                    }
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
            catch
            {
                return;
            }
        }
    }
}
