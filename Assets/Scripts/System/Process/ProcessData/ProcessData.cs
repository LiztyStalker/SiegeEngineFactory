namespace SEF.Process 
{
    using Data;

    public class AssetProcessData : IProcessData    
    {

        private IAssetData _assetData;
        private float _increaseValue;
        private float _processTime;

        public float ProcessTime => _processTime;

        public void SetValue(IAssetData data, float increaseValue, float processTime)
        {
            _assetData = data;
            _increaseValue = increaseValue;
            _processTime = processTime;
        }

        public IAssetData GetAssetData(UpgradeData upgradeData)
        {
            var data = (IAssetData)_assetData.Clone();
            data.SetIsolationInterest(_increaseValue, upgradeData.Value);
            return data;
        }

        public IAssetData GetAssetData(UpgradeData upgradeData, int processCount)
        {
            var data = GetAssetData(upgradeData);
            data.AssetValue *= processCount;
            return data;
        }
    }
}