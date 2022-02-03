
namespace SEF.Quest
{
    using System.Collections.Generic;
    using System.Linq;

    public class ArrivedLevelConditionQuestData : IConditionQuestData { }
    public class DestroyEnemyConditionQuestData : IConditionQuestData { }
    public class UpgradeUnitConditionQuestData : IConditionQuestData { }
    public class TechUnitConditionQuestData : IConditionQuestData { }
    public class UpgradeBlacksmithConditionQuestData : IConditionQuestData { }
    public class TechBlacksmithConditionQuestData : IConditionQuestData { }
    public class UpgradeVillageConditionQuestData : IConditionQuestData { }
    public class TechVillageConditionQuestData : IConditionQuestData { }
    public class UpgradeCommanderConditionQuestData : IConditionQuestData { }
    public class SuccessedResearchConditionQuestData : IConditionQuestData { }
    public class SuccessedDailyConditionQuestData : IConditionQuestData { }
    public class SuccessedWeeklyConditionQuestData : IConditionQuestData { }
    public class SuccessedExpeditionConditionQuestData : IConditionQuestData { }






    public class ConditionQuestDataUtility
    {
        private Dictionary<System.Type, string> _dic;

        private static ConditionQuestDataUtility _current;

        public static ConditionQuestDataUtility Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new ConditionQuestDataUtility();
                }
                return _current;
            }
        }

        public string GetTypeToContext(System.Type type)
        {
            if (_dic.ContainsKey(type))
                return _dic[type];
            return null;
        }

        public System.Type[] GetTypes() => _dic.Keys.ToArray();

        public string[] GetValues() => _dic.Values.ToArray();

        public int FindIndex(System.Type type) => _dic.Keys.ToList().FindIndex(t => t == type);

        private ConditionQuestDataUtility()
        {
            _dic = new Dictionary<System.Type, string>();

            _dic.Add(typeof(ArrivedLevelConditionQuestData), "레벨 달성");

            _dic.Add(typeof(DestroyEnemyConditionQuestData), "적처치");

            _dic.Add(typeof(UpgradeUnitConditionQuestData), "유닛업그레이드");
            _dic.Add(typeof(TechUnitConditionQuestData), "유닛테크");
            _dic.Add(typeof(UpgradeBlacksmithConditionQuestData), "대장간업그레이드");
            _dic.Add(typeof(TechBlacksmithConditionQuestData), "대장간테크");

            _dic.Add(typeof(UpgradeVillageConditionQuestData), "마을업그레이드");
            _dic.Add(typeof(TechVillageConditionQuestData), "마을테크");
            _dic.Add(typeof(UpgradeCommanderConditionQuestData), "지휘관업그레이드");
            _dic.Add(typeof(SuccessedResearchConditionQuestData), "연구진행");

            _dic.Add(typeof(SuccessedDailyConditionQuestData), "일일퀘스트진행");
            _dic.Add(typeof(SuccessedWeeklyConditionQuestData), "주간퀘스트진행");
            _dic.Add(typeof(SuccessedExpeditionConditionQuestData), "원정진행");
        }
    }
}