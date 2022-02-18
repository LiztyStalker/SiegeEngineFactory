namespace SEF.Account
{
    using Utility.IO;

    [System.Serializable]
    public struct AccountStorableData
    {
        [UnityEngine.SerializeField] private StorableData _storableData;
        public StorableData LoadData()
        {
            //UnityEngine.Debug.Log("LoadData " + _storableData);
            return _storableData;
        }
        public void SaveData(StorableData data)
        {
            //UnityEngine.Debug.Log("SaveData " + data);
            _storableData = data;
        }
    }
}
