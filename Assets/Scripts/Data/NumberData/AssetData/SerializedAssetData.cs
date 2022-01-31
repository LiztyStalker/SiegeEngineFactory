namespace SEF.Data
{
    using UnityEngine;

    [System.Serializable]
    public struct SerializedAssetData
    {
        public enum TYPE_ASSET_DATA_ATTRIBUTE { Gold, Meteorite, Resource, Research, Population }

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
            _assetData = typeAssetDataAttribute;
            _assetValue = value;
        }

        public static IAssetData GetData(System.Type type, int value)
        {
            var assetData = (IAssetData)System.Activator.CreateInstance(type);
            assetData.SetValue(value.ToString());
            return assetData;
        }
#endif
    }
}