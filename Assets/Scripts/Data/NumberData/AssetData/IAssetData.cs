namespace SEF.Data
{
    using UnityEngine;
    using System.Numerics;
    using Utility.IO;

    #region ##### StorableData #####

    [System.Serializable]
    public class AssetEntityStorableData : StorableData
    {
        internal void SetData(StorableData child)
        {
            Children = new StorableData[1];
            Children[0] = child;
        }
    }

    [System.Serializable]
    public class AssetDataStorableData : StorableData
    {
        [SerializeField] private string _type;
        [SerializeField] private string _value;

        public string @Type => _type;
        public string Value => _value;

        internal void SetData(string type, string value)
        {
            _type = type;
            _value = value;
        }

        public IAssetData GetAssetData()
        {
            var type = System.Type.GetType(_type);
            if(type != null)
            {
                var assetData = (IAssetData)System.Activator.CreateInstance(type);
                assetData.SetValue(_value);
                return assetData;
            }
            return null;
        }
    }
    #endregion

    public interface IAssetData : INumberData
    {
        BigInteger AssetValue { get; set; }
        void SetValue(string value);
        System.Type GetType();
        System.Type AccumulativelyUsedStatisticsType();
        System.Type AccumulativelyGetStatisticsType();
        //void SetCompoundInterest(float nowValue, float rate, int length = 1);
        void SetIsolationInterest(float increaseValue, float increaseRate, int length = 1);
        StorableData GetStorableData();
    }
}