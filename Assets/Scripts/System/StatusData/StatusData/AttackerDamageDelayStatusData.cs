namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackerDamageDelayStatusData : IStatusData
    {
        private UniversalBigNumberData _value;
        public IAssetData[] AssetDataArray => throw new System.NotImplementedException();

        public float ProductTime => throw new System.NotImplementedException();

        public IStatusData.TYPE_STATUS_DATA TypeStatusData => throw new System.NotImplementedException();

        public UniversalBigNumberData GetValue() => _value;
    }
}