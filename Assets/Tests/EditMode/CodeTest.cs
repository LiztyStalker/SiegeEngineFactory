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

    public class CodeTest
    {
        [Test]
        public void CodeTest_AddRemoveListener_Lambda()
        {
            System.Action act = null;
            act += () => Debug.Log("Rhamda");
            act?.Invoke();
            act -= () => Debug.Log("Rhamda");
            act?.Invoke();
        }

        [Test]
        public void CodeTest_Lambda_Action()
        {
            System.Action act = () => Debug.Log("Rhamda");
            act?.Invoke();
        }

        [Test]
        public void CodeTest_AddRemoveListener_Delegate()
        {
            System.Action act = null;
            act += delegate { Debug.Log("Rhamda"); };
            act?.Invoke();
            act -= delegate { Debug.Log("Rhamda"); };
            act?.Invoke();
        }


        [Test]
        public void CodeTest_Delegate_Action()
        {
            System.Action act = delegate { Debug.Log("Rhamda"); };
            act?.Invoke();
        }

        [Test]
        public void CodeTest_Predicate_Action()
        {
            System.Predicate<int> pre = value => value > 0;
            Debug.Log(pre?.Invoke(1));
        }


    }
}
#endif