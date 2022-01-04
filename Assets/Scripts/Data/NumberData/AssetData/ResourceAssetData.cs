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
    }
}