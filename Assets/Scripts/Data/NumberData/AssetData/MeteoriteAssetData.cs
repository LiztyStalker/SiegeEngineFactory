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
    }
}