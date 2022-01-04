namespace SEF.Data
{
    using UnityEngine;

    public interface INumberData
    {
        string GetValue();
        INumberData Clone();
        void CleanUp();
    }

    public class NumberDataUtility
    {
        public static T Create<T>() where T : INumberData
        {
            return System.Activator.CreateInstance<T>();
        }

        public static T CreateAssetData<T>() where T : IAssetData
        {
            return System.Activator.CreateInstance<T>();
        }
    }
}