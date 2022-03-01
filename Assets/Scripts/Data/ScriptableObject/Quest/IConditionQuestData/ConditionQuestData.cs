
namespace SEF.Quest
{
#if INCLUDE_UI_TOOLKIT
    using SEF.UI.Toolkit;
#else
    using SEF.UI;
#endif
    using System.Collections.Generic;
    using System.Linq;

    public class ArrivedWaveConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class AccumulativelyArrivedWaveConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class ArrivedLevelConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyArrivedLevelConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class CreateUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyCreateUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class DestroyEnemyConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => null;
    }

    public class AccumulativelyDestroyEnemyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class DestroyBossConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyDestroyBossConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class DestroyThemeBossConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyDestroyThemeBossConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class UpgradeUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class AccumulativelyUpgradeUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class TechUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class AccumulativelyTechUnitConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class ExpendWorkshopLineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class AccumulativelyExpendWorkshopLineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }


    public class UpgradeSmithyConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => typeof(UISmithy).Name;
    }

    public class AccumulativelyUpgradeSmithyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }
    public class TechSmithyConditionQuestData : IConditionQuestData 
    {
        public string AddressKey => typeof(UISmithy).Name;
    }

    public class AccumulativelyTechSmithyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }
    
    public class UpgradeVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }
    
    public class AccumulativelyUpgradeVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }
    
    public class TechVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }
    
    public class AccumulativelyTechVillageConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIVillage).Name;
    }

    //제작하지 않음
    public class UpgradeCommanderConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;//typeof(UICommander).Name;
    }
    //제작하지 않음
    public class AccumulativelyUpgradeCommanderConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;//typeof(UICommander).Name;
    }
    //제작하지 않음
    public class SuccessResearchConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    //제작하지 않음
    public class AccumulativelySuccessResearchConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AchievedDailyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyAchievedDailyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AchievedWeeklyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    
    public class AccumulativelyAchievedWeeklyConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AchievedChallengeConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyAchievedChallengeConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class AchievedGoalConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }

    public class AccumulativelyAchievedGoalConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    public class SuccesseExpeditionConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }

    public class AccumulativelySuccesseExpeditionConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }

    public class GetGoldAssetDataConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }
    public class AccumulativelyGetGoldAssetDataConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }
    public class UsedGoldAssetDataConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }
    public class AccumulativelyUsedGoldAssetDataConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null; //typeof(UIExpedition).Name;
    }
    







#if UNITY_EDITOR


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

            _dic.Add(typeof(ArrivedWaveConditionQuestData), "웨이브 달성");
            _dic.Add(typeof(AccumulativelyArrivedWaveConditionQuestData), "누적 웨이브 달성");

            _dic.Add(typeof(ArrivedLevelConditionQuestData), "레벨 달성");
            _dic.Add(typeof(AccumulativelyArrivedLevelConditionQuestData), "누적 레벨 달성");

            _dic.Add(typeof(DestroyEnemyConditionQuestData), "적 처치");
            _dic.Add(typeof(AccumulativelyDestroyEnemyConditionQuestData), "누적 적 처치");

            _dic.Add(typeof(DestroyBossConditionQuestData), "보스 처치");
            _dic.Add(typeof(AccumulativelyDestroyBossConditionQuestData), "누적 보스 처치");
            _dic.Add(typeof(DestroyThemeBossConditionQuestData), "테마 보스 처치");
            _dic.Add(typeof(AccumulativelyDestroyThemeBossConditionQuestData), "누적 테마 보스 처치");


            _dic.Add(typeof(UpgradeUnitConditionQuestData), "유닛 업그레이드");
            _dic.Add(typeof(AccumulativelyUpgradeUnitConditionQuestData), "누적 유닛 업그레이드");
            _dic.Add(typeof(TechUnitConditionQuestData), "유닛 테크");
            _dic.Add(typeof(AccumulativelyTechUnitConditionQuestData), "누적 유닛 테크");

            _dic.Add(typeof(UpgradeSmithyConditionQuestData), "대장간 업그레이드");
            _dic.Add(typeof(AccumulativelyUpgradeSmithyConditionQuestData), "누적 대장간 업그레이드");
            _dic.Add(typeof(TechSmithyConditionQuestData), "대장간 테크");
            _dic.Add(typeof(AccumulativelyTechSmithyConditionQuestData), "누적 대장간 테크");

            _dic.Add(typeof(UpgradeVillageConditionQuestData), "마을 업그레이드");
            _dic.Add(typeof(AccumulativelyUpgradeVillageConditionQuestData), "누적 마을 업그레이드");
            _dic.Add(typeof(TechVillageConditionQuestData), "마을 테크");
            _dic.Add(typeof(AccumulativelyTechVillageConditionQuestData), "누적 마을 테크");

            _dic.Add(typeof(UpgradeCommanderConditionQuestData), "지휘관 업그레이드 (미개발)");
            _dic.Add(typeof(AccumulativelyUpgradeCommanderConditionQuestData), "누적 지휘관 업그레이드 (미개발)");
            _dic.Add(typeof(SuccessResearchConditionQuestData), "연구 진행 (미개발)");
            _dic.Add(typeof(AccumulativelySuccessResearchConditionQuestData), "누적 연구 진행 (미개발)");

            _dic.Add(typeof(AchievedDailyConditionQuestData), "일일 퀘스트 진행");
            _dic.Add(typeof(AccumulativelyAchievedDailyConditionQuestData), "누적 일일 퀘스트 진행");
            _dic.Add(typeof(AchievedWeeklyConditionQuestData), "주간 퀘스트 진행");
            _dic.Add(typeof(AccumulativelyAchievedWeeklyConditionQuestData), "누적 주간 퀘스트 진행");
            _dic.Add(typeof(AchievedChallengeConditionQuestData), "도전 퀘스트 진행");
            _dic.Add(typeof(AccumulativelyAchievedChallengeConditionQuestData), "누적 주간 퀘스트 진행");
            _dic.Add(typeof(AchievedGoalConditionQuestData), "목표 퀘스트 진행");
            _dic.Add(typeof(AccumulativelyAchievedGoalConditionQuestData), "누적 주간 퀘스트 진행");
            _dic.Add(typeof(SuccesseExpeditionConditionQuestData), "원정 진행");
            _dic.Add(typeof(AccumulativelySuccesseExpeditionConditionQuestData), "누적 원정 진행");
        }
    }
#endif
}