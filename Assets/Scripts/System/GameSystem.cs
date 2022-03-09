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

            //��� ����
            SetStatisticsData(assetData.AccumulativelyGetStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //�Ｎ ����Ʈ
                //BigInteger -> int
                //BigInteger �״�� �����ϵ��� ���� �ʿ�

                var str = assetData.AssetValue.ToString();
                var value = (str.Length >= int.MaxValue.ToString().Length - 1) ?
                    int.Parse(assetData.AssetValue.ToString().Substring(0, int.MaxValue.ToString().Length - 1)) :
                    int.Parse(assetData.AssetValue.ToString());

                AddQuestValue<GetGoldAssetDataConditionQuestData>(value);

                //���� ����Ʈ
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

            //��� ����
            AddStatisticsData(assetData.AccumulativelyUsedStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //�Ｎ ����Ʈ
                AddQuestValue<UsedGoldAssetDataConditionQuestData>((int)assetData.AssetValue);

                //���� ����Ʈ
                SetQuestValue<AccumulativelyUsedGoldAssetDataConditionQuestData>((int)GetStatisticsValue<AccumulativelyGoldUsedAssetStatisticsData>().Value);
            }
        }

        public void SetAsset(IAssetData assetData)
        {
            _assetPackage.Set(assetData);

            //��� ����
            SetStatisticsData(assetData.AccumulativelyGetStatisticsType(), assetData.AssetValue);

            if (assetData is GoldAssetData)
            {
                //�Ｎ ����Ʈ
                AddQuestValue<GetGoldAssetDataConditionQuestData>((int)assetData.AssetValue);

                //���� ����Ʈ
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

                //���� �ְ� ���� ��ǥ ����Ʈ �� ��� ���� 
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

            //��� ����
            AddStatisticsData<UpgradeUnitStatisticsData>();

            //Ư�� ��� ����
            var type = FindType(key, typeof(UpgradeUnitStatisticsData));
            if (type != null)
            {
                _statistics.AddStatisticsData(type, 1);
            }

            //����Ʈ ����
            AddQuestValue<UpgradeUnitConditionQuestData>();
            //���� ����Ʈ ����
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

            //����Ʈ ����
            AddQuestValue<TechUnitConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyTechUnitConditionQuestData>((int)GetStatisticsValue<TechUnitStatisticsData>());

        }

        public void ExpandWorkshop()
        {
            var assetData = _workshopManager.ExpandAssetData;
            int count = _workshopManager.Expand();

            SubjectAsset(assetData);

            //��� ����
            SetStatisticsData<ExpandWorkshopLineStatisticsData>(count);

            //����Ʈ ����
            SetQuestValue<ExpandWorkshopLineConditionQuestData>(count);
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyExpandWorkshopLineConditionQuestData>((int)GetStatisticsValue<ExpandWorkshopLineStatisticsData>());

            _workshopManager.Refresh();
        }


        private bool IsConditionProductUnitEvent(UnitEntity unitEntity)
        {
            var population = new PopulationAssetData(unitEntity.Population);
            return !IsOverflow(population);
        }

        //BlacksmithCondition ����
        //VillageCondition����



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
                    //[System.Obsolete("StatusPackage ���� ����")]
                    AddAsset(enemyActor.GetRewardAssetData());

                    //��� ����
                    AddStatisticsData<DestroyEnemyStatisticsData>();

                    //Ư�� ��� ����
                    var enemyType = FindType(enemyActor.Key, typeof(DestroyEnemyStatisticsData));
                    if (enemyType != null)
                    {
                        _statistics.AddStatisticsData(enemyType, 1);
                    }

                    //����Ʈ ����
                    AddQuestValue<DestroyEnemyConditionQuestData>();
                    //���� ����Ʈ ����
                    SetQuestValue<AccumulativelyDestroyEnemyConditionQuestData>((int)GetStatisticsValue<DestroyEnemyStatisticsData>().Value);


                    var levelWaveData = enemyActor.GetLevelWaveData();

                    //UnityEngine.Debug.Log(levelWaveData.IsBoss());

                    if (levelWaveData.IsBoss())
                    {
                        //����
                        //��� ����
                        AddStatisticsData<DestroyBossStatisticsData>();

                        //����Ʈ ����
                        AddQuestValue<DestroyBossConditionQuestData>();
                        //���� ����Ʈ ����
                        SetQuestValue<AccumulativelyDestroyBossConditionQuestData>((int)GetStatisticsValue<DestroyBossStatisticsData>().Value);
                    }
                    else if (levelWaveData.IsThemeBoss())
                    {
                        //�׸�����
                        //��� ����
                        AddStatisticsData<DestroyThemeBossStatisticsData>();

                        //����Ʈ ����
                        AddQuestValue<DestroyThemeBossConditionQuestData>();
                        //���� ����Ʈ ����
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

            //�������
            AddStatisticsData<UpgradeSmithyStatisticsData>();
            //����Ʈ ����
            AddQuestValue<UpgradeSmithyConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyUpgradeSmithyConditionQuestData>((int)GetStatisticsValue<UpgradeSmithyStatisticsData>());
        }

        public void UpTechSmithy(int index)
        {
            var assetData = _smithyManager.UpTech(index);

            SubjectAsset(assetData);
            //�������
            AddStatisticsData<TechSmithyStatisticsData>();
            //����Ʈ ����
            AddQuestValue<TechSmithyConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyTechSmithyConditionQuestData>((int)GetStatisticsValue<TechSmithyStatisticsData>());
        }
#endregion



#region ##### Villiage #####
        public void UpgradeVillage(int index)
        {
            var assetData = _villageManager.Upgrade(index);
            SubjectAsset(assetData);
            //�������
            AddStatisticsData<UpgradeVillageStatisticsData>();
            //����Ʈ ����
            AddQuestValue<UpgradeVillageConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyUpgradeVillageConditionQuestData>((int)GetStatisticsValue<UpgradeVillageStatisticsData>());
        }

        public void UpTechVillage(int index)
        {
            _villageManager.UpTech(index);
            //�������
            AddStatisticsData<TechVillageStatisticsData>();
            //����Ʈ ����
            AddQuestValue<TechVillageConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyTechVillageConditionQuestData>((int)GetStatisticsValue<TechVillageStatisticsData>());
        }
        #endregion

        #region ##### Mine #####
        public void UpgradeMine(int index)
        {
            var assetData = _mineManager.Upgrade(index);
            SubjectAsset(assetData);
            //�������
            AddStatisticsData<UpgradeMineStatisticsData>();
            //����Ʈ ����
            AddQuestValue<UpgradeMineConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyUpgradeMineConditionQuestData>((int)GetStatisticsValue<UpgradeMineStatisticsData>());
        }

        public void UpTechMine(int index)
        {
            var assetData = _mineManager.UpTech(index);

            SubjectAsset(assetData);
            //�������
            AddStatisticsData<TechMineStatisticsData>();
            //����Ʈ ����
            AddQuestValue<TechMineConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyTechMineConditionQuestData>((int)GetStatisticsValue<TechMineStatisticsData>());
        }

        public void ExpandMine()
        {
            var assetData = _mineManager.ExpandAssetData;
            int count = _mineManager.Expand();

            SubjectAsset(assetData);

            //��� ����
            SetStatisticsData<ExpandMineStatisticsData>(count);

            //����Ʈ ����
            SetQuestValue<ExpandMineConditionQuestData>(count);
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelyExpandMineConditionQuestData>((int)GetStatisticsValue<ExpandMineStatisticsData>());

            _mineManager.Refresh();
        }
        #endregion



        #region ##### Research #####
        public void SuccessResearchData()
        {
            //�������
            AddStatisticsData<SuccessResearchStatisticsData>();
            //����Ʈ ����
            AddQuestValue<SuccessResearchConditionQuestData>();
            //���� ����Ʈ ����
            SetQuestValue<AccumulativelySuccessResearchConditionQuestData>((int)GetStatisticsValue<SuccessResearchStatisticsData>());
        }
#endregion




#region ##### LevelWave #####

        private LevelWaveData _nowLevelWaveData;
        public void ArrivedLevelWave(LevelWaveData levelWaveData)
        {
            //���̺�
            //���̺� ���
            AddStatisticsData<ArrivedWaveStatisticsData>();


            //Ư�� ���̺� ����
            //

            //����Ʈ �޼�
            AddQuestValue<ArrivedWaveConditionQuestData>();

            //���� ����Ʈ �޼�
            SetQuestValue<AccumulativelyArrivedWaveConditionQuestData>((int)GetStatisticsValue<ArrivedWaveStatisticsData>().Value);


            ////////////////////////////////////////////////////////////////////


            //����

            //���� ����
            if (_nowLevelWaveData == null)
            {
                _nowLevelWaveData = (LevelWaveData)levelWaveData.Clone();
            }
            else
            {
                var length = levelWaveData.GetLevel() - _nowLevelWaveData.GetLevel();

                if (length > 0)
                {

                    //���� ���� ���
                    AddStatisticsData<ArrivedLevelStatisticsData>(length);

                    //�ִ� ���� �޼� ���
                    var maxLevel = GetStatisticsValue<MaxArrivedLevelStatisticsData>();
                    if (maxLevel != null)
                    {
                        if(levelWaveData.GetLevel() > maxLevel.Value)
                        {
                            SetStatisticsData<MaxArrivedLevelStatisticsData>(levelWaveData.GetLevel());
                        }
                    }

                    //Ư�� ���� ����
                    var type = FindType($"Lv{levelWaveData.GetLevel()}", typeof(ArrivedLevelStatisticsData));
                    if (type != null)
                    {
                        if (levelWaveData.GetWave() == 0)
                            SetStatisticsData(type, 1);
                    }

                    //����Ʈ �޼�
                    AddQuestValue<ArrivedLevelConditionQuestData>(length);
                    //���� ����Ʈ �޼�
                    SetQuestValue<AccumulativelyArrivedLevelConditionQuestData>((int)GetStatisticsValue<ArrivedLevelStatisticsData>().Value);

                    _nowLevelWaveData = (LevelWaveData)levelWaveData.Clone();
                }
            }
        }
#endregion




#region ##### Listener #####
        //IEntityResult
        //UnitEntityResult = UI�� System�� ��ſ� ������
        //index, UnitEntity, float nowTime, bool isUpgrade, bool isTech
        public void AddOnRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveOnRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        //����
        //�ڿ�
        //�Ѱ�

        //��ũ
        //�ڿ�
        //�Ѱ�

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

                //���� ����
                AddStatisticsData<CreateUnitStatisticsData>();

                //Ư�� ���� ����
                var type = System.Type.GetType($"SEF.Statistics.{entity.UnitData.Key}CreateUnitStatisticsData");
                if(type != null)
                {
                    AddStatisticsData(type, 1);
                }

                //����Ʈ �޼�
                AddQuestValue<CreateUnitConditionQuestData>();
                //���� ����Ʈ �޼�
                SetQuestValue<AccumulativelyCreateUnitConditionQuestData>((int)GetStatisticsValue<CreateUnitStatisticsData>().Value);

            });            
        }
        public void RemoveOnProductUnitListener(System.Action<UnitEntity> act)
        {
            _workshopManager.RemoveProductUnitListener(entity =>
            {
                act?.Invoke(entity);

                //���� ����
                AddStatisticsData<CreateUnitStatisticsData>(1);

                //Ư�� ���� ����
                var type = System.Type.GetType($"SEF.Statistics.{entity.UnitData.Key}CreateUnitStatisticsData");
                if (type != null)
                {
                    AddStatisticsData(type, 1);
                }
                //����Ʈ �޼�
                AddQuestValue<CreateUnitConditionQuestData>();
                //���� ����Ʈ �޼�
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