namespace SEF.Data
{
    using UnityEngine;

    public class AssetData : BigNumberData
    {
        public AssetData() : base() { }
        protected AssetData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new AssetData(this);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AssetData Create_Test()
        {
            var data = new AssetData();
            data.Value = 100;
            return data;
        }
#endif
    }
}