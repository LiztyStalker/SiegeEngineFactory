#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Quest;
    using Manager;
    using Data;
    using Entity;

    public class QuestTest
    {

        private QuestPackage _package;
        private QuestManager _manager;
        private GameSystem _gameSystem;

        private class Test1 : IQuestData
        {
        }
        private class Test2 : IQuestData
        {

        }
        private class Test3 : IQuestData
        {
        }




        [SetUp]
        public void SetUp()
        {
            _package = QuestPackage.Create();
            _package.Initialize(null);

            _manager = QuestManager.Create();
            _manager.Initialize();

            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();
            _gameSystem.AddOnRefreshQuestEntityListener(entity =>
            {
                Debug.Log($"{entity.Key} {entity.NowValue}/{entity.GoalValue} {entity.HasQuestGoal()}");
            });
        }

        [TearDown]
        public void TearDown()
        {
            _package.CleanUp();
            _manager.CleanUp();
            _gameSystem.CleanUp();
            _gameSystem.RemoveOnRefreshQuestEntityListener(entity =>
            {
                Debug.Log($"{entity.Key} {entity.NowValue}/{entity.GoalValue}");
            });
        }
                
        [Test]
        public void QuestManagerTest_QuestData()
        {
            _manager.AddOnRefreshListener(entity =>
            {
                Debug.Log($"{entity.Key} {entity.NowValue}/{entity.GoalValue}");
            });
            _manager.RefreshAllQuests();
        }

        [Test]
        public void QuestTest_Initialize()
        {

        }

        [Test]
        public void QuestTest_SetQuestData_Test1()
        {
            var test1 = new Test1();
            _package.SetQuestData(test1, 1);
            Assert.IsTrue(_package.HasQuestData(test1, 1));
        }

        [Test]
        public void QuestTest_SetQuestData_Test2_x2()
        {
            var test2 = new Test2();
            _package.SetQuestData(test2, 2);
            Assert.IsTrue(_package.HasQuestData(test2, 2));
        }

        [Test]
        public void QuestTest_SetQuestData_Test1_Test2()
        {
            var test1 = new Test1();
            _package.SetQuestData(test1, 1);
            Assert.IsTrue(_package.HasQuestData(test1, 1));

            var test2 = new Test2();
            _package.SetQuestData(test2, 2);
            Assert.IsTrue(_package.HasQuestData(test2, 2));

        }

        [Test]
        public void QuestTest_SetQuestData_Test3_5()
        {
            var test3 = new Test3();
            _package.SetQuestData(test3, 1);
            Assert.IsTrue(_package.HasQuestData(test3, 1));
            _package.SetQuestData(test3, 5);
            Assert.IsTrue(_package.HasQuestData(test3, 5));

        }


        private Test1 _test1 = new Test1();
        private Test2 _test2 = new Test2();
        private Test2 _test3 = new Test2();

        private void Initialize_Test()
        {
            _test1 = new Test1();
            _test2 = new Test2();

            _package.SetQuestData(_test1, 1);
            _package.SetQuestData(_test2, 2);
        }




        [Test]
        public void QuestTest_HasQuestData_Test1_1()
        {
            Initialize_Test();
            Assert.IsTrue(_package.HasQuestData(_test1, 1));
        }
        [Test]
        public void QuestTest_HasQuestData_Test1_2()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasQuestData(_test1, 2));
        }

        [Test]
        public void QuestTest_HasQuestData_Test2_1()
        {
            Initialize_Test();
            Assert.IsTrue(_package.HasQuestData(_test2, 1));
        }

        [Test]
        public void QuestTest_HasQuestData_Test2_3()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasQuestData(_test2, 3));
        }

        [Test]
        public void QuestTest_HasQuestData_Test3_1()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasQuestData(_test3, 1));

        }

        [Test]
        public void QuestTest_HasQuestData_Test1_n1()
        {
            Initialize_Test();
            try
            {
                _package.HasQuestData(_test1, -1);
                Assert.Fail("음수 적용 불가");
            }
            catch
            {
                Assert.Pass();
            }
        }


        #region ##### 즉석웨이브달성 누적웨이브달성 #####
        [Test]
        public void QuestIntegrationTest_ArrivedWave_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ArrivedWaveConditionQuestData), 1, typeof(GoldAssetData), 100);
            var data2 = QuestData.Create_Test("AccumulativeryTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyArrivedWaveConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
        }

        #endregion



        #region ##### 즉석레벨달성 누적레벨달성 #####

        [Test]
        public void QuestIntegrationTest_ArrivedLevel_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ArrivedLevelConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyArrivedLevelConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);


            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            for(int i = 0; i <= 30; i++)
            {
                levelWaveData.IncreaseNumber();
                _gameSystem.ArrivedLevelWave(levelWaveData);
            }
        }
        #endregion

        [Test]
        public void QuestIntegrationTest_DestroyEnemyConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(DestroyEnemyConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();

            var enemyActor = SEF.Unit.EnemyActor.Create_Test();
            var enemyEntity = new EnemyEntity();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor.SetData(enemyEntity);

            _gameSystem.DestroyedActor(enemyActor);
        }

        [Test]
        public void QuestIntegrationTest_DestroyEnemyConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(DestroyEnemyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);


            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();

            var enemyEntity = new EnemyEntity();
            enemyEntity.SetData(enemyData, levelWaveData);



            var enemyActor1 = SEF.Unit.EnemyActor.Create_Test();
            enemyActor1.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor1);
            var enemyActor2 = SEF.Unit.EnemyActor.Create_Test();
            enemyActor2.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor2);
            var enemyActor3 = SEF.Unit.EnemyActor.Create_Test();
            enemyActor3.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor3);

        }

        [Test]
        public void QuestIntegrationTest_UpgradeUnitConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeUnitConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeWorkshop(0);
        }

        [Test]
        public void QuestIntegrationTest_UpgradeUnitConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
        }

        [Test]
        public void QuestIntegrationTest_TechUnitConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechUnitConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
        }
        [Test]
        public void QuestIntegrationTest_TechUnitConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitData.Create_Test());
            //결과가 3/1이 나옴
        }

        [Test]
        public void QuestIntegrationTest_UpgradeBlacksmithConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeSmithyConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeSmithy(0);
        }
        [Test]
        public void QuestIntegrationTest_UpgradeBlacksmithConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeSmithy(0);
            _gameSystem.UpgradeSmithy(0);
            _gameSystem.UpgradeSmithy(0);
        }
        [Test]
        public void QuestIntegrationTest_TechBlacksmithConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechSmithyConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechSmithy(0);
        }
        [Test]
        public void QuestIntegrationTest_TechBlacksmithConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechSmithy(0);
            _gameSystem.UpTechSmithy(0);
            _gameSystem.UpTechSmithy(0);
        }
        [Test]
        public void QuestIntegrationTest_UpgradeVillageConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeVillageConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeVillage(0);
        }
        [Test]
        public void QuestIntegrationTest_UpgradeVillageConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeVillage(0);
            _gameSystem.UpgradeVillage(0);
            _gameSystem.UpgradeVillage(0);
        }
        [Test]
        public void QuestIntegrationTest_TechVillageConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechVillageConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechVillage(0);
        }
        [Test]
        public void QuestIntegrationTest_TechVillageConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpTechVillage(0);
            _gameSystem.UpTechVillage(0);
            _gameSystem.UpTechVillage(0);
        }
        [Test]
        public void QuestIntegrationTest_UpgradeCommanderConditionQuestData()
        {
            Assert.Fail("개발되지 않음");
        }
        [Test]
        public void QuestIntegrationTest_UpgradeCommanderConditionQuestData_x3()
        {
            Assert.Fail("개발되지 않음");
        }
        [Test]
        public void QuestIntegrationTest_SuccessedResearchConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(SuccessResearchConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.SuccessedResearchData();
        }
        [Test]
        public void QuestIntegrationTest_SuccessedResearchConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(SuccessResearchConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.SuccessedResearchData();
            _gameSystem.SuccessedResearchData();
            _gameSystem.SuccessedResearchData();

            
        }

        [Test]
        public void QuestIntegrationTest_SuccessedDailyConditionQuestData()
        {
            Assert.Fail("개발되지 않음");

        }
        [Test]
        public void QuestIntegrationTest_SuccessedDailyConditionQuestData_x3()
        {
            Assert.Fail("개발되지 않음");

        }
        [Test]
        public void QuestIntegrationTest_SuccessedWeeklyConditionQuestData()
        {
            Assert.Fail("개발되지 않음");

        }
        [Test]
        public void QuestIntegrationTest_SuccessedWeeklyConditionQuestData_x3()
        {
            Assert.Fail("개발되지 않음");

        }
        [Test]
        public void QuestIntegrationTest_SuccessedExpeditionConditionQuestData()
        {
            Assert.Fail("개발되지 않음");

        }
        [Test]
        public void QuestIntegrationTest_SuccessedExpeditionConditionQuestData_x3()
        {
            Assert.Fail("개발되지 않음");
        }

        [Test]
        public void QuestTest_GetRewardAssetData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeUnitConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeWorkshop(0);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "Test"); 
        }

        [Test]
        public void QuestTest_GetRewardAssetData_Multiple()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeUnitConditionQuestData), 1, typeof(GoldAssetData), 100);

            var arr = new QuestConditionData[3];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = QuestConditionData.Create_Test(typeof(UpgradeUnitConditionQuestData), i + 1, typeof(GoldAssetData), 100);
            }
            data.SetQuestDataArray_Test(arr);

            var entity = QuestEntity.Create();
            entity.SetData(data);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);

            _gameSystem.UpgradeWorkshop(0);
            
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "Test");

            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "Test");

            _gameSystem.UpgradeWorkshop(0);
        }
    }
}
#endif