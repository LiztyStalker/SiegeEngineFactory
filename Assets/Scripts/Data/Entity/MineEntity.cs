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

    public struct MineEntity : IProcessProvider
    {
        private const int DEFAULT_MAX_UPGRADE = 10;

        //Member
        private MineData _data;
        private int _nowTechValue;

        //Lazy Member
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        //���� �۾� �ʿ� - TranslatorStorage
        public string Name => _data.Key;
        public string Key => _data.Key;
        public string Content => _data.Key;
        public string Ability => _data.Key;

        public int NowUpgradeValue => _upgradeData.Value;

        public int UpgradableValue
        {
            get
            {
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeMineStatusData, UniversalBigNumberData>(new UniversalBigNumberData(DEFAULT_MAX_UPGRADE));
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

        public void SetData(MineData data)
        {
            _data = data;
            OnProcessEntityEvent(this);
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;

            OnProcessEntityEvent(this);
        }

        //���׷��̵� ����
        //���׷��̵� �ʱ�ȭ
        public void UpTech()
        {
            _nowTechValue++;
            _upgradeData.SetValue(0);

            OnProcessEntityEvent(this);
        }

        public bool IsNextTech() => _nowTechValue + 1 < _data.MaxTechValue;
        public bool IsUpgradable() => NowUpgradeValue < UpgradableValue;
        public bool IsMaxUpgrade() => NowUpgradeValue >= MaxUpgradeValue;
        private IAssetData CalculateUpgradeData() => _data.GetUpgradeAssetData(_nowTechValue, _upgradeData);

        public IAssetData RewardOffline(System.TimeSpan timeSpan)
        {
            var processData = _data.GetProcessData(_nowTechValue);
            var processCount = (int)(timeSpan.TotalSeconds / processData.ProcessTime);
            return ((AssetProcessData)processData).GetAssetData(_upgradeData, processCount);           
        }

        #region ##### Listener #####

        private System.Action<IProcessProvider, ProcessEntity> _processEntityEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _processEntityEvent = act;

        private void OnProcessEntityEvent(IProcessProvider provider)
        {
            var entity = new ProcessEntity(_data.GetProcessData(_nowTechValue), _upgradeData);
            _processEntityEvent?.Invoke(provider, entity);
        }

        #endregion



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new MineEntityStorableData();
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