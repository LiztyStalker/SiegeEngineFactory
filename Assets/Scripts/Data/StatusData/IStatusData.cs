namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;

    public interface IStatusData
    {
        public IAssetData[] AssetDataArray { get; }
        public float ProductTime { get; }
    }
}