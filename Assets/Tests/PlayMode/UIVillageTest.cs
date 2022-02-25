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

    public class UIVillageTest
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
        public IEnumerator UIVillageLineTest_Initialize()
        {
            var line = UIVillageLine_Test.Create();
            line.Initialize();
            yield return new WaitForSeconds(1f);
            line.Dispose();
        }

        [UnityTest]
        public IEnumerator UIVillageTest_Initialize()
        {
            var system = UIVillage_Test.Create();
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
        public IEnumerator UIVillageTest_Create_UIVillageLine()
        {
            VillageEntity entity = new VillageEntity();
            entity.Initialize();
            entity.SetData(VillageData.Create_Test());
            var system = UIVillage_Test.Create();
            system.Initialize();
            system.Instance.RefreshVillage(0, entity);
            yield return new WaitForSeconds(1f);
            system.Dispose();
        }


        /// <summary>
        /// UIBlackSmith
        /// UIBlacksmithLine 5개 생성
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIVillageTest_Create_UIVillageLine_x5()
        {
            VillageEntity entity = new VillageEntity();
            entity.Initialize();
            entity.SetData(VillageData.Create_Test());

            var system = UIVillage_Test.Create();
            system.Initialize();
            system.Instance.RefreshVillage(0, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshVillage(1, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshVillage(2, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshVillage(3, entity);
            yield return new WaitForSeconds(0.5f);
            system.Instance.RefreshVillage(4, entity);
            yield return new WaitForSeconds(0.5f);
            system.Dispose();
        }




        /// <summary>
        /// UIBlacksmithLine UpgradeButton 비활성화
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UIVillageLineTest_UpgradeButton_Disable()
        {

            VillageEntity entity = new VillageEntity();
            entity.Initialize();
            entity.SetData(VillageData.Create_Test());

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();


            var line = UIVillageLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshVillageLine(entity);
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
        public IEnumerator UIVillageLineTest_UpgradeButton_Enable()
        {

            VillageEntity entity = new VillageEntity();
            entity.Initialize();
            entity.SetData(VillageData.Create_Test());

            IAssetData assetData = GoldAssetData.Create_Test(500);

            AssetPackage assetEntity = AssetPackage.Create();
            assetEntity.Initialize();
            assetEntity.Add(assetData);


            var line = UIVillageLine_Test.Create();
            line.Initialize();
            line.Instance.RefreshVillageLine(entity);
            line.Instance.RefreshAssetEntity(assetEntity);

            yield return new WaitForSeconds(1f);
                        
            assetEntity.CleanUp();
            line.Dispose();
        }
    }
}
#endif