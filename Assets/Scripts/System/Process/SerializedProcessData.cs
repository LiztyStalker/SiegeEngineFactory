namespace SEF.Process
{
    using SEF.Data;
    using UnityEngine;

    [System.Serializable]
    public struct SerializedProcessData
    {
        [SerializeField]
        private string _classTypeName;
        [SerializeField]
        private SerializedAssetData _processAssetData;
        [SerializeField]
        private float _increaseValue;
        [SerializeField]
        private float _processTime;


        public IProcessData GetData()
        {
            var data = (IProcessData)System.Activator.CreateInstance(System.Type.GetType(_classTypeName));
            data.SetValue(_processAssetData.GetData(), _increaseValue, _processTime);
            return data;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static SerializedProcessData Create_Test(System.Type type)
        {
            return new SerializedProcessData(type);
        }

        private SerializedProcessData(System.Type type)
        {
            _classTypeName = type.FullName;
            _processAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
            _increaseValue = 1f;
            _processTime = 1f;
        }

        public void SetData(string classTypeName, string typeAssetData, string assetValue, string increaseValue, string processTime)
        {
            _classTypeName = $"SEF.Data.{classTypeName}ProcessData";
            _processAssetData.SetData(typeAssetData, assetValue);
            _increaseValue = int.Parse(increaseValue);
            _processTime = float.Parse(processTime);
        }

        public void GetData(out string classTypeName, out string typeAssetData, out string assetValue, out string increaseValue, out string processTime)
        {
            var split = _classTypeName.Split('.');
            var typeName = split[split.Length - 1].Replace("ProcessData", "");

            classTypeName = typeName;
            _processAssetData.GetData(out typeAssetData, out assetValue);
            increaseValue = _increaseValue.ToString();
            processTime = _processTime.ToString();
        }
#endif
    }
}