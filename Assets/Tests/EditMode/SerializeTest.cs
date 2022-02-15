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
        public void SerializeTest_UnitEntity()
        {
            var data = _unitEntity.GetStorableData();
            Debug.Log(JsonUtility.ToJson(data));
        }

        [Test]
        public void SerializeTest_WorkshopLine()
        {
            var data = _workshopLine.GetStorableData();
            Debug.Log(JsonUtility.ToJson(data));
        }


        [Test]
        public void SerializeTest_WorkshopManager()
        {
            var data = _workshopManager.GetStorableData();
            Debug.Log(JsonUtility.ToJson(data));
        }

        [Test]
        public void SerializeTest_GameSystem()
        {
            var data = _gameSystem.GetStorableData();
            Debug.Log(JsonUtility.ToJson(data));
        }



    }
}
#endif