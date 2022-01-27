namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;

    public interface IStatusData
    {
        public enum TYPE_STATUS_DATA { Value, Rate, Absolute}

        public IAssetData[] AssetDataArray { get; }
        public float ProductTime { get; }

        public TYPE_STATUS_DATA TypeStatusData { get; }
        public UniversalBigNumberData GetValue();
    }
}