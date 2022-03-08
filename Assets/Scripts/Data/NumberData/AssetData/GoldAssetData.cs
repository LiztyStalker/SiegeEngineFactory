namespace SEF.Data
{
    using System.Numerics;
    using Utility.IO;

    [System.Serializable]
    public class GoldAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { 
            get => Value.Value;
            set
            {
                var val = Value;
                val.Value = value;
                Value = val;
            }
        }
        public static GoldAssetData Create(string value)
        {
            var data = new GoldAssetData();
            data.ValueText = value;
            return data;
        }
        public GoldAssetData() { }
        private GoldAssetData(GoldAssetData data) : base(data) { }

        public override INumberData Clone()
        {
            return new GoldAssetData(this);
        }
        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            //Value = NumberDataUtility.GetCompoundInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value) + (upgradeData.Value);
            Value = NumberDataUtility.GetIsolationInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value);
        }

        public void SetAssetData(BigNumberData data, int increaseValue, float increaseRate, int value)
        {
            //Value = NumberDataUtility.GetCompoundInterest(data.Value, increaseValue, increateRate, value);
            Value = NumberDataUtility.GetIsolationInterest(data.Value, increaseValue, increaseRate, value);
        }


        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = levelWaveData.GetLevel();
            var wave = levelWaveData.GetWave();

            //var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, level);
            var levelValue = NumberDataUtility.GetIsolationInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, level);

//            var waveValue = (levelValue * (UnityEngine.Mathf.Round((float)((levelWaveData.Value % 10) - enemyData.IncreaseWaveRewardAssetValue) * enemyData.IncreaseWaveRewardAssetRate)));
            var waveValue = NumberDataUtility.GetIsolationInterest(levelValue, enemyData.IncreaseWaveRewardAssetValue, enemyData.IncreaseWaveRewardAssetRate, wave);


            //themeValue

            var value = waveValue;


            if (levelWaveData.IsThemeBoss())
                value *= 4;
            else if (levelWaveData.IsBoss())
                value *= 2;

            Value = value;
        }



        public System.Type AccumulativelyUsedStatisticsType() => typeof(Statistics.AccumulativelyGoldUsedAssetStatisticsData);
        public System.Type AccumulativelyGetStatisticsType() => typeof(Statistics.AccumulativelyGoldGetAssetStatisticsData);

        public StorableData GetStorableData()
        {
            var data = new AssetDataStorableData();
            data.SetData(GetType().AssemblyQualifiedName, AssetValue.ToString());
            return data;
        }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static GoldAssetData Create_Test() 
        {
            var data = new GoldAssetData();
            data.ValueText = "10";
            return data;
        }

        public static GoldAssetData Create_Test(int value)
        {
            var data = new GoldAssetData();
            data.ValueText = value.ToString();
            return data;
        }

#endif

    }


}