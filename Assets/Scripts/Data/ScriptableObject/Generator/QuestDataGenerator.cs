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

        private readonly static string _dataWeeklyPath = "Assets/Data/Quests/Weekly";
        private readonly static string _bundleWeeklyName = "data/quests/weekly";
        private readonly static string _worksheetWeeklyKey = "Quest_Weekly_Data";

        private readonly static string _dataGoalPath = "Assets/Data/Quests/Goal";
        private readonly static string _bundleGoalName = "data/quests/goal";
        private readonly static string _worksheetGoalKey = "Quest_Goal_Data";

        private readonly static string _dataChallengePath = "Assets/Data/Quests/Challenge";
        private readonly static string _bundleChallengeName = "data/quests/challenge";
        private readonly static string _worksheetChallengeKey = "Quest_Challenge_Data";



        [MenuItem("Data/Quest/Daily/Create And Update All Daily Quests")]
        private static void CreateAndUpdateAllQuestDaily()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<QuestData>(_sheetKey, _worksheetDailyKey, _dataDailyPath, _bundleDailyName);
        }


        [MenuItem("Data/Quest/Daily/Upload All Daily Quests")]
        private static void UploadAllQuestDaily()
        {
            GoogleSheetGenerator.UploadAllData<QuestData>(_sheetKey, _worksheetDailyKey, _dataDailyPath, () => UnityEngine.Debug.Log("Upload End"));
        }


        [MenuItem("Data/Quest/Goal/Create And Update All Goal Quests")]
        private static void CreateAndUpdateAllQuestGoal()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<QuestData>(_sheetKey, _worksheetGoalKey, _dataGoalPath, _bundleGoalName);
        }


        [MenuItem("Data/Quest/Goal/Upload All Goal Quests")]
        private static void UploadAllQuestGoal()
        {
            GoogleSheetGenerator.UploadAllData<QuestData>(_sheetKey, _worksheetGoalKey, _dataGoalPath, () => UnityEngine.Debug.Log("Upload End"));
        }


        [MenuItem("Data/Quest/Challenge/Create And Update All Challenge Quests")]
        private static void CreateAndUpdateAllQuestChallenge()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<QuestData>(_sheetKey, _worksheetChallengeKey, _dataChallengePath, _bundleChallengeName);
        }


        [MenuItem("Data/Quest/Challenge/Upload All Challenge Quests")]
        private static void UploadAllQuestChallenge()
        {
            GoogleSheetGenerator.UploadAllData<QuestData>(_sheetKey, _worksheetChallengeKey, _dataChallengePath, () => UnityEngine.Debug.Log("Upload End"));
        }


        [MenuItem("Data/Quest/Weekly/Create And Update All Weekly Quests")]
        private static void CreateAndUpdateAllQuestWeekly()
        {
            GoogleSheetGenerator.CreateAndUpdateAllData<QuestData>(_sheetKey, _worksheetWeeklyKey, _dataWeeklyPath, _bundleWeeklyName);
        }


        [MenuItem("Data/Quest/Weekly/Upload All Weekly Quests")]
        private static void UploadAllQuestWeekly()
        {
            GoogleSheetGenerator.UploadAllData<QuestData>(_sheetKey, _worksheetWeeklyKey, _dataWeeklyPath, () => UnityEngine.Debug.Log("Upload End"));
        }
    }
}
#endif