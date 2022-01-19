#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Data;
    using Entity;


    public class StatusPackageTest
    {

        private class StatusProvider_Test : IStatusProvider {}

        private class StatusData_Test : IStatusData
        {
            public IAssetData[] AssetDataArray{
                get
                {
                    var arr = new IAssetData[1];
                    arr[0] = GoldAssetData.Create_Test();
                    return arr;
                }
            }

            public float ProductTime => 1f;
        }

        private StatusPackage _statusPackage;
        

        [SetUp]
        public void SetUp()
        {
            _statusPackage = StatusPackage.Create();
            _statusPackage.Initialize();

            Assert.IsNotNull(_statusPackage, "StatusPackage 를 생성하지 못했습니다");

        }

        [TearDown]
        public void TearDown()
        {
            _statusPackage.CleanUp();
        }

        [Test]
        public void StatusPackageTest_Initialize()
        {
        }

        [Test]
        public void StatusPackageTest_AddStatusData()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_AddStatusData_5()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider3, statusData3);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider3), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider4 = new StatusProvider_Test();
            var statusData4 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider4, statusData4);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider4), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider5 = new StatusProvider_Test();
            var statusData5 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider5, statusData5);
            Assert.IsTrue(_statusPackage.HasStatusProvider(statusProvider5), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_AddStatusData_And_UpdateStatusData()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData1), "StatusProvider 가 저장되지 않았습니다");
            
            var statusData2 = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData2), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_GetStatusData()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToAssetData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test());
            Debug.Log(value.GetValue());
        }

        [UnityTest]
        public IEnumerator StatusPackageTest_RunProcess()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            bool isRun = true;
            _statusPackage.AddOnProductListener(delegate
            {
                Debug.Log("End");
                isRun = false;
            });
            while (isRun)
            {
                _statusPackage.RunProcess(Time.deltaTime);
                yield return null;
            }

            yield return null;
        }

    }
}
#endif