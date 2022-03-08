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
#if INCLUDE_UI_TOOLKIT
    using Toolkit;
#endif
    using Data;
    using Entity;

    public class UIMineTest
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
        public IEnumerator UIMineTest_Initialize()
        {
            var system = UIMine_Test.Create();
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
        public IEnumerator UIMineTest_Create_UIMineLine()
        {
            MineEntity entity = new MineEntity();
            entity.Initialize();
            entity.SetData(MineData.Create_Test());
            var system = UIMine_Test.Create();
            system.Initialize();
            system.Instance.RefreshMine(0, entity);
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }


        /// <summary>
        /// UIBlackSmith
        /// UIBlacksmithLine 5개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIMineTest_Create_UIMineLine_x5()
        {
            MineEntity entity = new MineEntity();
            entity.Initialize();
            entity.SetData(MineData.Create_Test());

            var system = UIMine_Test.Create();
            system.Initialize();
            system.Instance.RefreshMine(0, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshMine(1, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshMine(2, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshMine(3, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshMine(4, entity);
            yield return new WaitForSeconds(0.5f);
            system.Dispose();
        }




        ///// <summary>
        ///// UIBlacksmithLine UpgradeButton 비활성화
        ///// </summary>
        ///// <returns></returns>
        //[UnityTest]
        //public IEnumerator UIMineLineTest_UpgradeButton_Disable()
        //{

        //    MineEntity entity;
        //    entity.Initialize();
        //    entity.SetData(MineData.Create_Test());

        //    AssetPackage assetEntity = AssetPackage.Create();
        //    assetEntity.Initialize();


        //    var line = UIMineLine_Test.Create();
        //    line.Initialize();
        //    line.Instance.RefreshMineLine(entity);
        //    line.Instance.RefreshAssetEntity(assetEntity);

        //    yield return new WaitForSeconds(1f);

        //    assetEntity.CleanUp();
        //    line.Dispose();
        //}


        ///// <summary>
        ///// UIBlacksmithLine UpgradeButton 활성화
        ///// </summary>
        ///// <returns></returns>
        //[UnityTest]
        //public IEnumerator UIMineLineTest_UpgradeButton_Enable()
        //{

        //    MineEntity entity;
        //    entity.Initialize();
        //    entity.SetData(MineData.Create_Test());

        //    IAssetData assetData = GoldAssetData.Create_Test(500);

        //    AssetPackage assetEntity = AssetPackage.Create();
        //    assetEntity.Initialize();
        //    assetEntity.Add(assetData);


        //    var line = UIMineLine_Test.Create();
        //    line.Initialize();
        //    line.Instance.RefreshMineLine(entity);
        //    line.Instance.RefreshAssetEntity(assetEntity);

        //    yield return new WaitForSeconds(1f);
                        
        //    assetEntity.CleanUp();
        //    line.Dispose();
        //}
    }
}
#endif