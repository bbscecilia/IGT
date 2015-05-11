using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RegistryGACCustomAction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> errors = new List<string>();
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders",
                true);
            if (regKey != null)
            {
                if (args[0].Trim().ToLower() == "install")
                {
                    Console.WriteLine("Registering LocalEGM library...");
                    RegistryKey assemblyKey = regKey.CreateSubKey("LocalEGM");
                    if (assemblyKey != null)
                    {
                        assemblyKey.SetValue("", @"c:\windows\assembly"); // Setting the (Default) value
                        assemblyKey.Close();
                        assemblyKey = null;
                    }
                    else
                    {
                        errors.Add(@"Could not create sub-key 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\LocalEGM'.");
                    }
                }
                else if (args[0].Trim().ToLower() == "uninstall")
                {
                    Console.WriteLine("Unregistering LocalEGM library...");
                    if (regKey.OpenSubKey("LocalEGM") != null)
                    {
                        regKey.DeleteSubKey("LocalEGM");
                    }
                }

                regKey.Close();
            }
            else
            {
                errors.Add(@"Could not open sub-key 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\AssemblyFolders'.");
            }

            regKey = null;
            if (errors.Count() > 0)
            {
                MessageBox.Show(errors.Aggregate((err1, err2) => err1 + "\n" + err2), "Errors During Install",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
