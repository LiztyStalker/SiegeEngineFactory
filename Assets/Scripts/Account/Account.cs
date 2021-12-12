namespace SEF.Account
{
    using Storage;

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
                    _current.Initialize();
                }
                return _current;
            }
        }

        private void Initialize()
        {
            //AccountData √ ±‚»≠
        }

        public void Load(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            AccountIO.Load(loadCallback, endCallback);
        }

        public void Save(System.Action saveCallback, System.Action endCallback)
        {
            AccountIO.Save(saveCallback, endCallback);
        }

        public void Dispose()
        {
            _current = null;
        }
    }

    internal class AccountIO
    {

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static void Load(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback) 
        {
            var loader = DataLoader.Create();
            loader.LoadTest(loadCallback, result =>
            {
                endCallback?.Invoke(result);
                loader.Dispose();
            });
        }

        public static void Save(System.Action saveCallback, System.Action endCallback) 
        {
            saveCallback?.Invoke();
            endCallback?.Invoke();
        }
#else

        public static void Load(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback) 
        {
            var loader = DataLoader.Create();
            loader.Load(loadCallback, result =>
            {
                endCallback?.Invoke(result);
                loader.Dispose();
            });
        }

        public static void Save(System.Action saveCallback, System.Action endCallback) 
        {
            saveCallback?.Invoke();
            endCallback?.Invoke();
        }

#endif
    }
}