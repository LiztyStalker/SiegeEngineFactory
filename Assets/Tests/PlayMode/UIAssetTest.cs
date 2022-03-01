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

    public class UIAssetTest
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
        public IEnumerator UIAssetBlockTest_Initialize()
        {
            var block = UIAssetBlock_Test.Create();
            block.Initialize();
            yield return new WaitForSeconds(1f);
            block.Dispose();
        }



        [UnityTest]
        public IEnumerator UIAssetBlockTest_Refresh()
        {
            //var data = AssetData.Create_Test(TYPE_ASSET.Gold, 100);
            var data = GoldAssetData.Create_Test(100);
            var block = UIAssetBlock_Test.Create();
            block.Initialize();
            block.Instance.RefreshAssetData(data);
            yield return new WaitForSeconds(1f);
            block.Dispose();
        }

        [UnityTest]
        public IEnumerator UIAssetBlockTest_Refresh_Random()
        {
            var data = GoldAssetData.Create_Test(100);
//            var data = AssetData.Create_Test(TYPE_ASSET.Gold, 100);
            var block = UIAssetBlock_Test.Create();
            block.Initialize();
            int count = 0;
            while (true)
            {
                var value = (int)Random.Range(0, 1000000000);
                data.Value += value;
                block.Instance.RefreshAssetData(data);
                count++;
                if (count > 1000)
                    break;
                yield return null;
            }
            block.Dispose();
        }


        [UnityTest]
        public IEnumerator UIAssetTest_Initialize()
        {
            var asset = UIAsset_Test.Create();
            asset.Initialize();
            yield return new WaitForSeconds(1f);
            asset.Dispose();
        }

        [UnityTest]
        public IEnumerator UIAssetTest_Refresh()
        {
            var gold = GoldAssetData.Create_Test(100);
//            var gold = AssetData.Create_Test(TYPE_ASSET.Gold, 100);
//            var ore = AssetData.Create_Test(TYPE_ASSET.Ore, 100);
//            var res = AssetData.Create_Test(TYPE_ASSET.Resource, 100);
//            var met = AssetData.Create_Test(TYPE_ASSET.Meteorite, 100);
//            var pop = AssetData.Create_Test(TYPE_ASSET.Population, 100);


            var asset = UIAsset_Test.Create();
            asset.Initialize();
            asset.Instance.RefreshAssetData(gold);
//            asset.Instance.RefreshAssetData(ore);
//            asset.Instance.RefreshAssetData(res);
//            asset.Instance.RefreshAssetData(met);
//            asset.Instance.RefreshAssetData(pop);
            yield return new WaitForSeconds(1f);
            asset.Dispose();
        }

        [UnityTest]
        public IEnumerator UIAssetTest_Refresh_Random()
        {
            IAssetData[] assets = new IAssetData[]
            {
                GoldAssetData.Create_Test(100),
//                AssetData.Create_Test(TYPE_ASSET.Ore, 100),
//                AssetData.Create_Test(TYPE_ASSET.Resource, 100),
//                AssetData.Create_Test(TYPE_ASSET.Meteorite, 100)
            };

            //AssetData[] assets = new AssetData[]
            //{
            //    AssetData.Create_Test(TYPE_ASSET.Gold, 100),
            //    AssetData.Create_Test(TYPE_ASSET.Ore, 100),
            //    AssetData.Create_Test(TYPE_ASSET.Resource, 100),
            //    AssetData.Create_Test(TYPE_ASSET.Meteorite, 100)
            //};


            var asset = UIAsset_Test.Create();
            asset.Initialize();
            int count = 0;
            while (true)
            {
                var index = Random.Range(0, assets.Length);
                var value = (int)Random.Range(0, 1000000000);
                assets[index].AssetValue += value;
                asset.Instance.RefreshAssetData(assets[index]);
                count++;
                if (count > 1000)
                    break;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            asset.Dispose();
        }
    }
}
#endif