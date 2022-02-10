namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public interface IAssetData : INumberData
    {
        BigInteger AssetValue { get; set; }
        void SetValue(string value);
        System.Type GetType();
        System.Type UsedStatisticsType();
        System.Type GetStatisticsType();
        System.Type AccumulateStatisticsType();
        void SetCompoundInterest(float nowValue, float rate, int length = 1);
    }
}