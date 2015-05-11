using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

namespace Sweet3RNGTool
{
    class Helpers
    {
        public static List<string> stops = new List<string>();       
        public static List<string> RNG = new List<string>();
        public static List<string> WinCats = new List<string>();

        public static void ReadTxt(string filepath)
        {
            string reelSectionName = Convert.ToString(ConfigurationSettings.AppSettings["ReelSectionName"]);
            string gamelist = Convert.ToString(ConfigurationSettings.AppSettings["PaytableConfig_GameList"]);
            int ReelCount = int.Parse(ConfigurationSettings.AppSettings["PaytableConfig_ReelCount"]);

            stops.Clear();

            int lmark = 0;
            int rmark = 0;
            bool begin = false;
            int lmark1 = 0;
            int rmark1 = 0;
            bool begin1 = false;
            int j = 0;
            string[] reels = { "0", "1", "2", "3", "4"};
            int reel = 0;

            var lines = File.ReadAllLines(filepath);
            foreach (var game in gamelist.Split(';'))
            {             
                foreach (var line in lines)
                {
                    if (line.Trim().Equals(game))
                    {
                        lmark = 0;
                        rmark = 0;
                        begin = true;
                        //MessageBox.Show(line);
                    }
                    //Mark { of Game
                    if (line.Trim().Equals("{") && begin)
                    {
                        lmark = lmark + 1;
                    }
                    //Mark } of Game
                    if (line.Trim().Equals("}") && begin)
                    {
                        rmark = rmark + 1;
                    }
                    if (begin)
                    {
                        string a = reelSectionName;
                        string reeltoget = a + reel.ToString();//reels[j];
                        if (line.Trim().ToString().Contains(reeltoget.Trim()) && line != null)
                        {

                            lmark1 = 0;
                            rmark1 = 0;
                            begin1 = true;
                        }
                        //Mark { of reel
                        if (line.Trim().Equals("{") && begin1)
                        {
                            lmark1 = lmark1 + 1;
                        }
                        //Mark } of reel
                        if (line.Trim().Equals("}") && begin1)
                        {
                            rmark1 = rmark1 + 1;
                        }
                        if (begin1)
                        {
                            if (line.ToString().Contains("stop = "))
                            {
                                string[] split = line.ToString().Split(',');
                                string stop = split[0].Substring(split[0].Length - 2, 2);
                                //string stop = line.ToString().Substring(line.ToString().IndexOf("stop = ") + 7, 2);
                                //stops.Add(game + "," + "reel" + j.ToString() + "," + stop);
                                stops.Add(game + "," + "reel" + reel.ToString() + "," + stop);
                            }
                        }

                        if (lmark1 == rmark1 && lmark1 != 0 && rmark1 != 0 && begin1)
                        {
                            begin1 = false;
                            reel++;
                            if (reel == ReelCount)
                            {
                                reel = 0;
                            }
                        }
                    }
                    if (lmark == rmark && lmark != 0 && rmark != 0 && begin)
                    {
                        begin = false;
                    }
                }
            }
        }

        //string SStable = Convert.ToString(ConfigurationSettings.AppSettings["BaseSSTable"]);
        public static void LoadSSTable(string filepath)
        {           
            //stops.Clear();
           // string reelSectionName = Convert.ToString(ConfigurationSettings.AppSettings["ReelSectionName"]);
            //string gamelist = Convert.ToString(ConfigurationSettings.AppSettings["PaytableConfig_GameList"]);
            //int ReelCount = int.Parse(ConfigurationSettings.AppSettings["PaytableConfig_ReelCount"]);
            string SStable = Convert.ToString(ConfigurationSettings.AppSettings["BaseSSTable"]);
            string WinCategory = Convert.ToString(ConfigurationSettings.AppSettings["WinCategory"]);

            int lmark = 0;
            int rmark = 0;
            bool begin = false;
            int lmark1 = 0;
            int rmark1 = 0;
            bool begin1 = false;
            int j = 0;
            string[] reels = { "0", "1", "2", "3", "4" };
            int reel = 0;

            var lines = File.ReadAllLines(filepath);
            foreach (var stable in SStable.Split(';'))
            {
                foreach (var line in lines)
                {
                    if (line.Trim().Equals(stable))
                    {
                        lmark = 0;
                        rmark = 0;
                        begin = true;
                        //MessageBox.Show(line);
                    }
                    //Mark { of Game
                    if (line.Trim().Equals("{") && begin)
                    {
                        lmark = lmark + 1;
                    }
                    //Mark } of Game
                    if (line.Trim().Equals("}") && begin)
                    {
                        rmark = rmark + 1;
                    }
                    if (begin)
                    {
                        string a = WinCategory;
                        //string WinCategorytoget = a + reel.ToString();//reels[j];
                        if (line.Trim().ToString().Contains(a.Trim()) && line != null)
                        {

                            lmark1 = 0;
                            rmark1 = 0;
                            begin1 = true;
                        }
                        //Mark { of reel
                        if (line.Trim().Equals("{") && begin1)
                        {
                            lmark1 = lmark1 + 1;
                        }
                        //Mark } of reel
                        if (line.Trim().Equals("}") && begin1)
                        {
                            rmark1 = rmark1 + 1;
                        }
                        if (begin1)
                        {
                            if (line.ToString().Contains("win = bet (all), 0, "))
                            {
                                string winCat = line.ToString().Substring(28).Trim();
                                WinCats.Add(winCat);
                            }
                        }

                        if (lmark1 == rmark1 && lmark1 != 0 && rmark1 != 0 && begin1)
                        {
                            begin1 = false;
                            //reel++;
                            //if (reel == ReelCount)
                            //{
                               // reel = 0;
                            //}
                        }
                    }
                    if (lmark == rmark && lmark != 0 && rmark != 0 && begin)
                    {
                        begin = false;
                    }
                }
            }
        }

        public static List<string> LoadRNG(string filepath)
        {
            try
            {

                var lines = File.ReadAllLines(filepath);

                foreach (var line in lines)
                {
                    RNG.Add(line);
                }
                return RNG;
            }
            catch
            {
                return null;
            }
        }
    }
}
