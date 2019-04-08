using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.IO;

namespace Dime.PublishToAzureDevOps
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class PublishToNugetCommand
    {
        /// <summary>
        /// Command ID
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID)
        /// </summary>
        public static readonly Guid CommandSet = new Guid("57b1508e-928a-47fb-817d-e6dbfe26fe79");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishToNugetCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private PublishToNugetCommand(Package package)
        {
            if (package == null)
                throw new ArgumentNullException("package");

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static PublishToNugetCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new PublishToNugetCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            // Get the current project
            DTE2 dTE2 = Package.GetGlobalService(typeof(EnvDTE.DTE)) as DTE2;
            Project project = ((Array)dTE2.ActiveSolutionProjects).GetValue(0) as Project;

            // Get the settings (nuget.exe path and VSTS name)
            string nugetPath = ((PublishToNugetCommandPackage)Instance.package).options.Value.Path;
            string feedName = ((PublishToNugetCommandPackage)Instance.package).options.Value.Source;

            // Populate the nuget pack string format and execute it
            string projectFolder = Path.GetDirectoryName(Path.Combine(project.FullName));
            string solutionConfiguration = "Release";
            string outputFolder = projectFolder + $@"\bin\{solutionConfiguration}";
            string packageCmdParams = project.FormatPack(outputFolder, solutionConfiguration);
            NuGetRunner.Pack(nugetPath, packageCmdParams);

            // After the package was created it can be published to the feed
            string assemblyName = project.Properties.Item("AssemblyName").Value.ToString();
            string publishCmdParams = project.FormatPush(outputFolder, feedName, project.GetLatestPackageVersion(), assemblyName);
            NuGetRunner.Push(nugetPath, publishCmdParams);
        }
    }
}