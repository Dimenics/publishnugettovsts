using System.Diagnostics;
using System.IO;

namespace Dime.PublishToAzureDevOps
{
    internal static class NuGetRunner
    {
        /// <summary>
        /// Executes the pack command
        /// </summary>
        /// <param name="nugetPath">The path to the nuget executable</param>
        /// <param name="cmdParameters">The parameters to be passed into the nuget executable</param>
        internal static void Pack(string nugetPath, string cmdParameters)
        {
            ProcessStartInfo packageCmd = new ProcessStartInfo();
            packageCmd.FileName = Path.GetFileName(nugetPath);
            packageCmd.WorkingDirectory = Path.GetDirectoryName(nugetPath);
            packageCmd.Arguments = cmdParameters;

            Process packageProcess = Process.Start(packageCmd);
            packageProcess.WaitForExit();
        }

        /// <summary>
        /// Executes the push command
        /// </summary>
        /// <param name="nugetPath">The path to the nuget executable</param>
        /// <param name="cmdParameters">The parameters to be passed into the nuget executable</param>
        internal static void Push(string nugetPath, string cmdParameters)
        {
            ProcessStartInfo publishCmd = new ProcessStartInfo();
            publishCmd.FileName = Path.GetFileName(nugetPath);
            publishCmd.WorkingDirectory = Path.GetDirectoryName(nugetPath);
            publishCmd.Arguments = cmdParameters;

            Process publishProcess = new Process() { StartInfo = publishCmd };
            publishProcess.Start();
        }
    }
}