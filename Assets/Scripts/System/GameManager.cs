namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SEF.UI.Toolkit;
    using Account;
    using Unit;
    using SEF.Data;
    using Utility.IO;

    #region ##### StorableData #####
    [System.Serializable]
    public class GameManagerStorableData : StorableData
    {
        public void SetData(StorableData unitStorableData, StorableData systemStorableData)
        {
            Children = new StorableData[2];
            Children[0] = unitStorableData;
            Children[1] = systemStorableData;
        }
    }
    #endregion

    public class GameManager : MonoBehaviour
    {
        private UIGame _uiGame;
        private GameSystem _gameSystem;
        private UnitManager _unitManager;

        public static GameManager Create()
        {
            var obj = new GameObject();
            obj.name = "GameManager";
            return obj.AddComponent<GameManager>();
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            //Initialize
            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _unitManager = UnitManager.Create();
            _unitManager.Initialize();

            _uiGame = UIGame.Create();
            _uiGame.Initialize();


            //Event 연결
            //RefreshEntity 통합 필요?
            _gameSystem.AddRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.AddOnRefreshBlacksmithListener(_uiGame.RefreshBlacksmith);
            _gameSystem.AddOnRefreshVillageListener(_uiGame.RefreshVillage);

            _gameSystem.AddRefreshAssetEntityListener(_uiGame.RefreshAssetEntity);
            _gameSystem.AddRefreshAssetDataListener(_uiGame.RefreshAssetData);
            _gameSystem.AddProductUnitListener(_unitManager.ProductUnitActor);
            _gameSystem.AddOnRefreshQuestEntityListener(_uiGame.RefreshQuest);
            _gameSystem.AddRefreshExpendListener(_uiGame.RefreshExpend);

            _unitManager.AddOnHitListener(_uiGame.ShowHit);
            _unitManager.AddOnDestoryedListener(OnDestroyedEvent);
            _unitManager.AddOnNextEnemyListener(OnNextEnemyEvent);
            _unitManager.AddOnRefreshPopulationListener(_gameSystem.SetAsset);

            _uiGame.AddOnWorkshopUpgradeListener(_gameSystem.UpgradeWorkshop);
            _uiGame.AddOnBlacksmithUpgradeListener(_gameSystem.UpgradeBlacksmith);
            _uiGame.AddOnVillageUpgradeListener(_gameSystem.UpgradeVillage);
            _uiGame.AddUpTechListener(_gameSystem.UpTechWorkshop);
            _uiGame.AddExpendListener(_gameSystem.ExpendWorkshop);

            _uiGame.AddOnRefreshQuestListener(_gameSystem.RefreshQuest);


            _uiGame.AddOnRewardQuestListener(_gameSystem.GetRewardAssetData);
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            //Refresh
            _gameSystem.Refresh();
            _unitManager.Refresh();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            //Event 해제
            _gameSystem.RemoveRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.RemoveOnRefreshBlacksmithListener(_uiGame.RefreshBlacksmith);
            _gameSystem.RemoveOnRefreshVillageListener(_uiGame.RefreshVillage);


            _gameSystem.RemoveRefreshAssetEntityListener(_uiGame.RefreshAssetEntity);
            _gameSystem.RemoveRefreshAssetDataListener(_uiGame.RefreshAssetData);
            _gameSystem.RemoveProductUnitListener(_unitManager.ProductUnitActor);
            _gameSystem.RemoveOnRefreshQuestEntityListener(_uiGame.RefreshQuest);

            _unitManager.RemoveOnHitListener(_uiGame.ShowHit);
            _unitManager.RemoveOnDestoryedListener(OnDestroyedEvent);
            _unitManager.RemoveOnNextEnemyListener(OnNextEnemyEvent);
            _unitManager.RemoveOnRefreshPopulationListener(_gameSystem.SetAsset);

            _uiGame.RemoveOnWorkshopUpgradeListener(_gameSystem.UpgradeWorkshop);
            _uiGame.RemoveOnBlacksmithUpgradeListener(_gameSystem.UpgradeBlacksmith);
            _uiGame.RemoveOnVillageUpgradeListener(_gameSystem.UpgradeVillage);
            _uiGame.RemoveUpTechListener(_gameSystem.UpTechWorkshop);
            _uiGame.RemoveExpendListener(_gameSystem.ExpendWorkshop);

            _uiGame.RemoveOnRewardQuestListener(_gameSystem.GetRewardAssetData);

            //CleanUp
            _gameSystem.CleanUp();
            _unitManager.CleanUp();
            _uiGame.CleanUp();
        }
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            RunProcess(deltaTime);
        }

        private void RunProcess(float deltaTime)
        {
            _gameSystem.RunProcess(deltaTime);
            _unitManager.RunProcess(deltaTime);
            _uiGame.RunProcess(deltaTime);
        }

        private void OnDestroyedEvent(PlayActor playActor) 
        {
            _gameSystem.DestroyedActor(playActor);
        }

        public void OnNextEnemyEvent(EnemyActor enemyActor, LevelWaveData levelWaveData)
        {
            _gameSystem.ArrivedLevelWave(levelWaveData);
            _uiGame.RefreshNextEnemyUnit(enemyActor, levelWaveData);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void AddAssetData_Test(IAssetData data)
        {
            _gameSystem.AddAsset(data);
        }

        public StorableData GetStorableData()
        {
            var data = new GameManagerStorableData();
            data.SetData(_unitManager.GetStorableData(), _gameSystem.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            Account.Current.SetStorableData(data);
            _unitManager.SetStorableData(data.Children[0]);
            _gameSystem.SetStorableData(data.Children[1]);
        }

        public void Initialize_Test()
        {
            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _unitManager = UnitManager.Create();
            _unitManager.Initialize();
        }

        public void CleanUp_Test()
        {
            _gameSystem.CleanUp();
            _unitManager.CleanUp();
        }
#endif

        //저장
        public void SaveData()
        {
            var data = new GameManagerStorableData();
            data.SetData(_unitManager.GetStorableData(), _gameSystem.GetStorableData());
            Account.Current.SetStorableData(data);
            Account.Current.SaveData(null, result =>
            {
                Debug.Log("Save " + result);
            });
        }

        //불러오기
        public void LoadData()
        {

            Account.Current.LoadData(null, result =>
            {
                Debug.Log("Load " + result);

                if (result == TYPE_IO_RESULT.Success)
                {
                    var data = Account.Current.GetStorableData();
                    //Debug.Log("Children " + data.Children);
                    _unitManager.SetStorableData(data.Children[0]);
                    _gameSystem.SetStorableData(data.Children[1]);
                }
            });
        }

    }
}