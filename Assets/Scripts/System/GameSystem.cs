namespace SEF.Manager
{
    using Data;
    using Entity;
    using Process;
    using Quest;
    using Statistics;
    using Status;
    using System.Collections.Generic;
    using Unit;
    using Utility.IO;



    #region ##### StorableData #####
    [System.Serializable]
    public class SystemStorableData : StorableData
    {
        [UnityEngine.SerializeField] private Dictionary<string, int> _dic;

        public Dictionary<string, int> Dictionary => _dic;

        public void SaveData(StorableData[] children)
        {
            Children = children;
            _dic = new Dictionary<string, int>();
            for(int i = 0; i < children.Length; i++)
            {
                _dic.Add(children[i].GetType().Name, i);
            }
        }
    }

    #endregion



    public class GameSystem
    {
        //SaveLoad
        private WorkshopManager _workshopManager;
        private SmithyManager _smithyManager;
        private VillageManager _villageManager;
        //ResearchManager


        //SaveLoad
        private AssetPackage _assetPackage;

        private StatisticsPackage _statistics;
        private QuestManager _questManager;

        //None SaveLoad
        private ProcessPackage _process;


        public static GameSystem Create()
        {
            return new GameSystem();
        }

        public void Initialize() 
        {
            StatusPackage.Current.Initialize();

            _assetPackage = AssetPackage.Create();
            _assetPackage.AddRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetPackage.Initialize();

            _workshopManager = WorkshopManager.Create();

            _workshopManager.SetOnConditionProductUnitListener(IsConditionProductUnitEvent);
            _workshopManager.Initialize();

            _smithyManager = SmithyManager.Create();
            _smithyManager.Initialize();

            _villageManager = VillageManager.Create();
            _villageManager.Initialize();
            _villageManager.SetOnProcessEntityListener(OnSetProcessEntityEvent);

            //ResearchManager

            _statistics = StatisticsPackage.Create();
            _statistics.Initialize();

            _process = ProcessPackage.Create();
            _process.Initialize();
            _process.AddOnCompleteProcessEvent(OnCompleteProcessEvent);

            _questManager = QuestManager.Create();
            _questManager.Initialize();            
        }

        public void CleanUp()
        {
            _questManager.CleanUp();

            StatusPackage.Current.Dispose();

            _statistics.CleanUp();

            _process.RemoveOnCompleteProcessEvent(OnCompleteProcessEvent);
            _process.CleanUp();

            _workshopManager.CleanUp();
            _smithyManager.CleanUp();
            _villageManager.CleanUp();
            //ResearchManager

            _assetPackage.RemoveRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetPackage.CleanUp();
        }

        public void RunProcess(float deltaTime)
        {
            _workshopManager.RunProcess(deltaTime);
            _process.RunProcess(deltaTime);
        }

        public void Refresh()
        {
            _workshopManager.Refresh();
            _smithyManager.Refresh();
            _villageManager.Refresh();

            _questManager.RefreshAllQuests();

            RefreshAssetEntity();
        }


        public void SetStorableData(StorableData storableData)
        {
            var data = (SystemStorableData)storableData;

            if (data.Dictionary != null)
            {
                if (data.Dictionary.ContainsKey(typeof(WorkshopManagerStorableData).Name))
                {
                    var key = typeof(WorkshopManagerStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _workshopManager.SetStorableData(child);
                }

                if (data.Dictionary.ContainsKey(typeof(SmithyManagerStorableData).Name))
                {
                    var key = typeof(SmithyManagerStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _smithyManager.SetStorableData(child);
                }

                if (data.Dictionary.ContainsKey(typeof(VillageManagerStorableData).Name))
                {
                    var key = typeof(VillageManagerStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _villageManager.SetStorableData(child);
                }

                if (data.Dictionary.ContainsKey(typeof(StatisticsPackageStorableData).Name))
                {
                    var key = typeof(StatisticsPackageStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _statistics.SetStorableData(child);
                }

                if (data.Dictionary.ContainsKey(typeof(AssetPackageStorableData).Name))
                {
                    var key = typeof(AssetPackageStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _assetPackage.SetStorableData(child);
                }

                if (data.Dictionary.ContainsKey(typeof(QuestManagerStorableData).Name))
                {
                    var key = typeof(QuestManagerStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _questManager.SetStorableData(child);
                }

            }
        }
      
        public StorableData GetStorableData()
        {
            var _storableData = new SystemStorableData();
            List<StorableData> list = new List<StorableData>();

            list.Add(_workshopManager.GetStorableData());
            list.Add(_smithyManager.GetStorableData());
            list.Add(_villageManager.GetStorableData());
            list.Add(_statistics.GetStorableData());
            list.Add(_assetPackage.GetStorableData());
            list.Add(_questManager.GetStorableData());
            _storableData.SaveData(list.ToArray());
            return _storableData;
        }




        #region ##### AssetEntity #####



        public void AddAsset(IAssetData assetData)
        {
            _assetPackage.Add(assetData);
        }

        public void SubjectAsset(IAssetData assetData)
        {
            _assetPackage.Subject(assetData);
            AddStatisticsData(assetData.UsedStatisticsType(), assetData.AssetValue);
            AddStatisticsData(assetData.AccumulateStatisticsType(), assetData.AssetValue);
        }

        public void SetAsset(IAssetData assetData)
        {
            _assetPackage.Set(assetData);
            SetStatisticsData(assetData.GetStatisticsType(), assetData.AssetValue);
        }

        public bool IsEnoughAsset(IAssetData assetData)
        {
            return _assetPackage.IsEnough(assetData);
        }

        public bool IsOverflow(IAssetData assetData)
        {
            return _assetPackage.IsOverflow(assetData);
        }

        public bool IsUnderflow(IAssetData assetData)
        {
            return _assetPackage.IsUnderflow(assetData);
        }

        public void RefreshAssetEntity()
        {
            _assetPackage.RefreshAssets();
        }

        #endregion




        #region ##### Statistics #####
        public void AddStatisticsData<T>(int value = 1) where T : IStatisticsData => _statistics.AddStatisticsData<T>(value);
        public void SetStatisticsData<T>(int value) where T : IStatisticsData => _statistics.SetStatisticsData<T>(value);
        public void AddStatisticsData<T>(System.Numerics.BigInteger value) where T : IStatisticsData => _statistics.AddStatisticsData<T>(value);
        public void SetStatisticsData<T>(System.Numerics.BigInteger value) where T : IStatisticsData => _statistics.SetStatisticsData<T>(value);
        public void AddStatisticsData(System.Type type, int value = 1) => _statistics.AddStatisticsData(type, value);
        public void SetStatisticsData(System.Type type, int value = 1) => _statistics.SetStatisticsData(type, value);
        public void AddStatisticsData(System.Type type, System.Numerics.BigInteger value) => _statistics.AddStatisticsData(type, value);
        public void SetStatisticsData(System.Type type, System.Numerics.BigInteger value) => _statistics.SetStatisticsData(type, value);
        public System.Numerics.BigInteger? GetStatisticsValue<T>() where T : IStatisticsData => _statistics.GetStatisticsValue<T>();
        public System.Numerics.BigInteger? GetStatisticsValue(System.Type type) => _statistics.GetStatisticsValue(type);
        private System.Type FindType(string key, System.Type classType) => StatisticsPackage.FindType(key, classType);
        public void SetOnRefreshStatisticsListener(System.Action<StatisticsEntity> act) => _statistics.SetOnRefreshStatisticsListener(act);

        #endregion




        #region ##### Process #####

        private void OnSetProcessEntityEvent(IProcessProvider provider, ProcessEntity entity) => _process.SetProcessEntity(provider, entity);

        private void OnCompleteProcessEvent(ProcessEntity entity)
        {
            var assetData = entity.GetAssetData();
            if (assetData != null) AddAsset(assetData);
        }

        #endregion




        #region ##### QuestManager #####

        public void GetRewardAssetData(QuestData.TYPE_QUEST_GROUP typeQuestGroup, string key)
        {
            var assetData = _questManager.GetRewardAssetData(typeQuestGroup, key);
            if(assetData != null) AddAsset(assetData);
        }
        public void RefreshQuest(QuestData.TYPE_QUEST_GROUP typeQuestGroup)
        {
            _questManager.RefreshAllQuests(typeQuestGroup);
        }
        public void SetQuestValue(System.Type type, int value) => _questManager.SetQuestValue(type, value);
        public void SetQuestValue<T>(int value) where T : IConditionQuestData => _questManager.SetQuestValue<T>(value);
        public void AddQuestValue(System.Type type, int value = 1) => _questManager.AddQuestValue(type, value);
        public void AddQuestValue<T>(int value = 1) where T : IConditionQuestData => _questManager.AddQuestValue<T>(value);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void AddQuestEntity(QuestData.TYPE_QUEST_GROUP typeQuestGroup, QuestEntity entity)
        {
            _questManager.SetQuestEntity_Test(typeQuestGroup, entity);
        }
#endif

#endregion




        #region ##### Workshop #####
        public void UpgradeWorkshop(int index)
        {
            var assetData = _workshopManager.Upgrade(index, out string key);
            SubjectAsset(assetData);

            AddStatisticsData<UpgradeUnitStatisticsData>();
            var type = FindType(key, typeof(UpgradeUnitStatisticsData));
            if (type != null)
            {
                _statistics.AddStatisticsData(type, 1);
            }

            AddQuestValue<UpgradeUnitConditionQuestData>();
        }


        public void ExpendWorkshop()
        {
            var assetData = _workshopManager.ExpendAssetData;
            int count = _workshopManager.ExpendWorkshop();
            SetStatisticsData<ExpendWorkshopLineStatisticsData>(count);
            SubjectAsset(assetData);
        }
        public void UpTechWorkshop(int index, UnitData unitData)
        {
            _workshopManager.UpTechWorkshop(index, unitData);

            //unitData TechAssetData 소비

            AddStatisticsData<TechUnitStatisticsData>();
            var type = FindType(unitData.Key, typeof(TechUnitStatisticsData));
            if (type != null)
            {
                _statistics.AddStatisticsData(type, 1);
            }
            AddQuestValue<TechUnitConditionQuestData>();
        }

        private bool IsConditionProductUnitEvent(UnitEntity unitEntity)
        {
            var population = new PopulationAssetData(unitEntity.Population);
            return !IsOverflow(population);
        }

        //BlacksmithCondition 적용
        //VillageCondition적용



        public void DestroyedActor(PlayActor playActor)
        {
            switch (playActor)
            {
                case UnitActor unitActor:
                    AddStatisticsData<DestroyUnitStatisticsData>();
                    var unitType = FindType(unitActor.Key, typeof(DestroyUnitStatisticsData));
                    if (unitType != null)
                    {
                        _statistics.AddStatisticsData(unitType, 1);
                    }
                    break;
                case EnemyActor enemyActor:
                    //[System.Obsolete("StatusPackage 적용 보상")]
                    AddAsset(enemyActor.GetRewardAssetData());

                    AddStatisticsData<DestroyEnemyStatisticsData>();
                    var enemyType = FindType(enemyActor.Key, typeof(DestroyEnemyStatisticsData));
                    if (enemyType != null)
                    {
                        _statistics.AddStatisticsData(enemyType, 1);
                    }

                    AddQuestValue<DestroyEnemyConditionQuestData>(1);

                    break;
            }
        }


        #endregion




        #region ##### Smithy #####
        public void UpgradeSmithy(int index)
        {
            var assetData = _smithyManager.Upgrade(index);
            SubjectAsset(assetData);
            AddStatisticsData<UpgradeSmithyStatisticsData>();
            AddQuestValue<UpgradeSmithyConditionQuestData>();
        }
        public void UpTechSmithy(int index)
        {
            _smithyManager.UpTech(index);
            AddStatisticsData<TechSmithyStatisticsData>();
            AddQuestValue<TechSmithyConditionQuestData>();
        }
        #endregion




        #region ##### Villiage #####
        public void UpgradeVillage(int index)
        {
            var assetData = _villageManager.Upgrade(index);
            SubjectAsset(assetData);
            AddStatisticsData<UpgradeVillageStatisticsData>();
            AddQuestValue<UpgradeVillageConditionQuestData>();
        }

        public void UpTechVillage(int index)
        {
            _villageManager.UpTech(index);
            AddStatisticsData<TechVillageStatisticsData>();
            AddQuestValue<TechVillageConditionQuestData>();
        }
        #endregion




        #region ##### Research #####
        public void SuccessedResearchData()
        {
            AddStatisticsData<SuccessResearchStatisticsData>();
            AddQuestValue<SuccessedResearchConditionQuestData>();
        }
        #endregion




        #region ##### LevelWave #####

        private LevelWaveData _nowLevelWaveData;
        public void ArrivedLevelWave(LevelWaveData levelWaveData)
        {
            SetStatisticsData<ArrivedLevelStatisticsData>(levelWaveData.GetLevel());

            var type = FindType($"Lv{levelWaveData.GetLevel()}", typeof(ArrivedLevelStatisticsData));
            if (type != null)
            {
                if (levelWaveData.GetWave() == 0)
                    _statistics.SetStatisticsData(type, 1);
            }

            if (_nowLevelWaveData == null)
            {
                _nowLevelWaveData = levelWaveData;
            }
            else
            {
                var length = levelWaveData.GetLevel() - _nowLevelWaveData.GetLevel();
                if (length > 0)
                    AddQuestValue<ArrivedLevelConditionQuestData>(length);
            }
        }
        #endregion




        #region ##### Listener #####
        public void AddRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        public void AddOnRefreshBlacksmithListener(System.Action<int, SmithyEntity> act) => _smithyManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshBlacksmithListener(System.Action<int, SmithyEntity> act) => _smithyManager.RemoveOnRefreshListener(act);
        public void AddOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.RemoveOnRefreshListener(act);

        public void AddProductUnitListener(System.Action<UnitEntity> act) 
        {
            _workshopManager.AddProductUnitListener(entity =>
            {
                act?.Invoke(entity);

                //유닛 생성
                AddStatisticsData<CreateUnitStatisticsData>(1);

                //특정 유닛 생성
                var type = System.Type.GetType($"SEF.Statistics.{entity.UnitData.Key}CreateUnitStatisticsData");
                if(type != null)
                {
                    _statistics.AddStatisticsData(type, 1);
                }
            });            
        }
        public void RemoveProductUnitListener(System.Action<UnitEntity> act)
        {
            _workshopManager.RemoveProductUnitListener(entity =>
            {
                act?.Invoke(entity);

                //유닛 생성
                AddStatisticsData<CreateUnitStatisticsData>(1);

                //특정 유닛 생성
                var type = System.Type.GetType($"SEF.Statistics.{entity.UnitData.Key}CreateUnitStatisticsData");
                if (type != null)
                {
                    _statistics.AddStatisticsData(type, 1);
                }
            });
        }

        private System.Action<IAssetData, bool> _refreshExpendEvent;
        public void AddRefreshExpendListener(System.Action<IAssetData, bool> act) => _refreshExpendEvent += act;
        public void RemoveRefreshExpendListener(System.Action<IAssetData, bool> act) => _refreshExpendEvent -= act;
        private void OnRefreshExpendEvent(IAssetData assetData)
        {
            var isActive = assetData.AssetValue >= _workshopManager.ExpendAssetData.AssetValue;
            _refreshExpendEvent?.Invoke(_workshopManager.ExpendAssetData, isActive);
        }



        public void AddRefreshAssetEntityListener(System.Action<AssetPackage> act) => _assetPackage.AddRefreshAssetEntityListener(act);
        public void RemoveRefreshAssetEntityListener(System.Action<AssetPackage> act) => _assetPackage.RemoveRefreshAssetEntityListener(act);

        private System.Action<IAssetData> _refreshAsseData;
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData += act;
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData -= act;
        private void OnRefreshAssetDataEvent(IAssetData assetData)
        {
            _refreshAsseData?.Invoke(assetData);
            OnRefreshExpendEvent(assetData);
        }


        public void AddOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.RemoveOnRefreshListener(act);


        #endregion


    }

}