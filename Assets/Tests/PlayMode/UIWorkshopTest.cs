#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.UI.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;
#if INCLUDE_UI_TOOLKIT
    using Toolkit;
#endif
    using Data;
    using Entity;

    public class UIWorkshopTest
    {

        private Camera _camera;
        private Light2D _light;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
        }

        [TearDown]
        public void TearDown()
        {
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);

        }

        [UnityTest]
        public IEnumerator UIWorkshopTest_Initialize()
        {
            var workshop = UIWorkshop_Test.Create();
            workshop.Initialize();
            yield return new WaitForSeconds(1f);
            workshop.Dispose();
        }

        [UnityTest]
        public IEnumerator UIWorkshopTest_Create_UIWorkshopLine()
        {
            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());
            var workshop = UIWorkshop_Test.Create();
            workshop.Initialize();
            workshop.Instance.RefreshEntity(0, unitEntity, 0f);
            yield return new WaitForSeconds(1f);
            workshop.Dispose();
        }


        [UnityTest]
        public IEnumerator UIWorkshopTest_Create5_UIWorkshopLine()
        {
            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());
            var workshop = UIWorkshop_Test.Create();
            workshop.Initialize();
            workshop.Instance.RefreshEntity(0, unitEntity, 0f);
            yield return new WaitForSeconds(0.5f);
            workshop.Instance.RefreshEntity(1, unitEntity, 0f);
            yield return new WaitForSeconds(0.5f);
            workshop.Instance.RefreshEntity(2, unitEntity, 0f);
            yield return new WaitForSeconds(0.5f);
            workshop.Instance.RefreshEntity(3, unitEntity, 0f);
            yield return new WaitForSeconds(0.5f);
            workshop.Instance.RefreshEntity(4, unitEntity, 0f);
            yield return new WaitForSeconds(0.5f);
            workshop.Dispose();
        }

    }
}
#endif