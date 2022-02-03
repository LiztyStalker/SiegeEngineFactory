namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Entity;
    using Account;
    using Unit;
    using Statistics;
    using Research;
    using Quest;


    public class GameSystem
    {

        private Account _account;

        private StatusPackage _status;

        private WorkshopManager _workshopManager;
        private BlacksmithManager _blacksmithManager;
        private VillageManager _villageManager;
        //ResearchManager


        private StatisticsPackage _statistics;

        private QuestManager _questManager;


        public static GameSystem Create()
        {
            return new GameSystem();
        }

        public void Initialize(IAccountData data = null) 
        {
            _account = Account.Current;

            _workshopManager = WorkshopManager.Create();

            _workshopManager.SetOnConditionProductUnitListener(IsConditionProductUnitEvent);
            _workshopManager.SetOnStatusPackageListener(GetStatusPackage);
            _workshopManager.Initialize(null);

            _blacksmithManager = BlacksmithManager.Create();
            _blacksmithManager.SetOnStatusPackageListener(GetStatusPackage);
            _blacksmithManager.Initialize(null);

            _villageManager = VillageManager.Create();
            _villageManager.SetOnStatusPackageListener(GetStatusPackage);
            _villageManager.Initialize(null);

            //ResearchManager

            _statistics = StatisticsPackage.Create();
            _statistics.Initialize(null);

            _questManager = QuestManager.Create();
            _questManager.Initialize(null);

            _status = StatusPackage.Create();
            _status.Initialize();
            _status.AddOnProductListener(OnStatusProductEvent);


        }

        public void CleanUp()
        {
            _questManager.CleanUp();

            _statistics.CleanUp();

            _status.RemoveOnProductListener(OnStatusProductEvent);
            _status.CleanUp();

            _workshopManager.CleanUp();
            _blacksmithManager.CleanUp();
            _villageManager.CleanUp();
            //ResearchManager
        }

        public void RunProcess(float deltaTime)
        {
            _workshopManager.RunProcess(deltaTime);
            _blacksmithManager.RunProcess(deltaTime);
            //VillageManager

            _status.RunProcess(deltaTime);
        }

        public void Refresh()
        {
            _workshopManager.Refresh();
            _blacksmithManager.Refresh();
            _villageManager.Refresh();

            _questManager.RefreshAllQuests();

            //UI는 나중에 갱신 필요 - Data -> UI
            _account.RefreshAssetEntity();
        }



        #region ##### StatusEntity #####

        //        public U GetStatusDataToBigNumberData<T, U>(U data) where T : IStatusData where U : BigNumberData => _statusEntity.GetStatusDataToBigNumberData<T, U>(data);

        public StatusPackage GetStatusPackage() => _status;

        private void OnStatusProductEvent(IAssetData[] assetDataArr)
        {
            for(int i = 0; i < assetDataArr.Length; i++)
            {
                AddAsset(assetDataArr[i]);
            }
        }

        #endregion



        #region ##### AssetEntity #####

        public void AddAsset(IAssetData assetData)
        {
            _account.AddAsset(assetData);
        }
        public void SubjectAsset(IAssetData assetData)
        {
            _account.SubjectAsset(assetData);
            AddStatisticsData(assetData.UsedStatisticsType(), assetData.AssetValue);
            AddStatisticsData(assetData.AccumulateStatisticsType(), assetData.AssetValue);
        }
        public void SetAsset(IAssetData assetData)
        {
            _account.SetAsset(assetData);
            SetStatisticsData(assetData.GetStatisticsType(), assetData.AssetValue);
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

        #endregion



        #region ##### QuestManager #####

        public void GetRewardAssetData(QuestData.TYPE_QUEST_GROUP typeQuestGroup, string key)
        {
            var assetData = _questManager.GetRewardAssetData(typeQuestGroup, key);
            _account.AddAsset(assetData);
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



        public IAssetData GetAssetData()
        {
            return null;
        }


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
                    _account.AddAsset(enemyActor.GetRewardAssetData());

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



        #region ##### Workshop #####
        public void UpgradeWorkshop(int index)
        {
            var assetData = _workshopManager.Upgrade(index, out string key);
            _account.SubjectAsset(assetData);

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
            int count = _workshopManager.ExpendWorkshop();
            SetStatisticsData<ExpendWorkshopLineStatisticsData>(count);
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
            return !_account.IsOverflow(population);
        }

        //BlacksmithCondition 적용
        //VillageCondition적용


        #endregion


        public void UpgradeBlacksmith(int index)
        {
            var assetData = _blacksmithManager.Upgrade(index);
            _account.SubjectAsset(assetData);
            AddStatisticsData<UpgradeBlacksmithStatisticsData>();
            AddQuestValue<UpgradeBlacksmithConditionQuestData>();
        }
        public void UpTechBlacksmith(int index)
        {
            _blacksmithManager.UpTech(index);
            AddStatisticsData<TechBlacksmithStatisticsData>();
            AddQuestValue<TechBlacksmithConditionQuestData>();
        }
        public void UpgradeVillage(int index)
        {
            var assetData = _villageManager.Upgrade(index);
            _account.SubjectAsset(assetData);
            AddStatisticsData<UpgradeVillageStatisticsData>();
            AddQuestValue<UpgradeVillageConditionQuestData>();
        }

        public void UpTechVillage(int index)
        {
            _villageManager.UpTech(index);
            AddStatisticsData<TechVillageStatisticsData>();
            AddQuestValue<TechVillageConditionQuestData>();
        }

        public void SuccessedResearchData()
        {
            AddStatisticsData<SuccessResearchStatisticsData>();
            AddQuestValue<SuccessedResearchConditionQuestData>();
        }


        #region ##### Listener #####
        public void AddRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        public void AddOnRefreshBlacksmithListener(System.Action<int, BlacksmithEntity> act) => _blacksmithManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshBlacksmithListener(System.Action<int, BlacksmithEntity> act) => _blacksmithManager.RemoveOnRefreshListener(act);
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
        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.AddRefreshAssetEntityListener(act);
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.RemoveRefreshAssetEntityListener(act);
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _account.AddRefreshAssetDataListener(act);
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _account.RemoveRefreshAssetDataListener(act);



        public void AddOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshQuestEntityListener(System.Action<QuestEntity> act) => _questManager.RemoveOnRefreshListener(act);


        #endregion




        #region ##### Data #####

        public void Save() { }

        #endregion




        #region ##### LevelWave #####

        public void ArrivedLevelWave(LevelWaveData levelWaveData)
        {
            SetStatisticsData<ArrivedLevelStatisticsData>(levelWaveData.GetLevel());

            var type = FindType($"Lv{levelWaveData.GetLevel()}", typeof(ArrivedLevelStatisticsData));
            if (type != null)
            {
                if(levelWaveData.GetWave() == 0)
                    _statistics.SetStatisticsData(type, 1);
            }

            AddQuestValue<ArrivedLevelConditionQuestData>(1);
        }
        #endregion
    }

}