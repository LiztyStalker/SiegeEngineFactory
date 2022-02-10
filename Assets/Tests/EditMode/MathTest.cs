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
    }
}
#endif