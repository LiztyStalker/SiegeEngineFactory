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

            private IStatusData.TYPE_STATUS_DATA _typeStatusData;

            public void SetTypeStatusData(IStatusData.TYPE_STATUS_DATA typeStatusData)
            {
                _typeStatusData = typeStatusData;
            }

            public IStatusData.TYPE_STATUS_DATA TypeStatusData => _typeStatusData;

            private UniversalBigNumberData _value;

            public void SetValue(UniversalBigNumberData assetData)
            {
                _value = assetData;
            }

            public UniversalBigNumberData GetValue() => _value;
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
        public void StatusPackageTest_GetStatusData_Absolute()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            statusData.SetValue(UniversalBigNumberData.Create_Test());
            statusData.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.100A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Absolute_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(UniversalBigNumberData.Create_Test());
            statusData1.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(UniversalBigNumberData.Create_Test());
            statusData2.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            statusData3.SetValue(UniversalBigNumberData.Create_Test());
            statusData3.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(statusProvider3, statusData3);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.300A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            statusData.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.010A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData1.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData2.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            statusData3.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData3.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider3, statusData3);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.030A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Value()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            statusData.SetValue(UniversalBigNumberData.Create(100));
            statusData.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider, statusData);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.100A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Value_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(UniversalBigNumberData.Create(100));
            statusData1.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(UniversalBigNumberData.Create(100));
            statusData2.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            statusData3.SetValue(UniversalBigNumberData.Create(100));
            statusData3.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider3, statusData3);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.300A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_Value()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData1.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(UniversalBigNumberData.Create(100));
            statusData2.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.110A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_x3_And_Value_x3()
        {
            var statusProvider11 = new StatusProvider_Test();
            var statusData11 = new StatusData_Test();
            statusData11.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData11.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider11, statusData11);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider11, statusData11), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider12 = new StatusProvider_Test();
            var statusData12 = new StatusData_Test();
            statusData12.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData12.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider12, statusData12);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider12, statusData12), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider13 = new StatusProvider_Test();
            var statusData13 = new StatusData_Test();
            statusData13.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData13.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider13, statusData13);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider13, statusData13), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider21 = new StatusProvider_Test();
            var statusData21 = new StatusData_Test();
            statusData21.SetValue(UniversalBigNumberData.Create(100));
            statusData21.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider21, statusData21);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider21, statusData21), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider22 = new StatusProvider_Test();
            var statusData22 = new StatusData_Test();
            statusData22.SetValue(UniversalBigNumberData.Create(100));
            statusData22.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider22, statusData22);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider22, statusData22), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider23 = new StatusProvider_Test();
            var statusData23 = new StatusData_Test();
            statusData23.SetValue(UniversalBigNumberData.Create(100));
            statusData23.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider23, statusData23);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider23, statusData23), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.330A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Absolute_Rate_Value()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(UniversalBigNumberData.Create(100));
            statusData1.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(statusProvider1, statusData1);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(UniversalBigNumberData.Create(0.01f));
            statusData2.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(statusProvider2, statusData2);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            statusData3.SetValue(UniversalBigNumberData.Create(100));
            statusData3.SetTypeStatusData(IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(statusProvider3, statusData3);
            Assert.IsTrue(_statusPackage.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = _statusPackage.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.100A");
        }
    }
}
#endif