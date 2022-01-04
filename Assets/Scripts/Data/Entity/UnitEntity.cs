namespace SEF.Entity
{
    using Data;
    public struct UnitEntity// : IEntity
    {
        private UnitData _unitData;
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        public UnitData UnitData => _unitData;

        public int UpgradeValue => _upgradeData.Value;
        //        public UpgradeData UpgradeData => _upgradeData;
        public int Population => 1; //_unitData.Population;

        public IAssetData UpgradeAssetData
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

        private IAssetData CalculateUpgradeData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_unitData, _upgradeData);
            return assetData;
        }
    }
}