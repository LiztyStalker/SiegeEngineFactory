namespace SEF.Process
{
    using SEF.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IProcessData
    {
        float ProcessTime { get; }
        public void SetValue(IAssetData data, float increaseValue, float processTime);
        public IAssetData GetAssetData(UpgradeData upgradeData);
    }
}