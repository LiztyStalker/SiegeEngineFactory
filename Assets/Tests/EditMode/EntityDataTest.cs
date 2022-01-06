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
        public void AssetDataTest_UpgradeGoldAssetData()
        {
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 100);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 110);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 121);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 133);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 146);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.UpgradeAssetData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.UpgradeAssetData.AssetValue == 161);
        }

        [Test]
        public void AssetDataTest_UpgradeHealthAssetData()
        {
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1000);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1100);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1210);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1331);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1464);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_unitEntity_Dummy.HealthData.Value == 1610);
        }

        [Test]
        public void AssetDataTest_UpgradeAttackAssetData()
        {
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 100);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 110);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 121);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 133);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 146);
            _unitEntity_Dummy.Upgrade();
            Debug.Log(_unitEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_unitEntity_Dummy.AttackData.Value == 161);
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyHealthData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 99);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 100);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 101);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 102);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 103);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 104);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 324);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.HealthData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.HealthData.Value == 210);
        }

        [Test]
        public void AssetDataTest_Upgrade_EnemyAttackData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 30);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 60);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.AttackData.Value);
            Assert.IsTrue(_enemyEntity_Dummy.AttackData.Value == 36);
        }


        [Test]
        public void AssetDataTest_Upgrade_EnemyRewardAssetData()
        {
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 9);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 10);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 11);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 12);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 13);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 14);
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 36);
            _levelWaveData.IncreaseNumber();
            _enemyEntity_Dummy.SetData(_levelWaveData);
            Debug.Log(_levelWaveData.GetValue() + " " + _enemyEntity_Dummy.RewardAssetData.AssetValue);
            Assert.IsTrue(_enemyEntity_Dummy.RewardAssetData.AssetValue == 19);
        }
    }
}
#endif