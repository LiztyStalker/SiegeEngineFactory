namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class UniversalBigNumberData : BigNumberData
    {
        //¼Ò¼ýÁ¡
        private int _dot = 0;
        public UniversalBigNumberData() : base() { }
        //public UniversalBigNumberData(double value)
        //{
        //    Value = ConvertToBigInteger((decimal)value);
        //}
        //public UniversalBigNumberData(float value)
        //{
        //    Value = ConvertToBigInteger((decimal)value);
        //}
        //public UniversalBigNumberData(decimal value)
        //{
        //    Value = ConvertToBigInteger(value);
        //}
        //public UniversalBigNumberData(int value)
        //{
        //    Value = ConvertToBigInteger(value);
        //}

        //public decimal GetDecimalValue()
        //{
        //    return ((decimal)Value) * (decimal)Mathf.Pow(0.1f, _dot);

        //}

        //private System.Numerics.BigInteger ConvertToBigInteger(decimal value)
        //{
        //    int integer = 0;
        //    CalculateDot((decimal)value, out _dot, out integer);
        //    return new System.Numerics.BigInteger(integer);
        //}

        //public static void CalculateDot(decimal value, out int dot, out int integer)
        //{
        //    decimal nowValue = value;
        //    dot = 0;
        //    while (true)
        //    {
        //        if (nowValue % 1 == 0)
        //        {
        //            break;
        //        }
        //        nowValue *= 10;
        //        dot++;
        //    }
        //    integer = (int)nowValue;
        //}

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

        public static UniversalBigNumberData Create(int value)
        {
            var data = new UniversalBigNumberData();
            data.ValueText = value.ToString();
            return data;
        }
#endif
    }
}