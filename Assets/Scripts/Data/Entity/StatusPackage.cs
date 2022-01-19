namespace SEF.Entity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using System.Linq;

    public class StatusPackage
    {

        #region ##### StatusCase #####
        private class StatusCase
        {
            private IStatusData _statusData;
            private float _nowTime;

            internal IStatusData StatusData => _statusData;

            internal void SetStatusData(IStatusData statusData)
            {
                Debug.Log(statusData);
                _statusData = statusData;
            }

            internal void RunProcess(float deltaTime)
            {
                _nowTime += deltaTime;
                if(_nowTime > _statusData.ProductTime)
                {
                    ProductEvent?.Invoke(_statusData.AssetDataArray);
                    _nowTime -= _statusData.ProductTime;
                }
            }

            internal event System.Action<IAssetData[]> ProductEvent;
        }

        #endregion

        private Dictionary<IStatusProvider, StatusCase> _dic;

        private Dictionary<IStatusProvider, StatusCase> Dictionary
        {
            get
            {
                Debug.Assert(_dic != null, "Initialize를 진행하지 않았습니다");
                return _dic;
            }
        }

        public void Initialize()
        {
            _dic = new Dictionary<IStatusProvider, StatusCase>();
        }

        public void CleanUp()
        {
            Dictionary.Clear();
        }

        public void SetStatusData(IStatusProvider provider, IStatusData statusData)
        {
            if (!Dictionary.ContainsKey(provider))
            {
                var statusCase = new StatusCase();
                statusCase.ProductEvent += OnProductEvent;
                Dictionary.Add(provider, statusCase);
            }
            Dictionary[provider].SetStatusData(statusData);
        }

        public U GetStatusDataToAssetData<T, U>(U data) where T : IStatusData where U : IAssetData
        {
            var value = Dictionary.Values.Where(data => data.StatusData is T);
            //value rate absolute
            return data;
        }

        public U GetStatusDataToNumberData<T, U>(U data) where T : IStatusData where U : INumberData
        {
            var value = Dictionary.Values.Where(data => data.StatusData is T);
            return default;
        }

        public U GetStatusDataToCommonDataType<T, U>(U value) where T : IStatusData
        {
            return default;
        }

        public void RunProcess(float deltaTime)
        {
            foreach(var value in Dictionary.Values)
            {
                value.RunProcess(deltaTime);
            }
        }

        public static StatusPackage Create()
        {
            return new StatusPackage();
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TEST
        public bool HasStatusProvider(IStatusProvider provider)
        {
            return Dictionary.ContainsKey(provider);
        }

        public bool HasStatusProviderAndStatusData(IStatusProvider provider, IStatusData statusData)
        {
            if (HasStatusProvider(provider))
            {
                Debug.Log(Dictionary[provider].StatusData.GetHashCode() + " " + statusData.GetHashCode());
                return Dictionary[provider].StatusData == statusData;
            }
            return false;            
        }
#endif


        #region ##### Listener #####

        private System.Action<IAssetData[]> _productEvent;
        public void AddOnProductListener(System.Action<IAssetData[]> act) => _productEvent += act;
        public void RemoveOnProductListener(System.Action<IAssetData[]> act) => _productEvent -= act;

        private void OnProductEvent(IAssetData[] assetDataArr)
        {
            _productEvent?.Invoke(assetDataArr);
        }

        #endregion
    }
}