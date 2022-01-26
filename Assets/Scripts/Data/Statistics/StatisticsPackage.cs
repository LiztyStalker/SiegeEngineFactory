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
            var index = GetIndex<T>();
            if(index == -1)
            {
                _list.Add(StatisticsEntity.Create<T>());
                index = _list.Count - 1;
            }
            var entity = _list[index];
            entity.AddStatisticsData(value);
            _list[index] = entity;
        }

        public void SetStatisticsData<T>(int value) where T : IStatisticsData
        {
            SetStatisticsData<T>(new BigInteger(value));
        }

        public void SetStatisticsData<T>(BigInteger value) where T : IStatisticsData
        {
            var index = GetIndex<T>();
            if (index == -1)
            {
                _list.Add(StatisticsEntity.Create<T>());
                index = _list.Count - 1;
            }
            var entity = _list[index];
            entity.SetStatisticsData(value);
            _list[index] = entity;
        }

        private int GetIndex<T>() => _list.FindIndex(entity => entity.GetStatisticsType() == typeof(T));

        public BigInteger? GetStatisticsValue<T>() where T : IStatisticsData
        {
            var index = GetIndex<T>();
            if (index != -1)
            {
                return _list[index].GetStatisticsValue();
            }
            return null;
        }

        public IAccountData GetSaveData()
        {
            return null;
        }

    }
}