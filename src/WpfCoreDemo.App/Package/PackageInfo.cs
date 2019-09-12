using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;

namespace WpfCoreDemo.App.Packaging
{
    public class PackageInfo
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string AppInstallerUri { get; set; }
        public bool IsPackaged { get; set; }
    }
}
