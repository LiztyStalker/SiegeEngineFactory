namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackerDamageValueStatusData : IStatusData
    {
        private UniversalBigNumberData _value;
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;

        public IAssetData[] AssetDataArray => null;

        public float ProductTime => -1f;

        public IStatusData.TYPE_STATUS_DATA TypeStatusData => _typeStatusData;

        public UniversalBigNumberData GetValue() => _value;

        public AttackerDamageValueStatusData(int value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _value = NumberDataUtility.Create<UniversalBigNumberData>();
            _value.ValueText = value.ToString();
            _value.SetValue();
            _typeStatusData = typeStatusData;
        }

        public AttackerDamageValueStatusData(float value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _value = NumberDataUtility.Create<UniversalBigNumberData>();
            _value.ValueText = value.ToString();
            _value.SetValue();
            _typeStatusData = typeStatusData;
        }
    }
}