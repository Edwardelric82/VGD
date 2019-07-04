using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pinwheel.PolarisStarter
{
    /// <summary>
    /// Utility class contains product info
    /// </summary>
    public static class VersionInfo
    {
        public static string ProVersionLink
        {
            get
            {
                return "https://www.assetstore.unity3d.com/#!/content/123717?aid=1100l3QbW";
            }
        }

        public static string BasicVersionLink
        {
            get
            {
                return "https://www.assetstore.unity3d.com/#!/content/118854?aid=1100l3QbW";
            }
        }

        public static string Code
        {
            get
            {
                return "1.0.0";
            }
        }

        public static string ProductName
        {
            get
            {
                return "Polaris Starter - Low Poly Terrain Engine";
            }
        }

        public static string ProductNameAndVersion
        {
            get
            {
                return string.Format("{0} v{1}", ProductName, Code);
            }
        }
    }
}
