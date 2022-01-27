namespace SEF.Data
{
    using System;
    using System.Numerics;

    public class ResearchAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { get => Value; set => Value = value; }

        public ResearchAssetData() { }
        private ResearchAssetData(ResearchAssetData researchAssetData) : base(researchAssetData) {  }
        public override INumberData Clone()
        {
            return new ResearchAssetData(this);
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

        public Type UsedStatisticsType() => typeof(Statistics.ResearchUsedAssetStatisticsData);

        public Type GetStatisticsType() => typeof(Statistics.ResearchGetAssetStatisticsData);

        public Type AccumulateStatisticsType() => typeof(Statistics.ResearchAccumulateAssetStatisticsData);
#endif
    }
}