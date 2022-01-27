namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.UIElements;
    using UtilityManager.Test;
    using SEF.UI.Toolkit;
    using Entity;
    using Data;
    using SEF.Unit;
    using SEF.Manager;
    using Statistics;

    public class StatisticsPlayTest
    {
        private Camera _camera;
        private Light2D _light;

        private GameSystem _gameSystem;
        private UnitManager _unitManager;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _unitManager = UnitManager.Create();
            _unitManager.Initialize_Empty();
        }

        [TearDown]
        public void TearDown()
        {
            _gameSystem.CleanUp();
            _unitManager.CleanUp();

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }



        #region ##### 유닛 생산 수 / 특정 유닛 생산 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_CreateUnitStatisticsData()
        {
            int count = 0;
            _gameSystem.AddProductUnitListener(delegate
            {
                count++;
            });
            while (true)
            {
                _gameSystem.RunProcess(Time.deltaTime);
                if (count >= 3)
                    break;
                yield return null;
            }
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>() == count);
        }
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TestCreateUnitStatisticsData()
        {
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());

            int count = 0;
            _gameSystem.AddProductUnitListener(delegate
            {
                count++;
            });
            while (true)
            {
                _gameSystem.RunProcess(Time.deltaTime);
                if (count >= 3)
                    break;
                yield return null;
            }
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<TestCreateUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TestCreateUnitStatisticsData>() == count);
        }
        #endregion



        #region ##### 유닛 파괴된 수/ 특정 유닛 파괴된 수#####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_DestoryUnitStatisticsData()
        {
            int count = 0;
            List<UnitActor> list = new List<UnitActor>();
            _gameSystem.AddProductUnitListener(entity => 
            {
                var actor = UnitActor.Create();
                actor.SetData(entity);
                actor.Activate();
                list.Add(actor);
                count++;
            });
            while (true)
            {
                _gameSystem.RunProcess(Time.deltaTime);
                if (count >= 3)
                    break;
                yield return null;
            }
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>() == count);


            while (list.Count > 0)
            {
                _gameSystem.DestroyedActor(list[0]);
                list.Remove(list[0]);
                yield return new WaitForSeconds(1f);
            }
            Debug.Log(_gameSystem.GetStatisticsValue<DestroyUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<DestroyUnitStatisticsData>() == count);

            yield return null;
        }

        [UnityTest]
        public IEnumerator StatisticsPlayTest_TestDestoryUnitStatisticsData()
        {
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());

            int count = 0;
            List<UnitActor> list = new List<UnitActor>();
            _gameSystem.AddProductUnitListener(entity =>
            {
                var actor = UnitActor.Create();
                actor.SetData(entity);
                actor.Activate();
                list.Add(actor);
                count++;
            });
            while (true)
            {
                _gameSystem.RunProcess(Time.deltaTime);
                if (count >= 3)
                    break;
                yield return null;
            }
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<CreateUnitStatisticsData>() == count);


            while (list.Count > 0)
            {
                _gameSystem.DestroyedActor(list[0]);
                list.Remove(list[0]);
                yield return new WaitForSeconds(1f);
            }
            Debug.Log(_gameSystem.GetStatisticsValue<TestDestroyUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TestDestroyUnitStatisticsData>() == count);

            yield return null;
        }

        #endregion



        #region ##### 소비된 재화 / 획득한 재화 / 누적된 재화 #####

        private static string[] _values = new string[] {"Gold","Resource","Meteorite","Population","Research"};

        [UnityTest]
        public IEnumerator StatisticsPlayTest_UsedAssetStatisticsData([ValueSource("_values")] string value)
        {
            Debug.Log(typeof(GoldAssetData));
            var type = System.Type.GetType("SEF.Data.GoldAssetData");
            Debug.Log(type);
            var instance = System.Activator.CreateInstance(type);
            var assetData = (IAssetData)instance;
            assetData.AssetValue = 100;
            _gameSystem.SetAsset(assetData);

            yield return null;

            var typeStatistics = System.Type.GetType($"SEF.Data.{value}UsedAssetStatisticsData");
            Debug.Log(_gameSystem.GetStatisticsValue(typeStatistics));
            Assert.IsTrue(_gameSystem.GetStatisticsValue(typeStatistics) == 100);
        }


        [UnityTest]
        public IEnumerator StatisticsPlayTest_GetAssetStatisticsData([ValueSource("_values")] string value)
        {
            Debug.Log(typeof(GoldAssetData));
            var type = System.Type.GetType("SEF.Data.GoldAssetData");
            Debug.Log(type);
            var instance = System.Activator.CreateInstance(type);
            var assetData = (IAssetData)instance;
            assetData.AssetValue = 100;
            _gameSystem.SetAsset(assetData);

            yield return null;

            var typeStatistics = System.Type.GetType($"SEF.Data.{value}GetAssetStatisticsData");
            Debug.Log(_gameSystem.GetStatisticsValue(typeStatistics));
            Assert.IsTrue(_gameSystem.GetStatisticsValue(typeStatistics) == 100);
        }

        [UnityTest]
        public IEnumerator StatisticsPlayTest_AccumulateAssetStatisticsData([ValueSource("_values")] string value)
        {
            Debug.Log(typeof(GoldAssetData));
            var type = System.Type.GetType("SEF.Data.GoldAssetData");
            Debug.Log(type);
            var instance = System.Activator.CreateInstance(type);
            var assetData = (IAssetData)instance;
            assetData.AssetValue = 100;
            _gameSystem.SetAsset(assetData);

            yield return null;

            var typeStatistics = System.Type.GetType($"SEF.Data.{value}AccumulateAssetStatisticsData");
            Debug.Log(_gameSystem.GetStatisticsValue(typeStatistics));
            Assert.IsTrue(_gameSystem.GetStatisticsValue(typeStatistics) == 100);
        }

        #endregion



        #region ##### 유닛 테크 진행 수 / 특정 유닛으로 테크 진행 수 #####
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TechUnitStatisticsData()
        {
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<TechUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TechUnitStatisticsData>() == 3);
        }
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TestTechUnitStatisticsData()
        {
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<TestTechUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TestTechUnitStatisticsData>() == 3);
        }

        #endregion



        #region ##### 유닛 업글 진행 수 / 특정 유닛 업글 진행 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_UpgradeUnitStatisticsData()
        {
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<UpgradeUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<UpgradeUnitStatisticsData>() == 5);
        }
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TestUpgradeUnitStatisticsData()
        {
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<TestUpgradeUnitStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TestUpgradeUnitStatisticsData>() == 5);
        }
        #endregion



        #region ##### 적 파괴 수 / 특정 적 파괴 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_DestroyEnemyStatisticsData()
        {
            int create = 3;
            List<EnemyActor> list = new List<EnemyActor>();

            for(int i = 0; i < create; i++)
            {
                var entity = new EnemyEntity();
                entity.SetData(EnemyData.Create_Test());
                entity.Initialize();

                var actor = EnemyActor.Create();
                actor.SetData(entity);
                actor.Activate();
                list.Add(actor);
            }

            while (list.Count > 0)
            {
                _gameSystem.DestroyedActor(list[0]);
                list.Remove(list[0]);
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<DestroyEnemyStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<DestroyEnemyStatisticsData>() == create);
        }

        [UnityTest]
        public IEnumerator StatisticsPlayTest_TestDestroyEnemyStatisticsData()
        {
            int create = 3;
            List<EnemyActor> list = new List<EnemyActor>();

            for (int i = 0; i < create; i++)
            {
                var entity = new EnemyEntity();
                entity.SetData(EnemyData.Create_Test());
                entity.Initialize();

                var actor = EnemyActor.Create();
                actor.SetData(entity);
                actor.Activate();
                list.Add(actor);
            }

            while (list.Count > 0)
            {
                _gameSystem.DestroyedActor(list[0]);
                list.Remove(list[0]);
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<TestDestroyEnemyStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TestDestroyEnemyStatisticsData>() == create);
        }

        #endregion



        #region ##### 웨이브 도달 / 레벨 도달 / 특정 레벨 도달 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_ArrivedLevelStatisticsData()
        {
            var data = new LevelWaveData();
            for (int i = 0; i < 50; i++)
            {
                data.IncreaseNumber();
                _gameSystem.ArrivedLevelWave(data);
            }


            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<ArrivedLevelStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<ArrivedLevelStatisticsData>() == 5);
        }

        [UnityTest]
        public IEnumerator StatisticsPlayTest_Lv001ArrivedLevelStatisticsData()
        {
            var data = new LevelWaveData();
            for (int i = 0; i < 20; i++)
            {
                data.IncreaseNumber();
                _gameSystem.ArrivedLevelWave(data);
            }


            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<Lv1ArrivedLevelStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<Lv1ArrivedLevelStatisticsData>() == 1);
        }
        #endregion



        #region ##### 제작소 라인 증축 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_ExpendWorkshopLineStatisticsData()
        {
            _gameSystem.ExpendWorkshop();
            _gameSystem.ExpendWorkshop();
            _gameSystem.ExpendWorkshop();
            _gameSystem.ExpendWorkshop();

            yield return null;

            Debug.Log(_gameSystem.GetStatisticsValue<ExpendWorkshopLineStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<ExpendWorkshopLineStatisticsData>() == 5);
        }

        #endregion



        #region ##### 마을 업글 진행 수 / 마을 테크 진행 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_UpgradeVillageStatisticsData()
        {
            _gameSystem.UpgradeVillage(0);
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<UpgradeVillageStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<UpgradeVillageStatisticsData>() == 1);
        }
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TechVillageStatisticsData()
        {
            _gameSystem.UpTechVillage(0);
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<TechVillageStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TechVillageStatisticsData>() == 1);
        }

        #endregion



        #region ##### 대장간 업글 진행 수 / 대장간 테크 진행 수 #####
        [UnityTest]
        public IEnumerator StatisticsPlayTest_UpgradeBlacksmithStatisticsData()
        {
            _gameSystem.UpgradeBlacksmith(0);
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<UpgradeBlacksmithStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<UpgradeBlacksmithStatisticsData>() == 1);

        }
        [UnityTest]
        public IEnumerator StatisticsPlayTest_TechBlacksmithStatisticsData()
        {
            _gameSystem.UpTechBlacksmith(0);
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<TechBlacksmithStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<TechBlacksmithStatisticsData>() == 1);

        }
        #endregion



        #region ##### 연구 진행 수 #####

        [UnityTest]
        public IEnumerator StatisticsPlayTest_SuccessResearchStatisticsData()
        {
            _gameSystem.SuccessedResearchData();
            yield return null;
            Debug.Log(_gameSystem.GetStatisticsValue<SuccessResearchStatisticsData>());
            Assert.IsTrue(_gameSystem.GetStatisticsValue<SuccessResearchStatisticsData>() == 1);

        }
        #endregion
    }
}