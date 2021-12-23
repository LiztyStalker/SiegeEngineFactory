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
    }
}
#endif