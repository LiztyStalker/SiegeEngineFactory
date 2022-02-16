namespace SEF.Statistics
{
    using System.Numerics;
    using Utility.IO;



    #region ##### StorableData #####
    [System.Serializable]
    public class StatisticsEntityStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _type;
        [UnityEngine.SerializeField] private string _value;

        public string @Type => _type;
        public string Value => _value;


        internal void SetData(string type, string value)
        {
            _type = type;
            _value = value;
        }
    }
    #endregion




    internal struct StatisticsEntity
    {
        private System.Type _type;
        private BigInteger _value;

        private StatisticsEntity(System.Type type)
        {
            _type = type;
            _value = new BigInteger();
        }

        internal void AddStatisticsData(BigInteger value)
        {
            _value += value;
//#if UNITY_EDITOR
//            UnityEngine.Debug.Log(_type.Name + " " + _value);
//#endif
        }
        internal void SetStatisticsData(BigInteger value)
        {
            _value = value;
//#if UNITY_EDITOR
//            UnityEngine.Debug.Log(_type.Name + " " + _value);
//#endif
        }
        internal BigInteger GetStatisticsValue() => _value;
        internal System.Type GetStatisticsType() => _type;

        internal static StatisticsEntity Create<T>() where T : IStatisticsData
        {
            return new StatisticsEntity(typeof(T));
        }
        
        internal static StatisticsEntity Create(System.Type type)
        {
            return new StatisticsEntity(type);
        }

        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new StatisticsEntityStorableData();
            data.SetData(_type.FullName, _value.ToString());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (StatisticsEntityStorableData)data;
            _type = System.Type.GetType(storableData.Type);
            var bigInt = new BigInteger();
            var str = storableData.Value;
            int index = 0;
            while (true)
            {
                if(index > str.Length)
                {
                    break;
                }
                var sub = ((index + int.MaxValue.ToString().Length - 1) < str.Length) ?
                    (str.Substring(index, int.MaxValue.ToString().Length - 1)) :
                    (str.Substring(index)) ;

                var val = int.Parse(sub);

                bigInt *= BigInteger.Pow(10, sub.Length);
                bigInt += val;
                index += sub.Length;
            }
            _value = bigInt;
        }
        #endregion
    }
}