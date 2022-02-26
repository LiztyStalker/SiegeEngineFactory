#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class UnitDataGenerator
    {
        public enum TYPE_SHEET_COLUMNS
        {
            Key,
            Group,
            StartHealthValue,
            IncreaseHealthValue,
            IncreaseHealthRate,
            ProductTime,
            StartDamageValue,
            IncreaseDamageValue,
            IncreaseDamageRate,
            TypeAttackRange,
            TypeAttackAction,
            AttackPopulation,
            AttackCount,
            AttackDelay,
            BulletDataKey,
            BulletScale,
            AttackDataKeys,
            StartUpgradeAsset,
            IncreaseUpgradeAssetValue,
            IncreaseUpgradeAssetRate,
            DefaultMaxUpgradeValue,
            //ConditionTechTree, //AA-ZZ까지 사용 가능하면 활성화
            //ConditionTechTreeValue,
            TypeTechList,
            TechUnitKeys,
            TypeTechAssets,
            TechAssetValues,
        }

        private readonly static string _dataPath = "Assets/Data/Units";
        private readonly static string _bundleName = "data/units";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Unit_Data";



        [MenuItem("Data/Unit/Create And Update All Units")]
        private static void CreateAndUpdateAllUnits()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<UnitData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Unit/Upload All Units")]
        private static void UploadAllUnits()
        {
            GoogleSheetGenerator.UploadAllUnits<UnitData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif