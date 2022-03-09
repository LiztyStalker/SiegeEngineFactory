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
        private MineManager _mineManager;
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

            _mineManager = MineManager.Create();
            _mineManager.Initialize();
            _mineManager.SetOnProcessEntityListener(OnSetProcessEntityEvent);

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
            _mineManager.CleanUp();
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
            _mineManager.Refresh();
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

                if (data.Dictionary.ContainsKey(typeof(MineManagerStorableData).Name))
                {
                    var key = typeof(MineManagerStorableData).Name;
                    var child = data.Children[data.Dictionary[key]];
                    _mineManager.SetStorableData(child);
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
            list.Add(_mineManager.GetStorableData());
            list.Add(_statistics.GetStorableData());
            list.Add(_assetPackage.GetStorableData());
            list.Add(_questManager.GetStorableData());
            _storableData.SaveData(list.ToArray());
            return _storableData;
        }




        #region ##### Offline #####
        public RewardAssetPackage RewardOffline(System.TimeSpan timeSpan)
        {
            return _mineManager.RewardOffline(timeSpan);
        }
        #endregion


        #region ##### AssetEntity #####



        public void AddAsset(IAssetData assetData)
        {
            _assetPackage.Add(assetData);

            //통계 적용
            SetStatisticsData(assetData.AccumulativelyGetStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //즉석 퀘스트
                //BigInteger -> int
                //BigInteger 그대로 적용하도록 수정 필요

                var str = assetData.AssetValue.ToString();
                var value = (str.Length >= int.MaxValue.ToString().Length - 1) ?
                    int.Parse(assetData.AssetValue.ToString().Substring(0, int.MaxValue.ToString().Length - 1)) :
                    int.Parse(assetData.AssetValue.ToString());

                AddQuestValue<GetGoldAssetDataConditionQuestData>(value);

                //누적 퀘스트
                var accStr = GetStatisticsValue<AccumulativelyGoldGetAssetStatisticsData>().Value.ToString();
                var accValue = (accStr.Length >= int.MaxValue.ToString().Length - 1) ?
                    int.Parse(GetStatisticsValue<AccumulativelyGoldGetAssetStatisticsData>().Value.ToString().Substring(0, int.MaxValue.ToString().Length - 1)) :
                    int.Parse(GetStatisticsValue<AccumulativelyGoldGetAssetStatisticsData>().Value.ToString());

                AddQuestValue<AccumulativelyGetGoldAssetDataConditionQuestData>(accValue);
            }
        }

        public void AddAssetPackage(RewardAssetPackage assetPackage)
        {
            var arr = assetPackage.GetAssetArray();
            for(int i = 0; i < arr.Length; i++)
            {
                AddAsset(arr[i]);
            }
        }

        public void SubjectAsset(IAssetData assetData)
        {
            _assetPackage.Subject(assetData);

            //통계 적용
            AddStatisticsData(assetData.AccumulativelyUsedStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //즉석 퀘스트
                AddQuestValue<UsedGoldAssetDataConditionQuestData>((int)assetData.AssetValue);

                //누적 퀘스트
                SetQuestValue<AccumulativelyUsedGoldAssetDataConditionQuestData>((int)GetStatisticsValue<AccumulativelyGoldUsedAssetStatisticsData>().Value);
            }
        }

        public void SetAsset(IAssetData assetData)
        {
            _assetPackage.Set(assetData);

            //통계 적용
            SetStatisticsData(assetData.AccumulativelyGetStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //즉석 퀘스트
                AddQuestValue<GetGoldAssetDataConditionQuestData>((int)assetData.AssetValue);

                //누적 퀘스트
                SetQuestValue<AccumulativelyGetGoldAssetDataConditionQuestData>((int)GetStatisticsValue<AccumulativelyGoldGetAssetStatisticsData>().Value);
            }
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
            if (assetData != null)
            {
                AddAsset(assetData);

                //일일 주간 도전 목표 퀘스트 및 통계 적용 
                switch (typeQuestGroup)
                {
                    case QuestData.TYPE_QUEST_GROUP.Daily:
                        AddStatisticsData<AchievedDailyStatisticsData>();
                        AddQuestValue<AchievedDailyConditionQuestData>();
                        SetQuestValue<AccumulativelyAchievedDailyConditionQuestData>((int)GetStatisticsValue<AchievedDailyStatisticsData>().Value);
                        break;
                    case QuestData.TYPE_QUEST_GROUP.Weekly:
                        AddStatisticsData<AchievedWeeklyStatisticsData>();
                        AddQuestValue<AchievedWeeklyConditionQuestData>();
                        SetQuestValue<AccumulativelyAchievedWeeklyConditionQuestData>((int)GetStatisticsValue<AchievedWeeklyStatisticsData>().Value);
                        break;
                    case QuestData.TYPE_QUEST_GROUP.Challenge:
                        AddStatisticsData<AchievedChallengeStatisticsData>();
                        AddQuestValue<AchievedChallengeConditionQuestData>();
                        SetQuestValue<AccumulativelyAchievedChallengeConditionQuestData>((int)GetStatisticsValue<AchievedChallengeStatisticsData>().Value);
                        break;
                    case QuestData.TYPE_QUEST_GROUP.Goal:
                        AddStatisticsData<AchievedGoalStatisticsData>();
                        AddQuestValue<AchievedGoalConditionQuestData>();
                        SetQuestValue<AccumulativelyAchievedGoalConditionQuestData>((int)GetStatisticsValue<AchievedGoalStatisticsData>().Value);
                        break;
                }
            }
        }
        public void RefreshQuest(System.DateTime utcSavedTime)
        {
            _questManager.RefreshAllQuests(utcSavedTime);
        }
        public void RefreshQuest(QuestData.TYPE_QUEST_GROUP typeQuestGroup)
        {
            _questManager.RefreshAllQuests(typeQuestGroup);
        }
        public void SetQuestValue(System.Type type, int value) => _questManager.SetQuestValue(type, value);
        public void SetQuestValue<T>(int value) where T : IConditionQuestData => _questManager.SetQuestValue<T>(value);
        public void AddQuestValue(System.Type type, int value = 1) => _questManager.AddQuestValue(type, value);
        public void AddQuestValue<T>(int value = 1) where T : IConditionQuestData => _questManager.AddQuestValue<T>(value);
//        public void AddQuestValue<T>(System.Numerics.BigInteger value) where T : IConditionQuestData => _questManager.AddQuestValue<T>(value);

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

            //통계 적용
            AddStatisticsData<UpgradeUnitStatisticsData>();

            //특정 통계 적용
            var type = FindType(key, typeof(UpgradeUnitStatisticsData));
            if (type != null)
            {
                _statistics.AddStatisticsData(type, 1);
            }

            //퀘스트 적용
            AddQuestValue<UpgradeUnitConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyUpgradeUnitConditionQuestData>((int)GetStatisticsValue<UpgradeUnitStatisticsData>());
        }


       
        public void UpTechWorkshop(int index, UnitTechData data)
        {
            var assetData = _workshopManager.UpTechWorkshop(index, data);

            SubjectAsset(assetData);
            
            AddStatisticsData<TechUnitStatisticsData>();
            var type = FindType(data.TechUnitKey, typeof(TechUnitStatisticsData));
            if (type != null)
            {
                _statistics.AddStatisticsData(type, 1);
            }

            //퀘스트 적용
            AddQuestValue<TechUnitConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyTechUnitConditionQuestData>((int)GetStatisticsValue<TechUnitStatisticsData>());

        }

        public void ExpandWorkshop()
        {
            var assetData = _workshopManager.ExpandAssetData;
            int count = _workshopManager.Expand();

            SubjectAsset(assetData);

            //통계 적용
            SetStatisticsData<ExpandWorkshopLineStatisticsData>(count);

            //퀘스트 적용
            SetQuestValue<ExpandWorkshopLineConditionQuestData>(count);
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyExpandWorkshopLineConditionQuestData>((int)GetStatisticsValue<ExpandWorkshopLineStatisticsData>());

            _workshopManager.Refresh();
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

                    //통계 적용
                    AddStatisticsData<DestroyEnemyStatisticsData>();

                    //특정 통계 적용
                    var enemyType = FindType(enemyActor.Key, typeof(DestroyEnemyStatisticsData));
                    if (enemyType != null)
                    {
                        _statistics.AddStatisticsData(enemyType, 1);
                    }

                    //퀘스트 적용
                    AddQuestValue<DestroyEnemyConditionQuestData>();
                    //누적 퀘스트 적용
                    SetQuestValue<AccumulativelyDestroyEnemyConditionQuestData>((int)GetStatisticsValue<DestroyEnemyStatisticsData>().Value);


                    var levelWaveData = enemyActor.GetLevelWaveData();

                    //UnityEngine.Debug.Log(levelWaveData.IsBoss());

                    if (levelWaveData.IsBoss())
                    {
                        //보스
                        //통계 적용
                        AddStatisticsData<DestroyBossStatisticsData>();

                        //퀘스트 적용
                        AddQuestValue<DestroyBossConditionQuestData>();
                        //누적 퀘스트 적용
                        SetQuestValue<AccumulativelyDestroyBossConditionQuestData>((int)GetStatisticsValue<DestroyBossStatisticsData>().Value);
                    }
                    else if (levelWaveData.IsThemeBoss())
                    {
                        //테마보스
                        //통계 적용
                        AddStatisticsData<DestroyThemeBossStatisticsData>();

                        //퀘스트 적용
                        AddQuestValue<DestroyThemeBossConditionQuestData>();
                        //누적 퀘스트 적용
                        SetQuestValue<AccumulativelyDestroyThemeBossConditionQuestData>((int)GetStatisticsValue<DestroyThemeBossStatisticsData>().Value);
                    }
                    break;
            }
        }


        #endregion




        #region ##### Smithy #####
        public void UpgradeSmithy(int index)
        {
            var assetData = _smithyManager.Upgrade(index);
            
            SubjectAsset(assetData);

            //통계적용
            AddStatisticsData<UpgradeSmithyStatisticsData>();
            //퀘스트 적용
            AddQuestValue<UpgradeSmithyConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyUpgradeSmithyConditionQuestData>((int)GetStatisticsValue<UpgradeSmithyStatisticsData>());
        }

        public void UpTechSmithy(int index)
        {
            var assetData = _smithyManager.UpTech(index);

            SubjectAsset(assetData);
            //통계적용
            AddStatisticsData<TechSmithyStatisticsData>();
            //퀘스트 적용
            AddQuestValue<TechSmithyConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyTechSmithyConditionQuestData>((int)GetStatisticsValue<TechSmithyStatisticsData>());
        }
