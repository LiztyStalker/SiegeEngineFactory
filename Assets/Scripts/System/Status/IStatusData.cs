namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using System.Linq;

    public interface IStatusData
    {
        public enum TYPE_STATUS_DATA { Value, Rate, Absolute}

        public TYPE_STATUS_DATA TypeStatusData { get; }

        public UniversalBigNumberData GetValue(UpgradeData upgradeData);

        public void SetValue(float startValue, float increaseValue, TYPE_STATUS_DATA typeStatusData);

    }

    public class StatusData
    {
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;
        private UniversalBigNumberData _startValue;
        private UniversalBigNumberData _increaseValue;

        public IStatusData.TYPE_STATUS_DATA TypeStatusData { get => _typeStatusData; protected set => _typeStatusData = value; } 
        protected UniversalBigNumberData StartValue { get => _startValue; set => _startValue = value; }
        protected UniversalBigNumberData IncreaseValue { get => _increaseValue; set => _increaseValue = value; }

        public virtual UniversalBigNumberData GetValue(UpgradeData upgradeData) => new UniversalBigNumberData(_startValue.Value + _increaseValue.Value * (upgradeData.Value - 1));
        public virtual void SetValue(float startValue, float increaseValue, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _typeStatusData = typeStatusData;
            _startValue = new UniversalBigNumberData(startValue);
            _increaseValue = new UniversalBigNumberData(increaseValue);
        }

    }




#if UNITY_EDITOR


    public class StatusDataUtility
    {
        private Dictionary<System.Type, string> _dic;

        private static StatusDataUtility _current;

        public static StatusDataUtility Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new StatusDataUtility();
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

        private StatusDataUtility()
        {
            _dic = new Dictionary<System.Type, string>();


            _dic.Add(typeof(UnitDamageValueStatusData), "공격력");
            _dic.Add(typeof(UnitDamageDelayStatusData), "공격딜레이");
            _dic.Add(typeof(AttackerDamageValueStatusData), "포탑공격력");
            _dic.Add(typeof(AttackerDamageDelayStatusData), "포탑공격딜레이");
            _dic.Add(typeof(UnitProductTimeStatusData), "생산시간");
            _dic.Add(typeof(UnitHealthValueStatusData), "체력");


            _dic.Add(typeof(IncreaseMaxPopulationStatusData), "생산인구");
            _dic.Add(typeof(IncreaseMaxUpgradeMineStatusData), "광산 업글 최대치");
            _dic.Add(typeof(IncreaseMaxUpgradeSmithyStatusData), "대장간 업글 최대치");
            _dic.Add(typeof(IncreaseMaxUpgradeUnitStatusData), "유닛 업글 최대치");
            _dic.Add(typeof(IncreaseMaxUpgradeVillageStatusData), "마을 업글 최대치");

        }


        public static IStatusData Create<T>(int startValue, int increaseValue, IStatusData.TYPE_STATUS_DATA typeStatusData) where T : IStatusData
        {
            var data = (T)System.Activator.CreateInstance<T>();
            data.SetValue(startValue, increaseValue, typeStatusData);
            return data;
        }

        public static IStatusData Create<T>(float startValue, float increaseValue, IStatusData.TYPE_STATUS_DATA typeStatusData) where T : IStatusData
        {
            var data = (T)System.Activator.CreateInstance<T>();
            data.SetValue(startValue, increaseValue, typeStatusData);
            return data;
        }

    }
#endif
}