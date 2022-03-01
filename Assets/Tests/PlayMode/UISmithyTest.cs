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

    public class UISmithyTest
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
        public IEnumerator UISmithyLineTest_Initialize()
        {
            var line = UISmithyLine_Test.Create();
            line.Initialize();
            yield return new WaitForSeconds(1f);
            line.Dispose();
        }

        [UnityTest]
        public IEnumerator UISmithyTest_Initialize()
        {
            var system = UISmithy_Test.Create();
            system.Initialize();
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }

        /// <summary>
        /// UISmithy
        /// UISmithyLine 1개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UISmithyTest_Create_UISmithyLine()
        {
            SmithyEntity entity = new SmithyEntity();
            entity.Initialize();
            entity.SetData(SmithyData.Create_Test());
            var system = UISmithy_Test.Create();
            system.Initialize();
            system.Instance.RefreshSmithy(0, entity);
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }


        /// <summary>
        /// UISmithy
        /// UISmithyLine 5개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UISmithyTest_Create_UISmithyLine_x5()
        {
            SmithyEntity entity = new SmithyEntity();
            entity.Initialize();
            entity.SetData(SmithyData.Create_Test());

            var system = UISmithy_Test.Create();
            system.Initialize();
            system.Instance.RefreshSmithy(0, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshSmithy(1, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshSmithy(2, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshSmithy(3, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshSmithy(4, entity);
            yield return new WaitForSeconds(0.5f);
            system.Dispose();
        }




        /// <summary>
        /// UISmithyLine UpgradeButton 비활성화
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UISmithyLineTest_UpgradeButton_Disable()
        {

            SmithyEntity entity = new SmithyEntity();
            entity.Initialize();
            entity.SetData(SmithyData.Create_Test());

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();


            var line = UISmithyLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshSmithyLine(entity);
            line.Instance.RefreshAssetEntity(assetEntity);

            yield return new WaitForSeconds(1f);

            assetEntity.CleanUp();
            line.Dispose();
        }


        /// <summary>
        /// UISmithyLine UpgradeButton 활성화
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UISmithyLineTest_UpgradeButton_Enable()
        {

            SmithyEntity entity = new SmithyEntity();
            entity.Initialize();
            entity.SetData(SmithyData.Create_Test());

            IAssetData assetData = GoldAssetData.Create_Test(500);

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();
            assetEntity.Add(assetData);


            var line = UISmithyLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshSmithyLine(entity);
            line.Instance.RefreshAssetEntity(assetEntity);

            yield return new WaitForSeconds(1f);
                        
            assetEntity.CleanUp();
            line.Dispose();
        }
    }
}
#endif