using Microsoft.Win32;
using System.Text;

namespace InfoGather
{
    class LogInstalledSoftwareClass
    {
        const string FORMAT = "{0,-100} {1,-20} {2,-30} {3,-8}\n";

        public static string LogInstalledSoftware()
        {
            var line = string.Format(FORMAT, "DisplayName", "Version", "Publisher", "InstallDate");
            line += string.Format(FORMAT, "-----------", "-------", "---------", "-----------");
            var sb = new StringBuilder(line, 100000);
            ReadRegistryUninstall(ref sb, RegistryView.Registry32);
            sb.Append($"\n[64 bit section]\n\n{line}");
            ReadRegistryUninstall(ref sb, RegistryView.Registry64);
            return sb.ToString();
        }


        public static void ReadRegistryUninstall(ref StringBuilder sb, RegistryView view)
        {
            const string REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view);
            using var subKey = baseKey.OpenSubKey(REGISTRY_KEY);
            foreach (string subkey_name in subKey.GetSubKeyNames())
            {
                using RegistryKey key = subKey.OpenSubKey(subkey_name);
                if (!string.IsNullOrEmpty(key.GetValue("DisplayName") as string))
                {
                    var line = string.Format(FORMAT,
                        key.GetValue("DisplayName"),
                        key.GetValue("DisplayVersion"),
                        key.GetValue("Publisher"),
                        key.GetValue("InstallDate"));
                    sb.Append(line);
                }
                key.Close();
            }
            subKey.Close();
            baseKey.Close();
        }
    }
}
