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
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 11);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 13);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 14);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 16);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 17);
        }

        [Test]
        public void AssetDataTest_Upgrade_UnitHealthAssetData()
        {
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 100);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 106);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 112);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 118);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 125);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 132);
        }

        [Test]
        public void AssetDataTest_Upgrade_UnitAttackAssetData()
        {
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 15);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 16);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 18);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 20);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 22);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.DamageData.Value);
            Assert.IsTrue(_unitEntity_Dummy.DamageData.Value == 24);
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyHealthData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 100);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 101);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 102);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 103);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 104);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 105);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 327);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 212);
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyAttackData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 60);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 36);
        }


        [Test]
        public void AssetDataTest_Upgrade_EnemyRewardAssetData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 10);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 11);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 12);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 13);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 14);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 15);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 38);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData_Test(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 21);
        }
    }
}
#endif