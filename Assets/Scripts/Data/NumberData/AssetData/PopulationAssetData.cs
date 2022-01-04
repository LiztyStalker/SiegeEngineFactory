namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public class PopulationAssetData : NumberData, IAssetData
    {
        public BigInteger AssetValue { get => new BigInteger(Value); set => Value = int.Parse(value.ToString()); }

        public PopulationAssetData() 
        {
            Value = 1;
        }
        public PopulationAssetData(int value) { Value = value; }
        private PopulationAssetData(PopulationAssetData data) 
        {
            Value = data.Value;
        }

        public override INumberData Clone()
        {
            return new PopulationAssetData(this);
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static PopulationAssetData Create_Test()
        {
            var data = new PopulationAssetData();
            data.Value = 1;
            return data;
        }

        public static PopulationAssetData Create_Test(int value)
        {
            var data = new PopulationAssetData();
            data.Value = value;
            return data;
        }
#endif
    }
}