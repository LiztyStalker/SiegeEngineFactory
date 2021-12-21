#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Manager;
    using Data;
    using Entity;

    public class WorkshopManagerTest
    {
        private WorkshopManager _workshopManager;

        [SetUp]
        public void SetUp()
        {
            _workshopManager = WorkshopManager.Create();
            _workshopManager.Initialize_Test(null);
        }

        [TearDown]
        public void TearDown()
        {
            _workshopManager.CleanUp();
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_Initialize()
        {
            Assert.That(_workshopManager.Count == 1, "WorkshopManager 생성이 제대로 이루어지지 않았습니다");
            yield return null;
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_RunProcess()
        {
            bool isProduct = false;
            _workshopManager.AddProductUnitListener(delegate
            {
                isProduct = true;
            });

            while (true)
            {
                _workshopManager.RunProcess(Time.deltaTime);
                if (isProduct)
                {
                    break;
                }
                yield return null;
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_Expend()
        {
            _workshopManager.ExpendWorkshop_Test();
            Assert.IsTrue(_workshopManager.Count == 2, "WorkshopLine이 증축되지 않았습니다");
            yield return null;
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_RunProcess_5_Line_for_20_Product()
        {
            _workshopManager.ExpendWorkshop_Test();
            _workshopManager.ExpendWorkshop_Test();
            _workshopManager.ExpendWorkshop_Test();
            _workshopManager.ExpendWorkshop_Test();
            Assert.IsTrue(_workshopManager.Count == 5, "WorkshopLine이 증축되지 않았습니다");
            yield return null;

            int productCount = 0;
            _workshopManager.AddProductUnitListener(delegate
            {
                productCount++;
                Debug.Log($"Product {productCount}");
            });

            while (true)
            {
                _workshopManager.RunProcess(Time.deltaTime);
                if (productCount > 20)
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }


        [UnityTest]
        public IEnumerator WorkshopManagerTest_Upgrade()
        {
            _workshopManager.AddRefreshListener(UpgradeMessageEvent);
            _workshopManager.UpgradeWorkshop(0);
            yield return null;
            _workshopManager.RemoveRefreshListener(UpgradeMessageEvent);
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_UpTech()
        {
            var newUnitData = UnitData.Create_Test();
            _workshopManager.AddRefreshListener((index, unit, time) =>
            {
                Assert.IsTrue(unit.UnitData == newUnitData, "UpTech 가 진행되지 않았습니다");
            });
            _workshopManager.UpTechWorkshop(0, newUnitData);
            yield return null;
        }

        private void UpgradeMessageEvent(int index, UnitEntity unitEntity, float nowTime)
        {
            Assert.IsTrue(unitEntity.UpgradeData.GetValue() == "2", "Upgrade 가 진행되지 않았습니다");
        }


        [UnityTest]
        public IEnumerator WorkshopManagerTest_UpTech_And_InitializeUpgrade()
        {
            _workshopManager.AddRefreshListener(UpgradeMessageEvent);
            _workshopManager.UpgradeWorkshop(0);
            yield return null;
            _workshopManager.RemoveRefreshListener(UpgradeMessageEvent);


            var newUnitData = UnitData.Create_Test();
            _workshopManager.AddRefreshListener((index, unit, time) =>
            {
                Assert.IsTrue(unit.UnitData == newUnitData, "UpTech 가 진행되지 않았습니다");
                Assert.IsTrue(unit.UpgradeData.GetValue() == "1", "Upgrade 초기화가 진행되지 않았습니다");
            });
            _workshopManager.UpTechWorkshop(0, newUnitData);
            yield return null;
        }

        [UnityTest]
        public IEnumerator WorkshopManagerTest_Information()
        {
            yield return null;

        }
    }
}
#endif