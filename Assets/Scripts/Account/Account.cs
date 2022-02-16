namespace SEF.Account
{
    using Data;
    using Entity;
    using Storage;
    using Utility.IO;

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


        private AccountStorableData _storableData;



        private void Initialize()
        {
            //AccountData 초기화
            _assetEntity = AssetEntity.Create();
            _assetEntity.AddRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetEntity.Initialize();
        }
        public void Dispose()
        {
            _assetEntity.RemoveRefreshAssetDataListener(OnRefreshAssetDataEvent);
            _assetEntity.CleanUp();
            _current = null;
        }

        public void LoadData(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            //복호화 적용 필요
            //StorableDataIO.Current.LoadFileData("test", loadCallback, (result, obj) =>
            StorableDataIO.Current.LoadFileData_NotCrypto("test", loadCallback, (result, obj) =>
            {
                endCallback?.Invoke(result);
                if (obj != null)
                {
                    _storableData = (AccountStorableData)obj;
                }
                else
                {
                    _storableData = new AccountStorableData();
                }                    
            });
        }

        public void SaveData(System.Action saveCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            //암호화 적용 필요
            //StorableDataIO.Current.SaveFileData(_storableData, "test", endCallback);
            StorableDataIO.Current.SaveFileData_NotCrypto(_storableData, "test", endCallback);
        }



        #region ##### StorableData #####

        public void SetStorableData(StorableData data) => _storableData.SaveData(data);

        public StorableData GetStorableData() => _storableData.LoadData();

        #endregion





        private AssetEntity _assetEntity;

        public void AddAsset(IAssetData assetData)
        {
            _assetEntity.Add(assetData);
        }

        public void SubjectAsset(IAssetData assetData)
        {
            _assetEntity.Subject(assetData);
        }

        public void SetAsset(IAssetData assetData)
        {
            _assetEntity.Set(assetData);
        }

        public bool IsEnoughAsset(IAssetData assetData)
        {
            return _assetEntity.IsEnough(assetData);
        }
               
        public bool IsOverflow(IAssetData assetData)
        {
            return _assetEntity.IsOverflow(assetData);
        }

        public bool IsUnderflow(IAssetData assetData)
        {
            return _assetEntity.IsUnderflow(assetData);
        }

        public void RefreshAssetEntity()
        {
            _assetEntity.RefreshAssets();
        }

        //public void AddAsset(AssetData assetData)
        //{
        //    _assetEntity.Add(assetData);
        //}

        //public void SubjectAsset(AssetData assetData)
        //{
        //    _assetEntity.Subject(assetData);
        //}

        //public bool IsEnoughAsset(AssetData assetData)
        //{
        //    return _assetEntity.IsEnough(assetData);
        //}



        #region ##### Listener #####

        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _assetEntity.AddRefreshAssetEntityListener(act);
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _assetEntity.RemoveRefreshAssetEntityListener(act);



        //private System.Action<AssetData> _refreshAsseData;
        //public void AddRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAsseData += act;
        //public void RemoveRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAsseData -= act;
        //private void OnRefreshAssetDataEvent(AssetData assetData)
        //{
        //    _refreshAsseData?.Invoke(assetData);
        //}


        private System.Action<IAssetData> _refreshAsseData;
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData += act;
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAsseData -= act;
        private void OnRefreshAssetDataEvent(IAssetData assetData)
        {
            _refreshAsseData?.Invoke(assetData);
        }

        #endregion

    }

}