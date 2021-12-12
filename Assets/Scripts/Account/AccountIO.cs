namespace SEF.Account
{
    using Storage;
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