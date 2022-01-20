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

    public class StatusPackageEntityDataTest
    {
        private UnitEntity _unitEntity_Dummy;
        private EnemyEntity _enemyEntity_Dummy;
        private LevelWaveData _levelWaveData;
        private UpgradeData _upgradeData;
        private StatusPackage _statusPackage;
        private IStatusProvider _statusProvider;

        private class Provider_Test : IStatusProvider
        {

        }

        [SetUp]
        public void SetUp()
        {
            _unitEntity_Dummy.Initialize();
            _unitEntity_Dummy.UpTech(UnitData.Create_Test());
            
            _enemyEntity_Dummy.Initialize();
            _enemyEntity_Dummy.SetData(EnemyData.Create_Test(), NumberDataUtility.Create<LevelWaveData>());
            
            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            _upgradeData = NumberDataUtility.Create<UpgradeData>();

            _statusPackage = new StatusPackage();
            _statusPackage.Initialize();

            _statusProvider = new Provider_Test();
        }


        [TearDown]
        public void TearDown()
        {
            _unitEntity_Dummy.CleanUp();
            _enemyEntity_Dummy.CleanUp();
            _levelWaveData = null;
            _upgradeData = null;
            _statusPackage.CleanUp();
            _statusProvider = null;
        }

        [Test]
        public void StatusPackageEntityDataTest_SetDamageValueStatusData_Value()
        {
            var DamageValueStatusData = new DamageValueStatusData(1, IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(_statusProvider, DamageValueStatusData);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "16", "서로 다른 값입니다");
        }

        private StatusPackage GetStatusPackage() => _statusPackage;

        
    }
}
#endif