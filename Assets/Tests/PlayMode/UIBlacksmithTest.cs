#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.UI.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;
    using Toolkit;
    using Data;
    using Entity;

    public class UIBlacksmithTest
    {

        private Camera _camera;
        private Light2D _light;

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
        public IEnumerator UIBlacksmithLineTest_Initialize()
        {
            var line = UIBlacksmithLine_Test.Create();
            line.Initialize();
            yield return new WaitForSeconds(1f);
            line.Dispose();
        }

        [UnityTest]
        public IEnumerator UIBlacksmithTest_Initialize()
        {
            var system = UIBlacksmith_Test.Create();
            system.Initialize();
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }

        /// <summary>
        /// UIBlackSmith
        /// UIBlacksmithLine 1개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIBlacksmithTest_Create_UIBlacksmithLine()
        {
            BlacksmithEntity entity;
            entity.Initialize();
            entity.SetData(BlacksmithData.Create_Test());
            var system = UIBlacksmith_Test.Create();
            system.Initialize();
            system.Instance.RefreshBlacksmith(0, entity);
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }


        /// <summary>
        /// UIBlackSmith
        /// UIBlacksmithLine 5개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIBlacksmithTest_Create_UIBlacksmithLine_x5()
        {
            BlacksmithEntity entity;
            entity.Initialize();
            entity.SetData(BlacksmithData.Create_Test());

            var system = UIBlacksmith_Test.Create();
            system.Initialize();
            system.Instance.RefreshBlacksmith(0, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshBlacksmith(1, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshBlacksmith(2, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshBlacksmith(3, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshBlacksmith(4, entity);
            yield return new WaitForSeconds(0.5f);
            system.Dispose();
        }




        /// <summary>
        /// UIBlacksmithLine UpgradeButton 비활성화
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIBlacksmithLineTest_UpgradeButton_Disable()
        {

            BlacksmithEntity entity;
            entity.Initialize();
            entity.SetData(BlacksmithData.Create_Test());

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();


            var line = UIBlacksmithLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshBlacksmithLine(entity);
            line.Instance.RefreshAssetEntity(assetEntity);

            yield return new WaitForSeconds(1f);

            assetEntity.CleanUp();
            line.Dispose();
        }


        /// <summary>
        /// UIBlacksmithLine UpgradeButton 활성화
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIBlacksmithLineTest_UpgradeButton_Enable()
        {

            BlacksmithEntity entity;
            entity.Initialize();
            entity.SetData(BlacksmithData.Create_Test());

            IAssetData assetData = GoldAssetData.Create_Test(500);

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();
            assetEntity.Add(assetData);


            var line = UIBlacksmithLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshBlacksmithLine(entity);
            line.Instance.RefreshAssetEntity(assetEntity);

            yield return new WaitForSeconds(1f);
                        
            assetEntity.CleanUp();
            line.Dispose();
        }
    }
}
#endif