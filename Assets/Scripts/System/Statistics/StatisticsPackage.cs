namespace SEF.Statistics
{
    using SEF.Account;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Linq;
    using Utility.IO;

    #region ##### StorableData #####
    [System.Serializable]
    public class StatisticsPackageStorableData : StorableData
    {
        internal void SetData(StorableData[] children)
        {
            Children = children;
        }
    }
    #endregion


    public class StatisticsPackage
    {
        private List<StatisticsEntity> _list;



        public static StatisticsPackage Create()
        {
            return new StatisticsPackage();
        }

        public void Initialize()
        {
            _list = new List<StatisticsEntity>();
        }

        public void CleanUp()
        {
            _list.Clear();
        }

        public void Refresh()
        {
            for(int i = 0; i < _list.Count; i++)
            {
                var entity = _list[i];
                OnRefreshStatisticsEvent(entity);
            }
        }

        public void AddStatisticsData<T>(int value = 1) where T : IStatisticsData
        {
            AddStatisticsData<T>(new BigInteger(value));
        }
        public void AddStatisticsData<T>(BigInteger value) where T : IStatisticsData
        {
            AddStatisticsData(typeof(T), value);
        }
        public void AddStatisticsData(System.Type type, BigInteger value)
        {
            if (type != null)
            {
                var iType = type.GetInterface(typeof(IStatisticsData).Name);
                if (iType != null)
                {
                    var index = GetIndex(type);
                    if (index == -1)
                    {
                        _list.Add(StatisticsEntity.Create(type));
                        index = _list.Count - 1;
                    }
                    var entity = _list[index];
                    entity.AddStatisticsData(value);
                    _list[index] = entity;

                    OnRefreshStatisticsEvent(entity);
                }
            }
        }

        public void SetStatisticsData<T>(int value) where T : IStatisticsData
        {
            SetStatisticsData<T>(new BigInteger(value));
        }

        public void SetStatisticsData<T>(BigInteger value) where T : IStatisticsData
        {
            SetStatisticsData(typeof(T), value);
        }

        public void SetStatisticsData(System.Type type, BigInteger value)
        {
            if (type != null)
            {
                var iType = type.GetInterface(typeof(IStatisticsData).Name);
                if (iType != null)
                {

                    var index = GetIndex(type);
                    if (index == -1)
                    {
                        _list.Add(StatisticsEntity.Create(type));
                        index = _list.Count - 1;
                    }
                    var entity = _list[index];
                    entity.SetStatisticsData(value);
                    _list[index] = entity;

                    OnRefreshStatisticsEvent(entity);
                }
            }
        }

        private void SetStatisticsData(StatisticsEntityStorableData data)
        {
            var entity = new StatisticsEntity();
            entity.SetStorableData(data);
            _list.Add(entity);
        }

        private int GetIndex(System.Type type) => _list.FindIndex(entity => entity.GetStatisticsType() == type);
        public BigInteger? GetStatisticsValue<T>() where T : IStatisticsData
        {
            return GetStatisticsValue(typeof(T));
        }

        public BigInteger? GetStatisticsValue(System.Type type)
        {
            if (type != null)
            {
                var iType = type.GetInterface(typeof(IStatisticsData).Name);
                if (iType != null)
                {
                    var index = GetIndex(type);
                    if (index == -1)
                    {
                        _list.Add(StatisticsEntity.Create(type));
                        index = _list.Count - 1;
                    }
                    return _list[index].GetStatisticsValue();
                }
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarning($"GetStatisticsValue {type} ?? ???????? ??????????");
#endif
            }
#if UNITY_EDITOR            
            UnityEngine.Debug.LogWarning("GetStatisticsValue type?? ???????? ??????????");
#endif
            return null;
        }
        public static System.Type FindType(string key, System.Type classType) => System.Type.GetType($"SEF.Statistics.{key}{classType.Name}");

        #region ##### Listener #####
        private System.Action<StatisticsEntity> _refreshEvent;
        public void SetOnRefreshStatisticsListener(System.Action<StatisticsEntity> act) => _refreshEvent = act;

        private void OnRefreshStatisticsEvent(StatisticsEntity entity)
        {
            _refreshEvent?.Invoke(entity);
        }
        #endregion

        public void GetData(out Utility.IO.StorableData data)
        {
            data = null;
        }

        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new StatisticsPackageStorableData();

            List<StorableData> list = new List<StorableData>();
            for (int i = 0; i < _list.Count; i++)
            {
                list.Add(_list[i].GetStorableData());
            }
            data.SetData(list.ToArray());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (StatisticsPackageStorableData)data;
            for(int i = 0; i < storableData.Children.Length; i++)
            {
                var child = (StatisticsEntityStorableData)storableData.Children[i];
                SetStatisticsData(child);
            }
        }
        #endregion

    }


    #region ##### Utility #####
    public class StatisticsUtility
    {
        private Dictionary<System.Type, string> _dic;

        private static StatisticsUtility _current;

        public static StatisticsUtility Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new StatisticsUtility();
                }
                return _current;
            }
        }

        public string GetTypeToContext(System.Type type)
        {
            if (_dic.ContainsKey(type))
                return _dic[type];
            return null;
        }

        public System.Type[] GetTypes() => _dic.Keys.ToArray();

        public string[] GetValues() => _dic.Values.ToArray();

        public int FindIndex(System.Type type) => _dic.Keys.ToList().FindIndex(t => t == type);

        private StatisticsUtility()
        {
            _dic = new Dictionary<System.Type, string>();

            _dic.Add(typeof(CreateUnitStatisticsData), "???? ???? ??");
            
            _dic.Add(typeof(DestroyUnitStatisticsData), "???? ?????? ??");

            _dic.Add(typeof(AccumulativelyGoldUsedAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyResourceUsedAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyMeteoriteUsedAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyResearchUsedAssetStatisticsData), "???? ???? ????");

            _dic.Add(typeof(AccumulativelyGoldGetAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyResourceGetAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyMeteoriteGetAssetStatisticsData), "???? ???? ????");
            _dic.Add(typeof(AccumulativelyResearchGetAssetStatisticsData), "???? ???? ????");

            _dic.Add(typeof(TechUnitStatisticsData), "???? ???? ???? ??");

            _dic.Add(typeof(UpgradeUnitStatisticsData), "???? ???? ???? ??");

            _dic.Add(typeof(DestroyEnemyStatisticsData), "?? ???? ??");
            _dic.Add(typeof(DestroyBossStatisticsData), "???? ???? ??");
            _dic.Add(typeof(DestroyThemeBossStatisticsData), "???? ???? ???? ??");

            _dic.Add(typeof(ArrivedLevelStatisticsData), "???? ???? ????");
            _dic.Add(typeof(MaxArrivedLevelStatisticsData), "???? ???? ????");

            _dic.Add(typeof(ExpandWorkshopLineStatisticsData), "?????? ???? ???? ??");

            _dic.Add(typeof(UpgradeVillageStatisticsData), "???? ???? ???? ??");
            _dic.Add(typeof(TechVillageStatisticsData), "???? ???? ???? ??");

            _dic.Add(typeof(UpgradeSmithyStatisticsData), "?????? ???? ???? ??");
            _dic.Add(typeof(TechSmithyStatisticsData), "?????? ???? ???? ??");

            _dic.Add(typeof(SuccessResearchStatisticsData), "???? ???? ?? (??????)");
            //_dic.Add(typeof(UpgradeCommanderStatisticsData), "?????? ???? ???? ??");

            _dic.Add(typeof(AchievedDailyStatisticsData), "???? ???? ?????? ???? ??");
            _dic.Add(typeof(AchievedWeeklyStatisticsData), "???? ???? ?????? ???? ??");
            _dic.Add(typeof(AchievedChallengeStatisticsData), "???? ???? ?????? ???? ??");
            _dic.Add(typeof(AchievedGoalStatisticsData), "???? ???? ?????? ???? ??");

            //_dic.Add(typeof(SuccessExpeditionStatisticsData), "???? ???? ???? ?? (??????)");



        }
    }
    #endregion
}