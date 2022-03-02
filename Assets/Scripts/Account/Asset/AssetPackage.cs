namespace SEF.Entity
{
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using System.Numerics;
    using Utility.IO;
    using Status;

    #region ##### StorableData #####
    [System.Serializable]
    public class AssetPackageStorableData : StorableData
    {
        internal void SetData(StorableData[] children)
        {
            Children = children;
        }
    }
    #endregion


    public class AssetPackage
    {
        private Dictionary<string, IAssetData> _dic = new Dictionary<string, IAssetData>();

        public static AssetPackage Create()
        {
            return new AssetPackage();
        }

        public void Initialize()
        {
            _dic.Add(typeof(GoldAssetData).Name, CreateAssetDataCase<GoldAssetData>());
            _dic.Add(typeof(ResourceAssetData).Name, CreateAssetDataCase<ResourceAssetData>());
            _dic.Add(typeof(MeteoriteAssetData).Name, CreateAssetDataCase<MeteoriteAssetData>());
            _dic.Add(typeof(ResearchAssetData).Name, CreateAssetDataCase<ResearchAssetData>());
            _dic.Add(typeof(PopulationAssetData).Name, CreateAssetDataCase<PopulationAssetData>(new PopulationAssetData(5)));
        }

        private AssetEntity CreateAssetDataCase<T>(IAssetData limitData = null) where T : IAssetData
        {
            var dataCase = new AssetEntity(NumberDataUtility.Create<T>());
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
            Debug.Assert(assetData != null, "Account AssetData�� ã�� �� �����ϴ�");
            assetData.AssetValue += data.AssetValue;
            RefreshAssets(assetData);
        }

        public void Subject(IAssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData�� ã�� �� �����ϴ�");
            assetData.AssetValue -= data.AssetValue;
            RefreshAssets(assetData);
        }

        public void Set(IAssetData data)
        {
            var assetData = FindAssetData(data);
            Debug.Assert(assetData != null, "Account AssetData�� ã�� �� �����ϴ�");
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
            if(assetData is AssetEntity)
            {
                return ((AssetEntity)assetData).IsOverflow(data);
            }
            return false;
        }

        public bool IsUnderflow(IAssetData data)
        {
            var assetData = FindAssetData(data);
            if (assetData == null) return true;
            if (assetData is AssetEntity)
            {
                return ((AssetEntity)assetData).IsUnderflow(data);
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

        private System.Action<AssetPackage> _refreshAssetEntityEvent;
        public void AddRefreshAssetEntityListener(System.Action<AssetPackage> act) => _refreshAssetEntityEvent += act;
        public void RemoveRefreshAssetEntityListener(System.Action<AssetPackage> act) => _refreshAssetEntityEvent -= act;
        public void OnRefreshAssetEntityEvent(AssetPackage assetEntity)
        {
            _refreshAssetEntityEvent?.Invoke(assetEntity);
        }
        //���Ŀ� �����丵�� refreshAssetDataEvent�� ����� ���ɼ� ����


        private System.Action<IAssetData> _refreshAssetDataEvent;
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAssetDataEvent += act;
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _refreshAssetDataEvent -= act;
        public void OnRefreshAssetDataEvent(IAssetData assetData) => _refreshAssetDataEvent?.Invoke(assetData);

        #endregion


        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new AssetPackageStorableData();

            List<StorableData> list = new List<StorableData>();
            foreach(var key in _dic.Keys)
            {
                list.Add(_dic[key].GetStorableData());
            }
            data.SetData(list.ToArray());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (AssetPackageStorableData)data;
            for (int i = 0; i < storableData.Children.Length; i++)
            {
                var child = (AssetEntityStorableData)storableData.Children[i];
                var now = (AssetDataStorableData)child.Children[0];
                Set(now.GetAssetData());
            }
        }
        #endregion

    }
}