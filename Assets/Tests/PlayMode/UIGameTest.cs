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
    using Entity;
    using Data;

    public class UIGameTest
    {

        private Camera _camera;
        private Light2D _light;
        private UIGame _uiGame;


        private void CreateUIGame()
        {
            _uiGame = UIGame.Create();
            _uiGame.Initialize();
        }

        private void DestroyUIGame()
        {
            _uiGame.CleanUp();
            Object.DestroyImmediate(_uiGame.gameObject);
        }

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            CreateUIGame();
        }

        [TearDown]
        public void TearDown()
        {
            DestroyUIGame();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIGameTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIGameTest_RefreshWorkshop()
        {

            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            _uiGame.RefreshUnit(0, unitEntity, 0f);

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIGameTest_RefreshWorkshop_line5()
        {

            UnitEntity unitEntity = new UnitEntity();
            unitEntity.Initialize();
            unitEntity.UpTech(UnitData.Create_Test());

            _uiGame.RefreshUnit(0, unitEntity, 0f);
            _uiGame.RefreshUnit(1, unitEntity, 0f);
            _uiGame.RefreshUnit(2, unitEntity, 0f);
            _uiGame.RefreshUnit(3, unitEntity, 0f);
            _uiGame.RefreshUnit(4, unitEntity, 0f);

            yield return new WaitForSeconds(1f);
        }
    }
}
#endif