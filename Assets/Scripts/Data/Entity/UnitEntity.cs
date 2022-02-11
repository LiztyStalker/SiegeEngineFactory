namespace SEF.Entity
{
    using Status;
    using Data;
    public struct UnitEntity : IEntity
    {
        private UnitData _unitData;
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;
        private HealthData _healthData;
        private DamageData _damageData;

        public UnitData UnitData => _unitData;
        public UpgradeData UpgradeData => _upgradeData;
        public int UpgradeValue => _upgradeData.Value;
        public int Population => 1; //_unitData.Population;


        public float AttackDelay
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<DamageDelayStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_unitData.AttackDelay));
                return (float)data.Value;
            }
        }

        public float ProductTime
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<ProductTimeStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_unitData.ProductTime));
                return (float)data.Value;
            }
        }

        public HealthData HealthData
        {
            get
            {
                if (_healthData == null)
                {
                    _healthData = CalculateHealthData();
                }
                return StatusPackage.Current.GetStatusDataToBigNumberData<HealthDataStatusData, HealthData>(_healthData);
            }
        }


        public DamageData DamageData
        {
            get
            {
                if (_damageData == null)
                {
                    _damageData = CalculateAttackData();
                }
                return StatusPackage.Current.GetStatusDataToBigNumberData<DamageValueStatusData, DamageData>(_damageData);
            }
        }

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
            //UpTech와 Initialize의 순서가 필요하지 않도록 제작 필요
            _upgradeData = NumberDataUtility.Create<UpgradeData>();            
        }
        public void CleanUp()
        {
            _unitData = null;
            _upgradeData = null;
            _damageData = null;
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
            _healthData = null;
            _damageData = null;
        }

        private IAssetData CalculateUpgradeData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_unitData, _upgradeData);
            return assetData;
        }

        private HealthData CalculateHealthData()
        {
            var assetData = new HealthData();
            assetData.SetAssetData(_unitData, _upgradeData);
            return assetData;
        }

        private DamageData CalculateAttackData()
        {
            var assetData = new DamageData();
            assetData.SetAssetData(_unitData, _upgradeData);
            return assetData;
        }
    }
}