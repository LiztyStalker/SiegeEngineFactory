namespace SEF.Entity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Account;

    public class AssetEntity
    {
        private Dictionary<TYPE_ASSET, AssetData> _dic = new Dictionary<TYPE_ASSET, AssetData>();

        public static AssetEntity Create()
        {
            return new AssetEntity();
        }

        public void Initialize()
        {
            for(int i = 0; i < System.Enum.GetValues(typeof(TYPE_ASSET)).Length; i++)
            {
                var assetData = NumberDataUtility.Create<AssetData>();
                assetData.SetTypeAsset((TYPE_ASSET)i);
                _dic.Add((TYPE_ASSET)i, assetData);
            }
        }

        public void CleanUp()
        {
            _dic.Clear();
            _refreshEvent = null;
        }

        public void Add(AssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            assetData.Value += data.Value;
            OnRefreshEvent(assetData);
        }

        public void Subject(AssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            assetData.Value -= data.Value;
            OnRefreshEvent(assetData);
        }

        public bool IsEnough(AssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            return assetData.Value >= data.Value;
        }

        

        public void Refresh()
        {
            foreach(var value in _dic.Values)
            {
                OnRefreshEvent(value);
            }
        }

        public AssetData FindAssetData(AssetData data)
        {
            return FindAssetData(data.TypeAsset);
        }

        public AssetData FindAssetData(TYPE_ASSET typeAsset)
        {
            if (_dic.ContainsKey(typeAsset))
            {
                return _dic[typeAsset];
            }
            return null;
        }

        #region ##### Listener #####

        private System.Action<AssetData> _refreshEvent;

        public void AddRefreshListener(System.Action<AssetData> act) => _refreshEvent += act;
        public void RemoveRefreshListener(System.Action<AssetData> act) => _refreshEvent -= act;

        public void OnRefreshEvent(AssetData assetData)
        {
            _refreshEvent?.Invoke(assetData);
        }

        #endregion


        #region ##### Data #####

        public IAccountData GetData()
        {
            return null;
        }

        public void SetData(IAccountData data)
        {

        }

        #endregion
    }
}