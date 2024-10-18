using UnityEngine;
using System;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(GoogleAdMobController))]
public class GoogleAdMobControllerEditor : Editor
{
    GUIStyle myStyle1, boxStyle;
    private bool showingTestIDS;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // variables
        string dependenyManagerVersion = GetDependenyVersion();
        string googleMobileAdsSDKAndroid = GetSDKAndroidVersion();
        string googleMobileAdsSDKIOS = GetSDKIOSVersion();
        string googleMessagingPlatform = GetMessagingPlatformVersion();
        string pluginVersion = GetPluginVersion();
        string unityMediationVersion = GetUnityMediationVersion();

        // box Style
        if (boxStyle == null)
        {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
            boxStyle.fontStyle = FontStyle.Bold;
            boxStyle.alignment = TextAnchor.UpperLeft;
        }

        // style
        if (myStyle1 == null)
        {
            myStyle1 = new GUIStyle(GUI.skin.label);
            myStyle1.richText = true;
            myStyle1.fontStyle = FontStyle.Bold;
        }

        EditorGUILayout.Space(10f);
        EditorGUILayout.BeginVertical(boxStyle);
        EditorGUILayout.LabelField("External Dependency Manager: " + dependenyManagerVersion, myStyle1);
        EditorGUILayout.LabelField("Google MobileAds SDK Android: " + googleMobileAdsSDKAndroid, myStyle1);
        EditorGUILayout.LabelField("Google MobileAds SDK IOS: " + googleMobileAdsSDKIOS, myStyle1);
        EditorGUILayout.LabelField("Google User Messaging Platform: " + googleMessagingPlatform, myStyle1);
        EditorGUILayout.LabelField("Google MobileAds Version: " + pluginVersion, myStyle1);
        EditorGUILayout.LabelField("Unity Mediation Version: " + unityMediationVersion, myStyle1);
        EditorGUILayout.EndVertical();

        if (showingTestIDS)
        {
            if (GUILayout.Button("HideTestIDs", GUILayout.MaxHeight(25f)))
            {
                showingTestIDS = false;
            }

            EditorGUILayout.BeginVertical(boxStyle);
            EditorGUILayout.TextField("App ID: " + GoogleAdMobController.TestIDs.AppId, myStyle1);
            EditorGUILayout.TextField("Banner ID: " + GoogleAdMobController.TestIDs.BannerId, myStyle1);
            EditorGUILayout.TextField("RectBanner ID: " + GoogleAdMobController.TestIDs.RectBannerId, myStyle1);
            EditorGUILayout.TextField("Interstitial ID: " + GoogleAdMobController.TestIDs.InterstitialId, myStyle1);
            EditorGUILayout.TextField("Rewarded ID: " + GoogleAdMobController.TestIDs.RectBannerId, myStyle1);
            EditorGUILayout.TextField("AppOpen ID: " + GoogleAdMobController.TestIDs.AppOpenId, myStyle1);
            EditorGUILayout.EndVertical();
        }
        else
        {
            if (GUILayout.Button("ShowTestIDS", GUILayout.MaxHeight(25f)))
            {
                showingTestIDS = true;
            }
        }
    }

    private void OnDisable()
    {
        showingTestIDS = false;
    }

    private string GetDependenyVersion()
    {
        string path = "Assets/ExternalDependencyManager/Editor";
        string[] paths = AssetDatabase.GetSubFolders(path);

        if (paths.Length > 1)
        {
            Debug.LogError("More than One External Dependency Manager Found");
            return String.Empty;
        }

        return paths[0].Substring(path.Length + 1);
    }

    private string GetSDKAndroidVersion()
    {
        string path = "Assets/GoogleMobileAds/Editor/GoogleMobileAdsDependencies.xml";

        // if File Exists At Path
        if (!File.Exists(path))
        {
            return String.Empty;
        }

        string file = File.ReadAllText(path);

        // search for
        string searchFor = "com.google.android.gms:play-services-ads:";

        // find index for this
        int indexOf = file.IndexOf(searchFor);

        string[] splited = file.Substring(indexOf + searchFor.Length).Split('\"');

        return splited[0];
    }

    private string GetSDKIOSVersion()
    {
        string path = "Assets/GoogleMobileAds/Editor/GoogleMobileAdsDependencies.xml";

        // if File Exists At Path
        if (!File.Exists(path))
        {
            return String.Empty;
        }

        string file = File.ReadAllText(path);

        string searchFor = "Google-Mobile-Ads-SDK";

        int indexOf = file.IndexOf(searchFor);

        string[] lines = file.Substring(indexOf + searchFor.Length).Split(' ');

        return lines[2].Split('\"')[0];
    }

    private string GetPluginVersion()
    {
        string path = "Assets/GoogleMobileAds/CHANGELOG.md";

        // if File Exists At Path
        if (!File.Exists(path))
        {
            return String.Empty;
        }

        string file = File.ReadAllText(path);

        string searchFor = "Version";

        int indexOf = file.IndexOf(searchFor);

        return file.Substring(indexOf).Split(' ')[1].Split('\n')[0];
    }

    private string GetMessagingPlatformVersion()
    {
        string path = "Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml";

        // if File Exists At Path
        if (!File.Exists(path))
        {
            return String.Empty;
        }

        string file = File.ReadAllText(path);

        string searchFor = "com.google.android.ump:user-messaging-platform:";

        int indexOf = file.IndexOf(searchFor);

        return file.Substring(indexOf + searchFor.Length).Split('\"')[0];
    }

    private string GetUnityMediationVersion()
    {
        string path = "Assets\\GoogleMobileAds\\Mediation\\UnityAds\\Editor\\UnityMediationDependencies.xml";

        // if File Exists At Path
        if (!File.Exists(path))
        {
            return String.Empty;
        }

        string file = File.ReadAllText(path);

        string searchFor = "com.google.ads.mediation:unity:";

        int indexOf = file.IndexOf(searchFor);

        return file.Substring(indexOf + searchFor.Length).Split('\"')[0];
    }
}