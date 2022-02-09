namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HealthDataStatusData : IStatusData
    {
        private UniversalBigNumberData _value;
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;

        public IAssetData[] AssetDataArray => null;

        public float ProductTime => -1f;

        public IStatusData.TYPE_STATUS_DATA TypeStatusData => _typeStatusData;

        public UniversalBigNumberData GetValue() => _value;


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static HealthDataStatusData Create(int value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            return new HealthDataStatusData(value, typeStatusData);
        }

#endif

        public HealthDataStatusData(int value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _value = NumberDataUtility.Create<UniversalBigNumberData>();
            _value.ValueText = value.ToString();
            _value.SetValue();
            _typeStatusData = typeStatusData;
        }

        public HealthDataStatusData(float value, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            _value = NumberDataUtility.Create<UniversalBigNumberData>();
            _value.ValueText = value.ToString();
            _value.SetValue();
            _typeStatusData = typeStatusData;
        }
    }
}