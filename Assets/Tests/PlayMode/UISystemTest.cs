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
    using Toolkit;
    using Data;
    using Entity;

    public class UISystemTest
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
        public IEnumerator UISystemTest_Initialize()
        {
            var uisystem = UISystem_Test.Create();
            uisystem.Initialize();
            yield return new WaitForSeconds(1f);
            uisystem.Dispose();
        }

        [UnityTest]
        public IEnumerator UISystemTest_RefreshWorkshop()
        {
            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            var uisystem = UISystem_Test.Create();
            uisystem.Initialize();
            uisystem.Instance.RefreshUnit(0, unitEntity, 0f);
            yield return new WaitForSeconds(1f);

            uisystem.Dispose();
        }

    }
}
#endif