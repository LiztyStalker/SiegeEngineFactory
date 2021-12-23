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

    public class UIProgressbarTest
    {


        private Camera _camera;
        private Light2D _light;

        private UIProgressbar_Test _uiProgressbar;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiProgressbar = UIProgressbar_Test.Create();
            _uiProgressbar.Initialize();

        }

        [TearDown]
        public void TearDown()
        {
            _uiProgressbar.Dispose();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIProgressbarTest_Initialize()
        {
            _uiProgressbar.Instance.FillAmount = 0.3f;
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIProgressbarTest_RunProcess()
        {
            var nowTime = 0f;

            while (true)
            {
                nowTime += Time.deltaTime * 5f;
                _uiProgressbar.Instance.FillAmount = nowTime;
                if(nowTime >= 1f)
                {
                    break;
                }
                Debug.Log(_uiProgressbar.Instance.FillAmount);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}