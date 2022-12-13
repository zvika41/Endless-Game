using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class BuildAssetBundle : Editor
{
   [MenuItem("Assets/Build AssetBundles")]
   private static void BuildAllAssetBundles()
   {
      string assetBundleDirectory = "Assets/StreamingAssets";

      if (!Directory.Exists(Application.streamingAssetsPath))
      {
         Directory.CreateDirectory(assetBundleDirectory);
      }

      BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
   }
}
