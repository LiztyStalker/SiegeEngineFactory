namespace SEF.Entity
{
    using Data;
    using Status;
    using Utility.IO;



    #region ##### StorableData #####
    [System.Serializable]
    public class SmithyEntityStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _key;
        [UnityEngine.SerializeField] private int _upgradeValue;
        [UnityEngine.SerializeField] private int _nowIndex;

        public string Key => _key;
        public int UpgradeValue => _upgradeValue;
        public int NowIndex => _nowIndex;

        internal void SetData(string key, int value, int nowIndex)
        {
            _key = key;
            _upgradeValue = value;
            _nowIndex = nowIndex;
            Children = null;
        }
    }
    #endregion


    public struct SmithyEntity : IStatusProvider
    {
        //Member
        private SmithyData _data;
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
                var data = StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxUpgradeSmithyStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_data.GetMaxUpgradeData(_nowIndex)));
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
        }
        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
        }

        public void SetData(SmithyData data)
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
            _nowIndex++;
            _upgradeData.SetValue(0);
        }

        public bool IsNextTech() => _nowIndex + 1 < _data.MaximumIndex;

        private void SetStatusEntity()
        {
            var entity = new StatusEntity(_data.GetStatusData(_nowIndex), _upgradeData);
            StatusPackage.Current.SetStatusEntity(this, entity);
        }

        private IAssetData CalculateUpgradeData() => _data.GetUpgradeAssetData(_nowIndex, _upgradeData);

        public bool IsMaxUpgrade() => NowUpgradeValue >= MaxUpgradeValue;



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new SmithyEntityStorableData();
            data.SetData(_data.Key, NowUpgradeValue, _nowIndex);
            return data;
        }

        public void SetStorableData(UpgradeData upgradeData, int nowIndex)
        {
            _upgradeData = upgradeData;
            _nowIndex = nowIndex;
        }
        #endregion


    }
}