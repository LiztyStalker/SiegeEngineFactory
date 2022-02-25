#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class SmithyDataGenerator
    {
        public enum TYPE_SHEET_COLUMNS
        {
            Key,
            TypeStatusData,
            TypeStatusValue,
            StartStatusValue,
            IncreaseStatusValue,
            DefaultMaxUpgardeValue,
            TypeUpgradeAsset,
            StartUpgradeValue,
            IncreaseUpgradeValue,
            IncreaseUpgrateRate,
            ConditionUnlockData,
            ConditionUnlockValue,
            TypeTechAsset,
            TechAssetValue,            
        }

        private readonly static string _dataPath = "Assets/Data/Smithy";
        private readonly static string _bundleName = "data/smithys";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Smithy_Data";

        [MenuItem("Data/Smithy/Create And Update All Smithy")]
        private static void CreateAndUpdateAllUnits()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<SmithyData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Smithy/Upload All Smithy")]
        private static void UploadAllUnits()
        {
            GoogleSheetGenerator.UploadAllUnits<SmithyData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif