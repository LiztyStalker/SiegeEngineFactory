namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;
    using UnityEngine;

    [System.Serializable]
    public class UniversalBigNumberData : BigNumberData
    {
        //¼Ò¼ýÁ¡
        //private int _dot = 0;
        public UniversalBigNumberData() : base() { }
        public UniversalBigNumberData(double value)
        {
            Value = new System.Numerics.BigDecimal(value);
        }
        public UniversalBigNumberData(float value)
        {
            Value = new System.Numerics.BigDecimal(value);
        }
        public UniversalBigNumberData(decimal value)
        {
            Value = new System.Numerics.BigDecimal(value);
        }
        public UniversalBigNumberData(int value)
        {
            Value = new System.Numerics.BigDecimal(value);
        }
        public UniversalBigNumberData(BigDecimal bigdec)
        {
            Value = bigdec;
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
}