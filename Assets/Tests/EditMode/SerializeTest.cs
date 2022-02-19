#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using NUnit.Framework;
    using SEF.Data;
    using SEF.Entity;
    using SEF.Manager;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Statistics;
    using SEF.Quest;
    using SEF.Unit;

    public class SerializeTest
    {
        [System.Serializable]
        public class StorableData_Test
        {
            [SerializeField] public string _string = "StorableData_Test";
            [SerializeField] public int _int = 1234567;
            [SerializeField] public float _float = 1234.567f;
        }

        [System.Serializable]
        public class Statistics_Test : IStatisticsData
        {
        }

        [System.Serializable]
        public class ConditionQuest_Test : IConditionQuestData
        {
            public string AddressKey => "Test";
        }

        public class UnitActor_Test : UnitActor, IMonoBehaviourTest
        {
            public bool IsTestFinished => true;
        }

        private UnitActor _unitActor;
        private EnemyActor _enemyActor;
        private UnitManager _unitManager;

        private EnemyEntity _enemyEntity;
        private UnitEntity _unitEntity;
        private WorkshopLine _workshopLine;
        private WorkshopManager _workshopManager;

        private SmithyEntity _smithyEntity;
        private SmithyLine _smithyLine;
        private SmithyManager _smithyManager;

        private StatisticsEntity _statisticsEntity;
        private StatisticsPackage _statisticsPackage;

        private AssetEntity _assetEntity;
        private AssetPackage _assetPackage;

        private QuestEntity _questEntity;
        private QuestManager _questManager;

        private GameSystem _gameSystem;

        private GameManager _gameManager;

        [SetUp]
        public void SetUp()
        {

            _unitEntity = new UnitEntity();
            _unitEntity.Initialize();
            _unitEntity.UpTech(UnitData.Create_Test());

            _unitActor = new UnitActor();
            _unitActor.SetData_Test(_unitEntity);

            _enemyEntity = new EnemyEntity();
            _enemyEntity.Initialize();
            _enemyEntity.SetData(EnemyData.Create_Test(), NumberDataUtility.Create<LevelWaveData>());

            _enemyActor = new EnemyActor();
            _enemyActor.SetData_Test(_enemyEntity);
            Debug.Log(_enemyActor);

            _unitManager = UnitManager.Create();
            _unitManager.Initialize();

            _unitManager.ProductUnitActor(_unitEntity);

            _workshopLine = WorkshopLine.Create();
            _workshopLine.Initialize();
            _workshopLine.SetIndex(0);
            _workshopLine.UpTech(UnitData.Create_Test());

            _workshopManager = WorkshopManager.Create();
            _workshopManager.Initialize();


            _smithyEntity = new SmithyEntity();
            _smithyEntity.Initialize();
            _smithyEntity.SetData(SmithyData.Create_Test("Test"));

            _smithyLine = SmithyLine.Create();
            _smithyLine.Initialize();
            _smithyLine.SetIndex(0);
            _smithyLine.SetData(SmithyData.Create_Test("Test"));

            _smithyManager = SmithyManager.Create();
            _smithyManager.Initialize();

            _statisticsEntity = StatisticsEntity.Create(typeof(Statistics_Test));
            _statisticsPackage = StatisticsPackage.Create();
            _statisticsPackage.Initialize();

            _assetEntity = new AssetEntity(NumberDataUtility.Create<GoldAssetData>());

            _assetPackage = AssetPackage.Create();
            _assetPackage.Initialize();

            _questEntity = new QuestEntity();
            _questEntity.Initialize();
            _questEntity.SetData(QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ConditionQuest_Test), 1, typeof(GoldAssetData), 100));

            _questManager = QuestManager.Create();
            _questManager.Initialize();

            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _gameManager = GameManager.Create();
            _gameManager.Initialize_Test();
        }

        [TearDown]
        public void TearDown()
        {
            _enemyActor.CleanUp();
            _enemyEntity.CleanUp();
            _unitEntity.CleanUp();
            _unitActor.CleanUp();
            _unitManager.CleanUp();

            _workshopLine.CleanUp();
            _workshopLine = null;
            _workshopManager.CleanUp();
            _workshopManager = null;

            _smithyEntity.CleanUp();
            _smithyLine.CleanUp();
            _smithyLine = null;
            _smithyManager.CleanUp();
            _smithyManager = null;

            _statisticsPackage.CleanUp();

            _assetEntity.CleanUp();
            _assetPackage.CleanUp();

            _questEntity.CleanUp();
            _questManager.CleanUp();


            _gameSystem.CleanUp();
            _gameSystem = null;

            _gameManager.CleanUp_Test();
            _gameManager = null;
        }


        [Test]
        public void SerializeTest_UnitEntity_Serialize()
        {
            var data = _unitEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_UnitEntity_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (UnitEntityStorableData)data;
                Debug.Log(des.UnitKey + " " + des.UpgradeValue);

                var unitData = UnitData.Create_Test(des.UnitKey);
                var upgradeData = new UpgradeData();
                upgradeData.SetValue(des.UpgradeValue);

                var entity = new UnitEntity();
                entity.SetStorableData(unitData, upgradeData);

                Debug.Log(_unitEntity.UnitData.Key + " " + entity.UnitData.Key);
                Debug.Log(_unitEntity.UpgradeValue + " " + entity.UpgradeValue);

                Assert.AreEqual(_unitEntity.UnitData.Key, entity.UnitData.Key);
                Assert.AreEqual(_unitEntity.UpgradeValue, entity.UpgradeValue);
            });
        }

        [Test]
        public void SerializeTest_UnitEntity_Serialize_Deserialize()
        {            
            var data = _unitEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (UnitEntityStorableData)data;
                Debug.Log(des.UnitKey + " " + des.UpgradeValue);

                var unitData = UnitData.Create_Test(des.UnitKey);
                var upgradeData = new UpgradeData();
                upgradeData.SetValue(des.UpgradeValue);

                var entity = new UnitEntity();
                entity.SetStorableData(unitData, upgradeData);

                Debug.Log(_unitEntity.UnitData.Key + " " + entity.UnitData.Key);
                Debug.Log(_unitEntity.UpgradeValue + " " + entity.UpgradeValue);

                Assert.AreEqual(_unitEntity.UnitData.Key, entity.UnitData.Key);
                Assert.AreEqual(_unitEntity.UpgradeValue, entity.UpgradeValue);
            });
        }


        [Test]
        public void SerializeTest_WorkshopLine_Serialize()
        {
            var data = _workshopLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_WorkshopLine_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (WorkshopLineStorableData)data;
                _workshopLine.SetStorableData(des);
                Debug.Log(des.Index + " " + des.NowTime + " " + des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_WorkshopLine_Serialize_And_Deserialize()
        {
            var data = _workshopLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (WorkshopLineStorableData)data;
                _workshopLine.SetStorableData(des);

                Debug.Log(des.Index + " " + des.NowTime + " " + des.Children.Length);
            });
        }


        [Test]
        public void SerializeTest_WorkshopManager_Serialize()
        {
            var data = _workshopManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_WorkshopManager_Deserialize()
        {
            LoadFileData(data =>
            {
                _workshopManager.SetStorableData(data);
                Debug.Log(data.Children.Length);
            });
        }


        [Test]
        public void SerializeTest_WorkshopManager_Serialize_Deserialize()
        {
            var data = _workshopManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                _workshopManager.SetStorableData(data);

                Debug.Log(data.Children.Length);
            });
        }



        [Test]
        public void SerializeTest_SmithyEntity_Serialize()
        {            
            var data = _smithyEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_SmithyEntity_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (SmithyEntityStorableData)data;
                Debug.Log(des.UpgradeValue);

                var upgradeData = new UpgradeData();
                upgradeData.SetValue(des.UpgradeValue);

                var entity = new SmithyEntity();
                entity.SetStorableData(upgradeData);
                entity.SetData(SmithyData.Create_Test("Test"));

                Debug.Log(_smithyEntity.Key + " " + entity.Key);
                Debug.Log(_smithyEntity.UpgradeValue + " " + entity.UpgradeValue);

                Assert.AreEqual(_smithyEntity.Key, entity.Key);
                Assert.AreEqual(_smithyEntity.UpgradeValue, entity.UpgradeValue);
            });
        }


        [Test]
        public void SerializeTest_SmithyEntity_Serialize_Deserialize()
        {
            var data = _smithyEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (SmithyEntityStorableData)data;
                Debug.Log(des.UpgradeValue);

                var upgradeData = new UpgradeData();
                upgradeData.SetValue(des.UpgradeValue);

                var entity = new SmithyEntity();
                entity.SetStorableData(upgradeData);
                entity.SetData(SmithyData.Create_Test("Test"));

                Debug.Log(_smithyEntity.Key + " " + entity.Key);
                Debug.Log(_smithyEntity.UpgradeValue + " " + entity.UpgradeValue);

                Assert.AreEqual(_smithyEntity.Key, entity.Key);
                Assert.AreEqual(_smithyEntity.UpgradeValue, entity.UpgradeValue);
            });
        }


        [Test]
        public void SerializeTest_SmithyLine_Serialize()
        {
            var data = _smithyLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_SmithyLine_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (SmithyLineStorableData)data;
                _smithyLine.SetStorableData(des);

                Debug.Log(des.Index + " " + des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_SmithyLine_Serialize_Deserialize()
        {
            var data = _smithyLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data => 
            {
                var des = (SmithyLineStorableData)data;
                _smithyLine.SetStorableData(des);

                Debug.Log(des.Index + " " + des.Children.Length);
            });
        }


        [Test]
        public void SerializeTest_SmithyManager_Serialize()
        {
            var data = _smithyManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_SmithyManager_Deserialize()
        {
            LoadFileData(data =>
            {
                _smithyManager.SetStorableData(data);

                Debug.Log(data.Children.Length);
            });

        }

        [Test]
        public void SerializeTest_SmithyManager_Serialize_Deserialize()
        {
            var data = _smithyManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                _smithyManager.SetStorableData(data);

                Debug.Log(data.Children.Length);
            });
            
        }




        [Test]
        public void SerializeTest_StatisticsEntity_Serialize()
        {
            var data = _statisticsEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_StatisticsEntity_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (StatisticsEntityStorableData)data;
                Debug.Log(des.Value);

                var entity = new StatisticsEntity();
                entity.SetStorableData(des);

                Debug.Log(_statisticsEntity.GetStatisticsType() + " " + entity.GetStatisticsType());
                Debug.Log(_statisticsEntity.GetStatisticsValue() + " " + entity.GetStatisticsValue());
                Assert.AreEqual(_statisticsEntity.GetStatisticsType(), entity.GetStatisticsType());
                Assert.AreEqual(_statisticsEntity.GetStatisticsValue(), entity.GetStatisticsValue());
            });
        }


        [Test]
        public void SerializeTest_StatisticsEntity_Serialize_Deserialize()
        {
            var data = _statisticsEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (StatisticsEntityStorableData)data;
                Debug.Log(des.Value);

                var entity = new StatisticsEntity();
                entity.SetStorableData(des);

                Assert.AreEqual(_statisticsEntity.GetStatisticsType(), entity.GetStatisticsType());
                Assert.AreEqual(_statisticsEntity.GetStatisticsValue(), entity.GetStatisticsValue());
            });
        }


        [Test]
        public void SerializeTest_StatisticsPackage_Serialize()
        {
            var data = _smithyLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_StatisticsPackage_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (SmithyLineStorableData)data;
                _smithyLine.SetStorableData(des);

                Debug.Log(des.Index + " " + des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_StatisticsPackage_Serialize_Deserialize()
        {
            var data = _smithyLine.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (SmithyLineStorableData)data;
                _smithyLine.SetStorableData(des);

                Debug.Log(des.Index + " " + des.Children.Length);
            });
        }



        [Test]
        public void SerializeTest_AssetPackage_Serialize()
        {
            var data = _assetEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_AssetPackage_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (AssetEntityStorableData)data;
                var ddes = (AssetDataStorableData)data.Children[0];
                _assetEntity.SetValue(ddes.Value);

                Debug.Log(_assetEntity.AssetValue);
            });
        }

        [Test]
        public void SerializeTest_AssetPackage_Serialize_Deserialize()
        {
            var data = _assetEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (AssetEntityStorableData)data;
                var ddes = (AssetDataStorableData)data.Children[0];
                _assetEntity.SetValue(ddes.Value);

                Debug.Log(_assetEntity.AssetValue);
            });
        }




        [Test]
        public void SerializeTest_QuestEntity_Serialize()
        {
            var data = _questEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_QuestEntity_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (QuestEntityStorableData)data;
                Debug.Log(des.Value);

                var entity = new QuestEntity();
                entity.SetData(QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ConditionQuest_Test), 1, typeof(GoldAssetData), 100));
                entity.SetStorableData(des);

                Debug.Log(_questEntity.Key + " " + entity.Key);
                Debug.Log(_questEntity.NowValue + " " + entity.NowValue);
                Assert.AreEqual(_questEntity.Key, entity.Key);
                Assert.AreEqual(_questEntity.NowValue, entity.NowValue);
            });
        }


        [Test]
        public void SerializeTest_QuestEntity_Serialize_Deserialize()
        {
            var data = _questEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (QuestEntityStorableData)data;
                Debug.Log(des.Value);

                var entity = new QuestEntity();
                entity.SetData(QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Daily, typeof(ConditionQuest_Test), 1, typeof(GoldAssetData), 100));
                entity.SetStorableData(des);

                Debug.Log(_questEntity.Key + " " + entity.Key);
                Debug.Log(_questEntity.NowValue + " " + entity.NowValue);
                Assert.AreEqual(_questEntity.Key, entity.Key);
                Assert.AreEqual(_questEntity.NowValue, entity.NowValue);
            });
        }


        [Test]
        public void SerializeTest_QuestManager_Serialize()
        {
            var data = _questManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_QuestManager_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (QuestManagerStorableData)data;
                _questManager.SetStorableData(des);

                Debug.Log(des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_QuestManager_Serialize_Deserialize()
        {
            var data = _questManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (QuestManagerStorableData)data;
                _questManager.SetStorableData(des);

                Debug.Log(des.Children.Length);
            });
        }




        [UnityTest]
        public IEnumerator SerializeTest_UnitActor_Serialize()
        {
            var data = _unitActor.GetStorableData();
            Recursion(data);
            SaveFileData(data);
            yield return null;
        }


        [UnityTest]
        public IEnumerator SerializeTest_UnitActor_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (UnitActorStorableData)data;

                var unitActor = new UnitActor();
                unitActor.SetStorableData(des);
                
 
                Debug.Log(_unitActor.Key + " " + unitActor.Key);
                Assert.AreEqual(_unitActor.Key, unitActor.Key);
            });
            yield return null;
        }


        [UnityTest]
        public IEnumerator SerializeTest_UnitActor_Serialize_Deserialize()
        {
            var data = _unitActor.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            yield return null;

            LoadFileData(data =>
            {
                var des = (UnitActorStorableData)data;

                var unitActor = new UnitActor();
                unitActor.SetStorableData(des);


                Debug.Log(_unitActor.Key + " " + unitActor.Key);
                Assert.AreEqual(_unitActor.Key, unitActor.Key);
            });
        }

        [Test]
        public void SerializeTest_EnemyEntity_Serialize()
        {
            var data = _enemyEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_EnemyEntity_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (EnemyEntityStorableData)data;
                Debug.Log(des.UnitKey);

                var enemyData = Storage.DataStorage.Instance.GetDataOrNull<EnemyData>(des.UnitKey);
                var levelwaveData = new LevelWaveData();
                levelwaveData.SetValue(des.LevelWaveValue);

                EnemyEntity entity = new EnemyEntity();
                entity.Initialize();
                entity.SetData(enemyData, levelwaveData);

                Debug.Log(_enemyEntity.EnemyData + " " + entity.EnemyData);
                Debug.Log(_enemyEntity.GetLevelWaveData() + " " + entity.GetLevelWaveData());
                Assert.AreEqual(_enemyEntity.EnemyData, entity.EnemyData);
                Assert.AreEqual(_enemyEntity.GetLevelWaveData(), entity.GetLevelWaveData());
            });
        }


        [Test]
        public void SerializeTest_EnemyEntity_Serialize_Deserialize()
        {
            var data = _enemyEntity.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (EnemyEntityStorableData)data;
                Debug.Log(des.UnitKey);

                var enemyData = Storage.DataStorage.Instance.GetDataOrNull<EnemyData>(des.UnitKey);
                var levelwaveData = new LevelWaveData();
                levelwaveData.SetValue(des.LevelWaveValue);

                EnemyEntity entity = new EnemyEntity();
                entity.Initialize();
                entity.SetData(enemyData, levelwaveData);

                Debug.Log(_enemyEntity.EnemyData + " " + entity.EnemyData);
                Debug.Log(_enemyEntity.GetLevelWaveData() + " " + entity.GetLevelWaveData());
                Assert.AreEqual(_enemyEntity.EnemyData, entity.EnemyData);
                Assert.AreEqual(_enemyEntity.GetLevelWaveData(), entity.GetLevelWaveData());
            });
        }

        [Test]
        public void SerializeTest_EnemyActor_Serialize()
        {
            var data = _enemyActor.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_EnemyActor_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (EnemyActorStorableData)data;

                var actor = new EnemyActor();
                actor.SetStorableData(data);

                Debug.Log(_enemyActor + " " + actor);
                Assert.AreEqual(_enemyActor.Key, actor.Key);
            });
        }


        [Test]
        public void SerializeTest_EnemyActor_Serialize_Deserialize()
        {
            var data = _enemyActor.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (EnemyActorStorableData)data;

                var actor = new EnemyActor();
                actor.SetStorableData(data);

                Debug.Log(_enemyActor.Key + " " + actor.Key);
                Assert.AreEqual(_enemyActor.Key, actor.Key);
            });
        }


        [Test]
        public void SerializeTest_UnitManager_Serialize()
        {
            var data = _unitManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }

        [Test]
        public void SerializeTest_UnitManager_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (UnitManagerStorableData)data;
                _unitManager.SetStorableData(des);

                Debug.Log(des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_UnitManager_Serialize_Deserialize()
        {
            var data = _unitManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (UnitManagerStorableData)data;
                _unitManager.SetStorableData(des);

                Debug.Log(des.Children.Length);
            });
        }


        [Test]
        public void SerializeTest_GameSystem_Serialize()
        {
            var data = _gameSystem.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_GameSystem_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (SystemStorableData)data;
                _gameSystem.SetStorableData(des);
                Debug.Log(des.Children.Length + " " + des.Dictionary.Count);
            });
        }

        [Test]
        public void SerializeTest_GameSystem_Serialize_Deserialize()
        {
            var data = _gameSystem.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (SystemStorableData)data;
                _gameSystem.SetStorableData(des);
                Debug.Log(des.Children.Length + " " + des.Dictionary.Count);
            });
        }

        [Test]
        public void SerializeTest_GameManager_Serialize()
        {
            var data = _gameManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);
        }


        [Test]
        public void SerializeTest_GameManager_Deserialize()
        {
            LoadFileData(data =>
            {
                var des = (GameManagerStorableData)data;
                _gameManager.SetStorableData(des);
                Debug.Log(des.Children.Length);
            });
        }

        [Test]
        public void SerializeTest_GameManager_Serialize_Deserialize()
        {
            var data = _gameManager.GetStorableData();
            Recursion(data);
            SaveFileData(data);

            LoadFileData(data =>
            {
                var des = (GameManagerStorableData)data;
                _gameManager.SetStorableData(des);
            });
        }

        [UnityTest]
        public IEnumerator SerializeTest_Encryption()
        {
            var data = new StorableData_Test();
            Utility.IO.StorableDataIO.Current.SaveFileData(data, "strtest", null);
            yield return null;
            Utility.IO.StorableDataIO.Dispose();
        }

        [UnityTest]
        public IEnumerator SerializeTest_Decryption()
        {
            Utility.IO.StorableDataIO.Current.LoadFileData("strtest", null, (result, obj) =>
            {
                if (result == Utility.IO.TYPE_IO_RESULT.Success)
                {
                    var dec = obj as StorableData_Test;
                    Debug.Log($"{dec._string}{dec._int}{dec._float}");
                }
                else
                {
                    Debug.LogWarning("복호화 실패");
                }
            });
            yield return null;
            Utility.IO.StorableDataIO.Dispose();
        }

        [UnityTest]
        public IEnumerator SerializeTest_Encryption_Decryption()
        {
            yield return SerializeTest_Encryption();
            yield return SerializeTest_Decryption();
           
        }


        [Test]
        public void SerializeTest_SerializeBigNumberData_DeserializeBigNumberData()
        {
            var data = NumberDataUtility.Create<HealthData>();
            data.ValueText = "1000";
            data.SetValue();
            
            var sData = data.GetSerializeData();

            var dData = sData.GetDeserializeData();
            Debug.Log(dData.GetType().Name + " " + data.GetType().Name);
            Debug.Log(dData.GetValue() + " " + data.GetValue());
            Assert.AreEqual(dData.GetType(), data.GetType());
            Assert.AreEqual(dData.GetValue(), data.GetValue());
        }

        private void SaveFileData(Utility.IO.StorableData data)
        {
            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //System.IO.FileStream stream = new System.IO.FileStream(Application.dataPath + "TestFormatter.txt", System.IO.FileMode.Create);
            //formatter.Serialize(stream, data);
            //stream.Close();

            Utility.IO.StorableDataIO.Current.SaveFileData_NotCrypto(data, "test_file", result => {
                Debug.Log(result);
            });
        }

        private void LoadFileData(System.Action<Utility.IO.StorableData> callback)
        {
            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //System.IO.FileStream stream = new System.IO.FileStream(Application.dataPath + "TestFormatter.txt", System.IO.FileMode.Open);
            //var data = (Utility.IO.StorableData)formatter.Deserialize(stream);
            //stream.Close();
            Utility.IO.StorableDataIO.Current.LoadFileData_NotCrypto("test_file", null, (result, obj) =>
            {
                Debug.Log(result);
                callback?.Invoke((Utility.IO.StorableData)obj);
            });
        }

        private void Recursion(Utility.IO.StorableData data)
        {
            if(data.Children != null)
            {
                for(int i = 0; i < data.Children.Length; i++)
                {
                    Recursion(data.Children[i]);
                }
            }
            Debug.Log(data.ToString());
        }

        private void GetString(byte[] arr)
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                str += arr[i];
            }
            Debug.Log(str);
        }
    }
}
#endif