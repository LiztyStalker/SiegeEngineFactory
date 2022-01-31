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
            _manager.Initialize(null);

            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize(null);
            _gameSystem.AddOnRefreshQuestEntityListener(entity =>
            {
                Debug.Log($"{entity.Key} {entity.NowValue}/{entity.GoalValue}");
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

        [Test]
        public void QuestIntegrationTest_ArrivedLevelConditionQuestData()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ArrivedLevelConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);


            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);

        }

        [Test]
        public void QuestIntegrationTest_ArrivedLevelConditionQuestData_x3()
        {
            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ArrivedLevelConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity = QuestEntity.Create();
            entity.SetData(data);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity);


            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
            levelWaveData.IncreaseNumber();
            _gameSystem.ArrivedLevelWave(levelWaveData);
        }


        [Test]
        public void QuestIntegrationTest_DestroyEnemyConditionQuestData()
        {

        }

        [Test]
        public void QuestIntegrationTest_DestroyEnemyConditionQuestData_x3()
        {

        }

        [Test]
        public void QuestIntegrationTest_UpgradeUnitConditionQuestData()
        {

        }

        [Test]
        public void QuestIntegrationTest_UpgradeUnitConditionQuestData_x3()
        {

        }

        [Test]
        public void QuestIntegrationTest_TechUnitConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_TechUnitConditionQuestData_x3()
        {

        }

        [Test]
        public void QuestIntegrationTest_UpgradeBlacksmithConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_UpgradeBlacksmithConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_TechBlacksmithConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_TechBlacksmithConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_UpgradeVillageConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_UpgradeVillageConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_TechVillageConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_TechVillageConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_UpgradeCommanderConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_UpgradeCommanderConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedResearchConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedResearchConditionQuestData_x3()
        {

        }

        [Test]
        public void QuestIntegrationTest_SuccessedDailyConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedDailyConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedWeeklyConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedWeeklyConditionQuestData_x3()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedExpeditionConditionQuestData()
        {

        }
        [Test]
        public void QuestIntegrationTest_SuccessedExpeditionConditionQuestData_x3()
        {

        }       
    }
}
#endif