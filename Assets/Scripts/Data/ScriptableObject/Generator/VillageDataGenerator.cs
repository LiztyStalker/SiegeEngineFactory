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
        }

        private readonly static string _dataPath = "Assets/Data/Villages";
        private readonly static string _bundleName = "data/villages";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Village_Data";

        [MenuItem("Data/Villages/Create And Update All Villages")]
        private static void CreateAndUpdateAllUnits()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<VillageData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Villages/Upload All Villages")]
        private static void UploadAllUnits()
        {
            GoogleSheetGenerator.UploadAllUnits<VillageData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif