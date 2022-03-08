#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class EnemyDataGenerator
    {
        public enum TYPE_SHEET_COLUMNS
        {
            Key,
            TypeEnemyGroup,
            TypeThemeGroup,
            StartHealthValue,
            IncreaseLevelHealthValue,
            IncreaseLevelHealthRate,
            IncreaseWaveHealthValue,
            IncreaseWaveHealthRate,
            StartDamageValue,
            IncreaseLevelDamageValue,
            IncreaseLevelDamageRate,
            AttackCount,
            AttackDelay,
            AttackBulletKey,
            AttackerKeys,
            TypeRewardAsset,
            StartRewardAssetValue,
            IncreaseRewardLevelAssetValue,
            IncreaseRewardLevelAssetRate,
            IncreaseRewardWaveAssetValue,
            IncreaseRewardWaveAssetRate,
            AppearRate
        }

        private readonly static string _dataPath = "Assets/Data/Enemies";
        private readonly static string _bundleName = "data/enemies";
        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";
        private readonly static string _worksheetKey = "Enemy_Data";



        [MenuItem("Data/Enemy/Create And Update All Enemies")]
        private static void CreateAndUpdateAllData()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<EnemyData>(_sheetKey, _worksheetKey, _dataPath, _bundleName);
        }


        [MenuItem("Data/Enemy/Upload All Enemies")]
        private static void UploadAllData()
        {
            GoogleSheetGenerator.UploadAllUnits<EnemyData>(_sheetKey, _worksheetKey, _dataPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif