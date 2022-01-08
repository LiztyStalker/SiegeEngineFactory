namespace SEF.UI.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;
    using SEF.UI.Toolkit;

    public class UIHealthTest
    {
        private Camera _camera;
        private Light2D _light;

        private UIHealthContainer _uiContainer;
        private UIHealthBar _uiblock;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiContainer = UIHealthContainer.Create(null);
            Assert.IsNotNull(_uiContainer);
            _uiContainer.Initialize();

            _uiblock = UIHealthBar.Create();
            Assert.IsNotNull(_uiblock);
            _uiblock.Initialize();

        }

        [TearDown]
        public void TearDown()
        {
            _uiContainer.CleanUp();
            _uiblock.CleanUp();

            _uiContainer = null;
            _uiblock = null;

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIHealthTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }



        [UnityTest]
        public IEnumerator UIHealthTest_ShowHitBlock()
        {
            _uiblock.ShowHealth(0.4f, Vector2.one * 100);
            _uiblock.Activate();
            yield return new WaitForSeconds(1f);
        }

    }
}