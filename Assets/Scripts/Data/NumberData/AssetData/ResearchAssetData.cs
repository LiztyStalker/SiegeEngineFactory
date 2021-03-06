namespace SEF.Data
{
    using System;
    using System.Numerics;
    using Utility.IO;

    public class ResearchAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue
        {
            get => Value.Value;
            set
            {
                var val = Value;
                val.Value = value;
                Value = val;
            }
        }

        public Type AccumulativelyUsedStatisticsType() => typeof(Statistics.AccumulativelyResearchUsedAssetStatisticsData);

        public Type AccumulativelyGetStatisticsType() => typeof(Statistics.AccumulativelyResearchGetAssetStatisticsData);

        public ResearchAssetData() { }
        private ResearchAssetData(ResearchAssetData researchAssetData) : base(researchAssetData) {  }
        public override INumberData Clone()
        {
            return new ResearchAssetData(this);
        }


        public StorableData GetStorableData()
        {
            var data = new AssetDataStorableData();
            data.SetData(GetType().AssemblyQualifiedName, AssetValue.ToString());
            return data;
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static ResearchAssetData Create_Test()
        {
            var data = new ResearchAssetData();
            data.ValueText = "100";
            return data;
        }

        public static ResearchAssetData Create_Test(int value)
        {
            var data = new ResearchAssetData();
            data.ValueText = value.ToString();
            return data;
        }
#endif

    }
}