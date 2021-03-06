#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class MineDataGenerator
    {
        public enum TYPE_SHEET_COLUMNS
        {
            Key,
            TypeProcess,
            TypeProcessAsset,
            StartProcessAssetValue,
            IncreaseProcessAssetValue,
            ProcessTime,
            TypeUpgradeAsset,
            StartUpgradeValue,
            IncreaseUpgradeValue,
            IncreaseUpgradeRate,
            DefaultMaxUpgardeValue,
            ConditionUnlockData,
            ConditionUnlockValue,
            TypeTechAsset,
            TechAssetValue,
        }

        private readonly static string _dataPath = "Assets/Data/Mines";
        private readonly static string _bundleName = "data/mines";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Mine_Data";

        [MenuItem("Data/Mines/Create And Update All Mines")]
        private static void CreateAndUpdateAllData()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<MineData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Mines/Upload All Mines")]
        private static void UploadAllData()
        {
            GoogleSheetGenerator.UploadAllData<MineData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif