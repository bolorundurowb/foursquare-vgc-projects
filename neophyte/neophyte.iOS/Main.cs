using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace neophyte.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Plugin.InputKit.Platforms.iOS.Config.Init();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UINavigationBar.Appearance.TintColor = UIColor.Black;
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
