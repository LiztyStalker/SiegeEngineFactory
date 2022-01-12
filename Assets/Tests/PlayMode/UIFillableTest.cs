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

    public class UIFillableTest
    {


        private Camera _camera;
        private Light2D _light;

        private UIFillable_Test _uiTest;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiTest = UIFillable_Test.Create();
            _uiTest.Initialize();

        }

        [TearDown]
        public void TearDown()
        {
            _uiTest.Dispose();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIFillableTest_Initialize()
        {
            _uiTest.Instance.FillAmount = 0.3f;
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIFillableTest_RunProcess()
        {
            var nowTime = 0f;

            while (true)
            {
                nowTime += Time.deltaTime * 1f;
                _uiTest.Instance.FillAmount = nowTime;
                if(nowTime >= 1f)
                {
                    break;
                }
                Debug.Log(_uiTest.Instance.FillAmount);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}