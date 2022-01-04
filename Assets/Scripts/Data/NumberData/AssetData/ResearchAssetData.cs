namespace SEF.Data
{
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
    }
}