namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackerDamageDelayStatusData : IStatusData
    {
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;
        private UniversalBigNumberData _value;

        public IAssetData[] AssetDataArray => null;
        public float ProductTime => 0f;

        public IStatusData.TYPE_STATUS_DATA TypeStatusData => _typeStatusData;
        public UniversalBigNumberData GetValue() => _value;

        public AttackerDamageDelayStatusData(float value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            if (typeStatusData == IStatusData.TYPE_STATUS_DATA.Absolute)
                _value = UniversalBigNumberData.Create(value);
            else
                _value = UniversalBigNumberData.Create(-value);
            _typeStatusData = typeStatusData;
        }
    }
}