namespace SEF.Process
{
    using SEF.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public struct SerializedProcessData
    {
        [SerializeField]
        private string _classTypeName;// System.Type _classTypeName;
        [SerializeField]
        private SerializedAssetData _processAssetData;
        [SerializeField]
        private float _increaseValue;
        [SerializeField]
        private float _increaseRate;
        [SerializeField]
        private float _processTime;


        public IProcessData GetData()
        {
            var data = (IProcessData)System.Activator.CreateInstance(System.Type.GetType(_classTypeName));
            data.SetValue(_processAssetData.GetData(), _increaseValue, _increaseRate, _processTime);
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
            _increaseRate = 0.125f;
            _processTime = 1f;
        }
#endif
    }
}