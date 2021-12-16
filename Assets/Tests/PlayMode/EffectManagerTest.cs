#if UNITY_EDITOR && UNITY_INCLUDE_TESTS
namespace UtilityManager.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UtilityManager;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.Rendering.Universal;


    public class EffectManagerTest
    {
        private Camera _camera;
        private Light2D _light;

        private Vector2 _position = new Vector2(-2f, 2f);

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
        }

        [TearDown]
        public void TearDown()
        {
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }

        [UnityTest]
        public IEnumerator EffectManagerTest_Activate()
        {
            bool isRun = true;
            var effectData = EffectData.CreateTest();
            EffectManager.Current.Activate(effectData, _position, actor => { Debug.Log("Inactivate"); isRun = false; });
            while (isRun)
            {
                yield return null;
            }
            yield return null;
            EffectManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator EffectManagerTest_Activate5()
        {
            int inactivateCount = 0;
            for (int i = 0; i < 5; i++)
            {
                var effectData = EffectData.CreateTest();
                EffectManager.Current.Activate(effectData, _position, actor => { Debug.Log("Inactivate"); inactivateCount++; });
                yield return null;
            }

            while (true)
            {
                if (inactivateCount == 5)
                    break;
                yield return null;
            }
            yield return null;
            EffectManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator EffectManagerTest_Activate5_After_Activate3()
        {
            int inactivateCount = 0;
            for (int i = 0; i < 5; i++)
            {
                var effectData = EffectData.CreateTest();
                EffectManager.Current.Activate(effectData, _position, actor => { Debug.Log("Inactivate"); inactivateCount++; });
                yield return null;
            }

            while (true)
            {
                if (inactivateCount == 5)
                    break;
                yield return null;
            }
            yield return null;

            for (int i = 0; i < 3; i++)
            {
                var effectData = EffectData.CreateTest();
                EffectManager.Current.Activate(effectData, _position, actor => { Debug.Log("Inactivate"); inactivateCount++; });
                yield return null;
            }
            while (true)
            {
                if (inactivateCount == 8)
                    break;
                yield return null;
            }
            yield return null;
            EffectManager.Current.CleanUp();
        }


    }
}
#endif