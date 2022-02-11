
namespace SEF.Quest
{
    using SEF.UI.Toolkit;
    using System.Collections.Generic;
    using System.Linq;

    public class ArrivedLevelConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class DestroyEnemyConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => null;
    }
    public class UpgradeUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;

    }
    public class TechUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }
    public class UpgradeBlacksmithConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => typeof(UIBlacksmith).Name;
    }
    public class TechBlacksmithConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => typeof(UIBlacksmith).Name;
    }
    public class UpgradeVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }
    public class TechVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }
    public class UpgradeCommanderConditionQuestData : IConditionQuestData {
        public string AddressKey => null;
    }
    public class SuccessedResearchConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class SuccessedDailyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class SuccessedWeeklyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class SuccessedExpeditionConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }






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

            _dic.Add(typeof(ArrivedLevelConditionQuestData), "���� �޼�");

            _dic.Add(typeof(DestroyEnemyConditionQuestData), "��óġ");

            _dic.Add(typeof(UpgradeUnitConditionQuestData), "���־��׷��̵�");
            _dic.Add(typeof(TechUnitConditionQuestData), "������ũ");
            _dic.Add(typeof(UpgradeBlacksmithConditionQuestData), "���尣���׷��̵�");
            _dic.Add(typeof(TechBlacksmithConditionQuestData), "���尣��ũ");

            _dic.Add(typeof(UpgradeVillageConditionQuestData), "�������׷��̵�");
            _dic.Add(typeof(TechVillageConditionQuestData), "������ũ");
            _dic.Add(typeof(UpgradeCommanderConditionQuestData), "���ְ����׷��̵�");
            _dic.Add(typeof(SuccessedResearchConditionQuestData), "��������");

            _dic.Add(typeof(SuccessedDailyConditionQuestData), "��������Ʈ����");
            _dic.Add(typeof(SuccessedWeeklyConditionQuestData), "�ְ�����Ʈ����");
            _dic.Add(typeof(SuccessedExpeditionConditionQuestData), "��������");
        }
    }
}