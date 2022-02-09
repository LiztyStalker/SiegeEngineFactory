namespace SEF.Entity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using System.Linq;

    public class StatusPackage
    {


        private static StatusPackage _current;

        public static StatusPackage Current
        {
            get
            {
                if(_current == null)
                {
                    _current = Create();
                }
                return _current;
            }
        }


        #region ##### StatusCase #####
        private class StatusCase
        {
            private IStatusData _statusData;
            private float _nowTime;

            internal IStatusData StatusData => _statusData;

            internal void SetStatusData(IStatusData statusData)
            {
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

        public void Dispose()
        {
            Dictionary.Clear();
            _current = null;
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

        public void RemoveStatusData(IStatusProvider provider)
        {
            if (Dictionary.ContainsKey(provider))
            {
                Dictionary.Remove(provider);
            }
        }

        public U GetStatusDataToBigNumberData<T, U>(U data) where T : IStatusData where U : BigNumberData
        {
            U calData = (U)data.Clone();
            var values = Dictionary.Values.Where(sData => sData.StatusData is T).ToArray();

            U absoluteData = NumberDataUtility.Create<U>();
            U valueData = NumberDataUtility.Create<U>();
            float rate = 0;

            for (int i = 0; i < values.Length; i++)
            {
                var statusData = values[i].StatusData;
                switch (statusData.TypeStatusData)
                {
                    case IStatusData.TYPE_STATUS_DATA.Value:
                        valueData.Value += statusData.GetValue().Value;
                        break;
                    case IStatusData.TYPE_STATUS_DATA.Rate:
                        rate += (float)statusData.GetValue().Value;
                        //Debug.Log(rate);
                        break;
                    case IStatusData.TYPE_STATUS_DATA.Absolute:
                        absoluteData.Value += statusData.GetValue().Value;
                        break;
                    default:
                        Debug.Assert(false, $"{statusData.TypeStatusData} TypeStatusData 가 범위에서 벗어났습니다");
                        break;
                }
            }

            //data + absolute
            if (!absoluteData.Value.IsZero)
            {                
                calData.Value = absoluteData.Value;
            }
            //data + data * rate + value
            else
            {
                calData.Value += (data.Value * rate) + valueData.Value;
            }
            return calData;
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