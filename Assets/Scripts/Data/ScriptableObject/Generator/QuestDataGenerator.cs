#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEditor;
    using Utility.Generator;

    public class QuestDataGenerator
    {
        public enum TYPE_SHEET_COLUMNS
        {
            Key,
            Group,
            TypeConditionData,
            ConditionValue,
            TypeRewardAsset,
            RewardAssetValue
        }


        private readonly static string _sheetKey = "16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY";

        private readonly static string _dataDailyPath = "Assets/Data/Quests/Daily";
        private readonly static string _bundleDailyName = "data/quests/daily";
        private readonly static string _worksheetDailyKey = "Quest_Daily_Data";


        private readonly static string _dataGoalPath = "Assets/Data/Quests/Goal";
        private readonly static string _bundleGoalName = "data/quests/goal";
        private readonly static string _worksheetGoalKey = "Quest_Goal_Data";


        [MenuItem("Data/Quest/Daily/Create And Update All Daily Quests")]
        private static void CreateAndUpdateAllUnits()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<QuestData>(_sheetKey, _worksheetDailyKey, _dataDailyPath, _bundleDailyName);
        }


        [MenuItem("Data/Quest/Daily/Upload All Daily Quests")]
        private static void UploadAllUnits()
        {
            GoogleSheetGenerator.UploadAllUnits<QuestData>(_sheetKey, _worksheetDailyKey, _dataDailyPath, () => UnityEngine.Debug.Log("Upload End"));
        }


        [MenuItem("Data/Quest/Goal/Create And Update All Goal Quests")]
        private static void CreateAndUpdateAllQuestGoal()
        {
            GoogleSheetGenerator.CreateAndUpdateAllUnits<QuestData>(_sheetKey, _worksheetGoalKey, _dataGoalPath, _bundleGoalName);
        }


        [MenuItem("Data/Quest/Goal/Upload All Goal Quests")]
        private static void UploadAllQuestGoal()
        {
            GoogleSheetGenerator.UploadAllUnits<QuestData>(_sheetKey, _worksheetGoalKey, _dataGoalPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif