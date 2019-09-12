using System;
using System.Collections.Generic;
using System.Text;
using WpfCoreDemo.Data.Model;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;

namespace WpfCoreDemo.App.Packaging
{
    public static class PackageInfoService
    {

        public static PackageInfo GetPackageInfo()
        {
            try
            {
                return new PackageInfo()
                {
                    IsPackaged = true,
                    Version = Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor + "." + Package.Current.Id.Version.Revision + "." + Package.Current.Id.Version.Build,
                    Name = Package.Current.DisplayName,
                    AppInstallerUri = Package.Current.GetAppInstallerInfo()?.Uri.ToString()
                };
            }
            catch (InvalidOperationException)
            {
                // package has no identity
                return new PackageInfo();
            }
        }

        public static bool IsPreview => GetPackageInfo().Name?.Contains("[PREVIEW]") == true;
    }
}
