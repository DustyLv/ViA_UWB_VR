using UnityEditor;
using System.IO;
using UnityEngine;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        string assetBundleDirectory = "AssetBundles";

        string filePath = Path.Combine(Application.streamingAssetsPath, assetBundleDirectory);

        //if (!Directory.Exists(assetBundleDirectory))
        //{
        //    Directory.CreateDirectory(assetBundleDirectory);
        //}

        BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }
}   