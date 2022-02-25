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
        [UnityEngine.SerializeField] private int _upgradeValue;

        public string Key => _key;
        public int UpgradeValue => _upgradeValue;

        internal void SetData(string key, int value)
        {
            _key = key;
            _upgradeValue = value;
            Children = null;
        }
    }
    #endregion

    public struct VillageEntity : IStatusProvider
    {
        //Member
        private VillageData _data;

        //Lazy Member
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        //번역 작업 필요 - TranslatorStorage
        public string Name => _data.Key;
        public string Key => _data.Key;
        public string Content => _data.Key;
        public string Ability => _data.Key;

        public int NowUpgradeValue => _upgradeData.Value;

        public int MaxUpgradeValue
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeVillageStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.DefaultMaxUpgradeValue));
                return (int)data.Value;
            }
        }

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

        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }
        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
        }

        public void SetData(VillageData data)
        {
            _data = data;
        }

        public void SetData(VillageData data, UpgradeData upgradeData)
        {
            _data = data;
            _upgradeData = upgradeData;
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;
            SetStatusEntity();
        }

        private void SetStatusEntity()
        {
            var entity = new StatusEntity(_data.StatusData, _upgradeData);
            StatusPackage.Current.SetStatusEntity(this, entity);
        }
        private IAssetData CalculateUpgradeData() => _data.GetUpgradeAssetData(_upgradeData);

      
        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new VillageEntityStorableData();
            data.SetData(_data.Key, NowUpgradeValue);
            return data;
        }

        public void SetStorableData(UpgradeData upgradeData)
        {
            _upgradeData = upgradeData;
        }
        #endregion


    }
}