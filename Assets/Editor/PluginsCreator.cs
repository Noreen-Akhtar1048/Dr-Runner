using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PluginsCreator : EditorWindow
{

    //[MenuItem("Hassam/Create IAP Manager")]
    //public static void CreateIAPManager()
    //{
    //    GameObject go = new GameObject("IAP Manager");
    //    go.AddComponent<InAppHandler>();
    //    Selection.activeObject = go;
    //}
    [MenuItem("Bhutta/Create Ads Manager")]
    public static void CreateAdsManager()
    {
        GameObject go = new GameObject("Ads Manager");
        go.AddComponent<GoogleAdMobController>();
        Selection.activeObject = go;
    }

}
