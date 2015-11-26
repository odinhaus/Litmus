using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace Litmus
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		public override UIWindow Window {
			get;
			set;
		}

		public override void FinishedLaunching (UIApplication application)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes (
					                   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					                   new NSSet ());
			} else {
				var notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (notificationTypes);
			}
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			var token = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace (token)) {
				token = token.Trim ('<').Trim ('>');
			}

			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey ("PushDeviceToken");

			if (string.IsNullOrEmpty (oldDeviceToken) || !oldDeviceToken.Equals (token)) {
				// TODO: notify push server that device token changed.....
			}

			NSUserDefaults.StandardUserDefaults.SetString (token, "PushDeviceToken");
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			new UIAlertView ("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show ();
		}

		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

