namespace SEF.Data
{
    using System.Numerics;
    using System;
    using Utility.IO;

    public class PopulationAssetData : NumberData, IAssetData
    {
        public BigInteger AssetValue { get => new BigInteger(Value); set => Value = int.Parse(value.ToString()); }

        public PopulationAssetData() 
        {
            Value = 1;
        }
        public PopulationAssetData(int value) { Value = value; }
        private PopulationAssetData(PopulationAssetData data) 
        {
            Value = data.Value;
        }
        public void SetValue(string value)
        {
            Value = int.Parse(value);
        }
        public override INumberData Clone()
        {
            return new PopulationAssetData(this);
        }

        /// <summary>
        /// null 반환
        /// </summary>
        /// <returns></returns>
        public Type AccumulativelyUsedStatisticsType() => null;

        /// <summary>
        /// null 반환
        /// </summary>
        /// <returns></returns>
        public Type AccumulativelyGetStatisticsType() => null;

        public StorableData GetStorableData()
        {
            var data = new AssetDataStorableData();
            data.SetData(GetType().AssemblyQualifiedName, AssetValue.ToString());
            return data;
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static PopulationAssetData Create_Test()
        {
            var data = new PopulationAssetData();
            data.Value = 1;
            return data;
        }

        public static PopulationAssetData Create_Test(int value)
        {
            var data = new PopulationAssetData();
            data.Value = value;
            return data;
        }
#endif
    }
}