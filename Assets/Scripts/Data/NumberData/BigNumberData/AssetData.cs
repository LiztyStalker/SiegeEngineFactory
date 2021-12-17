namespace SEF.Data
{
    using UnityEngine;

    public class AssetData : BigNumberData
    {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AssetData Create_Test()
        {
            var data = new AssetData();
            data.Clear();
            data.Value.Add("0", 100);
            return data;
        }
#endif
    }
}