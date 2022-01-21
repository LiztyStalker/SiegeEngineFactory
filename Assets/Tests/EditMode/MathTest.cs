#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Unity.Mathematics;
    using System.Numerics;
    using Data;
    using Utility;

    public class MathTest
    {


        [UnityTest]

        public IEnumerator MathTest_Power_Test()
        {
            var value = 100;
            var power = 100;


            Debug.Log("Start");
            BigInteger mathfPow = BigInteger.Pow(value, power);
            Debug.Log("mathfPow : " + mathfPow);

            yield return null;
        }

        [UnityTest]
        public IEnumerator MathTest_Power_100_Test()
        {
            int count = 0;
            while (true)
            {
                MathTest_Power_Test();
                if (count > 100)
                    break;
                count++;
                yield return null;
            }
            yield return null;
        }

        [Test]
        public void MathTest_Float_To_BigInteger()
        {
            BigInteger bigInt1 = new BigInteger(0.1f);
            Debug.Log(bigInt1);

            BigInteger bigInt2 = new BigInteger(1.1f);
            Debug.Log(bigInt2);
        }

        [Test]
        public void MathTest_Float_Under_Count()
        {
            int dot = 0;
            float value = 0.12345f;
            Debug.Log(value);
            while (true)
            {
                if(value % 1 == 0)
                {
                    break;
                }
                value *= 10;
                dot++;
            }
            Debug.Log(value + " " + dot);
        }

        [Test]
        public void MathTest_BigDecimal_Value_Positive()
        {
            var value = new BigDecimal(0.12345f);
            Debug.Log(value.GetDecimalValue());
        }

        [Test]
        public void MathTest_BigDecimal_Value_Negative()
        {
            var value = new BigDecimal(-0.12345f);
            Debug.Log(value);
            Debug.Log(value.GetDecimalValue());
        }

        [Test]
        public void MathTest_BigDecimal_Add()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            Debug.Log((value1 + value2).GetDecimalValue());

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            Debug.Log((value3 + value4).GetDecimalValue());

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            Debug.Log((value5 + value6).GetDecimalValue());

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            Debug.Log((value7 + value8).GetDecimalValue());

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            Debug.Log((value9 + value10).GetDecimalValue());

        }

        [Test]
        public void MathTest_BigDecimal_Subject()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            Debug.Log((value1 - value2).GetDecimalValue());

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            Debug.Log((value3 - value4).GetDecimalValue());

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            Debug.Log((value5 - value6).GetDecimalValue());

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            Debug.Log((value7 - value8).GetDecimalValue());

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            Debug.Log((value9 - value10).GetDecimalValue());

        }

        [Test]
        public void MathTest_BigDecimal_Multiply()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            Debug.Log((value1 * value2).GetDecimalValue());

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            Debug.Log((value3 * value4).GetDecimalValue());

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            Debug.Log((value5 * value6).GetDecimalValue());

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            Debug.Log((value7 * value8).GetDecimalValue());

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            Debug.Log((value9 * value10).GetDecimalValue());
        }
        [Test]
        public void MathTest_BigDecimal_Divide()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            Debug.Log((value1 / value2).GetDecimalValue());

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            Debug.Log((value3 / value4).GetDecimalValue());

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            Debug.Log((value5 / value6).GetDecimalValue());

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            Debug.Log((value7 / value8).GetDecimalValue());

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            Debug.Log((value9 / value10).GetDecimalValue());
        }

    }
}
#endif