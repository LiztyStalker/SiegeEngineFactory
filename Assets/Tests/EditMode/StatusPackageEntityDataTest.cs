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
            _statusPackage.Dispose();
            _statusProvider = null;
        }

        [Test]
        public void StatusPackageEntityDataTest_SetDamageValueStatusData_Value_Rate_Absolute()
        {
            var DamageValueStatusData_Absolute = new DamageValueStatusData(1, IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(_statusProvider, DamageValueStatusData_Absolute);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "16", "서로 다른 값입니다");


            var DamageValueStatusData_Value = new DamageValueStatusData(1, IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(_statusProvider, DamageValueStatusData_Value);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "16", "서로 다른 값입니다");



            var DamageValueStatusData_Rate = new DamageValueStatusData(100, IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(_statusProvider, DamageValueStatusData_Rate);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "30", "서로 다른 값입니다");




            var provider = new Provider_Test();
            _statusPackage.SetStatusData(provider, DamageValueStatusData_Value);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.DamageData.GetValue());
            Assert.IsTrue(_unitEntity_Dummy.DamageData.GetValue() == "31", "서로 다른 값입니다");

        }

        //소숫점도 계산식 필요
        [Test]
        public void StatusPackageEntityDataTest_SetDamageDelayStatusData_Value_Rate_Absolute()
        {
            var DamageDelayStatusData_Absolute = new DamageDelayStatusData(0.01f, IStatusData.TYPE_STATUS_DATA.Absolute);
            _statusPackage.SetStatusData(_statusProvider, DamageDelayStatusData_Absolute);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.AttackDelay);
            Assert.IsTrue(_unitEntity_Dummy.AttackDelay == 0.99f, "서로 다른 값입니다");


            var DamageDelayStatusData_Value = new DamageDelayStatusData(1f, IStatusData.TYPE_STATUS_DATA.Value);
            _statusPackage.SetStatusData(_statusProvider, DamageDelayStatusData_Value);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.AttackDelay);
            Assert.IsTrue(_unitEntity_Dummy.AttackDelay == 0.99f, "서로 다른 값입니다");



            var DamageDelayStatusData_Rate = new DamageDelayStatusData(0.01f, IStatusData.TYPE_STATUS_DATA.Rate);
            _statusPackage.SetStatusData(_statusProvider, DamageDelayStatusData_Rate);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.AttackDelay);
            Assert.IsTrue(_unitEntity_Dummy.AttackDelay == 0.99f, "서로 다른 값입니다");


            var provider = new Provider_Test();
            _statusPackage.SetStatusData(provider, DamageDelayStatusData_Value);

            _unitEntity_Dummy.SetOnStatusPackageListener(GetStatusPackage);

            Debug.Log(_unitEntity_Dummy.AttackDelay);
            Assert.IsTrue(_unitEntity_Dummy.AttackDelay == 0.98f, "서로 다른 값입니다");

        }



        private StatusPackage GetStatusPackage() => _statusPackage;

        
    }
}
#endif