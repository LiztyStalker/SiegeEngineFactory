namespace SEF.UI.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.UIElements;
    using UtilityManager.Test;
#if INCLUDE_UI_TOOLKIT
    using Toolkit;
#endif
    using Entity;
    using Data;
    using SEF.Unit;

    public class UIPlayTest
    {
        private Camera _camera;
        private Light2D _light;

        private UIPlay_Test _uitest;

        private EnemyActor _enemyActor;
        private LevelWaveData _levelWaveData;

        private UnitActor _unitActor;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uitest = UIPlay_Test.Create();
            Assert.IsNotNull(_uitest);
            _uitest.Initialize();




            EnemyData enemyData = EnemyData.Create_Test();

            EnemyEntity enemyEntity = new EnemyEntity();
            enemyEntity.Initialize();

            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            _levelWaveData.IncreaseNumber();

            enemyEntity.SetData(enemyData, _levelWaveData);

            _enemyActor = EnemyActor.Create_Test();
            _enemyActor.SetData(enemyEntity);
            _enemyActor.Activate();


            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            _unitActor = UnitActor.Create_Test();
            _unitActor.SetData(unitEntity);
            _unitActor.Activate();
        }

        [TearDown]
        public void TearDown()
        {
            _enemyActor.CleanUp();
            _levelWaveData.CleanUp();

            _uitest.Dispose();

            _uitest = null;

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIPlayTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }

        //다음 적 등장
        [UnityTest]
        public IEnumerator UIPlayTest_NextEnemyAppear()
        {
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
        }

        //적 3단계 등장
        [UnityTest]
        public IEnumerator UIPlayTest_NextEnemyAppear_3Cycles()
        {
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
            _levelWaveData.IncreaseNumber();
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
            _levelWaveData.IncreaseNumber();
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
        }

        //다음적 보스 등장
        [UnityTest]
        public IEnumerator UIPlayTest_NextBossAppear()
        {
            _levelWaveData.SetValue(10);
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
        }

        //다음적 테마보스 등장
        [UnityTest]
        public IEnumerator UIPlayTest_NextThemeBossAppear()
        {
            _levelWaveData.SetValue(100);
            _uitest.Instance.RefreshNextEnemyUnit(_enemyActor, _levelWaveData);
            yield return new WaitForSeconds(1f);
        }

        //아군 피격
        [UnityTest]
        public IEnumerator UIPlayTest_HitUnit()
        {
            _uitest.Instance.ShowHit(_unitActor, DamageData.Create_Test());
            yield return new WaitForSeconds(1f);
        }

        //적군 피격
        [UnityTest]
        public IEnumerator UIPlayTest_HitEnemy()
        {
            _uitest.Instance.ShowHit(_enemyActor, DamageData.Create_Test());
            yield return new WaitForSeconds(1f);
        }

        //아군과 적군 피격 랜덤

        [UnityTest]
        public IEnumerator UIPlayTest_HitUnitAndEnemy_20Cycles()
        {
            yield return new WaitForSeconds(1f);
        }





    }
}