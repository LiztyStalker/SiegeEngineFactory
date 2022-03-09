namespace SEF.Manager
{
#if INCLUDE_UI_TOOLKIT
    using SEF.UI.Toolkit;
#else
    using SEF.UI;
#endif
    using UnityEngine;
    using Account;
    using Unit;
    using SEF.Data;
    using Utility.IO;

#region ##### StorableData #####
    [System.Serializable]
    public class GameManagerStorableData : StorableData
    {
        [SerializeField] private string _version;
        [SerializeField] private System.DateTime _utcSavedTime;
        public string Version => _version;
        public System.DateTime UTCSavedTime => _utcSavedTime;
        public void SetData(string version, System.DateTime utcSaveTime, StorableData unitStorableData, StorableData systemStorableData)
        {
            _version = version;
            _utcSavedTime = utcSaveTime;

            Children = new StorableData[2];
            Children[0] = unitStorableData;
            Children[1] = systemStorableData;
        }
    }
#endregion

    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private bool _isAutoLoad = true;

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
            Application.wantsToQuit += ApplicationQuit;
            Initialize();
            if (_isAutoLoad) LoadDataInMemory();
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
            _gameSystem.AddOnRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.AddOnRefreshSmithyListener(_uiGame.RefreshSmithy);
            _gameSystem.AddOnRefreshVillageListener(_uiGame.RefreshVillage);
            _gameSystem.AddOnRefreshMineListener(_uiGame.RefreshMine);

            _gameSystem.AddOnRefreshAssetPackageListener(_uiGame.RefreshAssetEntity);
            _gameSystem.AddOnRefreshAssetDataListener(_uiGame.RefreshAssetData);
            _gameSystem.AddOnProductUnitListener(_unitManager.ProductUnitActor);
            _gameSystem.AddOnRefreshQuestEntityListener(_uiGame.RefreshQuest);
            _gameSystem.AddOnRefreshExpendListener(_uiGame.RefreshExpand);

            _unitManager.AddOnHitListener(_uiGame.ShowHit);
            _unitManager.AddOnDestoryedListener(OnDestroyedEvent);
            _unitManager.AddOnNextEnemyListener(OnNextEnemyEvent);
            _unitManager.AddOnRefreshPopulationListener(_gameSystem.SetAsset);

            _uiGame.AddOnUpgradeWorkshopListener(_gameSystem.UpgradeWorkshop);
            _uiGame.AddOnUpTechWorkshopListener(_gameSystem.UpTechWorkshop);
            _uiGame.AddOnExpandWorkshopListener(_gameSystem.ExpendWorkshop);

            _uiGame.AddOnUpgradeSmithyListener(_gameSystem.UpgradeSmithy);
            _uiGame.AddOnUpTechSmithyListener(_gameSystem.UpTechSmithy);

            _uiGame.AddOnUpgradeVillageListener(_gameSystem.UpgradeVillage);
            _uiGame.AddOnUpTechVillageListener(_gameSystem.UpTechVillage);

            _uiGame.AddOnUpgradeMineListener(_gameSystem.UpgradeMine);
            _uiGame.AddOnUpTechMineListener(_gameSystem.UpTechMine);
            _uiGame.AddOnExpandMineListener(_gameSystem.ExpendMine);

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
            Application.wantsToQuit -= ApplicationQuit;
        }

        private void CleanUp()
        {
            //Event 해제
            _gameSystem.RemoveOnRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.RemoveOnRefreshSmithyListener(_uiGame.RefreshSmithy);
            _gameSystem.RemoveOnRefreshVillageListener(_uiGame.RefreshVillage);
            _gameSystem.RemoveOnRefreshMineListener(_uiGame.RefreshMine);

            _gameSystem.RemoveOnRefreshAssetPackageListener(_uiGame.RefreshAssetEntity);
            _gameSystem.RemoveOnRefreshAssetDataListener(_uiGame.RefreshAssetData);
            _gameSystem.RemoveOnProductUnitListener(_unitManager.ProductUnitActor);
            _gameSystem.RemoveOnRefreshQuestEntityListener(_uiGame.RefreshQuest);
            _gameSystem.RemoveOnRefreshExpendListener(_uiGame.RefreshExpand);

            _unitManager.RemoveOnHitListener(_uiGame.ShowHit);
            _unitManager.RemoveOnDestoryedListener(OnDestroyedEvent);
            _unitManager.RemoveOnNextEnemyListener(OnNextEnemyEvent);
            _unitManager.RemoveOnRefreshPopulationListener(_gameSystem.SetAsset);

            _uiGame.RemoveOnUpgradeWorkshopListener(_gameSystem.UpgradeWorkshop);
            _uiGame.RemoveOnUpTechWorkshopListener(_gameSystem.UpTechWorkshop);
            _uiGame.RemoveOnExpandWorkshopListener(_gameSystem.ExpendWorkshop);

            _uiGame.RemoveOnUpgradeSmithyListener(_gameSystem.UpgradeSmithy);
            _uiGame.RemoveOnUpTechSmithyListener(_gameSystem.UpTechSmithy);

            _uiGame.RemoveOnUpgradeVillageListener(_gameSystem.UpgradeVillage);
            _uiGame.RemoveOnUpTechVillageListener(_gameSystem.UpTechVillage);

            _uiGame.RemoveOnUpgradeMineListener(_gameSystem.UpgradeMine);
            _uiGame.RemoveOnUpTechMineListener(_gameSystem.UpTechMine);
            _uiGame.RemoveOnExpandMineListener(_gameSystem.ExpendMine);

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

        public void SetOnRefreshStatisticsListener(System.Action<Statistics.StatisticsEntity> act) => _gameSystem.SetOnRefreshStatisticsListener(act);
#endif


        public StorableData GetStorableData()
        {
            var data = new GameManagerStorableData();
            data.SetData(Application.version, System.DateTime.UtcNow, _unitManager.GetStorableData(), _gameSystem.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            Account.Current.SetStorableData(data);
            _unitManager.SetStorableData(data.Children[0]);
            _gameSystem.SetStorableData(data.Children[1]);
        }


        //메모리 저장
        public void SaveDataInMemory()
        {
            var data = GetStorableData();
            Account.Current.SetStorableData(data);
        }

        //저장
        public void SaveData(System.Action endCallback = null)
        {
            SaveDataInMemory();
            Account.Current.SaveData(null, result =>
            {
                Debug.Log("Save " + result);
                endCallback?.Invoke();
            });
        }

        //불러오기 
        public void LoadDataInMemory()
        {
            var data = Account.Current.GetStorableData();           
            _unitManager.SetStorableData(data.Children[0]);
            _gameSystem.SetStorableData(data.Children[1]);

            var mainData = (GameManagerStorableData)data;

            //오프라인 보상 및 퀘스트 초기화
            Debug.Log(mainData.UTCSavedTime);

            var timeSpan = System.DateTime.UtcNow - mainData.UTCSavedTime;

            //최소 보상시간 1분
            if (timeSpan.TotalSeconds >= 60)
            {
                if(timeSpan.TotalSeconds > 86400)
                {
                    //최대 보상시간 24시간
                    timeSpan = new System.TimeSpan(1, 0, 0, 0);
                }

                //오프라인 보상
                var assetPackage = _gameSystem.RewardOffline(timeSpan);
                assetPackage.AddAssetPackage(_unitManager.RewardOffline(timeSpan));

                _uiGame.ShowRewardOffline(assetPackage, 
                    delegate { 
                        _gameSystem.AddAssetPackage(assetPackage); 
                    },
                    delegate {
                        _gameSystem.AddAssetPackage(assetPackage);
                    });
            }

            //퀘스트 초기화
            _gameSystem.RefreshQuest(mainData.UTCSavedTime);

        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        //불러오기
        public void LoadData_Test()
        {
            Account.Current.LoadData(null, result =>
            {
                Debug.Log("Load " + result);

                if (result == TYPE_IO_RESULT.Success)
                {
                    LoadDataInMemory();
                }
            });
        }

#endif


        private bool ApplicationQuit()
        {
            SaveData(() =>
            {
                Application.Quit();
            });
            return false;
        }

    }
}