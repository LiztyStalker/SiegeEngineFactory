namespace SEF.Process 
{
    using SEF.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AssetProcessData : IProcessData    
    {

        private IAssetData _assetData;
        private float _increaseValue;
        private float _increaseRate;
        private float _processTime;

        public float ProcessTime => _processTime;

        public void SetValue(IAssetData data, float increaseValue, float increaseRate, float processTime)
        {
            _assetData = data;
            _increaseValue = increaseValue;
            _increaseRate = increaseRate;
            _processTime = processTime;
        }

        public IAssetData GetAssetData(UpgradeData upgradeData)
        {
            var data = (IAssetData)_assetData.Clone();
            data.SetCompoundInterest(_increaseValue, _increaseRate, upgradeData.Value);
            return data;
        }

    }
}