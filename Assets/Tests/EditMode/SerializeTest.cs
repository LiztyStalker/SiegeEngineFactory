#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Statistics;
    using SEF.Entity;
    using SEF.Data;
    using SEF.Manager;

    public class SerializeTest
    {

        private UnitEntity _unitEntity;
        private WorkshopLine _workshopLine;
        private WorkshopManager _workshopManager;
        private GameSystem _gameSystem;

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

            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _unitEntity.CleanUp();
            _workshopLine.CleanUp();
            _workshopLine = null;
            _workshopManager.CleanUp();
            _workshopManager = null;
            _gameSystem.CleanUp();
            _gameSystem = null;
        }


        [Test]
        public void SerializeTest_UnitEntity_Serialize()
        {
            var data = _unitEntity.GetStorableData();
            Recursion(data);
            WriteBinaryFormatter(data);
        }

        [Test]
        public void SerializeTest_UnitEntity_Serialize_Deserialize()
        {
            var data = _unitEntity.GetStorableData();
            Recursion(data);
            WriteBinaryFormatter(data);

            var des = (UnitEntityStorableData)ReadBinaryFormatter();

            Debug.Log(des.UnitKey + " " + des.UpgradeValue);

            var unitData = UnitData.Create_Test(des.UnitKey);
            var upgradeData = new UpgradeData();
            upgradeData.SetValue_Test(des.UpgradeValue);

            var entity = new UnitEntity();
            entity.SetStorableData(unitData, upgradeData);

            Debug.Log(_unitEntity.UnitData.Key + " " + entity.UnitData.Key);
            Debug.Log(_unitEntity.UpgradeValue + " " + entity.UpgradeValue);

            Assert.AreEqual(_unitEntity.UnitData.Key, entity.UnitData.Key);
            Assert.AreEqual(_unitEntity.UpgradeValue, entity.UpgradeValue);
        }


        [Test]
        public void SerializeTest_WorkshopLine()
        {
            var data = _workshopLine.GetStorableData();
            Recursion(data);
            WriteBinaryFormatter(data);
        }


        [Test]
        public void SerializeTest_WorkshopManager()
        {
            var data = _workshopManager.GetStorableData();
            Recursion(data);
            WriteBinaryFormatter(data);
        }

        [Test]
        public void SerializeTest_GameSystem()
        {
            var data = _gameSystem.GetStorableData();
            Recursion(data);
            WriteBinaryFormatter(data);
        }


        private void WriteBinaryFormatter(Utility.IO.StorableData data)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.FileStream stream = new System.IO.FileStream(Application.dataPath + "TestFormatter.txt", System.IO.FileMode.Create);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        private Utility.IO.StorableData ReadBinaryFormatter()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.FileStream stream = new System.IO.FileStream(Application.dataPath + "TestFormatter.txt", System.IO.FileMode.Open);
            var data = (Utility.IO.StorableData)formatter.Deserialize(stream);
            stream.Close();
            return data;

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