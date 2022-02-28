#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using NUnit.Framework;
    using UnityEngine;
    using Data;
    using Manager;
    using Entity;

    public class OfflineTest
    {
        private MineManager _mineManager;
        private MineLine _mineLine;

        [SetUp]
        public void SetUp()
        {
            var data = MineData.Create_Test();
            var entity = new MineEntity();
            entity.SetData(data);

            _mineLine = MineLine.Create();
            _mineLine.Initialize();
            _mineLine.SetData(data);

            _mineManager = MineManager.Create();
            _mineManager.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _mineLine.CleanUp();
            _mineManager.CleanUp();
        }

        [Test] 
        public void Offline_DateTime_Calculate()
        {
            System.DateTime nowTime = new System.DateTime(2022, 2, 1, 12, 0, 0);
            System.DateTime savedTime = new System.DateTime(2022, 2, 1, 6, 0, 0);

            var timespan = nowTime - savedTime;
            Debug.Log(timespan.TotalSeconds);
            Assert.AreEqual(timespan.TotalSeconds, 21600);
        }


        [Test]
        public void Offline_MineLine()
        {
            System.DateTime nowTime = new System.DateTime(2022, 2, 1, 12, 0, 0);
            System.DateTime savedTime = new System.DateTime(2022, 2, 1, 11, 59, 0);
            
            //60

            var timespan = nowTime - savedTime;

            var asset = _mineLine.RewardOffline(timespan);

            Debug.Log(asset.AssetValue);
            Assert.AreEqual(asset.AssetValue.ToString(), "60");
        }


        [Test]
        public void Offline_MineManager()
        {
            System.DateTime nowTime = new System.DateTime(2022, 2, 2, 12, 0, 0);
            System.DateTime savedTime = new System.DateTime(2022, 2, 1, 12, 0, 0);

            //21600

            var timespan = nowTime - savedTime;

            var asset = _mineManager.RewardOffline(timespan);

            var assetArray = asset.GetAssetArray();

            for(int i = 0; i < assetArray.Length; i++)
            {
                var data = assetArray[i];
                Debug.Log(data.AssetValue);
//                Assert.AreEqual(data.AssetValue.ToString(), "833760");
            }

        }

    }
}
#endif