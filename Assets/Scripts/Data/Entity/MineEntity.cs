namespace SEF.Entity
{
    using Data;
    using Process;
    using Utility.IO;
    using Status;


    #region ##### StorableData #####
    [System.Serializable]
    public class MineEntityStorableData : StorableData
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

    public struct MineEntity : IProcessProvider
    {
        //Member
        private MineData _data;

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
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeMineStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.DefaultMaxUpgradeValue));
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

        public void SetData(MineData data)
        {
            _data = data;
        }

        public void SetData(MineData data, UpgradeData upgradeData)
        {
            _data = data;
            _upgradeData = upgradeData;
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;
            OnProcessEntityEvent(this);
        }

        private IAssetData CalculateUpgradeData()
        {
            var assetData = (IAssetData)_data.StartUpgradeAssetData.Clone();
            assetData.SetCompoundInterest(_data.IncreaseUpgradeValue, _data.IncreaseUpgradeRate, _upgradeData.Value);
            return assetData;
        }

        #region ##### Listener #####

        private System.Action<IProcessProvider, ProcessEntity> _processEntityEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _processEntityEvent = act;

        private void OnProcessEntityEvent(IProcessProvider provider)
        {
            var entity = new ProcessEntity(_data.ProcessData, _upgradeData);
            _processEntityEvent?.Invoke(provider, entity);
        }

        #endregion



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new MineEntityStorableData();
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