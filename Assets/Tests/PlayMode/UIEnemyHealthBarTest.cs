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

    public class UIEnemyHealthBarTest
    {


        private Camera _camera;
        private Light2D _light;

        private UIEnemyHealthBar_Test _uiEnemyHealthBar;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiEnemyHealthBar = UIEnemyHealthBar_Test.Create();
            _uiEnemyHealthBar.Initialize();
            Assert.IsNotNull(_uiEnemyHealthBar.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _uiEnemyHealthBar.Dispose();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIEnemyHealthBarTest_Initialize()
        {
            yield return null;
            _uiEnemyHealthBar.Instance.FillAmount = 0.4f;
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIEnemyHealthBarTest_RunProcess()
        {
            var nowTime = 1f;

            while (true)
            {
                nowTime -= Time.deltaTime * 5f;
                _uiEnemyHealthBar.Instance.FillAmount = nowTime;
                if (nowTime < 0f)
                {
                    break;
                }
                Debug.Log(_uiEnemyHealthBar.Instance.FillAmount);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}