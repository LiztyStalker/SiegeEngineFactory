#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using NUnit.Framework;
    using UnityEngine;
    using Data;
    using Entity;
    using SEF.Unit;
    using Status;

    public class StatusPackageTest
    {

        private class StatusProvider_Test : IStatusProvider {}

        private class StatusData_Test : StatusData, IStatusData
        {
        }
      

        [SetUp]
        public void SetUp()
        {
            StatusPackage.Current.Initialize();
            Assert.IsNotNull(StatusPackage.Current, "StatusPackage 를 생성하지 못했습니다");

        }

        [TearDown]
        public void TearDown()
        {
            StatusPackage.Current.Dispose();
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
            var entity = new StatusEntity(statusData, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_AddStatusData_5()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            var entity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider3, entity3);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider3), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider4 = new StatusProvider_Test();
            var statusData4 = new StatusData_Test();
            var entity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider4, entity4);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider4), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider5 = new StatusProvider_Test();
            var statusData5 = new StatusData_Test();
            var entity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider5, entity5);
            Assert.IsTrue(StatusPackage.Current.HasStatusProvider(statusProvider5), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_AddStatusData_And_UpdateStatusData()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();

            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity1);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider, statusData1), "StatusProvider 가 저장되지 않았습니다");
            
            var statusData2 = new StatusData_Test();
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity2);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider, statusData2), "StatusProvider 가 저장되지 않았습니다");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Absolute()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();
            statusData.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var entity = new StatusEntity(statusData, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "100");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Absolute_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();
            statusData1.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();
            statusData2.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();
            statusData3.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var entity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider3, entity3);
            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "300");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();

            statusData.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity = new StatusEntity(statusData, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.010A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();

            statusData1.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();

            statusData2.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();

            statusData3.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider3, entity3);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.030A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Value()
        {
            var statusProvider = new StatusProvider_Test();
            var statusData = new StatusData_Test();


            statusData.SetValue(100, 0.01f, IStatusData.TYPE_STATUS_DATA.Value);
            var entity = new StatusEntity(statusData, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider, entity);


            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider, statusData), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.100A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Value_x3()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();

            statusData1.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();

            statusData2.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();

            statusData3.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider3, entity3);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.300A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_Value()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();

            statusData1.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();

            statusData2.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.110A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Rate_x3_And_Value_x3()
        {
            var statusProvider11 = new StatusProvider_Test();
            var statusData11 = new StatusData_Test();

            statusData11.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity11 = new StatusEntity(statusData11, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider11, entity11);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider11, statusData11), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider12 = new StatusProvider_Test();
            var statusData12 = new StatusData_Test();

            statusData12.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity12 = new StatusEntity(statusData12, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider12, entity12);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider12, statusData12), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider13 = new StatusProvider_Test();
            var statusData13 = new StatusData_Test();

            statusData13.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity13 = new StatusEntity(statusData13, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider13, entity13);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider13, statusData13), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider21 = new StatusProvider_Test();
            var statusData21 = new StatusData_Test();

            statusData21.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity21 = new StatusEntity(statusData21, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider21, entity21);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider21, statusData21), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider22 = new StatusProvider_Test();
            var statusData22 = new StatusData_Test();

            statusData22.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity22 = new StatusEntity(statusData22, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider22, entity22);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider22, statusData22), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider23 = new StatusProvider_Test();
            var statusData23 = new StatusData_Test();

            statusData23.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity23 = new StatusEntity(statusData23, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider23, entity23);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider23, statusData23), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
            Debug.Log(value.GetValue());
            Assert.IsTrue(value.GetValue() == "1.330A");
        }

        [Test]
        public void StatusPackageTest_GetStatusData_Absolute_Rate_Value()
        {
            var statusProvider1 = new StatusProvider_Test();
            var statusData1 = new StatusData_Test();


            statusData1.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var entity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, entity1);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider1, statusData1), "StatusProvider 가 저장되지 않았습니다");

            var statusProvider2 = new StatusProvider_Test();
            var statusData2 = new StatusData_Test();

            statusData2.SetValue(0.01f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var entity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, entity2);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider2, statusData2), "StatusProvider 가 저장되지 않았습니다");


            var statusProvider3 = new StatusProvider_Test();
            var statusData3 = new StatusData_Test();

            statusData3.SetValue(100, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var entity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider3, entity3);

            Assert.IsTrue(StatusPackage.Current.HasStatusProviderAndStatusData(statusProvider3, statusData3), "StatusProvider 가 저장되지 않았습니다");

            var value = StatusPackage.Current.GetStatusDataToBigNumberData<StatusData_Test, GoldAssetData>(GoldAssetData.Create_Test(1000));
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
            var unitEntity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<UnitDamageValueStatusData>(5, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(unitEntity1.DamageData.GetValue());
            Assert.AreEqual(unitEntity1.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();
            var statusData2 = StatusDataUtility.Create<UnitDamageValueStatusData>(0.5f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);


            Debug.Log(entity2.DamageData.GetValue());
            Assert.AreEqual(entity2.DamageData.GetValue(), "22");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();


            var statusData3 = StatusDataUtility.Create<UnitDamageValueStatusData>(3, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            var statusData4 = StatusDataUtility.Create<UnitDamageValueStatusData>(0.3f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity3.DamageData.GetValue());
            Assert.AreEqual(entity3.DamageData.GetValue(), "22");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = StatusDataUtility.Create<UnitDamageValueStatusData>(20, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(entity4.DamageData.GetValue());
            Assert.AreEqual(entity4.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }



        [Test]
        public void StatusPackageTest_UnitEntity_DamageDelayStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();


            var statusData1 = StatusDataUtility.Create<UnitDamageDelayStatusData>(0.1f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(entity1.AttackDelay);
            Assert.AreEqual(entity1.AttackDelay, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = StatusDataUtility.Create<UnitDamageDelayStatusData>(0.5f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity2.AttackDelay);
            Assert.AreEqual(entity2.AttackDelay, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();



            var statusProvider2 = new StatusProvider_Test();

            var statusData3 = StatusDataUtility.Create<UnitDamageDelayStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);

            var statusData4 = StatusDataUtility.Create<UnitDamageDelayStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity3.AttackDelay);
            Assert.AreEqual(entity3.AttackDelay, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = StatusDataUtility.Create<UnitDamageDelayStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);


            Debug.Log(entity4.AttackDelay);
            Assert.AreEqual(entity4.AttackDelay, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }



        [Test]
        public void StatusPackageTest_UnitEntity_ProductTimeStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();


            var statusData1 = StatusDataUtility.Create<UnitProductTimeStatusData>(0.1f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);


            Debug.Log(entity1.ProductTime);
            Assert.AreEqual(entity1.ProductTime, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();

            var statusData2 = StatusDataUtility.Create<UnitProductTimeStatusData>(0.5f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity2.ProductTime);
            Assert.AreEqual(entity2.ProductTime, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();



            var statusData3 = StatusDataUtility.Create<UnitProductTimeStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);

            var statusData4 = StatusDataUtility.Create<UnitProductTimeStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);

            Debug.Log(entity3.ProductTime);
            Assert.AreEqual(entity3.ProductTime, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = StatusDataUtility.Create<UnitProductTimeStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);


            Debug.Log(entity4.ProductTime);
            Assert.AreEqual(entity4.ProductTime, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }


        [Test]
        public void StatusPackageTest_UnitEntity_HealthDataStatusData()
        {
            var entity1 = CreateEntity();

            var statusProvider1 = new StatusProvider_Test();



            var statusData1 = StatusDataUtility.Create<UnitHealthValueStatusData>(5, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);


            Debug.Log(entity1.HealthData.GetValue());
            Assert.AreEqual(entity1.HealthData.GetValue(), "105");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity2 = CreateEntity();


            var statusData2 = StatusDataUtility.Create<UnitHealthValueStatusData>(0.5f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);


            Debug.Log(entity2.HealthData.GetValue());
            Assert.AreEqual(entity2.HealthData.GetValue(), "150");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var entity3 = CreateEntity();

            var statusProvider2 = new StatusProvider_Test();


            var statusData3 = StatusDataUtility.Create<UnitHealthValueStatusData>(3, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);

            var statusData4 = StatusDataUtility.Create<UnitHealthValueStatusData>(0.3f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);



            Debug.Log(entity3.HealthData.GetValue());
            Assert.AreEqual(entity3.HealthData.GetValue(), "133");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var entity4 = CreateEntity();

            var statusData5 = StatusDataUtility.Create<UnitHealthValueStatusData>(200, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);


            Debug.Log(entity4.HealthData.GetValue());
            Assert.AreEqual(entity4.HealthData.GetValue(), "200");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


        }




        [Test]
        public void StatusPackageTest_UnitEntity_AttackerDamageValueStatusData()
        {
            var actor1 = CreateAttackerActor();

            var statusProvider1 = new StatusProvider_Test();


            var statusData1 = StatusDataUtility.Create<AttackerDamageValueStatusData>(5, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(actor1.DamageData.GetValue());
            Assert.AreEqual(actor1.DamageData.GetValue(), "35");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor2 = CreateAttackerActor();


            var statusData2 = StatusDataUtility.Create<AttackerDamageValueStatusData>(0.5f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(actor2.DamageData.GetValue());
            Assert.AreEqual(actor2.DamageData.GetValue(), "45");

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor3 = CreateAttackerActor();

            var statusProvider2 = new StatusProvider_Test();


            var statusData3 = StatusDataUtility.Create<AttackerDamageValueStatusData>(3, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<AttackerDamageValueStatusData>(0.3f, 0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(actor3.DamageData.GetValue());
            Assert.AreEqual(actor3.DamageData.GetValue(), "42");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var actor4 = CreateAttackerActor();

            var statusData5 = StatusDataUtility.Create<AttackerDamageValueStatusData>(20, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(actor4.DamageData.GetValue());
            Assert.AreEqual(actor4.DamageData.GetValue(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);
        }



        [Test]
        public void StatusPackageTest_UnitEntity_AttackerDamageDelayStatusData()
        {
            var actor1= CreateAttackerActor();

            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<AttackerDamageDelayStatusData>(0.1f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity1 = new StatusEntity(statusData1, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);


            Debug.Log(actor1.AttackDelay);
            Assert.AreEqual(actor1.AttackDelay, 0.9f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor2 = CreateAttackerActor();

            var statusData2 = StatusDataUtility.Create<AttackerDamageDelayStatusData>(0.5f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(actor2.AttackDelay);
            Assert.AreEqual(actor2.AttackDelay, 0.5f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);



            var actor3 = CreateAttackerActor();

            var statusProvider2 = new StatusProvider_Test();



            var statusData3 = StatusDataUtility.Create<AttackerDamageDelayStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);

            var statusData4 = StatusDataUtility.Create<AttackerDamageDelayStatusData>(0.3f, 0.1f, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);



            Debug.Log(actor3.AttackDelay);
            Assert.AreEqual(actor3.AttackDelay, 0.4f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);



            var actor4 = CreateAttackerActor();

            var statusData5 = StatusDataUtility.Create<AttackerDamageDelayStatusData>(2f, 0.1f, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, NumberDataUtility.Create<UpgradeData>());
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);


            Debug.Log(actor4.AttackDelay);
            Assert.AreEqual(actor4.AttackDelay, 2f);

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }





        [Test]
        public void StatusPackageTest_AssetPackage_IncreaseMaxPopulationStatusData()
        {
            var assetEntity = new AssetEntity(new PopulationAssetData());
            assetEntity.SetLimitAssetData(new PopulationAssetData(5));

            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<IncreaseMaxPopulationStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var upgradeData = NumberDataUtility.Create<UpgradeData>();
            upgradeData.IncreaseNumber();
            var statusEntity1 = new StatusEntity(statusData1, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(assetEntity.LimitAssetData.AssetValue);
            Assert.AreEqual(assetEntity.LimitAssetData.AssetValue.ToString(), "6");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


            var statusData2 = StatusDataUtility.Create<IncreaseMaxPopulationStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, upgradeData);

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(assetEntity.LimitAssetData.AssetValue);
            Assert.AreEqual(assetEntity.LimitAssetData.AssetValue.ToString(), "10");

            StatusPackage.Current.RemoveStatusData(statusProvider1);




            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = StatusDataUtility.Create<IncreaseMaxPopulationStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<IncreaseMaxPopulationStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(assetEntity.LimitAssetData.AssetValue);
            Assert.AreEqual(assetEntity.LimitAssetData.AssetValue.ToString(), "11");


            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);




            var statusData5 = StatusDataUtility.Create<IncreaseMaxPopulationStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(assetEntity.LimitAssetData.AssetValue);
            Assert.AreEqual(assetEntity.LimitAssetData.AssetValue.ToString(), "2");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }


        [Test]
        public void StatusPackageTest_AssetPackage_IncreaseMaxUpgradeUnitStatusData()
        {
            var data = UnitData.Create_Test();
            var entity = new UnitEntity();
            entity.Initialize();
            entity.UpTech(data);
           

            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<IncreaseMaxUpgradeUnitStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var upgradeData = NumberDataUtility.Create<UpgradeData>();
            upgradeData.IncreaseNumber();
            var statusEntity1 = new StatusEntity(statusData1, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "11");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


            var statusData2 = StatusDataUtility.Create<IncreaseMaxUpgradeUnitStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, upgradeData);

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);




            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = StatusDataUtility.Create<IncreaseMaxUpgradeUnitStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<IncreaseMaxUpgradeUnitStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "21");


            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);




            var statusData5 = StatusDataUtility.Create<IncreaseMaxUpgradeUnitStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "2");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }



        [Test]
        public void StatusPackageTest_AssetPackage_IncreaseMaxUpgradeSmithyStatusData()
        {
            var data = SmithyData.Create_Test();
            var entity = new SmithyEntity();
            entity.Initialize();
            entity.SetData(data);


            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<IncreaseMaxUpgradeSmithyStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var upgradeData = NumberDataUtility.Create<UpgradeData>();
            upgradeData.IncreaseNumber();
            var statusEntity1 = new StatusEntity(statusData1, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "11");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


            var statusData2 = StatusDataUtility.Create<IncreaseMaxUpgradeSmithyStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, upgradeData);

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);




            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = StatusDataUtility.Create<IncreaseMaxUpgradeSmithyStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<IncreaseMaxUpgradeSmithyStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "21");


            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);




            var statusData5 = StatusDataUtility.Create<IncreaseMaxUpgradeSmithyStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "2");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }




        [Test]
        public void StatusPackageTest_AssetPackage_IncreaseMaxUpgradeVillageStatusData()
        {
            var data = VillageData.Create_Test();
            var entity = new VillageEntity();
            entity.Initialize();
            entity.SetData(data);


            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<IncreaseMaxUpgradeVillageStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var upgradeData = NumberDataUtility.Create<UpgradeData>();
            upgradeData.IncreaseNumber();
            var statusEntity1 = new StatusEntity(statusData1, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "11");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


            var statusData2 = StatusDataUtility.Create<IncreaseMaxUpgradeVillageStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, upgradeData);

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);




            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = StatusDataUtility.Create<IncreaseMaxUpgradeVillageStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<IncreaseMaxUpgradeVillageStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "21");


            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);




            var statusData5 = StatusDataUtility.Create<IncreaseMaxUpgradeVillageStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "2");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }


        [Test]
        public void StatusPackageTest_AssetPackage_IncreaseMaxUpgradeMineStatusData()
        {
            var data = MineData.Create_Test();
            var entity = new MineEntity();
            entity.Initialize();
            entity.SetData(data);


            var statusProvider1 = new StatusProvider_Test();

            var statusData1 = StatusDataUtility.Create<IncreaseMaxUpgradeMineStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var upgradeData = NumberDataUtility.Create<UpgradeData>();
            upgradeData.IncreaseNumber();
            var statusEntity1 = new StatusEntity(statusData1, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity1);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "11");

            StatusPackage.Current.RemoveStatusData(statusProvider1);


            var statusData2 = StatusDataUtility.Create<IncreaseMaxUpgradeMineStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity2 = new StatusEntity(statusData2, upgradeData);

            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity2);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "20");

            StatusPackage.Current.RemoveStatusData(statusProvider1);




            var statusProvider2 = new StatusProvider_Test();
            var statusData3 = StatusDataUtility.Create<IncreaseMaxUpgradeMineStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Value);
            var statusEntity3 = new StatusEntity(statusData3, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity3);


            var statusData4 = StatusDataUtility.Create<IncreaseMaxUpgradeMineStatusData>(1, 1, IStatusData.TYPE_STATUS_DATA.Rate);
            var statusEntity4 = new StatusEntity(statusData4, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider2, statusEntity4);


            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "21");


            StatusPackage.Current.RemoveStatusData(statusProvider1);
            StatusPackage.Current.RemoveStatusData(statusProvider2);




            var statusData5 = StatusDataUtility.Create<IncreaseMaxUpgradeMineStatusData>(2, 1, IStatusData.TYPE_STATUS_DATA.Absolute);
            var statusEntity5 = new StatusEntity(statusData5, upgradeData);
            StatusPackage.Current.SetStatusEntity(statusProvider1, statusEntity5);

            Debug.Log(entity.MaxUpgradeValue);
            Assert.AreEqual(entity.MaxUpgradeValue.ToString(), "2");

            StatusPackage.Current.RemoveStatusData(statusProvider1);

        }
    }
}
#endif