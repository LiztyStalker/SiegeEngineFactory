#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace Storage
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UtilityManager;

    public class DataStorageTest
    {
        [Test]
        public void DataStorageTest_GetBulletData()
        {
            var data = DataStorage.Instance.GetDataOrNull<BulletData>("BulletData_Arrow", null, null);
            Assert.IsNotNull(data);
            DataStorage.Dispose();
        }
    }
}
#endif