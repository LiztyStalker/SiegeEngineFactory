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

    public class UILevelWaveTest
    {


        private Camera _camera;
        private Light2D _light;

        private UILevelWave_Test _uiLevelWave;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiLevelWave = UILevelWave_Test.Create();
            _uiLevelWave.Initialize();
            Assert.IsNotNull(_uiLevelWave.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _uiLevelWave.Dispose();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UILevelWaveTest_Initialize()
        {
            yield return null;
            _uiLevelWave.Instance.Level = 1;
            _uiLevelWave.Instance.Wave = 4;
            _uiLevelWave.Instance.MaxValue = 10;
            _uiLevelWave.Instance.MinValue = 1;
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UILevelWaveTest_RunProcess()
        {

            yield return null;
            _uiLevelWave.Instance.Level = 0;
            _uiLevelWave.Instance.Wave = 1;
            _uiLevelWave.Instance.MaxValue = 10;
            _uiLevelWave.Instance.MinValue = 1;

            var nowTime = 0f;

            while (true)
            {
                nowTime += Time.deltaTime * 5f;
                if (nowTime >= 1f)
                {
                    if (_uiLevelWave.Instance.Wave + 1 == 11)
                    {
                        _uiLevelWave.Instance.Level++;
                        _uiLevelWave.Instance.Wave = 1;
                    }
                    else
                    {
                        _uiLevelWave.Instance.Wave++;
                    }

                    if (_uiLevelWave.Instance.Level == 3)
                        break;
                    nowTime -= 1f;
                }
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}