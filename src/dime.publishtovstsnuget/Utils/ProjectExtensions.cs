using EnvDTE;
using System;

namespace Dime.PublishToAzureDevOps
{
    internal static class ProjectExtensions
    {
        /// <summary>
        /// Gets the assembly version and formats it in a NuGet compatible way
        /// </summary>
        /// <param name="project">The active project</param>
        /// <returns>A formatted string of the assembly version, which is major.minor.build</returns>
        internal static string GetLatestPackageVersion(this Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            Version assemblyVersion = new Version(project.Properties.Item("AssemblyVersion").Value.ToString());
            return $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
        }
    }
}