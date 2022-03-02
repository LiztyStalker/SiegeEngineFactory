namespace SEF.Data
{
    using System;
    using System.Numerics;
    using Utility.IO;

    public class MeteoriteAssetData : NumberData, IAssetData
    {
        public BigInteger AssetValue { get => new BigInteger(Value); set => Value = int.Parse(value.ToString()); }

        public MeteoriteAssetData() { }
        private MeteoriteAssetData(MeteoriteAssetData data)
        {
            Value = data.Value;
        }
        public void SetValue(string value)
        {
            Value = int.Parse(value);
        }

        public override INumberData Clone()
        {
            return new MeteoriteAssetData(this);
        }

        public Type AccumulativelyUsedStatisticsType() => typeof(Statistics.AccumulativelyMeteoriteUsedAssetStatisticsData);
        public Type AccumulativelyGetStatisticsType() => typeof(Statistics.AccumulativelyMeteoriteGetAssetStatisticsData);


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static MeteoriteAssetData Create_Test()
        {
            var data = new MeteoriteAssetData();
            data.Value = 100;
            return data;
        }

        public static MeteoriteAssetData Create_Test(int value)
        {
            var data = new MeteoriteAssetData();
            data.Value = value;
            return data;
        }



#endif


        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new AssetDataStorableData();
            data.SetData(GetType().AssemblyQualifiedName, AssetValue.ToString());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (AssetDataStorableData)data;
            
        }
        #endregion
    }
}