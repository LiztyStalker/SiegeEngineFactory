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
    using Data;

    public class UILevelWaveTest
    {


        private Camera _camera;
        private Light2D _light;

        private UILevelWave_Test _uiLevelWave;
        private LevelWaveData _levelWaveData;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uiLevelWave = UILevelWave_Test.Create();
            _uiLevelWave.Initialize();
            Assert.IsNotNull(_uiLevelWave.Instance);

            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
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
            _uiLevelWave.Instance.MaxValue = 9;
            _uiLevelWave.Instance.MinValue = 0;
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UILevelWaveTest_RunProcess()
        {

            yield return null;
            _uiLevelWave.Instance.Level = 0;
            _uiLevelWave.Instance.Wave = 0;
            _uiLevelWave.Instance.MaxValue = 9;
            _uiLevelWave.Instance.MinValue = 0;

            var nowTime = 0f;
            int level = 0;
            int wave = 0;

            while (true)
            {
                nowTime += Time.deltaTime * 5f;
                if (nowTime >= 1f)
                {
                    if (wave + 1 == 10)
                    {
                        level++;
                        wave = 0;
                    }
                    else
                    {
                        wave++;
                    }

                    _uiLevelWave.Instance.ShowLevelWave(level, wave);

                    if (level == 3)
                        break;
                    nowTime -= 1f;
                }
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UILevelWaveTest_RunProcessWithLevelWaveData()
        {

            yield return null;
            _uiLevelWave.Instance.Level = 0;
            _uiLevelWave.Instance.Wave = 0;
            _uiLevelWave.Instance.MaxValue = 9;
            _uiLevelWave.Instance.MinValue = 0;

            var nowTime = 0f;

            while (true)
            {
                nowTime += Time.deltaTime * 5f;
                if (nowTime >= 1f)
                {
                    _levelWaveData.IncreaseNumber();

                    _uiLevelWave.Instance.ShowLevelWave(_levelWaveData.GetLevel(), _levelWaveData.GetWave());

                    if (_levelWaveData.GetLevel() == 3)
                        break;
                    nowTime -= 1f;
                }
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}