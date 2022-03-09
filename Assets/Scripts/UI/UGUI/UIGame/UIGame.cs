namespace SEF.UI
{
    using Data;
    using Entity;
    using SEF.Manager;
    using Unit;
    using UnityEngine;
    using UnityEngine.UI;
    using Utility.Address;

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

            _uiAsset = GetComponentInChildren<UIAsset>(true);
            if (_uiAsset == null) _uiAsset = UIAsset.Create();
            _uiAsset.gameObject.SetActive(true);

            _uiSystem = GetComponentInChildren<UISystem>(true);
            if (_uiSystem == null) _uiSystem = UISystem.Create();
            _uiSystem.gameObject.SetActive(true);

            _uiPlay = GetComponentInChildren<UIPlay>(true);
            if (_uiPlay == null) _uiPlay = UIPlay.Create();
            _uiPlay.gameObject.SetActive(true);

            _uiQuest = GetComponentInChildren<UIQuest>(true);
            if (_uiQuest == null) _uiQuest = UIQuest.Create();
            _uiQuest.Hide();

            _uiQuestTab = GetComponentInChildren<UIQuestTab>(true);
            if (_uiQuestTab == null) _uiQuestTab = UIQuestTab.Create();
            _uiQuestTab.gameObject.SetActive(true);

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
        public void RefreshExpandWorkshop(IAssetData assetData, bool isActive) => _uiSystem.RefreshExpandWorkshop(assetData, isActive);
        public void RefreshExpandMine(IAssetData assetData, bool isActive) => _uiSystem.RefreshExpandMine(assetData, isActive);
        public void RefreshSmithy(int index, SmithyEntity entity) => _uiSystem.RefreshSmithy(index, entity);
        public void RefreshVillage(int index, VillageEntity entity) => _uiSystem.RefreshVillage(index, entity);
        public void RefreshMine(int index, MineEntity entity) => _uiSystem.RefreshMine(index, entity);

        public void RefreshAssetEntity(AssetPackage assetEntity) => _uiSystem.RefreshAssetEntity(assetEntity);
        public void RefreshAssetData(IAssetData assetData) => _uiAsset.RefreshAssetData(assetData);
        public void ShowHit(PlayActor playActor, DamageData attackData) => _uiPlay.ShowHit(playActor, attackData);


        public void RefreshQuest(QuestEntity entity)
        {
            _uiQuest.RefreshQuest(entity);
            if (entity.TypeQuestGroup == QuestData.TYPE_QUEST_GROUP.Goal)
                _uiQuestTab.RefreshQuest(entity);
        }

        #region ##### Listener #####

        public void AddOnUpgradeWorkshopListener(System.Action<int> act) => _uiSystem.AddOnUpgradeWorkshopListener(act);
        public void RemoveOnUpgradeWorkshopListener(System.Action<int> act) => _uiSystem.RemoveOnUpgradeWorkshopListener(act);
        public void AddOnUpTechWorkshopListener(System.Action<int, UnitTechData> act) => _uiSystem.AddOnUpTechWorkshopListener(act);
        public void RemoveOnUpTechWorkshopListener(System.Action<int, UnitTechData> act) => _uiSystem.RemoveOnUpTechWorkshopListener(act);
        public void AddOnExpandWorkshopListener(System.Action act) => _uiSystem.AddOnExpandWorkshopListener(act);
        public void RemoveOnExpandWorkshopListener(System.Action act) => _uiSystem.RemoveOnExpandWorkshopListener(act);


        public void AddOnUpgradeSmithyListener(System.Action<int> act) => _uiSystem.AddOnUpgradeSmithyListener(act);
        public void RemoveOnUpgradeSmithyListener(System.Action<int> act) => _uiSystem.RemoveOnUpgradeSmithyListener(act);
        public void AddOnUpTechSmithyListener(System.Action<int> act) => _uiSystem.AddOnUpTechSmithyListener(act);
        public void RemoveOnUpTechSmithyListener(System.Action<int> act) => _uiSystem.RemoveOnUpTechSmithyListener(act);


        public void AddOnUpgradeVillageListener(System.Action<int> act) => _uiSystem.AddOnUpgradeVillageListener(act);
        public void RemoveOnUpgradeVillageListener(System.Action<int> act) => _uiSystem.RemoveOnUpgradeVillageListener(act);
        public void AddOnUpTechVillageListener(System.Action<int> act) => _uiSystem.AddOnUpTechVillageListener(act);
        public void RemoveOnUpTechVillageListener(System.Action<int> act) => _uiSystem.RemoveOnUpTechVillageListener(act);


        public void AddOnUpgradeMineListener(System.Action<int> act) => _uiSystem.AddOnUpgradeMineListener(act);
        public void RemoveOnUpgradeMineListener(System.Action<int> act) => _uiSystem.RemoveOnUpgradeMineListener(act);
        public void AddOnUpTechMineListener(System.Action<int> act) => _uiSystem.AddOnUpTechMineListener(act);
        public void RemoveOnUpTechMineListener(System.Action<int> act) => _uiSystem.RemoveOnUpTechMineListener(act);
        public void AddOnExpandMineListener(System.Action act) => _uiSystem.AddOnExpandMineListener(act);
        public void RemoveOnExpandMineListener(System.Action act) => _uiSystem.RemoveOnExpandMineListener(act);



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
        public void AddOnRefreshQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP> act) => _uiQuest.AddOnRefreshListener(act);
        public void RemoveOnRefreshQuestListener(System.Action<QuestData.TYPE_QUEST_GROUP> act) => _uiQuest.RemoveOnRefreshListener(act);
        #endregion

    }
}