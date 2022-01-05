namespace SEF.Data
{
    using System.Numerics;

    [System.Serializable]
    public class GoldAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { get => Value; set => Value = value; }

        public GoldAssetData() { }
        private GoldAssetData(GoldAssetData data) : base(data) { }

        public override INumberData Clone()
        {
            return new GoldAssetData(this);
        }
        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value - 1);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static GoldAssetData Create_Test() 
        {
            var data = new GoldAssetData();
            data.ValueText = "100";
            return data;
        }

        public static GoldAssetData Create_Test(int value)
        {
            var data = new GoldAssetData();
            data.ValueText = value.ToString();
            return data;
        }
#endif

    }


}