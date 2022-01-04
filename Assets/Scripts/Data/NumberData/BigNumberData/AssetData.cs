namespace SEF.Data
{
    using UnityEngine;
    public enum TYPE_ASSET { Gold, Ore, Resource, Meteorite, Population }

    [System.Serializable]
    [System.Obsolete("사용하지 않음")]
    public class AssetData : BigNumberData
    {

        private TYPE_ASSET _typeAsset;
        public TYPE_ASSET TypeAsset => _typeAsset;
        public AssetData() : base() {}
        protected AssetData(BigNumberData value) : base(value) { }

        public void SetTypeAsset(TYPE_ASSET typeAsset)
        {
            _typeAsset = typeAsset;
        }

        public override INumberData Clone()
        {
            return new AssetData(this);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AssetData Create_Test()
        {
            var data = new AssetData();
            data.ValueText = "100";
            return data;
        }
        public static AssetData Create_Test(TYPE_ASSET typeAsset, int value)
        {
            var data = new AssetData();
            data.ValueText = value.ToString();
            data.SetTypeAsset(typeAsset);
            return data;
        }

#endif

        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            SetTypeAsset(TYPE_ASSET.Gold);
            var upgradeValue = upgradeData.Value - 1;
            var increaseUpgradeAssetValue = unitData.IncreaseUpgradeAssetValue.Value;
            var increaseUpgradeAssetRate = Mathf.RoundToInt(unitData.IncreaseUpgradeAssetRate * 100f);
            //Value = unitData.StartUpgradeAsset.Value * Pow(increaseUpgradeAssetValue + increaseUpgradeAssetRate, upgradeValue); // 복리
            Value = unitData.StartUpgradeAsset.Value + (increaseUpgradeAssetValue * upgradeValue) + (increaseUpgradeAssetValue * upgradeValue * increaseUpgradeAssetRate / 100); //단리
        }

        ///string to bigint 파싱 필요
        ///1A => 1000
    }
}