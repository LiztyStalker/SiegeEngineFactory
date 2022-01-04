namespace SEF.Entity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Account;
    using System.Numerics;

    public struct AssetDataCase : IAssetData
    {
        private IAssetData _limitAssetData;
        private IAssetData _nowAssetData;

        public IAssetData LimitAssetData => _limitAssetData;
        public IAssetData NowAssetData => _nowAssetData;        

        public AssetDataCase(IAssetData nowAssetData)
        {
            _limitAssetData = null;
            _nowAssetData = nowAssetData;
        }

        public void SetLimitAssetData(IAssetData limitAssetData)
        {
            _limitAssetData = limitAssetData;
        }

        public BigInteger AssetValue { 
            get => _nowAssetData.AssetValue;
            set => _nowAssetData.AssetValue = value;
        }

        public bool IsOverflow(IAssetData data)
        {
            if (_limitAssetData == null) return false;
            return (_nowAssetData.AssetValue + data.AssetValue > _limitAssetData.AssetValue);
        }

        public bool IsUnderflow(IAssetData data)
        {
            if (_limitAssetData == null) return false;
            return (_nowAssetData.AssetValue - data.AssetValue < 0);
        }

        public string GetValue()
        {
            if(_limitAssetData == null)
            {
                return _nowAssetData.GetValue();
            }
            else
            {
                return $"{_nowAssetData.GetValue()} / {_limitAssetData.GetValue()}";
            }
        }

        public new System.Type GetType() => _nowAssetData.GetType();

        
    }

    public class AssetEntity
    {
        //private Dictionary<TYPE_ASSET, AssetData> _dic = new Dictionary<TYPE_ASSET, AssetData>();
        private Dictionary<string, IAssetData> _dic = new Dictionary<string, IAssetData>();

        public static AssetEntity Create()
        {
            return new AssetEntity();
        }

        public void Initialize()
        {
            _dic.Add(typeof(GoldAssetData).Name, NumberDataUtility.Create<GoldAssetData>());
            _dic.Add(typeof(ResourceAssetData).Name, NumberDataUtility.Create<ResourceAssetData>());
            _dic.Add(typeof(MeteoriteAssetData).Name, NumberDataUtility.Create<MeteoriteAssetData>());
            _dic.Add(typeof(ResearchAssetData).Name, NumberDataUtility.Create<ResearchAssetData>());

            var population = new AssetDataCase(NumberDataUtility.Create<PopulationAssetData>());
            population.SetLimitAssetData(new PopulationAssetData(10));

            _dic.Add(typeof(PopulationAssetData).Name, population);
            //for(int i = 0; i < System.Enum.GetValues(typeof(TYPE_ASSET)).Length; i++)
            //{
            //    var assetData = NumberDataUtility.Create<AssetData>();
            //    assetData.SetTypeAsset((TYPE_ASSET)i);
            //    assetData.SetValue("0");
            //    _dic.Add((TYPE_ASSET)i, assetData);
            //}
        }

        public void CleanUp()
        {
            _dic.Clear();
            _refreshAssetEntityEvent = null;
        }


        public void Add(IAssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            assetData.AssetValue += data.AssetValue;
            RefreshAssets(assetData);
        }

        public void Subject(IAssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            assetData.AssetValue -= data.AssetValue;
            RefreshAssets(assetData);
        }

        public void Set(IAssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
            assetData.AssetValue = data.AssetValue;
            RefreshAssets(assetData);
        }

        public bool IsEnough(IAssetData data)
        {
            var assetData = FindAssetData(data);
            if (assetData == null) return false;
            return assetData.AssetValue >= data.AssetValue;
        }
       
        public bool IsOverflow(IAssetData data)
        {
            var assetData = FindAssetData(data);
            if (assetData == null) return true;
            if(assetData is AssetDataCase)
            {
                return ((AssetDataCase)assetData).IsOverflow(data);
            }
            return false;
        }

        public bool IsUnderflow(IAssetData data)
        {
            var assetData = FindAssetData(data);
            if (assetData == null) return true;
            if (assetData is AssetDataCase)
            {
                return ((AssetDataCase)assetData).IsUnderflow(data);
            }
            return false;
        }

        //public void Add(AssetData data)
        //{
        //    var assetData = FindAssetData(data);
        //    Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
        //    assetData.Value += data.Value;
        //    RefreshAssets(assetData);
        //}

        //public void Subject(AssetData data)
        //{
        //    var assetData = FindAssetData(data);
        //    Debug.Assert(assetData != null, "Account AssetData를 찾을 수 없습니다");
        //    assetData.Value -= data.Value;
        //    RefreshAssets(assetData);
        //}

        //public bool IsEnough(AssetData data)
        //{
        //    var assetData = FindAssetData(data);
        //    if (assetData == null) return false;
        //    return assetData.Value >= data.Value;
        //}        

        public void RefreshAssets()
        {
            OnRefreshAssetEntityEvent(this);
            foreach(var value in _dic.Values)
            {
                OnRefreshAssetDataEvent(value);
            }
        }


        public void RefreshAssets(IAssetData assetData)
        {
            RefreshAssets();
            OnRefreshAssetDataEvent(assetData);
        }

        public IAssetData FindAssetData(IAssetData data)
        {
            var typeName = data.GetType().Name;
            if (_dic.ContainsKey(typeName))
            {
                return _dic[typeName];
            }
            return null;
        }


        //public void RefreshAssets(AssetData assetData)
        //{
        //    RefreshAssets();
        //    OnRefreshAssetDataEvent(assetData);
        //}

        //public AssetData FindAssetData(AssetData data)
        //{
        //    return FindAssetData(data.TypeAsset);
        //}

        //public AssetData FindAssetData(TYPE_ASSET typeAsset)
        //{
        //    if (_dic.ContainsKey(typeAsset))
        //    {
        //        return _dic[typeAsset];
        //    }
        //    return null;
        //}

        #region ##### Listener #####

        private System.Action<AssetEntity> _refreshAssetEntityEvent;
        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAssetEntityEvent += act;
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAssetEntityEvent -= act;
        public void OnRefreshAssetEntityEvent(AssetEntity assetEntity)
        {
            //Debug.Log(assetEntity);
            _refreshAssetEntityEvent?.Invoke(assetEntity);
        }
        //차후에 리팩토링시 refreshAssetDataEvent만 사용할 가능성 있음


        //private System.Action<AssetData> _refreshAssetDataEvent;
        //public void AddRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAssetDataEvent += act;
        //public void RemoveRefreshAssetDataListener(System.Action<AssetData> act) => _refreshAssetDataEvent -= act;
        //public void OnRefreshAssetDataEvent(AssetData assetData) => _refreshAssetDataEvent?.Invoke(assetData);


        private System.Action<IAssetData> _refreshAssetDataEvent;
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAssetDataEvent += act;
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAssetDataEvent -= act;
        public void OnRefreshAssetDataEvent(IAssetData assetData) => _refreshAssetDataEvent?.Invoke(assetData);

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