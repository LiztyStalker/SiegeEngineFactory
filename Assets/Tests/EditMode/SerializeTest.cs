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

    public class SerializeTest
    {
        [System.Serializable]
        public class StorableData_Test
        {
            [SerializeField] public string _string = "StorableData_Test";
            [SerializeField] public int _int = 1234567;
            [SerializeField] public float _float = 1234.567f;
        }




        private UnitEntity _unitEntity;
        private WorkshopLine _workshopLine;
        private WorkshopManager _workshopManager;

        private BlacksmithEntity _smithyEntity;
        private BlacksmithLine _smithyLine;
        private BlacksmithManager _smithyManager;

        private GameSystem _gameSystem;

        private GameManager _gameManager;

        [SetUp]
        public void SetUp()
        {
            _unitEntity = new UnitEntity();
            _unitEntity.Initialize();
            _unitEntity.UpTech(UnitData.Create_Test());

            _workshopLine = WorkshopLine.Create();
            _workshopLine.Initialize();
            _workshopLine.SetIndex(0);
            _workshopLine.UpTech(UnitData.Create_Test());

            _workshopManager = WorkshopManager.Create();
            _workshopManager.Initialize();


            _smithyEntity = new BlacksmithEntity();
            _smithyEntity.Initialize();
            _smithyEntity.SetData(BlacksmithData.Create_Test("Test"));

            _smithyLine = BlacksmithLine.Create();
            _smithyLine.Initialize();
            _smithyLine.SetIndex(0);
            _smithyLine.SetData(BlacksmithData.Create_Test("Test"));

            _smithyManager = BlacksmithManager.Create();
            _smithyManager.Initialize();

            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _gameManager = GameManager.Create();
            _gameManager.Initialize_Test();
        }

        [TearDown]
        public void TearDown()
        {
            _unitEntity.CleanUp();
            _workshopLine.CleanUp();
            _workshopLine = null;
            _workshopManager.CleanUp();
            _workshopManager = null;

            _smithyEntity.CleanUp();
            _smithyLine.CleanUp();
            _smithyLine = null;
            _smithyManager.CleanUp();
            _smithyManager = null;

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

                var entity = new BlacksmithEntity();
                entity.SetStorableData(upgradeData);
                entity.SetData(BlacksmithData.Create_Test("Test"));

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

                var entity = new BlacksmithEntity();
                entity.SetStorableData(upgradeData);
                entity.SetData(BlacksmithData.Create_Test("Test"));

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
                    Debug.LogWarning("��ȣȭ ����");
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