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
            //ClassName에 맞춰 데이터 가져오기
            var type = System.Type.GetType(_classTypeName);
            if (type != null) return GetData(type);
            return null;
        }

        public static IStatusData GetData(System.Type type)
        {
            return (IStatusData)System.Activator.CreateInstance(type);
        }
    }
}