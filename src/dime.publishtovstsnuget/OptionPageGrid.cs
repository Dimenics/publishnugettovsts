using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace Dime.PublishToAzureDevOps
{
    /// <summary>
    /// Provides a dialog box in the Tools -> Options page allowing to modify the source and the path
    /// </summary>
    public class OptionDialogPage : DialogPage
    {
        private string _path = @"";
        private string _source = "";

        /// <summary>
        /// Gets or sets the path of the nuget.exe file
        /// </summary>
        [Category("Dime")]
        [DisplayName("NuGet path")]
        [Description("NuGet path")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Gets or sets the unique name of the Azure DevOps NuGet feed
        /// </summary>
        [Category("Dime")]
        [DisplayName("Azure DevOps Source")]
        [Description("Azure DevOps Source")]
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
    }
}