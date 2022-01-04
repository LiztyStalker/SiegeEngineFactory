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
    }
}