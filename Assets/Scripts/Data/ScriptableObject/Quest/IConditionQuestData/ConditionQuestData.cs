
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

    public class ExpandWorkshopConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIWorkshop).Name;
    }

    public class AccumulativelyExpandWorkshopConditionQuestData : IConditionQuestData
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

    public class UpgradeMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIMine).Name;
    }

    public class AccumulativelyUpgradeMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UIMine).Name;
    }

    public class TechMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }

    public class AccumulativelyTechMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }
    public class ExpandMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }

    public class AccumulativelyExpandMineConditionQuestData : IConditionQuestData
    {
        public string AddressKey => typeof(UISmithy).Name;
    }



    //???????? ????
    public class UpgradeCommanderConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;//typeof(UICommander).Name;
    }
    //???????? ????
    public class AccumulativelyUpgradeCommanderConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;//typeof(UICommander).Name;
    }
    //???????? ????
    public class SuccessResearchConditionQuestData : IConditionQuestData
    {
        public string AddressKey => null;
    }
    //???????? ????
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

            _dic.Add(typeof(ArrivedWaveConditionQuestData), "?????? ????");
            _dic.Add(typeof(AccumulativelyArrivedWaveConditionQuestData), "???? ?????? ????");

            _dic.Add(typeof(ArrivedLevelConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyArrivedLevelConditionQuestData), "???? ???? ????");

            _dic.Add(typeof(DestroyEnemyConditionQuestData), "?? ????");
            _dic.Add(typeof(AccumulativelyDestroyEnemyConditionQuestData), "???? ?? ????");

            _dic.Add(typeof(DestroyBossConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyDestroyBossConditionQuestData), "???? ???? ????");
            _dic.Add(typeof(DestroyThemeBossConditionQuestData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyDestroyThemeBossConditionQuestData), "???? ???? ???? ????");


            _dic.Add(typeof(UpgradeUnitConditionQuestData), "???? ??????????");
            _dic.Add(typeof(AccumulativelyUpgradeUnitConditionQuestData), "???? ???? ??????????");
            _dic.Add(typeof(TechUnitConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyTechUnitConditionQuestData), "???? ???? ????");
            _dic.Add(typeof(ExpandWorkshopConditionQuestData), "?????? ????");
            _dic.Add(typeof(AccumulativelyExpandWorkshopConditionQuestData), "???? ?????? ????");

            _dic.Add(typeof(UpgradeSmithyConditionQuestData), "?????? ??????????");
            _dic.Add(typeof(AccumulativelyUpgradeSmithyConditionQuestData), "???? ?????? ??????????");
            _dic.Add(typeof(TechSmithyConditionQuestData), "?????? ????");
            _dic.Add(typeof(AccumulativelyTechSmithyConditionQuestData), "???? ?????? ????");

            _dic.Add(typeof(UpgradeVillageConditionQuestData), "???? ??????????");
            _dic.Add(typeof(AccumulativelyUpgradeVillageConditionQuestData), "???? ???? ??????????");
            _dic.Add(typeof(TechVillageConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyTechVillageConditionQuestData), "???? ???? ????");

            _dic.Add(typeof(UpgradeMineConditionQuestData), "???? ??????????");
            _dic.Add(typeof(AccumulativelyUpgradeMineConditionQuestData), "???? ???? ??????????");
            _dic.Add(typeof(TechMineConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyTechMineConditionQuestData), "???? ???? ????");
            _dic.Add(typeof(ExpandMineConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelyExpandMineConditionQuestData), "???? ???? ????");

            _dic.Add(typeof(UpgradeCommanderConditionQuestData), "?????? ?????????? (??????)");
            _dic.Add(typeof(AccumulativelyUpgradeCommanderConditionQuestData), "???? ?????? ?????????? (??????)");
            _dic.Add(typeof(SuccessResearchConditionQuestData), "???? ???? (??????)");
            _dic.Add(typeof(AccumulativelySuccessResearchConditionQuestData), "???? ???? ???? (??????)");

            _dic.Add(typeof(AchievedDailyConditionQuestData), "???? ?????? ????");
            _dic.Add(typeof(AccumulativelyAchievedDailyConditionQuestData), "???? ???? ?????? ????");
            _dic.Add(typeof(AchievedWeeklyConditionQuestData), "???? ?????? ????");
            _dic.Add(typeof(AccumulativelyAchievedWeeklyConditionQuestData), "???? ???? ?????? ????");
            _dic.Add(typeof(AchievedChallengeConditionQuestData), "???? ?????? ????");
            _dic.Add(typeof(AccumulativelyAchievedChallengeConditionQuestData), "???? ???? ?????? ????");
            _dic.Add(typeof(AchievedGoalConditionQuestData), "???? ?????? ????");
            _dic.Add(typeof(AccumulativelyAchievedGoalConditionQuestData), "???? ???? ?????? ????");
            _dic.Add(typeof(SuccesseExpeditionConditionQuestData), "???? ????");
            _dic.Add(typeof(AccumulativelySuccesseExpeditionConditionQuestData), "???? ???? ????");
        }
    }
#endif
}