namespace SEF.Entity
{
    using Data;
    public struct UnitEntity
    {
        private UnitData _unitData;
        private UpgradeData _upgradeData;

        public UnitData UnitData => _unitData;
        public UpgradeData UpgradeData => _upgradeData;

        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }
        public void CleanUp()
        {
            _unitData = null;
            _upgradeData = null;
        }

        public void UpTech(UnitData unitData)
        {
            _unitData = unitData;
            _upgradeData.Initialize();
        }

        public AssetData GetUpgradeAssetData()
        {
            var assetData = new AssetData();
            assetData.SetAssetData(UnitData, UpgradeData);
            return assetData;
        }
    }
}