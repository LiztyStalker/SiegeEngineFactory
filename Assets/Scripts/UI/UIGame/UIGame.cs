namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;
    using Data;
    using Unit;
    using Utility.Address;

    [RequireComponent(typeof(UIDocument))]
    public class UIGame : MonoBehaviour
    {
        public static event System.Action<float> ProcessEvent;

        private readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIGameUXML.uxml";


        private AddressDictionary _addressDictionary;

        private VisualElement _root = null;

        private UIAsset _uiAsset;
        private UISystem _uiSystem;
        private UIPlay _uiPlay;
        private UIQuest _uiQuest;
        private UIQuestTab _uiQuestTab;

        private Button _questButton;
        //private UIOfflineReward _uiOfflineReward;

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

            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_UXML);


            _questButton = _root.Q<Button>("quest-button");

            _uiAsset = UIAsset.Create();
            _uiSystem = _root.Q<UISystem>();
            _uiPlay = _root.Q<UIPlay>();
            _uiQuest = _root.Q<UIQuest>();
            _uiQuestTab = _root.Q<UIQuestTab>();
            

            Debug.Assert(_uiAsset != null, "_uiAsset 이 등록되지 않았습니다");
            Debug.Assert(_uiSystem != null, "_uiSystem 이 등록되지 않았습니다");
            Debug.Assert(_uiPlay != null, "_uiPlay 가 등록되지 않았습니다");
            Debug.Assert(_uiQuest != null, "_uiQuest 가 등록되지 않았습니다");
            Debug.Assert(_uiQuestTab != null, "_uiQuestTab 가 등록되지 않았습니다");

            _uiAsset.Initialize(_root.Q<VisualElement>("UIAsset"));
            _uiSystem.Initialize();
            _uiPlay.Initialize(transform);
            _uiQuest.Initialize();
            _uiQuestTab.Initialize();

            _addressDictionary = AddressDictionary.Create();
            _addressDictionary.Initialize();
            _addressDictionary.AddAddresses(_uiSystem.GetAddresses());

            _questButton.RegisterCallback<ClickEvent>(e => _uiQuest.Show());

        }

        public void CleanUp()
        {
            _questButton.UnregisterCallback<ClickEvent>(e => _uiQuest.Show());

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

        //오프라인 보상
        //public void CompensateOffline(AccountData)

        


        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime) => _uiSystem.RefreshUnit(index, unitEntity, nowTime);
        public void RefreshNextEnemyUnit(EnemyActor enemyActor, LevelWaveData levelWaveData) => _uiPlay.RefreshNextEnemyUnit(enemyActor, levelWaveData);

        public void RefreshBlacksmith(int index, SmithyEntity entity) => _uiSystem.RefreshBlacksmith(index, entity);
        public void RefreshVillage(int index, VillageEntity entity) => _uiSystem.RefreshVillage(index, entity);

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
        public void AddOnBlacksmithUpgradeListener(System.Action<int> act) => _uiSystem.AddOnSmithyUpgradeListener(act);
        public void RemoveOnSmithyUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnSmithyUpgradeListener(act);
        public void AddOnBlacksmithUpTechListener(System.Action<int> act) => _uiSystem.AddOnSmithyUpTechListener(act);
        public void RemoveOnSmithyUpTechListener(System.Action<int> act) => _uiSystem.RemoveOnSmithyUpTechListener(act);
        public void AddOnVillageUpgradeListener(System.Action<int> act) => _uiSystem.AddOnVillageUpgradeListener(act);
        public void RemoveOnVillageUpgradeListener(System.Action<int> act) => _uiSystem.RemoveOnVillageUpgradeListener(act);
        public void AddOnVillageUpTechListener(System.Action<int> act) => _uiSystem.AddOnVillageUpTechListener(act);
        public void RemoveOnVillageUpTechListener(System.Action<int> act) => _uiSystem.RemoveOnVillageUpTechListener(act);


        public void AddUpTechListener(System.Action<int, UnitData> act) => _uiSystem.AddUpTechListener(act);
        public void RemoveUpTechListener(System.Action<int, UnitData> act) => _uiSystem.RemoveUpTechListener(act);
        public void AddExpendListener(System.Action act) => _uiSystem.AddExpendListener(act);
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