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

    public class UIHitTest
    {
        private Camera _camera;
        private Light2D _light;

        private UIHitContainer _uiHitContainer;
        private UIHitBlock _uiHitBlock;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiHitContainer = UIHitContainer.Create(null);
            Assert.IsNotNull(_uiHitContainer);
            _uiHitContainer.Initialize();

            _uiHitBlock = UIHitBlock.Create();
            Assert.IsNotNull(_uiHitBlock);
            _uiHitBlock.Initialize();

        }

        [TearDown]
        public void TearDown()
        {
            _uiHitContainer.CleanUp();
            _uiHitBlock.CleanUp();

            _uiHitContainer = null;
            _uiHitBlock = null;

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIHitTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }



        [UnityTest]
        public IEnumerator UIHitTest_ShowHitBlock()
        {
            bool isRun = true;



            yield return new WaitForSeconds(1f);

            _uiHitBlock.SetOnRetrieveBlockListener(block =>
            {
                Debug.Log("End");
                isRun = false;
            });


            _uiHitBlock.ShowHit("1.234A", Vector2.zero);
            _uiHitBlock.Activate();

            while (isRun)
            {
                yield return null;
            }
        }

    }
}