#endregion



#region ##### Villiage #####
        public void UpgradeVillage(int index)
        {
            var assetData = _villageManager.Upgrade(index);
            SubjectAsset(assetData);
            //통계적용
            AddStatisticsData<UpgradeVillageStatisticsData>();
            //퀘스트 적용
            AddQuestValue<UpgradeVillageConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyUpgradeVillageConditionQuestData>((int)GetStatisticsValue<UpgradeVillageStatisticsData>());
        }

        public void UpTechVillage(int index)
        {
            _villageManager.UpTech(index);
            //통계적용
            AddStatisticsData<TechVillageStatisticsData>();
            //퀘스트 적용
            AddQuestValue<TechVillageConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyTechVillageConditionQuestData>((int)GetStatisticsValue<TechVillageStatisticsData>());
        }
        #endregion

        #region ##### Mine #####
        public void UpgradeMine(int index)
        {
            var assetData = _mineManager.Upgrade(index);
            SubjectAsset(assetData);
            //통계적용
            AddStatisticsData<UpgradeMineStatisticsData>();
            //퀘스트 적용
            AddQuestValue<UpgradeMineConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyUpgradeMineConditionQuestData>((int)GetStatisticsValue<UpgradeMineStatisticsData>());
        }

        public void UpTechMine(int index)
        {
            var assetData = _mineManager.UpTech(index);

            SubjectAsset(assetData);
            //통계적용
            AddStatisticsData<TechMineStatisticsData>();
            //퀘스트 적용
            AddQuestValue<TechMineConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyTechMineConditionQuestData>((int)GetStatisticsValue<TechMineStatisticsData>());
        }

        public void ExpandMine()
        {
            var assetData = _mineManager.ExpandAssetData;
            int count = _mineManager.Expand();

            SubjectAsset(assetData);

            //통계 적용
            SetStatisticsData<ExpandMineStatisticsData>(count);

            //퀘스트 적용
            SetQuestValue<ExpandMineConditionQuestData>(count);
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelyExpandMineConditionQuestData>((int)GetStatisticsValue<ExpandMineStatisticsData>());

            _mineManager.Refresh();
        }
        #endregion



        #region ##### Research #####
        public void SuccessResearchData()
        {
            //통계적용
            AddStatisticsData<SuccessResearchStatisticsData>();
            //퀘스트 적용
            AddQuestValue<SuccessResearchConditionQuestData>();
            //누적 퀘스트 적용
            SetQuestValue<AccumulativelySuccessResearchConditionQuestData>((int)GetStatisticsValue<SuccessResearchStatisticsData>());
        }
