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
            _unitManager.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _unitManager.CleanUp();
            DestoryCamera();
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_1()
        {
            var unitActor = _unitManager.CreateUnitActor();
            yield return null;
            Assert.IsTrue(unitActor != null, "UnitActor�� �������� �ʾҽ��ϴ�");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UnitManagerTest_Create_UnitActor_10()
        {
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
    }
}
#endif