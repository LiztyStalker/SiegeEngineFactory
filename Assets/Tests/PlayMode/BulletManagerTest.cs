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


    public class BulletManagerTest
    {
        private Camera _camera;
        private Light2D _light;

        private Vector2 _startPosition = new Vector2(-2f, 2f);
        private Vector2 _arrivePosition = new Vector2(2f, 2f);

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
        public IEnumerator BulletManagerTest_Activate()
        {
            bool isRun = true;
            var bulletData = BulletData.CreateTest();
            bulletData.SetData(BulletData.TYPE_BULLET_ACTION.Move, 1f, false);
            BulletManager.Current.Activate(bulletData, 1f, _startPosition, _arrivePosition, actor => { Debug.Log("Arrive"); isRun = false; });
            while (isRun)
            {
                yield return null;
            }
            yield return null;
            BulletManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator BulletManagerTest_Activate5()
        {
            int arrivedCount = 0;
            for (int i = 0; i < 5; i++)
            {
                var bulletData = BulletData.CreateTest();
                bulletData.SetData(BulletData.TYPE_BULLET_ACTION.Move, 1f, false);
                BulletManager.Current.Activate(bulletData, 1f, _startPosition, _arrivePosition, actor => { Debug.Log("Arrive"); arrivedCount++; });
                yield return null;
            }

            while (true)
            {
                if (arrivedCount == 5)
                    break;
                yield return null;
            }
            yield return null;
            BulletManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator BulletManagerTest_Activate5_After_Activate3()
        {
            int arrivedCount = 0;
            for (int i = 0; i < 5; i++)
            {
                var bulletData = BulletData.CreateTest();
                bulletData.SetData(BulletData.TYPE_BULLET_ACTION.Move, 1f, false);
                BulletManager.Current.Activate(bulletData, 1f, _startPosition, _arrivePosition, actor => { Debug.Log("Arrive"); arrivedCount++; });
                yield return null;
            }

            while (true)
            {
                if (arrivedCount == 5)
                    break;
                yield return null;
            }
            yield return null;

            for (int i = 0; i < 3; i++)
            {
                var bulletData = BulletData.CreateTest();
                bulletData.SetData(BulletData.TYPE_BULLET_ACTION.Move, 1f, false);
                BulletManager.Current.Activate(bulletData, 1f, _startPosition, _arrivePosition, actor => { Debug.Log("Arrive"); arrivedCount++; });
                yield return null;
            }
            while (true)
            {
                if (arrivedCount == 8)
                    break;
                yield return null;
            }
            yield return null;


            BulletManager.Current.CleanUp();
        }


    }
}
#endif