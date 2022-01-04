namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public class PopulationAssetData : NumberData, IAssetData
    {
        public BigInteger AssetValue {
            get
            {
                return new BigInteger(Value);
            }
            set
            {
                Value = int.Parse(value.ToString());
            }
        }

        public override INumberData Clone()
        {
            return new PopulationAssetData();
        }
    }
}