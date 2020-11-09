using System;

namespace InfoGather
{
    class CommandExecutor
    {
        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        public static string ExecuteCommandSync(object command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process proc = new System.Diagnostics.Process
                {
                    StartInfo = procStartInfo
                };
                proc.Start();

                string result = proc.StandardOutput.ReadToEnd();

                return "COMMAND: " + command + ":\n " + result;
            }
            catch (Exception objException)
            {
                return "ExecuteCommandSync failed" + objException.Message;
            }
        }
    }
}
