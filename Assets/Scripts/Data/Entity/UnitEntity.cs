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
        private const int DEFAULT_MAX_UPGRADE = 10;

        //Data Member
        private UnitData _data;
        private UpgradeData _upgradeData;

        //Lazy Data Member
        private IAssetData _upgradeAssetData;
        private HealthData _healthData;
        private DamageData _damageData;

        public UnitData UnitData => _data;
        public UpgradeData UpgradeData => _upgradeData;
        public int Population => 1; //_unitData.Population;
        public int NowUpgradeValue => _upgradeData.Value;
        public int UpgradableValue
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeUnitStatusData, UniversalBigNumberData>(new UniversalBigNumberData(DEFAULT_MAX_UPGRADE));
                return (int)data.Value;
            }
        }

        public int MaxUpgradeValue => _data.DefaultMaxUpgradeValue;

        public float AttackDelay
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<UnitDamageDelayStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.AttackDelay));
                return (float)data.Value;
            }
        }

        public float ProductTime
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<UnitProductTimeStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.ProductTime));
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
                return StatusPackage.Current.GetStatusDataToBigNumberData<UnitHealthValueStatusData, HealthData>(_healthData);
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
        public bool IsNextTech() => _data.UnitTechDataArray != null && _data.UnitTechDataArray.Length != 0;
        public bool IsUpgradable() => NowUpgradeValue < UpgradableValue;
        public bool IsMaxUpgrade() => _upgradeData.Value >= _data.DefaultMaxUpgradeValue;

        public void Initialize()
        {
            //UpTech�� Initialize�� ������ �ʿ����� �ʵ��� ���� �ʿ�
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }
        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
            _damageData = null;
        }

        public void UpTech(UnitData unitData)
        {
            _data = unitData;
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
            assetData.SetAssetData(_data, _upgradeData);
            return assetData;
        }

        private HealthData CalculateHealthData()
        {
            var assetData = new HealthData();
            assetData.SetAssetData(_data, _upgradeData);
            return assetData;
        }

        private DamageData CalculateAttackData()
        {
            var assetData = new DamageData();
            assetData.SetAssetData(_data, _upgradeData);
            return assetData;
        }

        #region ##### StorableData #####

        public StorableData GetStorableData()
        {
            var _storableData = new UnitEntityStorableData();
            _storableData.SetData(_data.Key, _upgradeData.Value);
            return _storableData;
        }

        public void SetStorableData(UnitData unitData, UpgradeData upgradeData)
        {
            _data = unitData;
            _upgradeData = upgradeData;
        }

        #endregion
    }
}