namespace SEF.Status
{
    using Data;
    using Editor;
    using UnityEngine;

    [System.Serializable]
    public struct SerializedStatusData
    {
        [SerializeField, StatusDataListToPopup]
        private string _classTypeName;

        [SerializeField]
        private IStatusData.TYPE_STATUS_DATA _typeStatusData;

        [SerializeField]
        private float _startValue;

        [SerializeField]
        private float _increaseValue;


        internal IStatusData GetSerializeData()
        {
            var type = System.Type.GetType(_classTypeName);
            if (type != null)
            {
                var data = GetData(type);
                data.SetValue(_startValue, _increaseValue, _typeStatusData);
                return data;
            }
            return null;
        }

        public static IStatusData GetData(System.Type type)
        {
            return (IStatusData)System.Activator.CreateInstance(type);
        }
    }
}