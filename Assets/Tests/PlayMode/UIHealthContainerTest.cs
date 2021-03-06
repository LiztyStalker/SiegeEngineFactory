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

    public class UIHealthContainerTest
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
            _uiContainer = UIHealthContainer.Create();
            Assert.IsNotNull(_uiContainer);

            _uiblock = UIHealthBar.Create();
            Assert.IsNotNull(_uiblock);
            _uiblock.Initialize();

            _uiContainer.Initialize();
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
        public IEnumerator UIHealthTest_ShowUnitBlock()
        {
            bool isRun = true;

            _uiblock.SetOnRetrieveBlockListener(block =>
            {
                Debug.Log("End");
                isRun = false;
            });

            _uiblock.ShowHealth(0.4f, Vector2.zero);
            _uiblock.Activate();

            while (isRun)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator UIHealthTest_ShowEnemyBlock()
        {
            _uiContainer.ShowEnemyHealthData("Test", "1.234A", 0.4f);
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIHealthTest_ShowHealthContainerEnemyBlock()
        {
            _uiContainer.ShowEnemyHealthData("Test", "1.234A", 0.4f);
            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator UIHealthTest_ShowHealthContainerUnitBlock()
        {
            _uiContainer.ShowUnitHealthData(0.4f, Vector2.zero);
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIHealthTest_ShowHealthContainerRandom_20()
        {
            int count = 0;

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 10; i++)
            {
                Vector2 pos = new Vector2(UnityEngine.Random.Range(-2f, 2f), 2f);
                _uiContainer.ShowUnitHealthData_Test(UnityEngine.Random.Range(0.1f, 1f), pos, block =>
                {
                    Debug.Log("End");
                    count++;
                });
                var value = UnityEngine.Random.Range(0.1f, 1f);
                _uiContainer.ShowEnemyHealthData("Test", value.ToString(), value);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
            }

            while (true)
            {
                if (count >= 10)
                    break;
                yield return null;
            }


            _uiContainer.ShowUnitHealthData(0.4f, Vector2.zero);
            yield return new WaitForSeconds(1f);
        }

    }
}