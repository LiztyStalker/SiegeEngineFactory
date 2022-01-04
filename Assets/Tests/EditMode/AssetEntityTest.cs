#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Account;
    using Data;
    using Entity;

    public class AssetEntityTest
    {

        private AssetEntity _entity;

        [SetUp]
        public void SetUp()
        {
            _entity = AssetEntity.Create();
            _entity.Initialize();
            _entity.AddRefreshAssetDataListener(data =>
            {
                Debug.Log($"{data.GetType().Name} : {data.GetValue()}");
            });
        }

        [TearDown]
        public void TearDown()
        {
            _entity.CleanUp();
        }

        [Test]
        public void AssetEntityTest_Initialize()
        {
            _entity.RefreshAssets();
        }

        [Test]
        public void AssetEntityTest_Add_Gold()
        {
            var data = GoldAssetData.Create_Test(100);

//            var data = AssetData.Create_Test(TYPE_ASSET.Gold, 100);

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Gold).Value == 500, "수치가 맞지 않습니다");
            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Ore()
        {
            //var data = AssetData.Create_Test(TYPE_ASSET.Ore, 100);

            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Ore).Value == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Resource()
        {
            //var data = AssetData.Create_Test(TYPE_ASSET.Resource, 100);

            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Resource).Value == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Meteorite()
        {
            //var data = AssetData.Create_Test(TYPE_ASSET.Meteorite, 100);

            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);
            //_entity.Add(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Meteorite).Value == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Gold()
        {
            AssetEntityTest_Add_Gold();

            var data = GoldAssetData.Create_Test(100);
            //var data = AssetData.Create_Test(TYPE_ASSET.Gold, 100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "수치가 맞지 않습니다");
            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Gold).Value == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Resource()
        {
            //AssetEntityTest_Add_Resource();

            //var data = AssetData.Create_Test(TYPE_ASSET.Resource, 100);

            //_entity.Subject(data);
            //_entity.Subject(data);
            //_entity.Subject(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Resource).Value == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Ore()
        {
            //AssetEntityTest_Add_Ore();

            //var data = AssetData.Create_Test(TYPE_ASSET.Ore, 100);

            //_entity.Subject(data);
            //_entity.Subject(data);
            //_entity.Subject(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Ore).Value == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Meteorite()
        {
            //AssetEntityTest_Add_Meteorite();

            //var data = AssetData.Create_Test(TYPE_ASSET.Meteorite, 100);

            //_entity.Subject(data);
            //_entity.Subject(data);
            //_entity.Subject(data);

            //Assert.IsTrue(_entity.FindAssetData(TYPE_ASSET.Meteorite).Value == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_IsEnough()
        {
            AssetEntityTest_Add_Gold();
//            AssetEntityTest_Add_Meteorite();
//            AssetEntityTest_Add_Ore();
//            AssetEntityTest_Add_Resource();

            var data1 = GoldAssetData.Create_Test(500);
//            var data1 = AssetData.Create_Test(TYPE_ASSET.Gold, 500);
//            var data2 = AssetData.Create_Test(TYPE_ASSET.Meteorite, 500);
//            var data3 = AssetData.Create_Test(TYPE_ASSET.Ore, 500);
//            var data4 = AssetData.Create_Test(TYPE_ASSET.Resource, 500);

            Assert.IsTrue(_entity.IsEnough(data1), "재화값이 부족합니다");
//            Assert.IsTrue(_entity.IsEnough(data2), "재화값이 부족합니다");
//            Assert.IsTrue(_entity.IsEnough(data3), "재화값이 부족합니다");
//            Assert.IsTrue(_entity.IsEnough(data4), "재화값이 부족합니다");

            data1.Value += 1;
//            data2.Value += 1;
//            data3.Value += 1;
//            data4.Value += 1;

            Assert.IsFalse(_entity.IsEnough(data1), "재화값이 충분합니다");
//            Assert.IsFalse(_entity.IsEnough(data2), "재화값이 충분합니다");
//            Assert.IsFalse(_entity.IsEnough(data3), "재화값이 충분합니다");
//            Assert.IsFalse(_entity.IsEnough(data4), "재화값이 충분합니다");

        }

        [Test]
        public void AssetEntityTest_GetUpgradeAssetData()
        {
            UnitEntity unitEntity;
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            var upgradeAssetData = unitEntity.UpgradeAssetData;

            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "100", "재화 계산이 잘못되었습니다");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "201", "재화 계산이 잘못되었습니다");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "302", "재화 계산이 잘못되었습니다");

        }

    }
}
#endif