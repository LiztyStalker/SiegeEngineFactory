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
        private Dictionary<string, IAssetData> _dic = new Dictionary<string, IAssetData>();

        public static AssetEntity Create()
        {
            return new AssetEntity();
        }

        public void Initialize()
        {
            _dic.Add(typeof(GoldAssetData).Name, CreateAssetDataCase<GoldAssetData>());
            _dic.Add(typeof(ResourceAssetData).Name, CreateAssetDataCase<ResourceAssetData>());
            _dic.Add(typeof(MeteoriteAssetData).Name, CreateAssetDataCase<MeteoriteAssetData>());
            _dic.Add(typeof(ResearchAssetData).Name, CreateAssetDataCase<ResearchAssetData>());
            _dic.Add(typeof(PopulationAssetData).Name, CreateAssetDataCase<PopulationAssetData>(new PopulationAssetData(5)));
        }

        private AssetDataCase CreateAssetDataCase<T>(IAssetData limitData = null) where T : IAssetData
        {
            var dataCase = new AssetDataCase(NumberDataUtility.CreateAssetData<T>());
            dataCase.SetLimitAssetData(limitData);
            return dataCase;
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

        #region ##### Listener #####

        private System.Action<AssetEntity> _refreshAssetEntityEvent;
        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAssetEntityEvent += act;
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _refreshAssetEntityEvent -= act;
        public void OnRefreshAssetEntityEvent(AssetEntity assetEntity)
        {
            _refreshAssetEntityEvent?.Invoke(assetEntity);
        }
        //차후에 리팩토링시 refreshAssetDataEvent만 사용할 가능성 있음


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