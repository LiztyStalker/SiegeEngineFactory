#if UNITY_EDITOR
namespace UtilityManager
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using Utility.Generator;

    public class TranslateGenerator
    {
        private readonly static string PATH = "Assets/TextAssets";
        private readonly static string _bundleName = "textassets";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Quest_Challenge_Data_Tr";


        [MenuItem("Data/Translate/Create And Update Translate")]
        static void CreateAndUpdate()
        {
            string directory = PATH;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            GoogleSheetGenerator.CreateAndUpdateTextAsset(_sheetKey, _worksheetKey, PATH, _bundleName);
        }

        //[MenuItem("AssetBundle/Build AssetBundles for Window")]
        //static void BuildAllAssetBundlesWindow()
        //{
        //    string assetBundleDirectory = PATH_BUNDLE;
        //    if (!Directory.Exists(assetBundleDirectory))
        //    {
        //        Directory.CreateDirectory(assetBundleDirectory);
        //    }
        //    BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //}
    }
}
#endif