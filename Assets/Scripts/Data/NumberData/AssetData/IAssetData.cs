namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public interface IAssetData
    {
        BigInteger AssetValue { get; set; }
        string GetValue();
    }
}