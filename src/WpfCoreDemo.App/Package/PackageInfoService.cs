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
                    Version = Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor + "." + Package.Current.Id.Version.Build + "." + Package.Current.Id.Version.Revision,
                    Name = Package.Current.DisplayName,
                    AppInstallerUri = Package.Current.GetAppInstallerInfo()?.Uri.ToString()
                };
            }
            catch (InvalidOperationException)
            {
                // the app is not running from the package, return and empty info
                return new PackageInfo();
            }
        }

        public static bool IsPreview => GetPackageInfo().Name?.Contains("[PREVIEW]") == true;
    }
}
