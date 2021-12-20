namespace SEF.Data
{
    using UnityEngine;
    public enum TYPE_ASSET { Gold, Ore, Resource, Meteorite }
    public class AssetData : BigNumberData
    {

        private TYPE_ASSET _typeAsset;
        public TYPE_ASSET TypeAsset => _typeAsset;
        public AssetData() : base() { }
        protected AssetData(BigNumberData value) : base(value) { }

        public void SetTypeAsset(TYPE_ASSET typeAsset)
        {
            _typeAsset = typeAsset;
        }

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
        public static AssetData Create_Test(TYPE_ASSET typeAsset, int value)
        {
            var data = new AssetData();
            data.SetTypeAsset(typeAsset);
            data.Value = value;
            return data;
        }

#endif
    }
}