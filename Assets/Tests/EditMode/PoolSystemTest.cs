#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using PoolSystem;


    public class PoolElement_Test : IPoolElement
    {
        public static PoolElement_Test Create()
        {
            return new PoolElement_Test();
        }
    }



    public class PoolSystemTest
    {

        private PoolSystem<PoolElement_Test> _pool;


        [SetUp]
        public void SetUp()
        {
            _pool = PoolSystem<PoolElement_Test>.Create();
            _pool.Initialize(PoolElement_Test.Create);
        }

        [TearDown]
        public void TearDown()
        {
            _pool.CleanUp();
        }

        [Test]
        public void PoolSystemTest_Give()
        {
            var element = _pool.GiveElement();
            Debug.Log(element.GetHashCode());
        }

        [Test]
        public void PoolSystemTest_Retrieve()
        {
            var element = _pool.GiveElement();
            _pool.RetrieveElement(element);
        }

        [Test]
        public void PoolSystemTest_RetrieveToGive()
        {
            var element = _pool.GiveElement();
            var hash1 = element.GetHashCode();
            _pool.RetrieveElement(element);
            element = _pool.GiveElement();
            var hash2 = element.GetHashCode();

            Assert.AreEqual(hash1, hash2);


            element = _pool.GiveElement();
            var hash3 = element.GetHashCode();

            Assert.AreNotEqual(hash1, hash3);
        }
    }
}
#endif