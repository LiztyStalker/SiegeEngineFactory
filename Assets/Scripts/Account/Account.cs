namespace SEF.Account
{
    using Data;
    using Entity;
    using Storage;

    public class Account
    {
        private static Account _current = null;

        public static Account Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Account();
                    _current.Initialize();
                }
                return _current;
            }
        }



        private void Initialize()
        {
            //AccountData √ ±‚»≠
            _assetEntity = AssetEntity.Create();
            _assetEntity.AddRefreshAssetEntityListener(OnRefreshAssetEntityEvent);
            _assetEntity.AddRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetEntity.Initialize();
        }
        public void Dispose()
        {
            _assetEntity.RemoveRefreshAssetEntityListener(OnRefreshAssetEntityEvent);
            _assetEntity.RemoveRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetEntity.CleanUp();
            _current = null;
        }

        public void Load(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            AccountIO.Load(loadCallback, endCallback);
        }

        public void Save(System.Action saveCallback, System.Action endCallback)
        {
            AccountIO.Save(saveCallback, endCallback);
        }




        private AssetEntity _assetEntity;

        public void AddAsset(AssetData assetData)
        {
            _assetEntity.Add(assetData);
        }

        public void SubjectAsset(AssetData assetData)
        {
            _assetEntity.Subject(assetData);
        }

        public bool IsEnoughAsset(AssetData assetData)
        {
            return _assetEntity.IsEnough(assetData);
        }


        #region ##### Listener #####

        private System.Action<AssetEntity> _refreshAsseEntity;
        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAsseEntity += act;
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAsseEntity -= act;
        private void OnRefreshAssetEntityEvent(AssetEntity assetEntity)
        {
            _refreshAsseEntity?.Invoke(assetEntity);
        }



        private System.Action<AssetData> _refreshAsseData;
        public void AddRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAsseData += act;
        public void RemoveRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAsseData -= act;
        private void OnRefreshAssetDataEvent(AssetData assetData)
        {
            _refreshAsseData?.Invoke(assetData);
        }

        #endregion

    }

}