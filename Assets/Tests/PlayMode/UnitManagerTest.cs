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
            public Vector2 NowPosition => Vector2.zero;

            public int hitCount;
            public void DecreaseHealth(AttackData attackData)
            {
                hitCount++;
                Debug.Log("Hit");
            }
        }

        private UnitEntity _unitEntity_Dummy;
        private EnemyEntity _enemyEntity_Dummy;
        private UnitManager _unitManager;
        private Camera _camera;
        private Light2D _light;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _unitManager = UnitManager.Create();
            _unitEntity_Dummy.Initialize();
            _unitEntity_Dummy.UpTech(UnitData.Create_Test());
            _enemyEntity_Dummy.Initialize();
            _enemyEntity_Dummy.SetData(EnemyData.Create_Test(), NumberDataUtility.Create<LevelWaveData>());
        }

        [TearDown]
        public void TearDown()
        {
            _unitEntity_Dummy.CleanUp();
            _enemyEntity_Dummy.CleanUp();
            _unitManager.CleanUp();
            PlayTestUtility.DestroyLight(_light);
            PlayTestUtility.DestroyCamera(_camera);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Initialize ()
        {
            _unitManager.Initialize();
            yield return null;

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy 가 적용되지 않았습니다");
            Assert.IsTrue(_unitManager.EnemyCount == 3, "UnitActor 가 생성되지 않았습니다");
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

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy 가 적용되지 않았습니다");
            Assert.IsTrue(_unitManager.EnemyCount == 3, "UnitActor 가 생성되지 않았습니다");
            yield return null;

        }


        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            
            yield return null;
            Assert.IsTrue(unitActor != null, "UnitActor가 생성되지 않았습니다");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_10()
        {
            _unitManager.InitializeUnitManager_Test();
            for (int i = 0; i < 10; i++)
            {
                _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, "UnitActor가 모두 생성되지 않았습니다");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_10_Retrieve_5()
        {
            _unitManager.InitializeUnitManager_Test();
            UnitActor[] arr = new UnitActor[5];
            for (int i = 0; i < 10; i++)
            {
                var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
                if (i % 2 == 0)
                    arr[i / 2] = unitActor;
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, $"UnitActor가 모두 생성되지 않았습니다{_unitManager.UnitCount}");
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


            Assert.IsTrue(_unitManager.UnitCount == 5, $"UnitActor가 모두 반납되지 않았습니다 {_unitManager.UnitCount}");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_EnemyActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var enemyActor = _unitManager.CreateEnemyActor();
            yield return null;
            Assert.IsTrue(enemyActor != null, "enemyActor 가 생성되지 않았습니다");
            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Appear_Dummy()
        {
            _unitManager.InitializeAll_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");
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
            _unitManager.Initialize_Empty();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");
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

            _unitManager.InitializeAll_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);
            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor.SetTarget(dummy);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");

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
        public IEnumerator UnitManagerTest_UnitActor_Action_Spine()
        {
            var dummy = new DummyTarget();

            _unitManager.Initialize_Empty();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);
            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor.SetTarget(dummy);
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");

            while (true)
            {
                unitActor.RunProcess(Time.deltaTime);
                if (dummy.hitCount > 5)
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
            _unitManager.InitializeAll_DummyTest();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);

            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");

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

            Assert.IsTrue(_unitManager.UnitCount == 0, "unitActor 가 제거되지 않았습니다");
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Destroy_Spine()
        {
            _unitManager.Initialize_Empty();
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);

            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor 가 생성되지 않았습니다");

            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);

            bool isRun = true;
            unitActor.AddOnDestoryedListener(delegate
            {
                isRun = false;
            });

            unitActor.Destory_Test_Spine();
            yield return null;

            while (isRun)
            {
                yield return null;
            }

            Assert.IsTrue(_unitManager.UnitCount == 0, "unitActor 가 제거되지 않았습니다");
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Idle_Dummy()
        {
            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
            yield return null;
            enemyActor.Activate();
            yield return new WaitForSeconds(1f);
            _unitManager.CleanUp();
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Idle_Spine()
        {
            _unitManager.InitializeAll_DummyTest();

            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            var enemyEntity = new EnemyEntity();
            enemyEntity.SetData(enemyData, levelWaveData);
            var enemyActor = _unitManager.CreateEnemyActor_Test(enemyEntity);
            yield return null;
            enemyActor.Activate();
            yield return new WaitForSeconds(1f);
            _unitManager.CleanUp();
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyActor_State_Ready_Dummy()
        {
            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
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
            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
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

            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
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
            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
            yield return null;
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            _unitManager.ChangeNowEnemy(enemyActor);

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
            _unitManager.InitializeAll_DummyTest();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
            var unitActor = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            yield return null;
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            unitActor.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitAndEnemy_BattleTest()
        {
            _unitManager.Initialize_Empty();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
            var unitActor1 = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            var unitActor2 = _unitManager.ProductUnitActor_Test(_unitEntity_Dummy);
            yield return null;
            enemyActor.SetPosition_Test(EnemyActor.ENEMY_ACTION_POSITION_TEST);
            enemyActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            unitActor1.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor1.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            unitActor2.SetPosition_Test(UnitActor.UNIT_ACTION_POSITION_TEST);
            unitActor2.SetTypeUnitState(TYPE_UNIT_STATE.Action);

            enemyActor.SetTarget(unitActor2);
            unitActor1.SetTarget(enemyActor);
            unitActor2.SetTarget(enemyActor);

            bool isRun = true;
            enemyActor.AddOnDestoryedListener(actor =>
            {
                isRun = false;
            });

            while (isRun)
            {
                _unitManager.RunProcess(Time.deltaTime);
                yield return null;          
            }

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyData_BigNumberData()
        {
            _unitManager.Initialize_Empty();
            var enemyActor = _unitManager.CreateEnemyActor_Test(_enemyEntity_Dummy);
            yield return null;

            Debug.Log(enemyActor.GetRewardAssetData().AssetValue);

            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyData_Boss_10To100()
        {
            _unitManager.Initialize_Empty();
            _enemyEntity_Dummy.Initialize();
            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();


            for (int i = 1; i < 11; i++)
            {
                levelWaveData.SetValue(10 * i);
                _enemyEntity_Dummy.SetData(enemyData, levelWaveData);

                Debug.Log(levelWaveData.IsBoss());
                if (i == 10)
                {
                    Assert.IsFalse(levelWaveData.IsBoss());
                }
                else
                {
                    Assert.IsTrue(levelWaveData.IsBoss());
                }
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_EnemyData_ThemeBoss_100To1000()
        {
            _unitManager.Initialize_Empty();
            _enemyEntity_Dummy.Initialize();
            var enemyData = EnemyData.Create_Test();
            var levelWaveData = NumberDataUtility.Create<LevelWaveData>();



            for(int i = 1; i < 11; i++)
            {
                levelWaveData.SetValue(100 * i);
                _enemyEntity_Dummy.SetData(enemyData, levelWaveData);

                Debug.Log(levelWaveData.IsThemeBoss());
                Assert.IsTrue(levelWaveData.IsThemeBoss());
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }


    }
}
#endif