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
        public void MathTest_BigInteger_Divide()
        {
            BigInteger bigInt1 = new BigInteger(100);
            BigInteger bigInt2 = new BigInteger(70);
            Debug.Log(BigInteger.Divide(bigInt1, bigInt2));
        }

        [Test]
        public void MathTest_BigInteger_Moduler()
        {
            BigInteger bigInt1 = new BigInteger(100);
            BigInteger bigInt2 = new BigInteger(70);
            Debug.Log(bigInt1 % bigInt2);
        }

        [Test]
        public void MathTest_Float_Moduler()
        {
            float f1 = 0.1f;
            float f2 = 0.022f;
            Debug.Log(f1 % f2);
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
            var result1 = value1 + value2;
            Debug.Log(result1.GetDecimalValue());
            Assert.IsTrue(result1.GetDecimalValue() == (decimal)0.24690);

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            var result2 = value3 + value4;
            Debug.Log(result2.GetDecimalValue());
            Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.24645);

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            var result3 = value5 + value6;
            Debug.Log(result3.GetDecimalValue());
            Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.24645);

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            var result4 = value7 + value8;
            Debug.Log(result4.GetDecimalValue());
            Assert.IsTrue(result4.GetDecimalValue() == (decimal)1.12346);

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            var result5 = value9 + value10;
            Debug.Log(result5.GetDecimalValue());
            Assert.IsTrue(result5.GetDecimalValue() == (decimal)1);

        }

        [Test]
        public void MathTest_BigDecimal_Subject()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            var result1 = value1 - value2;
            Debug.Log(result1.GetDecimalValue());
            Assert.IsTrue(result1.GetDecimalValue() == (decimal)0);

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            var result2 = value3 - value4;
            Debug.Log(result2.GetDecimalValue());
            Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.00045);

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            var result3 = value5 - value6;
            Debug.Log(result3.GetDecimalValue());
            Assert.IsTrue(result3.GetDecimalValue() == (decimal)-0.00045);

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            var result4 = value7 - value8;
            Debug.Log(result4.GetDecimalValue());
            Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.87654);

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            var result5 = value9 - value10;
            Debug.Log(result5.GetDecimalValue());
            Assert.IsTrue(result5.GetDecimalValue() == (decimal)-1);

        }

        [Test]
        public void MathTest_BigDecimal_Multiply()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            var result1 = value1 * value2;
            Debug.Log(result1.GetDecimalValue());
            Assert.IsTrue(result1.GetDecimalValue() == (decimal)0.01524);

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            var result2 = value3 * value4;
            Debug.Log(result2.GetDecimalValue());
            Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.01518);

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            var result3 = value5 * value6;
            Debug.Log(result3.GetDecimalValue());
            Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.01518);

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            var result4 = value7 * value8;
            Debug.Log(result4.GetDecimalValue());
            Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.12346f);

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            var result5 = value9 * value10;
            Debug.Log(result5.GetDecimalValue());
            Assert.IsTrue(result5.GetDecimalValue() == (decimal)0);
        }
        [Test]
        public void MathTest_BigDecimal_Divide()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            var result1 = value1 / value2;
            Debug.Log(result1.GetDecimalValue());
            Assert.IsTrue(result1.GetDecimalValue() == (decimal)1);

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            var result2 = value3 / value4;
            Debug.Log(result2.GetDecimalValue());
            Assert.IsTrue(result2.GetDecimalValue() == (decimal)1.00365);

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            var result3 = value5 / value6;
            Debug.Log(result3.GetDecimalValue());
            Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.99635);

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            var result4 = value7 / value8;
            Debug.Log(result4.GetDecimalValue());
            Assert.IsTrue(result4.GetDecimalValue() == (decimal)8.1);

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            var result5 = value9 / value10;
            Debug.Log(result5.GetDecimalValue());
            Assert.IsTrue(result5.GetDecimalValue() == (decimal)0);
        }

        [Test]
        public void MathTest_BigDecimal_Moduler()
        {
            var value1 = new BigDecimal(0.12345f);
            var value2 = new BigDecimal(0.12345f);
            var result1 = value1 % value2;
            Debug.Log(result1.GetDecimalValue());
            Assert.IsTrue(result1.GetDecimalValue() == (decimal)0);

            var value3 = new BigDecimal(0.12345f);
            var value4 = new BigDecimal(0.123f);
            var result2 = value3 % value4;
            Debug.Log(result2.GetDecimalValue());
            Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.00045);

            var value5 = new BigDecimal(0.123f);
            var value6 = new BigDecimal(0.12345f);
            var result3 = value5 % value6;
            Debug.Log(result3.GetDecimalValue());
            Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.123);

            var value7 = new BigDecimal(1f);
            var value8 = new BigDecimal(0.1234567f);
            var result4 = value7 % value8;
            Debug.Log(result4.GetDecimalValue());
            Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.01235);

            var value9 = new BigDecimal(0.0000001f);
            var value10 = new BigDecimal(0.9999999f);
            var result5 = value9 % value10;
            Debug.Log(result5.GetDecimalValue());
            Assert.IsTrue(result5.GetDecimalValue() == (decimal)0.00000);
        }
    }
}
#endif