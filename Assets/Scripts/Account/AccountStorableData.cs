namespace SEF.Account
{
    using Utility.IO;

    [System.Serializable]
    public struct AccountStorableData
    {
        [UnityEngine.SerializeField] private StorableData _storableData;
        public StorableData LoadData() => _storableData;
        public void SaveData(StorableData data)
        {
            _storableData = data;
        }
    }
}
