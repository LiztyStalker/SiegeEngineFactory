namespace SEF.Statistics
{
    using SEF.Account;
    using System.Collections.Generic;
    using System.Numerics;

    public class StatisticsPackage
    {
        private List<StatisticsEntity> _list;

        public static StatisticsPackage Create()
        {
            return new StatisticsPackage();
        }

        public void Initialize(IAccountData data)
        {
            _list = new List<StatisticsEntity>();

            if(data != null)
            {
                //AccountData 적용하기
            }
        }

        public void CleanUp()
        {
            _list.Clear();
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
            }
        }
        private int GetIndex(System.Type type) => _list.FindIndex(entity => entity.GetStatisticsType() == type);
        public BigInteger? GetStatisticsValue<T>() where T : IStatisticsData
        {
            return GetStatisticsValue(typeof(T));
        }

        public BigInteger? GetStatisticsValue(System.Type type)
        {
            var iType = type.GetInterface(typeof(IStatisticsData).Name);
            if (iType != null)
            {
                var index = GetIndex(type);
                if (index != -1)
                {
                    return _list[index].GetStatisticsValue();
                }
            }
            return null;
        }
        public static System.Type FindType(string key, System.Type classType) => System.Type.GetType($"SEF.Statistics.{key}{classType.Name}");

        public IAccountData GetSaveData()
        {
            return null;
        }

    }
}