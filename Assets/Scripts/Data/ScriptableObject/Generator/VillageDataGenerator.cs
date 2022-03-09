#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class VillageDataGenerator
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
            TechAssetValue
        }

        private readonly static string _dataPath = "Assets/Data/Villages";
        private readonly static string _bundleName = "data/villages";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Village_Data";

        [MenuItem("Data/Villages/Create And Update All Villages")]
        private static void CreateAndUpdateAllData()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<VillageData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Villages/Upload All Villages")]
        private static void UploadAllData()
        {
            GoogleSheetGenerator.UploadAllData<VillageData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif