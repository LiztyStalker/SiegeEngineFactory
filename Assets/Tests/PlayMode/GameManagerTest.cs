#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Manager;
    using Data;
    using Entity;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;


    public class GameManagerTest
    {
        private Camera _camera;
        private Light2D _light;

        private GameManager _gameManager;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();

            _gameManager = GameManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);

            Object.DestroyImmediate(_gameManager.gameObject);
        }




        [UnityTest]
        public IEnumerator GameManagerTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(_gameManager != null, "GameManager 생성에 실패했습니다");
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator GameManagerTest_Workshop_RunProcess_20seconds()
        {
            float nowTime = 0;
            yield return new WaitForSeconds(1f);
            while (true)
            {
                nowTime += Time.deltaTime;
                yield return null;
                if (nowTime > 20f)
                    break;
            }
        }
    }
}
#endif