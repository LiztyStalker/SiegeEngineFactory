namespace SEF.Data
{
    using System.Numerics;

    public class ResourceAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { get => Value; set => Value = value; }

        public ResourceAssetData() { }
        private ResourceAssetData(ResourceAssetData data) : base(data) { }

        public override INumberData Clone()
        {
            return new ResourceAssetData(this);
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static ResourceAssetData Create_Test()
        {
            var data = new ResourceAssetData();
            data.ValueText = "100";
            return data;
        }

        public static ResourceAssetData Create_Test(int value)
        {
            var data = new ResourceAssetData();
            data.ValueText = value.ToString();
            return data;
        }
#endif
    }
}