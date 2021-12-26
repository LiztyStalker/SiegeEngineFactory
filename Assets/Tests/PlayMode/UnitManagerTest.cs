#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using SEF.Unit;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.Rendering.Universal;
    using UtilityManager.Test;
    using Entity;
    using Data;

    public class UnitManagerTest
    {

        public class DummyTarget : ITarget
        {
            public int hitCount;
            public void DecreaseHealth()
            {
                hitCount++;
                Debug.Log("Hit");
            }
        }

        private UnitEntity _unitEntity;
        private UnitManager _unitManager;
        private Camera _camera;
        private Light2D _light;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _unitManager = UnitManager.Create();
            _unitEntity.Initialize();
            _unitEntity.UpTech(UnitData.Create_Test());
        }

        [TearDown]
        public void TearDown()
        {
            _unitEntity.CleanUp();
            _unitManager.CleanUp();
            PlayTestUtility.DestroyLight(_light);
            PlayTestUtility.DestroyCamera(_camera);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Initialize ()
        {
            _unitManager.Initialize();
            yield return null;

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy �� ������� �ʾҽ��ϴ�");
            Assert.IsTrue(_unitManager.WaitEnemyCount == 2, "UnitActor �� �������� �ʾҽ��ϴ�");
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_CreateAndChange()
        {
            _unitManager.Initialize();
            bool isRun = true;
            _unitManager.NowEnemy.Destroy_Test(delegate
            {
                isRun = false;
            });

            while (isRun)
            {
                yield return null;
            }

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy �� ������� �ʾҽ��ϴ�");
            Assert.IsTrue(_unitManager.WaitEnemyCount == 2, "UnitActor �� �������� �ʾҽ��ϴ�");
            yield return null;

        }


        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
            
            yield return null;
            Assert.IsTrue(unitActor != null, "UnitActor�� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_10()
        {
            _unitManager.InitializeUnitManager_Test();
            for (int i = 0; i < 10; i++)
            {
                _unitManager.ProductUnitActor_Test(_unitEntity);
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, "UnitActor�� ��� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_10_Retrieve_5()
        {
            _unitManager.InitializeUnitManager_Test();
            UnitActor[] arr = new UnitActor[5];
            for (int i = 0; i < 10; i++)
            {
                var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
                if (i % 2 == 0)
                    arr[i / 2] = unitActor;
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, $"UnitActor�� ��� �������� �ʾҽ��ϴ�{_unitManager.UnitCount}");
            yield return new WaitForSeconds(1f);


            int count = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i].Destroy_Test(delegate
                {
                    count++;
                });
            }
            yield return null;

            while (true)
            {
                if (count == arr.Length)
                    break;
                yield return null;
            }


            Assert.IsTrue(_unitManager.UnitCount == 5, $"UnitActor�� ��� �ݳ����� �ʾҽ��ϴ� {_unitManager.UnitCount}");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_EnemyActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            Assert.IsTrue(enemyActor != null, "enemyActor �� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Appear_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);

            while (true)
            {
                unitActor.RunProcess(Time.deltaTime);
                if (unitActor.IsArriveAction())
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Appear_Spine()
        {
            _unitManager.Initialize();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);

            while (true)
            {
                unitActor.RunProcess(Time.deltaTime);
                if (unitActor.IsArriveAction())
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Action_Dummy()
        {
            var dummy = new DummyTarget();

            _unitManager.InitializeUnitManager_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);
            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor.SetTarget(dummy);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");

            while (true)
            {
                unitActor.RunProcess(Time.deltaTime);
                if(dummy.hitCount > 5)
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Destroy_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);

            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");

            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);

            bool isRun = true;
            unitActor.Destroy_Test(delegate
            {
                isRun = false;
            });
            yield return null;

            while (isRun)
            {
                yield return null;
            }

            Assert.IsTrue(_unitManager.UnitCount == 0, "unitActor �� ���ŵ��� �ʾҽ��ϴ�");
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Idle_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            enemyActor.Activate();
            yield return new WaitForSeconds(1f);
            _unitManager.CleanUp();
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Ready_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            enemyActor.Activate();
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Ready);
            yield return new WaitForSeconds(1f);
            while (true)
            {
                enemyActor.RunProcess(Time.deltaTime);
                if (enemyActor.IsArriveReady())
                    break;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Appear_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            enemyActor.Activate();
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_READY_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Appear);
            yield return new WaitForSeconds(1f);
            while (true)
            {
                enemyActor.RunProcess(Time.deltaTime);
                if (enemyActor.IsArriveAction())
                    break;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Action_Dummy()
        {
            var dummy = new DummyTarget();

            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            enemyActor.Activate();
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);
            enemyActor.SetOnFindTargetListener(() => dummy);
            while (true)
            {
                enemyActor.RunProcess(Time.deltaTime);
                if (dummy.hitCount == 5)
                    break;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Destroy_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            _unitManager.ChangeNowEnemy();

            bool isRun = true;
            enemyActor.Destroy_Test(delegate
            {
                isRun = false;
            });
            while (isRun)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitAndEnemy_Position_Dummy()
        {
            _unitManager.InitializeUnitManager_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity);
            yield return null;
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            yield return new WaitForSeconds(1f);
        }
    }
}
#endif