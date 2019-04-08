using EnvDTE;

namespace Dime.PublishToAzureDevOps
{
    /// <summary>
    /// Formats a series of nuget commands
    /// </summary>
    internal static class NugetExtensions
    {
        /// <summary>
        /// Creates a nuget pack command with the variables passed in by the arguments
        /// </summary>
        /// <param name="project">The active project</param>
        /// <param name="outputFolder">The output folder where the nupkg file will be created</param>
        /// <param name="solutionConfiguration">The configuration</param>
        /// <returns>A nuget pack command that will create a package for the active project and drops it into the given output folder.</returns>
        internal static string FormatPack(this Project project, string outputFolder, string solutionConfiguration)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            string projectPath = project.FullName;
            return $@"pack {projectPath} -OutputDirectory {outputFolder} -symbols -IncludeReferencedProjects -Prop Configuration={solutionConfiguration}";
        }

        /// <summary>
        /// Creates a nuget pack command with the variables passed in by the arguments
        /// </summary>
        /// <param name="project">The active project</param>
        /// <param name="outputFolder">The output folder where the nupkg file will be created</param>
        /// <param name="feedName">The configuration</param>
        /// <returns>A nuget pack command that will create a package for the active project and drops it into the given output folder.</returns>
        internal static string FormatPush(this Project project, string outputFolder, string feedName, string packageVersion, string assemblyName)
        {
            return $@"push -Source {feedName} -ApiKey VSTS {outputFolder}\{assemblyName}." + packageVersion + ".nupkg";
        }
    }
}