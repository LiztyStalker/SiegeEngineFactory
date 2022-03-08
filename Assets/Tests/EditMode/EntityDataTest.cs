#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Unity.Mathematics;
    using System.Numerics;
    using Data;
    using Entity;

    public class EntityDataTest
    {
        private UnitEntity _unitEntity_Dummy;
        private EnemyEntity _enemyEntity_Dummy;
        private LevelWaveData _levelWaveData;

        [SetUp]
        public void SetUp()
        {
            _unitEntity_Dummy.Initialize();
            _unitEntity_Dummy.UpTech(UnitData.Create_Test());
            _enemyEntity_Dummy.Initialize();
            _enemyEntity_Dummy.SetData(EnemyData.Create_Test(), NumberDataUtility.Create<LevelWaveData>());
            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
        }


        [TearDown]
        public void TearDown()
        {
            _unitEntity_Dummy.CleanUp();
            _enemyEntity_Dummy.CleanUp();
        }

        [Test]
        public void AssetDataTest_Upgrade_UnitGoldAssetData()
        {
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 10);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 12);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 15);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 17);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 21);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 24);
        }

        [Test]
        public void AssetDataTest_Upgrade_UnitHealthAssetData()
        {
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "50");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "57");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "64");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "72");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "81");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.HealthData.GetValue() == "90");
        }

        [Test]
        public void AssetDataTest_Upgrade_UnitAttackAssetData()
        {
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "20");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "23");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "26");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "29");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "33");
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "37");
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyHealthData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 100);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 195);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 320);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 475);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 660);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 875);
            _levelWaveData.IncreaseNumber();//6
            _levelWaveData.IncreaseNumber();//7
            _levelWaveData.IncreaseNumber();//8
            _levelWaveData.IncreaseNumber();//9
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 6105);
            _levelWaveData.IncreaseNumber();//10
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 500);
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyAttackData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 20);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 40);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.GetValue());
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.GetValue() == "23");
        }


        [Test]
        public void AssetDataTest_Upgrade_EnemyRewardAssetData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 10);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 16);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 24);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 32);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 42);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 52);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 209);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 40);
        }
    }
}
#endif