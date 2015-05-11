using System;

namespace Sweet3RNGTool
{

    public class ScriptEventArgs : EventArgs
    {
        public ScriptEventArgs(string command, bool wait)
        {
            padCommand = command;
            waitForResponse = wait;
        }

        public ScriptEventArgs()
        {
            padCommand = "";
            waitForResponse = false;
        }

        private string padCommand;
        public string PADCommand
        {
            get
            {
                return padCommand;
            }
            set
            {
                padCommand = value;
            }
        }

        private bool waitForResponse;
        public bool WaitForResponse
        {
            get
            {
                return waitForResponse;
            }
            set
            {
                waitForResponse = value;
            }
        }
    }
}
