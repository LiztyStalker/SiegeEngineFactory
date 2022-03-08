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
        [UnityEngine.SerializeField] private int _nowIndex;

        public string Key => _key;
        public int UpgradeValue => _upgradeValue;

        internal void SetData(string key, int value, int nowIndex)
        {
            _key = key;
            _upgradeValue = value;
            _nowIndex = nowIndex;
            Children = null;
        }
    }
    #endregion

    public struct MineEntity : IProcessProvider
    {
        //Member
        private MineData _data;
        private int _nowIndex;

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
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeMineStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.GetMaxUpgradeData(_nowIndex)));
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

        public IAssetData TechAssetData => _data.GetTechAssetData(_nowIndex);


        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
            _nowIndex = 0;
        }
        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
            _nowIndex = 0;
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

        //업그레이드 유지
        //업그레이드 초기화
        public void UpTech()
        {
            _nowIndex++;
            _upgradeData.SetValue(0);
        }

        public bool IsNextTech() => _nowIndex + 1 < _data.MaximumIndex;
        public bool IsMaxUpgrade() => NowUpgradeValue >= MaxUpgradeValue;
        private IAssetData CalculateUpgradeData() => _data.GetUpgradeAssetData(_nowIndex, _upgradeData);

        public IAssetData RewardOffline(System.TimeSpan timeSpan)
        {
            var processData = _data.GetProcessData(_nowIndex);
            var processCount = (int)(timeSpan.TotalSeconds / processData.ProcessTime);
            return ((AssetProcessData)processData).GetAssetData(_upgradeData, processCount);           
        }

        #region ##### Listener #####

        private System.Action<IProcessProvider, ProcessEntity> _processEntityEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _processEntityEvent = act;

        private void OnProcessEntityEvent(IProcessProvider provider)
        {
            var entity = new ProcessEntity(_data.GetProcessData(_nowIndex), _upgradeData);
            _processEntityEvent?.Invoke(provider, entity);
        }

        #endregion



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new MineEntityStorableData();
            data.SetData(_data.Key, NowUpgradeValue, _nowIndex);
            return data;
        }

        public void SetStorableData(UpgradeData upgradeData)
        {
            _upgradeData = upgradeData;
        }
        #endregion


    }
}