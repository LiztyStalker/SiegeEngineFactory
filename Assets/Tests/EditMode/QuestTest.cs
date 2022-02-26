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


        #region ##### 즉석적파괴 누적적파괴 #####


        [Test]
        public void QuestIntegrationTest_DestroyEnemy_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(DestroyEnemyConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyDestroyEnemyConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();

            var enemyActor = SEF.Unit.EnemyActor.Create_Test();
            var enemyEntity = new EnemyEntity();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor.SetData(enemyEntity);

            var enemyActor1 = Unit.EnemyActor.Create_Test();
            enemyActor1.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor1);
            var enemyActor2 = Unit.EnemyActor.Create_Test();
            enemyActor2.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor2);
            var enemyActor3 = Unit.EnemyActor.Create_Test();
            enemyActor3.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor3);

        }
        #endregion



        #region ##### 즉석보스파괴 누적보스파괴 #####


        [Test]
        public void QuestIntegrationTest_DestroyBoss_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(DestroyBossConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyDestroyBossConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            levelWaveData.SetValue(8);

            var enemyEntity = new EnemyEntity();

            var enemyActor1 = Unit.EnemyActor.Create_Test();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor1.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor1);

            var enemyActor2 = Unit.EnemyActor.Create_Test();
            levelWaveData.IncreaseNumber();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor2.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor2);

            var enemyActor3 = Unit.EnemyActor.Create_Test();
            levelWaveData.IncreaseNumber();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor3.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor3);

        }
        #endregion



        #region ##### 즉석테마보스파괴 누적테마보스파괴 #####

        [Test]
        public void QuestIntegrationTest_DestroyThemeBoss_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(DestroyThemeBossConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyDestroyThemeBossConditionQuestData), 1, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            levelWaveData.SetValue(98);

            var enemyEntity = new EnemyEntity();

            var enemyActor1 = Unit.EnemyActor.Create_Test();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor1.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor1);

            var enemyActor2 = Unit.EnemyActor.Create_Test();
            levelWaveData.IncreaseNumber();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor2.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor2);

            var enemyActor3 = Unit.EnemyActor.Create_Test();
            levelWaveData.IncreaseNumber();
            enemyEntity.SetData(enemyData, levelWaveData);
            enemyActor3.SetData(enemyEntity);
            _gameSystem.DestroyedActor(enemyActor3);

        }
        #endregion




        #region ##### 즉석유닛생산 누적유닛생산 #####
        [Test]
        public void QuestIntegrationTest_CreateUnit_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(CreateUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var data2 = QuestData.Create_Test("AccumulativeryTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyCreateUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddProductUnitListener(entity => { Debug.Log("Product"); });

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.RunProcess(1f);
            _gameSystem.RunProcess(1f);
            _gameSystem.RunProcess(1f);

            _gameSystem.RemoveProductUnitListener(entity => { Debug.Log("Product"); });

        }

        #endregion

        #region ##### 유닛업글 누적유닛업글 #####

        [Test]
        public void QuestIntegrationTest_UpgradeUnit_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyUpgradeUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
            _gameSystem.UpgradeWorkshop(0);
        }

        #endregion



        #region ##### 유닛테크 누적유닛테크 #####

        [Test]
        public void QuestIntegrationTest_TechUnit_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyTechUnitConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpTechWorkshop(0, UnitTechData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitTechData.Create_Test());
            _gameSystem.UpTechWorkshop(0, UnitTechData.Create_Test());
        }

        #endregion



        #region ##### 즉석유닛확장 누적유닛확장 #####
        [Test]
        public void QuestIntegrationTest_ExpendWorkshopLine_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ExpendWorkshopLineConditionQuestData), 3, typeof(GoldAssetData), 100);
            var data2 = QuestData.Create_Test("AccumulativeryTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyExpendWorkshopLineConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.ExpendWorkshop();
            _gameSystem.ExpendWorkshop();
            _gameSystem.ExpendWorkshop();
        }

        #endregion




        #region ##### 대장간업글 누적대장간업글 #####
        [Test]
        public void QuestIntegrationTest_UpgradeSmithy_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyUpgradeSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpgradeSmithy(0);
            _gameSystem.UpgradeSmithy(0);
            _gameSystem.UpgradeSmithy(0);
        }
        #endregion


        #region ##### 대장간테크 누적대장간테크 #####
        [Test]
        public void QuestIntegrationTest_TechSmithy_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyTechSmithyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpTechSmithy(0);
            _gameSystem.UpTechSmithy(0);
            _gameSystem.UpTechSmithy(0);
        }
        #endregion



        #region ##### 마을업글 누적마을업글 #####

        [Test]
        public void QuestIntegrationTest_UpgradeVillage_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(UpgradeVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyUpgradeVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpgradeVillage(0);
            _gameSystem.UpgradeVillage(0);
            _gameSystem.UpgradeVillage(0);
        }
        #endregion


        #region ##### 마을테크 누적마을테크 #####
        [Test]
        public void QuestIntegrationTest_TechVillage_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(TechVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyTechVillageConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.UpTechVillage(0);
            _gameSystem.UpTechVillage(0);
            _gameSystem.UpTechVillage(0);
        }
        #endregion


        #region ##### 지휘관업글 누적지휘관업글 #####
        [Test]
        public void QuestIntegrationTest_UpgradeCommander_And_Accumulatively()
        {
            Assert.Fail("개발되지 않음");
        }
        #endregion



        #region ##### 연구진행 누적연구진행 #####
        [Test]
        public void QuestIntegrationTest_SuccessResearch_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(SuccessResearchConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelySuccessResearchConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.SuccessResearchData();
            _gameSystem.SuccessResearchData();
            _gameSystem.SuccessResearchData();
        }
        #endregion


        #region ##### 일일진행 누적일일진행 #####
        [Test]
        public void QuestIntegrationTest_AchievedDaily_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AchievedDailyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Daily, typeof(AccumulativelyAchievedDailyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Daily, entity2);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Daily, "InstantTest");
        }
        #endregion



        #region ##### 주간진행 누적주간진행 #####
        [Test]
        public void QuestIntegrationTest_AchievedWeekly_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Weekly, typeof(AchievedWeeklyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Weekly, typeof(AccumulativelyAchievedWeeklyConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Weekly, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Weekly, entity2);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Weekly, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Weekly, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Weekly, "InstantTest");
        }
        #endregion



        #region ##### 도전진행 누적도전진행 #####
        [Test]
        public void QuestIntegrationTest_AchievedChallenge_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Challenge, typeof(AchievedChallengeConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Challenge, typeof(AccumulativelyAchievedChallengeConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Challenge, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Challenge, entity2);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Challenge, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Challenge, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Challenge, "InstantTest");
        }
        #endregion

        #region ##### 목표진행 누적목표진행 #####
        [Test]
        public void QuestIntegrationTest_AchievedGoal_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(AchievedGoalConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(AccumulativelyAchievedGoalConditionQuestData), 3, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity2);

            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Goal, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Goal, "InstantTest");
            _gameSystem.GetRewardAssetData(QuestData.TYPE_QUEST_GROUP.Goal, "InstantTest");
        }
        #endregion


        #region ##### 원정진행 누적원정진행 #####

        [Test]
        public void QuestIntegrationTest_SuccesseExpedition_And_Accumulatively()
        {
            Assert.Fail("개발되지 않음");

        }
        #endregion



        #region ##### 골드획득 누적골드획득 #####
        [Test]
        public void QuestIntegrationTest_GetGoldAssetData_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(GetGoldAssetDataConditionQuestData), 300, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(AccumulativelyGetGoldAssetDataConditionQuestData), 300, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity2);


            var asset = new GoldAssetData();
            asset.ValueText = "100";
            asset.SetValue();

            _gameSystem.AddAsset(asset);
            _gameSystem.AddAsset(asset);
            _gameSystem.AddAsset(asset);
        }
        #endregion


        #region ##### 골드획득 누적골드획득 #####
        [Test]
        public void QuestIntegrationTest_UsedGoldAssetData_And_Accumulatively()
        {
            var data1 = QuestData.Create_Test("InstantTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(UsedGoldAssetDataConditionQuestData), 300, typeof(GoldAssetData), 100);
            var entity1 = QuestEntity.Create();
            entity1.SetData(data1);

            var data2 = QuestData.Create_Test("AccumulativelyTest", QuestData.TYPE_QUEST_GROUP.Goal, typeof(AccumulativelyUsedGoldAssetDataConditionQuestData), 300, typeof(GoldAssetData), 100);
            var entity2 = QuestEntity.Create();
            entity2.SetData(data2);

            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity1);
            _gameSystem.AddQuestEntity(QuestData.TYPE_QUEST_GROUP.Goal, entity2);


            var asset = new GoldAssetData();
            asset.ValueText = "500";
            asset.SetValue();

            _gameSystem.AddAsset(asset);

            asset.ValueText = "100";
            asset.SetValue();

            _gameSystem.SubjectAsset(asset);
            _gameSystem.SubjectAsset(asset);
            _gameSystem.SubjectAsset(asset);
        }
        #endregion


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