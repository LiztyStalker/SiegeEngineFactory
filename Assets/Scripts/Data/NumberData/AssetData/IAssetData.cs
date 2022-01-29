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
        void SetValue(string value);
        System.Type GetType();
        System.Type UsedStatisticsType();
        System.Type GetStatisticsType();
        System.Type AccumulateStatisticsType();
    }
}