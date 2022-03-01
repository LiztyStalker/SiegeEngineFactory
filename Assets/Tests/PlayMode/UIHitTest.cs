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

    public class UIHitTest
    {
        private Camera _camera;
        private Light2D _light;

        private UIHitContainer _uiHitContainer;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiHitContainer = UIHitContainer.Create();
            Assert.IsNotNull(_uiHitContainer);
            _uiHitContainer.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _uiHitContainer.CleanUp();
            _uiHitContainer = null;

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIHitTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UIHitTest_ShowHitContainer_10()
        {
            int count = 0;

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 10; i++)
            {
                Vector2 pos = new Vector2(UnityEngine.Random.Range(-2f, 2f), 2f);
                _uiHitContainer.ShowHit_Test("1.234A", pos, block =>
                {
                    Debug.Log("End");
                    count++;
                });
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
            }

            while (true)
            {
                if (count >= 10)
                    break;
                yield return null;
            }
        }

    }
}