using System;
using System.IO;

namespace InfoGather
{
    class LogSubDirectories
    {
        public static string GetSubDirectories(string root)
        {
            String em = "ALL " + root + " SUBDIRECTORIES:\n ";
            string[] subdirectoryEntries = Directory.GetDirectories(root);
            foreach (string subdirectory in subdirectoryEntries)
            {
                em += LoadSubDirs(subdirectory);
                em += "\n";
            }
            return em;
        }

        private static string LoadSubDirs(string dir)
        {
            String em = "";
            em += dir;
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            foreach (string subdirectory in subdirectoryEntries)
            {
                LoadSubDirs(subdirectory);
            }
            return em;
        }
    }
}
