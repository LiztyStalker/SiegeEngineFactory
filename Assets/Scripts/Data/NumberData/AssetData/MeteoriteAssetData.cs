namespace SEF.Data
{
    using System.Numerics;

    public class MeteoriteAssetData : NumberData, IAssetData
    {
        public BigInteger AssetValue { get => new BigInteger(Value); set => Value = int.Parse(value.ToString()); }

        public MeteoriteAssetData() { }
        private MeteoriteAssetData(MeteoriteAssetData data)
        {
            Value = data.Value;
        }
        public override INumberData Clone()
        {
            return new MeteoriteAssetData(this);
        }
    }
}