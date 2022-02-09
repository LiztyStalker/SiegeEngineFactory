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
    using SEF.Unit;

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
            StatusPackage.Current.Initialize();

            _statusPackage = StatusPackage.Create();
            _statusPackage.Initialize();

            Assert.IsNotNull(_statusPackage, "StatusPackage 를 생성하지 못했습니다");

        }

        [TearDown]
        public void TearDown()
        {
            StatusPackage.Current.Dispose();
            _statusPackage.Dispose();
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
            Assert.IsTrue(value.GetValue() == "100");
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
            Assert.IsTrue(value.GetValue() == "300");
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
            Assert.IsTrue(value.GetValue() == "100");
        }


        private UnitEntity CreateEntity()
        {
            var unitData = UnitData.Create_Test();
            var entity = new UnitEntity();
            entity.Initialize();
            entity.UpTech(unitData);
            return entity;
        }

        private AttackerActor CreateAttackerActor()
        {
            var data = AttackerData.Create_Test();
            var actor = new AttackerActor();
            actor.SetData_Test(data, NumberDataUtility.Create<UpgradeData>());
            return actor;
        }

        [Test]
        public void StatusPackageTest_UnitEntity_DamageValueStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new DamageValueStatusData(5, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(entity1.DamageData.GetValue());
            Assert.AreEqual(entity1.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = new DamageValueStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(entity2.DamageData.GetValue());
            Assert.AreEqual(entity2.DamageData.GetValue(), "22");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new DamageValueStatusData(3, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new DamageValueStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(entity3.DamageData.GetValue());
            Assert.AreEqual(entity3.DamageData.GetValue(), "22");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = new DamageValueStatusData(20, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(entity4.DamageData.GetValue());
            Assert.AreEqual(entity4.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }



        [Test]
        public void StatusPackageTest_UnitEntity_DamageDelayStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new DamageDelayStatusData(0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(entity1.AttackDelay);
            Assert.AreEqual(entity1.AttackDelay, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = new DamageDelayStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(entity2.AttackDelay);
            Assert.AreEqual(entity2.AttackDelay, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new DamageDelayStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new DamageDelayStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(entity3.AttackDelay);
            Assert.AreEqual(entity3.AttackDelay, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = new DamageDelayStatusData(2, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(entity4.AttackDelay);
            Assert.AreEqual(entity4.AttackDelay, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }



        [Test]
        public void StatusPackageTest_UnitEntity_ProductTimeStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new ProductTimeStatusData(0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(entity1.ProductTime);
            Assert.AreEqual(entity1.ProductTime, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = new ProductTimeStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(entity2.ProductTime);
            Assert.AreEqual(entity2.ProductTime, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new ProductTimeStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new ProductTimeStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(entity3.ProductTime);
            Assert.AreEqual(entity3.ProductTime, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = new ProductTimeStatusData(2, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(entity4.ProductTime);
            Assert.AreEqual(entity4.ProductTime, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }


        [Test]
        public void StatusPackageTest_UnitEntity_HealthDataStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new HealthDataStatusData(5, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(entity1.HealthData.GetValue());
            Assert.AreEqual(entity1.HealthData.GetValue(), "105");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = new HealthDataStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(entity2.HealthData.GetValue());
            Assert.AreEqual(entity2.HealthData.GetValue(), "150");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new HealthDataStatusData(3, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new HealthDataStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(entity3.HealthData.GetValue());
            Assert.AreEqual(entity3.HealthData.GetValue(), "133");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = new HealthDataStatusData(200, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(entity4.HealthData.GetValue());
            Assert.AreEqual(entity4.HealthData.GetValue(), "200");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


        }




        [Test]
        public void StatusPackageTest_UnitEntity_AttackerDamageValueStatusData()
        {
            var actor1 = CreateAttackerActor();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new AttackerDamageValueStatusData(5, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(actor1.DamageData.GetValue());
            Assert.AreEqual(actor1.DamageData.GetValue(), "35");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor2 = CreateAttackerActor();

            var statusData2 = new AttackerDamageValueStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(actor2.DamageData.GetValue());
            Assert.AreEqual(actor2.DamageData.GetValue(), "45");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor3 = CreateAttackerActor();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new AttackerDamageValueStatusData(3, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new AttackerDamageValueStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(actor3.DamageData.GetValue());
            Assert.AreEqual(actor3.DamageData.GetValue(), "42");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var actor4 = CreateAttackerActor();

            var statusData5 = new AttackerDamageValueStatusData(20, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(actor4.DamageData.GetValue());
            Assert.AreEqual(actor4.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
        }



        [Test]
        public void StatusPackageTest_UnitEntity_AttackerDamageDelayStatusData()
        {
            var actor1= CreateAttackerActor();

            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new AttackerDamageDelayStatusData(0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData1);

            Debug.Log(actor1.AttackDelay);
            Assert.AreEqual(actor1.AttackDelay, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor2 = CreateAttackerActor();

            var statusData2 = new AttackerDamageDelayStatusData(0.5f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData2);

            Debug.Log(actor1.AttackDelay);
            Assert.AreEqual(actor1.AttackDelay, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor3 = CreateAttackerActor();

            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = new AttackerDamageDelayStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusData4 = new AttackerDamageDelayStatusData(0.3f, IStatusData.TYPE_STATUS_DATA.Rate);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData3);
            StatusPackage.Current.SetStatusData(statusProvider2, statusData4);

            Debug.Log(actor1.AttackDelay);
            Assert.AreEqual(actor1.AttackDelay, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var actor4 = CreateAttackerActor();

            var statusData5 = new AttackerDamageDelayStatusData(2, IStatusData.TYPE_STATUS_DATA.Absolute);
            StatusPackage.Current.SetStatusData(statusProvider1, statusData5);

            Debug.Log(actor1.AttackDelay);
            Assert.AreEqual(actor1.AttackDelay, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }


    }
}
#endif