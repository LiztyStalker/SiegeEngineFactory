namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;
    using Data;
    using Unit;
    using Utility.Address;
    using SEF.Manager;

    public class UIGame : MonoBehaviour
    {
        public static event System.Action<float> ProcessEvent;

        private AddressDictionary _addressDictionary;

        private UIAsset _uiAsset;
        private UISystem _uiSystem;
        private UIPlay _uiPlay;
        private UIQuest _uiQuest;
        private UIQuestTab _uiQuestTab;

        [SerializeField]
        private Button _questButton;

        public static UIGame Create()
        {
            var uiGame = FindObjectOfType<UIGame>();
            if (uiGame == null)
            {
                var obj = new GameObject();
                obj.name = "UI@Game";
                uiGame = obj.AddComponent<UIGame>();
            }
            return uiGame;
        }

        public void Initialize()
        {
            //            _questButton = _root.Q<Button>("quest-button");

            _uiAsset = GetComponentInChildren<UIAsset>(true);
            if (_uiAsset == null) _uiAsset = UIAsset.Create();

            _uiSystem = GetComponentInChildren<UISystem>(true);
            if (_uiSystem == null) _uiSystem = UISystem.Create();

            _uiPlay = GetComponentInChildren<UIPlay>(true);
            if (_uiPlay == null) _uiPlay = UIPlay.Create();

            _uiQuest = GetComponentInChildren<UIQuest>(true);
            if (_uiQuest == null) _uiQuest = UIQuest.Create();

            _uiQuestTab = GetComponentInChildren<UIQuestTab>(true);
            if (_uiQuestTab == null) _uiQuestTab = UIQuestTab.Create();

            Debug.Assert(_uiAsset != null, "_uiAsset 이 등록되지 않았습니다");
            Debug.Assert(_uiSystem != null, "_uiSystem 이 등록되지 않았습니다");
            Debug.Assert(_uiPlay != null, "_uiPlay 가 등록되지 않았습니다");
            Debug.Assert(_uiQuest != null, "_uiQuest 가 등록되지 않았습니다");
            Debug.Assert(_uiQuestTab != null, "_uiQuestTab 가 등록되지 않았습니다");

            _uiAsset.Initialize();
            _uiSystem.Initialize();
            _uiPlay.Initialize();
            _uiQuest.Initialize();
            _uiQuestTab.Initialize();

            _addressDictionary = AddressDictionary.Create();
            _addressDictionary.Initialize();
            _addressDictionary.AddAddresses(_uiSystem.GetAddresses());

            _questButton.onClick.AddListener(_uiQuest.Show);

        }

        public void CleanUp()
        {
            _questButton.onClick.RemoveListener(_uiQuest.Show);

            _addressDictionary.CleanUp();

            _uiAsset.CleanUp();
            _uiSystem.CleanUp();
            _uiPlay.CleanUp();
            _uiQuest.CleanUp();
            _uiQuestTab.CleanUp();
        }

        public void RunProcess(float deltaTime)
        {
            ProcessEvent?.Invoke(deltaTime);
        }

        public void ShowRewardOffline(RewardAssetPackage assetPackage, System.Action rewardCallback, System.Action advertisementCallback)
        {
            var msg = "";
            var arr = assetPackage.GetAssetArray();
            for (int i = 0; i < arr.Length; i++)
            {
                msg += arr[i].GetType().Name + " " + arr[i].GetValue() + "\n";
            }
            UICommon.Current.ShowRewardOffline(msg, "보상", "광고보상", rewardCallback, advertisementCallback);
        }


        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime) => _uiSystem.RefreshUnit(index, unitEntity, nowTime);
        public void RefreshNextEnemyUnit(EnemyActor enemyActor, LevelWaveData levelWaveData) => _uiPlay.RefreshNextEnemyUnit(enemyActor, levelWaveData);

        public void RefreshSmithy(int index, SmithyEntity entity) => _uiSystem.RefreshBlacksmith(index, entity);
        public void RefreshVillage(int index, VillageEntity entity) => _uiSystem.RefreshVillage(index, entity);
        public void RefreshMine(int index, MineEntity entity) => _uiSystem.RefreshMine(index, entity);

        public void RefreshAssetEntity(AssetPackage assetEntity) => _uiSystem.RefreshAssetEntity(assetEntity);
        public void RefreshExpend(IAssetData assetData, bool isActive) => _uiSystem.RefreshExpend(assetData, isActive);
        public void RefreshAssetData(IAssetData assetData) => _uiAsset.RefreshAssetData(assetData);
        public void ShowHit(PlayActor playActor, DamageData attackData) => _uiPlay.ShowHit(playActor, attackData);


        public void RefreshQuest(QuestEntity entity)
        {
            _uiQuest.RefreshQuest(entity);
            if (entity.TypeQuestGroup == QuestData.TYPE_QUEST_GROUP.Goal)
                _uiQuestTab.RefreshQuest(entity);
        }

        #region ##### Listener #####

        ///
        public void AddOnWorkshopUpgradeListener(System.Action<int> act) => _uiSystem.AddOnWorkshopUpgradeListener(act);
        public void RemoveOnWorkshopUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnWorkshopUpgradeListener(act);
        public void AddOnSmithyUpgradeListener(System.Action<int> act) => _uiSystem.AddOnSmithyUpgradeListener(act);
        public void RemoveOnSmithyUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnSmithyUpgradeListener(act);
        public void AddOnSmithyUpTechListener(System.Action<int> act) => _uiSystem.AddOnSmithyUpTechListener(act);
        public void RemoveOnSmithyUpTechListener(System.Action<int> act) => _uiSystem.RemoveOnSmithyUpTechListener(act);
        public void AddOnVillageUpgradeListener(System.Action<int> act) => _uiSystem.AddOnVillageUpgradeListener(act);
        public void RemoveOnVillageUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnVillageUpgradeListener(act);
        public void AddOnVillageUpTechListener(System.Action<int> act) => _uiSystem.AddOnVillageUpTechListener(act);
        public void RemoveOnVillageUpTechListener(System.Action<int> act) => _uiSystem.RemoveOnVillageUpTechListener(act);
        public void AddOnMineUpgradeListener(System.Action<int> act) => _uiSystem.AddOnMineUpgradeListener(act);
        public void RemoveOnMineUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnMineUpgradeListener(act);

        public void AddOnUpWorkshopTechListener(System.Action<int, UnitTechData> act) => _uiSystem.AddUpTechListener(act);
        public void RemoveUpTechListener(System.Action<int, UnitTechData> act) => _uiSystem.RemoveUpTechListener(act);
        public void AddOnWorkshopExpendListener(System.Action act) => _uiSystem.AddExpendListener(act);
        public void RemoveExpendListener(System.Action act) => _uiSystem.RemoveExpendListener(act);


        private System.Action<QuestData.TYPE_QUEST_GROUP, string> _rewardQuestEvent;

        public void AddOnRewardQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP, string> act)
        {
            _rewardQuestEvent += act;
            _uiQuest.AddOnRewardListener(OnRewardQuestEvent);
            _uiQuestTab.AddOnRewardListener(OnRewardQuestEvent);
        }
        public void RemoveOnRewardQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP, string> act)
        {
            _rewardQuestEvent -= act;
            _uiQuest.RemoveOnRewardListener(OnRewardQuestEvent);
            _uiQuestTab.RemoveOnRewardListener(OnRewardQuestEvent);
        }

        private void OnRewardQuestEvent(QuestData.TYPE_QUEST_GROUP typeQuestGroup, string questKey, string addressKey, bool hasGoal)
        {
            //Debug.Log(questKey + " " + addressKey + " " + hasGoal);
            if (hasGoal)
            {
                _rewardQuestEvent?.Invoke(typeQuestGroup, questKey);
            }
            else
            {
                _uiQuest.Hide();
                _addressDictionary.ShowAddress(addressKey);
            }
        }


        public void AddOnRefreshQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP> act)
        {
            _uiQuest.AddOnRefreshListener(act);
        }
        public void RemoveOnRefreshQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP> act)
        {
            _uiQuest.RemoveOnRefreshListener(act);
        }
        #endregion

    }
}