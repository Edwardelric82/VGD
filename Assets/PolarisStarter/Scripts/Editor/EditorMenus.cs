using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Pinwheel.PolarisStarter
{
	public static class EditorMenus
	{
        [MenuItem("Polaris Starter/Go Pro")]
        public static void GetProVersion()
        {
            Application.OpenURL(VersionInfo.ProVersionLink);
        }

        [MenuItem("Polaris Starter/Basic Edition")]
        public static void GetBasicVersion()
        {
            Application.OpenURL(VersionInfo.BasicVersionLink);
        }

        [MenuItem("Polaris Starter/User Guide", priority = 100000)]
        public static void ShowUserGuide()
        {
            Application.OpenURL("https://docs.pinwheel.studio/polaris/");
        }

        [MenuItem("Polaris Starter/Forum Thread", priority = 100001)]
        public static void ShowForumThread()
        {
            Application.OpenURL("https://docs.pinwheel.studio/polaris/");
        }

        [MenuItem("Polaris Starter/Homepage", priority = 100002)]
        public static void ShowHomepage()
        {
            Application.OpenURL("http://pinwheel.studio/");
        }

        [MenuItem("Polaris Starter/Like Us", priority = 100003)]
        public static void ShowFacebookPage()
        {
            Application.OpenURL("https://www.facebook.com/pinwheel.technologies/");
        }

        [MenuItem("Polaris Starter/Support", priority = 1000000)]
        public static void SendSupportRequest()
        {
            string url = "mailto:support@pinwheel.studio" +
                "?subject=[Polaris Starter]%20SHORT%20QUESTION" +
                "&body=YOUR%20QUESTION%20IN%20DETAILED";
            Application.OpenURL(url);
        }

        [MenuItem("Polaris Starter/Version Info", priority = 1000009)]
        public static void ShowVersionInfo()
        {
            UnityEditor.EditorUtility.DisplayDialog(
                "Version Info",
                VersionInfo.ProductNameAndVersion,
                "OK");
        }
    }
}
