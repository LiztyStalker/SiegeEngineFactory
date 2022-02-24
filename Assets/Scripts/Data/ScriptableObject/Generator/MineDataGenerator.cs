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
            TypeAsset,
            StartProcessAssetValue,
            IncreaseProcessAssetValue,
            IncreaseProcessAssetRate,
            ProcessTime,
            TypeUpgradeAsset,
            StartUpgradeValue,
            IncreaseUpgradeValue,
            IncreaseUpgradeRate,
            MaxUpgardeValue,
            ConditionUnlockData,
            ConditionUnlockValue
        }

        private readonly static string _dataPath = "Assets/Data/Mines";
        private readonly static string _bundleName = "data/mines";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Mine_Data";

        [MenuItem("Data/Mines/Create And Update All Mines")]
        private static void CreateAndUpdateAllData()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<MineData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Villages/Upload All Mines")]
        private static void UploadAllData()
        {
            GoogleSheetGenerator.UploadAllUnits<MineData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif