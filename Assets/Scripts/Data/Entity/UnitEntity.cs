namespace SEF.Entity
{
    using Data;
    public struct UnitEntity
    {
        private UnitData _unitData;
        private UpgradeData _upgradeData;
        private AssetData _upgradeAssetData;

        public UnitData UnitData => _unitData;

        public int UpgradeValue => _upgradeData.Value;
//        public UpgradeData UpgradeData => _upgradeData;

        public AssetData UpgradeAssetData
        {
            get
            {
                if(_upgradeAssetData == null)
                {
                    _upgradeAssetData = CalculateUpgradeData();
                }
                return _upgradeAssetData;
            }
        }

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

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;
        }

        private AssetData CalculateUpgradeData()
        {
            var assetData = new AssetData();
            assetData.SetAssetData(UnitData, _upgradeData);
            return assetData;
        }
    }
}