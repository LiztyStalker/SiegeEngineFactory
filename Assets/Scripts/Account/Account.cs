namespace SEF.Account
{
    using Data;
    using Entity;
    using Storage;
    using Utility.IO;

    public class Account
    {
        private static Account _current = null;

        public static Account Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Account();
                }
                return _current;
            }
        }


        private AccountStorableData _storableData;


        public void LoadData(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            StorableDataIO.Current.LoadFileData_NotCrypto("test", loadCallback, (result, obj) =>
            //StorableDataIO.Current.LoadFileData("test", loadCallback, (result, obj) =>
            {
                endCallback?.Invoke(result);
                if (obj != null)
                {
                    _storableData = (AccountStorableData)obj;
                    UnityEngine.Debug.Log(_storableData);
                }
            });
        }

        public void SaveData(System.Action saveCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            //StorableDataIO.Current.SaveFileData(_storableData, "test", endCallback);
            StorableDataIO.Current.SaveFileData_NotCrypto(_storableData, "test", endCallback);
        }



        #region ##### StorableData #####

        public void SetStorableData(StorableData data) => _storableData.SaveData(data);

        public StorableData GetStorableData() => _storableData.LoadData();

        #endregion

    }

}