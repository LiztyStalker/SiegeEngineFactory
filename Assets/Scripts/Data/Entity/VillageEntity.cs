namespace SEF.Entity
{
    using Data;
    using Status;
    using Utility.IO;


    #region ##### StorableData #####
    [System.Serializable]
    public class VillageEntityStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _key;
        [UnityEngine.SerializeField] private int _nowUpgradeValue;
        [UnityEngine.SerializeField] private int _nowTechValue;

        public string Key => _key;
        public int NowUpgradeValue => _nowUpgradeValue;
        public int NowTechValue => _nowTechValue;

        internal void SetData(string key, int nowUpgradeValue, int nowTechValue)
        {
            _key = key;
            _nowUpgradeValue = nowUpgradeValue;
            _nowTechValue = nowTechValue;
            Children = null;
        }
    }
    #endregion

    public struct VillageEntity : IStatusProvider
    {
        private const int DEFAULT_MAX_UPGRADE = 10;

        //Member
        private VillageData _data;
        private int _nowTechValue;

        //Lazy Member
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        //번역 작업 필요 - TranslatorStorage
        public string Name => _data.Key;
        public string Key => _data.Key;
        public string Content => _data.Key;
        public string Ability => _data.Key;

        public int NowUpgradeValue => _upgradeData.Value;
        public int UpgradableValue
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeVillageStatusData, UniversalBigNumberData>(new UniversalBigNumberData(DEFAULT_MAX_UPGRADE));
                return (int)data.Value;
            }
        }
        public int MaxUpgradeValue => _data.GetMaxUpgradeData(_nowTechValue);

        public int NowTechValue => _nowTechValue;
        public int MaxTechValue => _data.MaxTechValue;

        public IAssetData UpgradeAssetData
        {
            get
            {
                if (_upgradeAssetData == null)
                {
                    _upgradeAssetData = CalculateUpgradeData();
                }
                return _upgradeAssetData;
            }
        }
        public IAssetData TechAssetData => _data.GetTechAssetData(_nowTechValue);


        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
            _nowTechValue = 0;
        }

        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
            _nowTechValue = 0;
        }

        public void SetData(VillageData data)
        {
            _data = data;
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;

            SetStatusEntity();
        }


        //업그레이드 유지
        //업그레이드 초기화
        public void UpTech()
        {
            _nowTechValue++;
            _upgradeData.SetValue(0);
        }

        public bool IsNextTech() => _nowTechValue + 1 < _data.MaxTechValue;
        public bool IsUpgradable() => NowUpgradeValue < UpgradableValue;
        public bool IsMaxUpgrade() => NowUpgradeValue >= MaxUpgradeValue;
        private IAssetData CalculateUpgradeData() => _data.GetUpgradeAssetData(_nowTechValue, _upgradeData);



        private void SetStatusEntity()
        {
            var entity = new StatusEntity(_data.GetStatusData(_nowTechValue), _upgradeData);
            StatusPackage.Current.SetStatusEntity(this, entity);
        }



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new VillageEntityStorableData();
            data.SetData(_data.Key, NowUpgradeValue, _nowTechValue);
            return data;
        }

        public void SetStorableData(UpgradeData upgradeData, int nowTechValue)
        {
            _upgradeData = upgradeData;
            _nowTechValue = nowTechValue;
        }
        #endregion


    }
}