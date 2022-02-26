namespace SEF.Entity
{
    using Status;
    using Data;
    using Utility.IO;



    #region ##### StorableData #####

    [System.Serializable]
    public class UnitEntityStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _unitKey;
        [UnityEngine.SerializeField] private int _upgradeValue;

        public string UnitKey => _unitKey;
        public int UpgradeValue => _upgradeValue;

        internal void SetData(string key, int value)
        {
            _unitKey = key;
            _upgradeValue = value;
            Children = null;
        }
    }

    #endregion



    [System.Serializable]
    public struct UnitEntity : IEntity
    {

        //Data Member
        private UnitData _unitData;
        private UpgradeData _upgradeData;

        //Lazy Data Member
        private IAssetData _upgradeAssetData;
        private HealthData _healthData;
        private DamageData _damageData;

        public UnitData UnitData => _unitData;
        public UpgradeData UpgradeData => _upgradeData;
        public int Population => 1; //_unitData.Population;
        public int NowUpgradeValue => _upgradeData.Value;
        public int MaxUpgradeValue
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeUnitStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_unitData.DefaultMaxUpgradeValue));
                return (int)data.Value;
            }
        }
        public float AttackDelay
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<UnitDamageDelayStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_unitData.AttackDelay));
                return (float)data.Value;
            }
        }

        public float ProductTime
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<UnitProductTimeStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_unitData.ProductTime));
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
                return StatusPackage.Current.GetStatusDataToBigNumberData<UnitHealthDataStatusData, HealthData>(_healthData);
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
                return StatusPackage.Current.GetStatusDataToBigNumberData<UnitDamageValueStatusData, DamageData>(_damageData);
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
        public bool IsMaxUpgrade() => _upgradeData.Value >= _unitData.DefaultMaxUpgradeValue;
        public bool IsNextTech() => _unitData.UnitTechDataArray != null && _unitData.UnitTechDataArray.Length != 0;

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
            _upgradeData.SetValue(0);
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

        #region ##### StorableData #####

        public StorableData GetStorableData()
        {
            var _storableData = new UnitEntityStorableData();
            _storableData.SetData(_unitData.Key, _upgradeData.Value);
            return _storableData;
        }

        public void SetStorableData(UnitData unitData, UpgradeData upgradeData)
        {
            _unitData = unitData;
            _upgradeData = upgradeData;
        }

        #endregion
    }
}