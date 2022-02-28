namespace SEF.Manager
{
    using System.Collections.Generic;
    using Data;

    public struct RewardAssetPackage
    {
        private List<IAssetData> _list;

        public void AddAssetData(IAssetData assetData)
        {
            if (_list == null) _list = new List<IAssetData>();

            var index = _list.FindIndex(data => data.GetType() == assetData.GetType());
            if (index == -1)
            {
                _list.Add((IAssetData)assetData.Clone());
            }
            else
            {
                var data = _list[index];
                data.AssetValue += assetData.AssetValue;
                _list[index] = data;
            }
        }

        public IAssetData[] GetAssetArray() => _list.ToArray();
    }
}