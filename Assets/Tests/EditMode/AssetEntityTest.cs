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

            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);
            _entity.Add(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "��ġ�� ���� �ʽ��ϴ�");
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

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "��ġ�� ���� �ʽ��ϴ�");
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

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "��ġ�� ���� �ʽ��ϴ�");
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

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 500, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Set_Population()
        {
            var data = PopulationAssetData.Create_Test(5);

            _entity.Set(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 5, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Subject_Gold()
        {
            AssetEntityTest_Add_Gold();

            var data = GoldAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Subject_Resource()
        {
            AssetEntityTest_Add_Resource();

            var data = ResourceAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Subject_Research()
        {
            AssetEntityTest_Add_Research();

            var data = ResearchAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Subject_Meteorite()
        {
            AssetEntityTest_Add_Meteorite();

            var data = MeteoriteAssetData.Create_Test(100);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 200, "��ġ�� ���� �ʽ��ϴ�");
        }

        [Test]
        public void AssetEntityTest_Subject_Population()
        {
            AssetEntityTest_Set_Population();

            var data = PopulationAssetData.Create_Test(1);

            _entity.Subject(data);
            _entity.Subject(data);
            _entity.Subject(data);

            Assert.IsTrue(_entity.FindAssetData(data).AssetValue == 2, "��ġ�� ���� �ʽ��ϴ�");
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

            Assert.IsFalse(_entity.IsOverflow(data1), "���ƽ��ϴ�");
            Assert.IsFalse(_entity.IsOverflow(data2), "���ƽ��ϴ�");
            Assert.IsFalse(_entity.IsOverflow(data3), "���ƽ��ϴ�");
            Assert.IsFalse(_entity.IsOverflow(data4), "���ƽ��ϴ�");
            Assert.IsFalse(_entity.IsOverflow(data5), "���ƽ��ϴ�");

            data1.AssetValue += 1;
            data2.AssetValue += 1;
            data3.AssetValue += 1;
            data4.AssetValue += 1;
            data5.AssetValue += 1;

            Assert.IsTrue(_entity.IsOverflow(data1), "��ġ�� �ʾҽ��ϴ�");
            Assert.IsTrue(_entity.IsOverflow(data2), "��ġ�� �ʾҽ��ϴ�");
            Assert.IsTrue(_entity.IsOverflow(data3), "��ġ�� �ʾҽ��ϴ�");
            Assert.IsTrue(_entity.IsOverflow(data4), "��ġ�� �ʾҽ��ϴ�");
            Assert.IsTrue(_entity.IsOverflow(data5), "��ġ�� �ʾҽ��ϴ�");
        }


        //[Test]
        //public void AssetEntityTest_IsUnderflow()
        //{
        //    var data1 = GoldAssetData.Create_Test(500);
        //    var data2 = ResearchAssetData.Create_Test(500);
        //    var data3 = ResourceAssetData.Create_Test(500);
        //    var data4 = MeteoriteAssetData.Create_Test(500);
        //    var data5 = PopulationAssetData.Create_Test(5);

        //    Assert.IsFalse(_entity.IsUnderflow(data1), "���ƽ��ϴ�");
        //    Assert.IsFalse(_entity.IsUnderflow(data2), "���ƽ��ϴ�");
        //    Assert.IsFalse(_entity.IsUnderflow(data3), "���ƽ��ϴ�");
        //    Assert.IsFalse(_entity.IsUnderflow(data4), "���ƽ��ϴ�");
        //    Assert.IsFalse(_entity.IsUnderflow(data5), "���ƽ��ϴ�");

        //    data1.AssetValue -= 1;
        //    data2.AssetValue -= 1;
        //    data3.AssetValue -= 1;
        //    data4.AssetValue -= 1;
        //    data5.AssetValue -= 1;

        //    Assert.IsTrue(_entity.IsUnderflow(data1), "��ġ�� �ʾҽ��ϴ�");
        //    Assert.IsTrue(_entity.IsUnderflow(data2), "��ġ�� �ʾҽ��ϴ�");
        //    Assert.IsTrue(_entity.IsUnderflow(data3), "��ġ�� �ʾҽ��ϴ�");
        //    Assert.IsTrue(_entity.IsUnderflow(data4), "��ġ�� �ʾҽ��ϴ�");
        //    Assert.IsTrue(_entity.IsUnderflow(data5), "��ġ�� �ʾҽ��ϴ�");
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

            Assert.IsTrue(_entity.IsEnough(data1), "��ȭ���� �����մϴ�");
            Assert.IsTrue(_entity.IsEnough(data2), "��ȭ���� �����մϴ�");
            Assert.IsTrue(_entity.IsEnough(data3), "��ȭ���� �����մϴ�");
            Assert.IsTrue(_entity.IsEnough(data4), "��ȭ���� �����մϴ�");
            Assert.IsTrue(_entity.IsEnough(data5), "��ȭ���� �����մϴ�");

            data1.AssetValue += 1;
            data2.AssetValue += 1;
            data3.AssetValue += 1;
            data4.AssetValue += 1;
            data5.AssetValue += 1;

            Assert.IsFalse(_entity.IsEnough(data1), "��ȭ���� ����մϴ�");
            Assert.IsFalse(_entity.IsEnough(data2), "��ȭ���� ����մϴ�");
            Assert.IsFalse(_entity.IsEnough(data3), "��ȭ���� ����մϴ�");
            Assert.IsFalse(_entity.IsEnough(data4), "��ȭ���� ����մϴ�");
            Assert.IsFalse(_entity.IsEnough(data5), "��ȭ���� ����մϴ�");
        }

        [Test]
        public void AssetEntityTest_GetUpgradeAssetData()
        {
            UnitEntity unitEntity;
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            var upgradeAssetData = unitEntity.UpgradeAssetData;

            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "100", "��ȭ ����� �߸��Ǿ����ϴ�");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "201", "��ȭ ����� �߸��Ǿ����ϴ�");

            unitEntity.Upgrade();
            upgradeAssetData = unitEntity.UpgradeAssetData;
            Debug.Log(upgradeAssetData.GetValue());
            Assert.IsTrue(upgradeAssetData.GetValue() == "302", "��ȭ ����� �߸��Ǿ����ϴ�");

        }

    }
}
#endif