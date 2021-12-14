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



        private UnitManager _unitManager;
        private Camera _camera;
        private Light2D _light;

        private void CreateCamera()
        {
            var obj = new GameObject();
            obj.name = "Camera";
            obj.transform.position = Vector3.back * 10f;
            _camera = obj.AddComponent<Camera>();
            _camera.clearFlags = CameraClearFlags.SolidColor;
            _camera.backgroundColor = Color.black;
        }

        private void DestoryCamera()
        {
            Object.DestroyImmediate(_camera.gameObject);
        }

        private void CreateLight()
        {
            var obj = new GameObject();
            obj.name = "Light@Global";
            obj.transform.position = Vector3.zero;
            _light = obj.AddComponent<Light2D>();
            _light.lightType = Light2D.LightType.Global;
        }

        private void DestoryLight()
        {
            Object.DestroyImmediate(_light.gameObject);
        }

        [SetUp]
        public void SetUp()
        {
            CreateCamera();
            CreateLight();
            _unitManager = UnitManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _unitManager.CleanUp();
            DestoryLight();
            DestoryCamera();

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

            yield return UnitManagerTest_Initialize();
            _unitManager.NowEnemy.DecreaseHealth();
            yield return null;

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy �� ������� �ʾҽ��ϴ�");
            Assert.IsTrue(_unitManager.WaitEnemyCount == 2, "UnitActor �� �������� �ʾҽ��ϴ�");
            yield return null;

        }


        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var unitActor = _unitManager.CreateUnitActor();
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
                _unitManager.CreateUnitActor();
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
                var unitActor = _unitManager.CreateUnitActor();
                if (i % 2 == 0)
                    arr[i / 2] = unitActor;
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, $"UnitActor�� ��� �������� �ʾҽ��ϴ�{_unitManager.UnitCount}");
            yield return new WaitForSeconds(1f);

            for(int i = 0; i < arr.Length; i++)
            {
                arr[i].DecreaseHealth();
            }
            yield return null;
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
        public IEnumerator UnitManagerTest_Create_EnemyActor_10()
        {
            _unitManager.InitializeUnitManager_Test();
            for (int i = 0; i < 10; i++)
            {
                _unitManager.CreateEnemyActor();
            }
            yield return null;
            Assert.IsTrue(_unitManager.WaitEnemyCount == 9, "enemyActor �� ��� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_AppearPosition()
        {
            _unitManager.InitializeUnitManager_PositionTest();
            _unitManager.CreateUnitActor();
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_AppearToActionPosition()
        {
            _unitManager.InitializeUnitManager_PositionTest();
            var unitActor = _unitManager.CreateUnitActor();
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 1, "unitActor �� �������� �ʾҽ��ϴ�");

            while (true)
            {
                unitActor.RunProcess(Time.deltaTime);
                if (unitActor.IsActionState())
                {
                    break;
                }
                yield return null;
            }

            Assert.IsTrue(unitActor.IsActionState(), "unitActor�� Action ���°� �ƴմϴ�");
            yield return null;
        }


        [UnityTest]
        public IEnumerator UnitManagerTest_UnitActor_Attack()
        {
            _unitManager.InitializeUnitManager_PositionTest();
            var dummy = new DummyTarget();
            var unitActor = _unitManager.CreateUnitActor();
            unitActor.SetTypeUnitState(TYPE_UNIT_STATE.Action);
            unitActor.SetPosition(new Vector2(-2f, 3f));
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

    }
}
#endif