#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using SEF.Account;
    using Data;
    using Entity;

    public class AssetEntityTest
    {

        private AssetPackage _entity;

        [SetUp]
        public void SetUp()
        {
            _entity = AssetPackage.Create();
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

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Resource()
        {
            var data = ResourceAssetData.Create_Test(100);

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Research()
        {
            var data = ResearchAssetData.Create_Test(100);

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Add_Meteorite()
        {
            var data = MeteoriteAssetData.Create_Test(100);

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Set_Population()
        {
            var data = PopulationAssetData.Create_Test(5);

            _entity.Set(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 5, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Gold()
        {
            AssetEntityTest_Add_Gold();

            var data = GoldAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Resource()
        {
            AssetEntityTest_Add_Resource();

            var data = ResourceAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Research()
        {
            AssetEntityTest_Add_Research();

            var data = ResearchAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Meteorite()
        {
            AssetEntityTest_Add_Meteorite();

            var data = MeteoriteAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_Subject_Population()
        {
            AssetEntityTest_Set_Population();

            var data = PopulationAssetData.Create_Test(1);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 2, "수치가 맞지 않습니다");
        }

        [Test]
        public void AssetEntityTest_IsOverflow()
        {

            AssetEntityTest_Add_Gold();
            AssetEntityTest_Add_Meteorite();
            AssetEntityTest_Add_Research();
            AssetEntityTest_Add_Resource();
            AssetEntityTest_Set_Population();

            var data1 = GoldAssetData.Create_Test(500);
            var data2 = ResearchAssetData.Create_Test(500);
            var data3 = ResourceAssetData.Create_Test(500);
            var data4 = MeteoriteAssetData.Create_Test(500);
            var data5 = PopulationAssetData.Create_Test(5);

            Assert.IsFalse(_entity.IsOverflow(data1), "넘쳤습니다");
            Assert.IsFalse(_entity.IsOverflow(data2), "넘쳤습니다");
            Assert.IsFalse(_entity.IsOverflow(data3), "넘쳤습니다");
            Assert.IsFalse(_entity.IsOverflow(data4), "넘쳤습니다");
            Assert.IsFalse(_entity.IsOverflow(data5), "넘쳤습니다");

            data1.AssetValue += 1;
            data2.AssetValue += 1;
            data3.AssetValue += 1;
            data4.AssetValue += 1;
            data5.AssetValue += 1;

            Assert.IsTrue(_entity.IsOverflow(data1), "넘치지 않았습니다");
            Assert.IsTrue(_entity.IsOverflow(data2), "넘치지 않았습니다");
            Assert.IsTrue(_entity.IsOverflow(data3), "넘치지 않았습니다");
            Assert.IsTrue(_entity.IsOverflow(data4), "넘치지 않았습니다");
            Assert.IsTrue(_entity.IsOverflow(data5), "넘치지 않았습니다");
        }

        [Test]
        public void AssetEntityTest_CompoundInterest()
        {
            var data = GoldAssetData.Create_Test(100);
            data.SetValue();
            data.SetCompoundInterest(1f, 0.125f, 1);
            Debug.Log(data.GetValue());

            var clone = (IAssetData)data.Clone();
            clone.SetCompoundInterest(1f, 0.125f, 1);
            Debug.Log(clone.GetValue());
        }


        //[Test]
        //public void AssetEntityTest_IsUnderflow()
        //{
        //    var data1 = GoldAssetData.Create_Test(500);
        //    var data2 = ResearchAssetData.Create_Test(500);
        //    var data3 = ResourceAssetData.Create_Test(500);
        //    var data4 = MeteoriteAssetData.Create_Test(500);
        //    var data5 = PopulationAssetData.Create_Test(5);

        //    Assert.IsFalse(_entity.IsUnderflow(data1), "넘쳤습니다");
        //    Assert.IsFalse(_entity.IsUnderflow(data2), "넘쳤습니다");
        //    Assert.IsFalse(_entity.IsUnderflow(data3), "넘쳤습니다");
        //    Assert.IsFalse(_entity.IsUnderflow(data4), "넘쳤습니다");
        //    Assert.IsFalse(_entity.IsUnderflow(data5), "넘쳤습니다");

        //    data1.AssetValue -= 1;
        //    data2.AssetValue -= 1;
        //    data3.AssetValue -= 1;
        //    data4.AssetValue -= 1;
        //    data5.AssetValue -= 1;

        //    Assert.IsTrue(_entity.IsUnderflow(data1), "넘치지 않았습니다");
        //    Assert.IsTrue(_entity.IsUnderflow(data2), "넘치지 않았습니다");
        //    Assert.IsTrue(_entity.IsUnderflow(data3), "넘치지 않았습니다");
        //    Assert.IsTrue(_entity.IsUnderflow(data4), "넘치지 않았습니다");
        //    Assert.IsTrue(_entity.IsUnderflow(data5), "넘치지 않았습니다");
        //}


        [Test]
        public void AssetEntityTest_IsEnough()
        {

            AssetEntityTest_Add_Gold();
            AssetEntityTest_Add_Meteorite();
            AssetEntityTest_Add_Research();
            AssetEntityTest_Add_Resource();
            AssetEntityTest_Set_Population();

            var data1 = GoldAssetData.Create_Test(500);
            var data2 = ResearchAssetData.Create_Test(500);
            var data3 = ResourceAssetData.Create_Test(500);
            var data4 = MeteoriteAssetData.Create_Test(500);
            var data5 = PopulationAssetData.Create_Test(5);

            Assert.IsTrue(_entity.IsEnough(data1), "재화값이 부족합니다");
            Assert.IsTrue(_entity.IsEnough(data2), "재화값이 부족합니다");
            Assert.IsTrue(_entity.IsEnough(data3), "재화값이 부족합니다");
            Assert.IsTrue(_entity.IsEnough(data4), "재화값이 부족합니다");
            Assert.IsTrue(_entity.IsEnough(data5), "재화값이 부족합니다");

            data1.AssetValue += 1;
            data2.AssetValue += 1;
            data3.AssetValue += 1;
            data4.AssetValue += 1;
            data5.AssetValue += 1;

            Assert.IsFalse(_entity.IsEnough(data1), "재화값이 충분합니다");
            Assert.IsFalse(_entity.IsEnough(data2), "재화값이 충분합니다");
            Assert.IsFalse(_entity.IsEnough(data3), "재화값이 충분합니다");
            Assert.IsFalse(_entity.IsEnough(data4), "재화값이 충분합니다");
            Assert.IsFalse(_entity.IsEnough(data5), "재화값이 충분합니다");
        }

        [Test]
        public void AssetEntityTest_GetUpgradeAssetData()
        {
            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            var upgradeAssetData = unitEntity.UpgradeAssetData;

            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "10", "재화 계산이 잘못되었습니다");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "11", "재화 계산이 잘못되었습니다");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "13", "재화 계산이 잘못되었습니다");

        }

        [Test]
        public void AssetDataTest_SerializedAssetDataTest()
        {
            SerializedAssetData data = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Meteorite, "100");
            var assetData = data.GetData();
            Debug.Log(assetData.GetType() + " " + assetData.AssetValue);
        }

    }
}
#endif