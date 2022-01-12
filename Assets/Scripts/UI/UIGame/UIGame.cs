namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;
    using Data;
    using Unit;


    [RequireComponent(typeof(UIDocument))]
    public class UIGame : MonoBehaviour
    {
        public static event System.Action<float> ProcessEvent;

        private readonly string PATH_UI_GAME_UXML = "Assets/Scripts/UI/UIGame/UIGameUXML.uxml";


        private VisualElement _root = null;

        private UIAsset _uiAsset;
        private UISystem _uiSystem;
        private UIPlay _uiPlay;
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
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_GAME_UXML);

            _uiAsset = UIAsset.Create();
            _uiSystem = _root.Q<UISystem>();
            _uiPlay = _root.Q<UIPlay>();

            Debug.Assert(_uiAsset != null, "_uiAsset 이 등록되지 않았습니다");
            Debug.Assert(_uiSystem != null, "_uiSystem 이 등록되지 않았습니다");
            Debug.Assert(_uiPlay != null, "_uiPlay 가 등록되지 않았습니다");

            _uiAsset.Initialize(_root.Q<VisualElement>("UIAssetUXML"));
            _uiSystem.Initialize();
            _uiPlay.Initialize(transform);
        }

        public void CleanUp()
        {            
            _uiAsset.CleanUp();
            _uiSystem.CleanUp();
            _uiPlay.CleanUp();
        }

        public void RunProcess(float deltaTime)
        {
            ProcessEvent?.Invoke(deltaTime);
        }

        //오프라인 보상
        //public void CompensateOffline(AccountData)

        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime) => _uiSystem.RefreshUnit(index, unitEntity, nowTime);
        public void RefreshNextEnemyUnit(EnemyActor enemyActor, LevelWaveData levelWaveData) => _uiPlay.RefreshNextEnemyUnit(enemyActor, levelWaveData);
        public void RefreshAssetEntity(AssetEntity assetEntity) => _uiSystem.RefreshAssetEntity(assetEntity);
        public void RefreshAssetData(IAssetData assetData) => _uiAsset.RefreshAssetData(assetData);
        public void ShowHit(PlayActor playActor, AttackData attackData) => _uiPlay.ShowHit(playActor, attackData);



        #region ##### Listener #####
        public void AddUpgradeListener(System.Action<int> act) => _uiSystem.AddUpgradeListener(act);
        public void RemoveUpgradeListener(System.Action<int> act) => _uiSystem.RemoveUpgradeListener(act);
        public void AddUpTechListener(System.Action<int, UnitData> act) => _uiSystem.AddUpTechListener(act);
        public void RemoveUpTechListener(System.Action<int, UnitData> act) => _uiSystem.RemoveUpTechListener(act);
        public void AddExpendListener(System.Action act) => _uiSystem.AddExpendListener(act);
        public void RemoveExpendListener(System.Action act) => _uiSystem.RemoveExpendListener(act);

        #endregion

    }
}