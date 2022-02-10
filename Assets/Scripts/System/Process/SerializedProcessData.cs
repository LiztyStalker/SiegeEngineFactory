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
    }
}