#endregion




#region ##### LevelWave #####

        private LevelWaveData _nowLevelWaveData;
        public void ArrivedLevelWave(LevelWaveData levelWaveData)
        {
            //웨이브
            //웨이브 통계
            AddStatisticsData<ArrivedWaveStatisticsData>();


            //특정 웨이브 도달
            //

            //퀘스트 달성
            AddQuestValue<ArrivedWaveConditionQuestData>();

            //누적 퀘스트 달성
            SetQuestValue<AccumulativelyArrivedWaveConditionQuestData>((int)GetStatisticsValue<ArrivedWaveStatisticsData>().Value);


            ////////////////////////////////////////////////////////////////////


            //레벨

            //현재 레벨
            if (_nowLevelWaveData == null)
            {
                _nowLevelWaveData = (LevelWaveData)levelWaveData.Clone();
            }
            else
            {
                var length = levelWaveData.GetLevel() - _nowLevelWaveData.GetLevel();

                if (length > 0)
                {

                    //누적 레벨 통계
                    AddStatisticsData<ArrivedLevelStatisticsData>(length);

                    //최대 레벨 달성 통계
                    var maxLevel = GetStatisticsValue<MaxArrivedLevelStatisticsData>();
                    if (maxLevel != null)
                    {
                        if(levelWaveData.GetLevel() > maxLevel.Value)
                        {
                            SetStatisticsData<MaxArrivedLevelStatisticsData>(levelWaveData.GetLevel());
                        }
                    }

                    //특정 레벨 도달
                    var type = FindType($"Lv{levelWaveData.GetLevel()}", typeof(ArrivedLevelStatisticsData));
                    if (type != null)
                    {
                        if (levelWaveData.GetWave() == 0)
                            SetStatisticsData(type, 1);
                    }

                    //퀘스트 달성
                    AddQuestValue<ArrivedLevelConditionQuestData>(length);
                    //누적 퀘스트 달성
                    SetQuestValue<AccumulativelyArrivedLevelConditionQuestData>((int)GetStatisticsValue<ArrivedLevelStatisticsData>().Value);

                    _nowLevelWaveData = (LevelWaveData)levelWaveData.Clone();
                }
            }
        }
#endregion




