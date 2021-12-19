#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test {

    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using System.Numerics;
    using Data;

    public class BigNumberDataTest
    {

        private class TestBigNumber_Data : BigNumberData
        {
            public override INumberData Clone()
            {
                return new TestBigNumber_Data();
            }
        }


        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

 
        [Test]
        public void BigNumberDataTest_BigNumberDigit()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = BigInteger.Pow(ulong.MaxValue, 1000);
            Debug.Log(assetData.GetDigitValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_999()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 999;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_1A()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 1000;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_10A()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 10000;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_100A()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 100000;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_1B()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 1000000;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_123456()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 123456;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_1111111()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 1111111;
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_MaxZ()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < 27; i++)
            {
                builder.Append("999");
            }
            assetData.Value = BigInteger.Parse(builder.ToString());
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_MaxZZ()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for(int i = 0; i < 703; i++)
            {
                builder.Append("999");
            }
            assetData.Value = BigInteger.Parse(builder.ToString());
            Debug.Log(assetData.GetValue());
        }

        [Test]
        public void BigNumberDataTest_Digit_MaxZZZ()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < 18279; i++)
            {
                builder.Append("999");
            }
            assetData.Value = BigInteger.Parse(builder.ToString());
            Debug.Log(assetData.GetValue());
        }


        //[Test]
        //public void BigNumberDataTest_Digit_MaxZZZZ()
        //{
        //    var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
        //    System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //    for (int i = 0; i < 475255; i++)
        //    {
        //        builder.Append("999");
        //    }
        //    assetData.Value = BigInteger.Parse(builder.ToString());
        //    Debug.Log(assetData.GetValue());
        //}

        [Test]
        public void BigNumberDataTest_TotalDigit_987654321012()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 987654321012;
            Debug.Log(assetData.GetDigitValue());
        }

        [Test]
        public void BigNumberDataTest_TotalDigit_222()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 222;
            Debug.Log(assetData.GetDigitValue());
        }


        [Test]
        public void BigNumberDataTest_TotalDigit_21214115()
        {
            var assetData = NumberDataUtility.Create<TestBigNumber_Data>();
            assetData.Value = 21214115;
            Debug.Log(assetData.GetDigitValue());
        }

        [Test]
        public void BigNumberDataTest_Sum()
        {
            var q11 = NumberDataUtility.Create<TestBigNumber_Data>();
            q11.Value = 1234;
            var q12 = NumberDataUtility.Create<TestBigNumber_Data>();
            q12.Value = 1234;
            q11.Value += q12.Value;
            Debug.Log(q11.GetValue());
            Assert.IsTrue(q11.Value == 2468);

            var q21 = NumberDataUtility.Create<TestBigNumber_Data>();
            q21.Value = 6789;
            var q22 = NumberDataUtility.Create<TestBigNumber_Data>();
            q22.Value = 6789;
            q21.Value += q22.Value;
            Debug.Log(q21.GetValue());
            Assert.IsTrue(q21.Value == 13578);


            var q31 = NumberDataUtility.Create<TestBigNumber_Data>();
            q31.Value = 999;
            var q32 = NumberDataUtility.Create<TestBigNumber_Data>();
            q32.Value = 999;
            q31.Value += q32.Value;
            Debug.Log(q31.GetValue());
            Assert.IsTrue(q31.Value == 1998);


            var q41 = NumberDataUtility.Create<TestBigNumber_Data>();
            q41.Value = 999000;
            var q42 = NumberDataUtility.Create<TestBigNumber_Data>();
            q42.Value = 999000;
            q41.Value += q42.Value;
            Debug.Log(q41.GetValue());
            Assert.IsTrue(q41.Value == 1998000);


            var q51 = NumberDataUtility.Create<TestBigNumber_Data>();
            q51.Value = 1000000;
            var q52 = NumberDataUtility.Create<TestBigNumber_Data>();
            q52.Value = 1000;
            q51.Value += q52.Value;
            Debug.Log(q51.GetValue());
            Assert.IsTrue(q51.Value == 1001000);
        }


        [Test]
        public void BigNumberDataTest_Subject()
        {
            var q11 = NumberDataUtility.Create<TestBigNumber_Data>();
            q11.Value = 1234;
            var q12 = NumberDataUtility.Create<TestBigNumber_Data>();
            q12.Value = 111;
            q11.Value -= q12.Value;
            Debug.Log(q11.GetValue());
            Assert.IsTrue(q11.Value == 1123);

            var q21 = NumberDataUtility.Create<TestBigNumber_Data>();
            q21.Value = 12340;
            var q22 = NumberDataUtility.Create<TestBigNumber_Data>();
            q22.Value = 999;
            q21.Value -= q22.Value;
            Debug.Log(q21.GetValue());
            Assert.IsTrue(q21.Value == 11341);


            var q31 = NumberDataUtility.Create<TestBigNumber_Data>();
            q31.Value = 999;
            var q32 = NumberDataUtility.Create<TestBigNumber_Data>();
            q32.Value = 999;
            q31.Value -= q32.Value;
            Debug.Log(q31.GetValue());
            Assert.IsTrue(q31.Value == 0);


            var q41 = NumberDataUtility.Create<TestBigNumber_Data>();
            q41.Value = 12340;
            var q42 = NumberDataUtility.Create<TestBigNumber_Data>();
            q42.Value = 6666;
            q41.Value -= q42.Value;
            Debug.Log(q41.GetValue());
            Assert.IsTrue(q41.Value == 5674);


            var q51 = NumberDataUtility.Create<TestBigNumber_Data>();
            q51.Value = 1000000000000;
            var q52 = NumberDataUtility.Create<TestBigNumber_Data>();
            q52.Value = 1;
            q51.Value -= q52.Value;
            Debug.Log(q51.GetValue());
            Assert.IsTrue(q51.Value == 999999999999);
        }


    }
}
#endif