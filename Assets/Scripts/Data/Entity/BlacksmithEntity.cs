namespace SEF.Entity
{
    using SEF.Data;
    using SEF.Status;
    

    public struct BlacksmithEntity : IStatusProvider
    {
        private BlacksmithData _data;
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        //번역 작업 필요 - TranslatorStorage
        public string Name => _data.Key;
        public string Content => _data.Key;
        public string Ability => _data.Key;

        public int UpgradeValue => _upgradeData.Value;

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

        public void SetData(BlacksmithData data)
        {
            _data = data;            
        }

        public void SetData(BlacksmithData data, UpgradeData upgradeData)
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

    }
}