#region ##### Listener #####
        //IEntityResult
        //UnitEntityResult = UI와 System간 통신용 데이터
        //index, UnitEntity, float nowTime, bool isUpgrade, bool isTech
        public void AddOnRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveOnRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        //업글
        //자원
        //한계

        //테크
        //자원
        //한계

        public void AddOnRefreshSmithyListener(System.Action<int, SmithyEntity> act) => _smithyManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshSmithyListener(System.Action<int, SmithyEntity> act) => _smithyManager.RemoveOnRefreshListener(act);
        public void AddOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.RemoveOnRefreshListener(act);
        public void AddOnRefreshMineListener(System.Action<int, MineEntity> act) => _mineManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshMineListener(System.Action<int, MineEntity> act) => _mineManager.RemoveOnRefreshListener(act);


        public void AddOnProductUnitListener(System.Action<UnitEntity> act)
        {
            _workshopManager.AddProductUnitListener(entity =>
            {
                act?.Invoke(entity);

                //유닛 생성
                AddStatisticsData<CreateUnitStatisticsData>();

                //특정 유닛 생성
                var type = System.Type.GetType($"SEF.Statistics.{entity.UnitData.Key}CreateUnitStatisticsData");
                if(type != null)
                {
                    AddStatisticsData(type, 1);
                }

                //퀘스트 달성
                AddQuestValue<CreateUnitConditionQuestData>();
                //누적 퀘스트 달성
                SetQuestValue<AccumulativelyCreateUnitConditionQuestData>((int)GetStatisticsValue<CreateUnitStatisticsData>().Value);

            });            
        }
        public void RemoveOnProductUnitListener(System.Action<UnitEntity> act)
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
                    AddStatisticsData(type, 1);
                }
                //퀘스트 달성
                AddQuestValue<CreateUnitConditionQuestData>();
                //누적 퀘스트 달성
                SetQuestValue<AccumulativelyCreateUnitConditionQuestData>((int)GetStatisticsValue<CreateUnitStatisticsData>().Value);
            });
        }



        private System.Action<IAssetData, bool> _refreshExpandWorkshopEvent;
        public void AddOnRefreshExpandWorkshopListener(System.Action<IAssetData, bool> act) => _refreshExpandWorkshopEvent += act;
        public void RemoveOnRefreshExpandWorkshopListener(System.Action<IAssetData, bool> act) => _refreshExpandWorkshopEvent -= act;
        private void OnRefreshExpandWorkshopEvent(IAssetData assetData)
        {
            var isActive = assetData.AssetValue >= _workshopManager.ExpandAssetData.AssetValue;
            _refreshExpandWorkshopEvent?.Invoke(_workshopManager.ExpandAssetData, isActive);
        }


        private System.Action<IAssetData, bool> _refreshExpandMineEvent;
        public void AddOnRefreshExpandMineListener(System.Action<IAssetData, bool> act) => _refreshExpandMineEvent += act;
        public void RemoveOnRefreshExpandMineListener(System.Action<IAssetData, bool> act) => _refreshExpandMineEvent -= act;
        private void OnRefreshExpandMineEvent(IAssetData assetData)
        {
            var isActive = assetData.AssetValue >= _mineManager.ExpandAssetData.AssetValue;
            _refreshExpandMineEvent?.Invoke(_mineManager.ExpandAssetData, isActive);
        }


        public void AddOnRefreshAssetPackageListener(System.Action<AssetPackage> act) => _assetPackage.AddRefreshAssetEntityListener(act);
        public void RemoveOnRefreshAssetPackageListener(System.Action<AssetPackage> act) => _assetPackage.RemoveRefreshAssetEntityListener(act);

        private System.Action<IAssetData> _refreshAsseData;
        public void AddOnRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData += act;
        public void RemoveOnRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData -= act;
        private void OnRefreshAssetDataEvent(IAssetData assetData)
        {
            _refreshAsseData?.Invoke(assetData);

            OnRefreshExpandWorkshopEvent(assetData);
            OnRefreshExpandMineEvent(assetData);
        }


        public void AddOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.RemoveOnRefreshListener(act);


#endregion


    }

}