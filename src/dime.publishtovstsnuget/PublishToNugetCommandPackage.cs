using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Dime.PublishToVSTS
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PublishToNugetCommandPackage.PackageGuidString)]
    [ProvideOptionPage(typeof(OptionDialogPage), "Dime", "VSTS Publish", 0, 0, true)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class PublishToNugetCommandPackage : Package
    {
        /// <summary>
        /// The GUID string that identifies this package.
        /// </summary>
        public const string PackageGuidString = "1754730c-e5c8-4b3d-aa18-bb6de0981d15";

        /// <summary>
        /// Exposes the settings for this package
        /// </summary>
        public readonly Lazy<OptionDialogPage> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishToNugetCommand"/> class.
        /// </summary>
        public PublishToNugetCommandPackage()
        {
            options = new Lazy<OptionDialogPage>(
                () => GetDialogPage(typeof(OptionDialogPage)) as OptionDialogPage, true);
        }

        /// <summary>
        /// Initializes the package
        /// </summary>
        protected override void Initialize()
        {
            PublishToNugetCommand.Initialize(this);
            base.Initialize();
        }
    }
}