#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

/// <summary>
/// Toan add SKAdnetwork for IOS 14
/// </summary>
public class SKAdNetworkIdentifier
{
    [PostProcessBuild]
    public static void ChangeXCodePlist(BuildTarget buildTarget, string pathToBuildTarget) {
        if(buildTarget == BuildTarget.iOS) {
#if UNITY_IOS
            string plistPath = $"{pathToBuildTarget}/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get root
            PlistElementDict rootDic = plist.root;

            //Create SKAdnetwork array
            PlistElementArray SKAdnetworkArr = rootDic.CreateArray("SKAdNetworkItems");

#if !TEST_LAB
            rootDic.SetString("NSUserTrackingUsageDescription", "Heroes Strike would like to access IDFA to deliver a better and personalized ads to you.");
#endif

            //Create SKAdnetwork for Ironsource
            PlistElementDict SKAdnetworkIronsource = SKAdnetworkArr.AddDict();
            SKAdnetworkIronsource.SetString("SKAdNetworkIdentifier", "SU67R6K2V3.skadnetwork");

            //Create SKAdnetwork for Admob
            PlistElementDict SKAdnetworkAdmob = SKAdnetworkArr.AddDict();
            SKAdnetworkAdmob.SetString("SKAdNetworkIdentifier", "cstr6suwn9.skadnetwork");

            //Create SKAdnetwork for Facebook
            PlistElementDict SKAdnetworkFacebook1 = SKAdnetworkArr.AddDict();
            SKAdnetworkFacebook1.SetString("SKAdNetworkIdentifier", "v9wttpbfk9.skadnetwork");
            PlistElementDict SKAdnetworkFacebook2 = SKAdnetworkArr.AddDict();
            SKAdnetworkFacebook2.SetString("SKAdNetworkIdentifier", "n38lu8286q.skadnetwor");

            //Create SKAdnetwork for Unity
            PlistElementDict SKAdnetworkUnity = SKAdnetworkArr.AddDict();
            SKAdnetworkUnity.SetString("SKAdNetworkIdentifier", "4dzt52r2t5.skadnetwork");


            //Create SKAdnetwork for Applovin
            PlistElementDict SKAdnetworkApplovin = SKAdnetworkArr.AddDict();
            SKAdnetworkApplovin.SetString("SKAdNetworkIdentifier", "ludvb6z3bs.skadnetwork");

            //Write to plist file
            File.WriteAllText(plistPath, plist.WriteToString());
#endif
        }
    }
}
#endif