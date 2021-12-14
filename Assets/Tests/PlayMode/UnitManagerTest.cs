#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using SEF.Unit;

    public class UnitManagerTest
    {
        private UnitManager _unitManager;
        private Camera _camera;


        private void CreateCamera()
        {
            var obj = new GameObject();
            obj.name = "Camera";
            obj.transform.position = Vector3.back * 10f;
            _camera = obj.AddComponent<Camera>();

        }

        private void DestoryCamera()
        {
            Object.DestroyImmediate(_camera.gameObject);
        }

        [SetUp]
        public void SetUp()
        {
            CreateCamera();
            _unitManager = UnitManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _unitManager.CleanUp();
            DestoryCamera();
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Initialize ()
        {
            _unitManager.Initialize();
            yield return null;

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy 가 적용되지 않았습니다");
            Assert.IsTrue(_unitManager.WaitEnemyCount == 2, "UnitActor 가 생성되지 않았습니다");
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_CreateAndChange()
        {

            yield return UnitManagerTest_Initialize();
            _unitManager.NowEnemy.DecreaseHealth();
            yield return null;

            Assert.IsNotNull(_unitManager.NowEnemy, "NowEnemy 가 적용되지 않았습니다");
            Assert.IsTrue(_unitManager.WaitEnemyCount == 2, "UnitActor 가 생성되지 않았습니다");
            yield return null;

        }


        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_1()
        {
            _unitManager.InitializeUnitManager_Test();
            var unitActor = _unitManager.CreateUnitActor();
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
                _unitManager.CreateUnitActor();
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
                var unitActor = _unitManager.CreateUnitActor();
                if (i % 2 == 0)
                    arr[i / 2] = unitActor;
            }
            yield return null;
            Assert.IsTrue(_unitManager.UnitCount == 10, $"UnitActor가 모두 생성되지 않았습니다{_unitManager.UnitCount}");
            yield return new WaitForSeconds(1f);

            for(int i = 0; i < arr.Length; i++)
            {
                arr[i].DecreaseHealth();
            }
            yield return null;
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
        public IEnumerator UnitManagerTest_Create_EnemyActor_10()
        {
            _unitManager.InitializeUnitManager_Test();
            for (int i = 0; i < 10; i++)
            {
                _unitManager.CreateEnemyActor();
            }
            yield return null;
            Assert.IsTrue(_unitManager.WaitEnemyCount == 9, "enemyActor 가 모두 생성되지 않았습니다");
            yield return new WaitForSeconds(1f);
        }

        


    }
}
#endif