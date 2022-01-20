using Microsoft.Win32;
using System;
using System.IO;
using System.Management;


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

            ManagementClass cls2 = new ManagementClass("Win32_StartupCommand");
            ManagementObjectCollection coll2 = cls2.GetInstances();

            foreach (ManagementObject obj in coll2)
            {
                Console.WriteLine("Location: " + obj["Location"].ToString());
                Console.WriteLine("Command: "+obj["Command"].ToString());
                Console.WriteLine("Description: "+obj["Description"].ToString());
                Console.WriteLine("Name: "+obj["Name"].ToString());
                Console.WriteLine("Location: "+obj["Location"].ToString());
                Console.WriteLine("User: "+obj["User"].ToString());

            }
            /////////////////

         /*   removefolder();
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
            ManagementClass cls1 = new ManagementClass("Win32_StartupCommand");
            ManagementObjectCollection coll1 = cls1.GetInstances();

            foreach (ManagementObject obj in coll1)
            {
                Console.WriteLine(obj["Location"].ToString());
                Console.WriteLine(obj["Command"].ToString());
                Console.WriteLine(obj["Description"].ToString());
                Console.WriteLine(obj["Name"].ToString());
                Console.WriteLine(obj["Location"].ToString());
                Console.WriteLine(obj["User"].ToString());

            }
            Console.ReadLine();*/
        }
    }
}
