namespace SEF.Data
{
    using UnityEngine;

    [System.Serializable]
    public struct SerializedAssetData
    {
        public enum TYPE_ASSET_DATA_ATTRIBUTE { Gold, Meteorite, Resource, Research, Population }

        [SerializeField]
        private string _typeAssetData;

        //_typeAssetData로 변경 예정
        //Editor 변경 필요
        [SerializeField]
        private TYPE_ASSET_DATA_ATTRIBUTE _assetData;

        [SerializeField]
        private string _assetValue;

        public IAssetData GetData()
        {
            var type = System.Type.GetType($"SEF.Data.{_assetData}AssetData");
            if (type != null)
            {
                var assetData = (IAssetData)System.Activator.CreateInstance(type);
                assetData.SetValue(_assetValue);
                return assetData;
            }
            return null;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static SerializedAssetData Create_Test(TYPE_ASSET_DATA_ATTRIBUTE typeAssetDataAttribute, string value)
        {
            return new SerializedAssetData(typeAssetDataAttribute, value);
        }

        private SerializedAssetData(TYPE_ASSET_DATA_ATTRIBUTE typeAssetDataAttribute, string value)
        {
            _typeAssetData = typeAssetDataAttribute.ToString();
            _assetData = typeAssetDataAttribute;
            _assetValue = value;
        }

        public static IAssetData GetData(System.Type type, int value)
        {
            var assetData = (IAssetData)System.Activator.CreateInstance(type);
            assetData.SetValue(value.ToString());
            return assetData;
        }

        public void SetData(string typeAssetData, string assetValue)
        {
            _typeAssetData = typeAssetData; //"SEF.Data.{Name}AssetData"
            _assetData = (TYPE_ASSET_DATA_ATTRIBUTE)System.Enum.Parse(typeof(TYPE_ASSET_DATA_ATTRIBUTE), typeAssetData);
            _assetValue = assetValue;
        }

        public void GetData(out string typeAssetData, out string assetValue)
        {
            typeAssetData = _typeAssetData;
            assetValue = _assetValue;
        }
#endif
    }
}