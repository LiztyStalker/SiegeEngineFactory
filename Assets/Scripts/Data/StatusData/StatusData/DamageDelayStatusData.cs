namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DamageDelayStatusData : IStatusData
    {
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;
        private float _value;

        public IAssetData[] AssetDataArray => null;
        public float ProductTime => 0f;

        public IStatusData.TYPE_STATUS_DATA TypeStatusData => _typeStatusData;
        public UniversalBigNumberData GetValue()
        {
            throw new System.NotImplementedException();
        }

        public DamageDelayStatusData(float value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _value = value;
            _typeStatusData = typeStatusData;
        }
    }
}