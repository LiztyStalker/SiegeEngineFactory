namespace SEF.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;
    using UnityEngine;
    using Utility.IO;

    [System.Serializable]
    public class UniversalBigNumberData : BigNumberData
    {
        //¼Ò¼ýÁ¡
        //private int _dot = 0;
        public UniversalBigNumberData() : base() { }
        public UniversalBigNumberData(double value)
        {
            Value = new BigDecimal(value);
        }
        public UniversalBigNumberData(float value)
        {
            Value = new BigDecimal(value);
        }
        public UniversalBigNumberData(decimal value)
        {
            Value = new BigDecimal(value);
        }
        public UniversalBigNumberData(int value)
        {
            Value = new BigDecimal(value);
        }
        public UniversalBigNumberData(BigDecimal bigdec)
        {
            Value = bigdec;
        }
        public UniversalBigNumberData(BigInteger bigdec)
        {
            Value = new BigDecimal(bigdec);
        }

        protected UniversalBigNumberData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new UniversalBigNumberData(this);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static UniversalBigNumberData Create_Test()
        {
            var data = new UniversalBigNumberData();
            data.ValueText = "100";
            return data;
        }

        public static UniversalBigNumberData Create_Test(int value)
        {
            var data = new UniversalBigNumberData();
            data.ValueText = value.ToString();
            return data;
        }
#endif

        public static UniversalBigNumberData Create(int value)
        {
            var data = new UniversalBigNumberData();
            data.ValueText = value.ToString();
            return data;
        }

        public static UniversalBigNumberData Create(float value)
        {
            var data = new UniversalBigNumberData();
            data.ValueText = value.ToString();
            return data;
        }
    }

    public class UniversalAssetData : IAssetData
    {
        private UniversalBigNumberData _value;

        public UniversalAssetData(UniversalBigNumberData data)
        {
            _value = data;
        }

        public BigInteger AssetValue { get => _value.Value.Value; set => _value.SetValue(value.ToString()); }

        public Type AccumulativelyGetStatisticsType() => null;

        public Type AccumulativelyUsedStatisticsType() => null;

        public void CleanUp() { }

        public INumberData Clone() => null;
    
        public StorableData GetStorableData() => null;

        public string GetValue() => AssetValue.ToString();

        public void SetCompoundInterest(float nowValue, float rate, int length = 1) { }

        public void SetIsolationInterest(float nowValue, int length = 1) { }

        public void SetValue(string value) { }
    }
}