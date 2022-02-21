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

        public GoldAssetData() { }
        private GoldAssetData(GoldAssetData data) : base(data) { }

        public override INumberData Clone()
        {
            return new GoldAssetData(this);
        }
        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value) + (upgradeData.Value);
        }

        public void SetAssetData(BigNumberData data, int increaseValue, float increateRate, int value)
        {
            Value = NumberDataUtility.GetCompoundInterest(data.Value, increaseValue, increateRate, value);
        }
            

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = levelWaveData.GetLevel();
            var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, level);
            var waveValue = (levelValue * (int)(UnityEngine.Mathf.Round((float)((levelWaveData.Value % 10) - enemyData.IncreaseWaveRewardAssetValue) * enemyData.IncreaseWaveRewardAssetRate * 100f))) / 100;
            var value = levelValue + waveValue;
//            UnityEngine.Debug.Log(levelValue + " " + waveValue + " " + value);
            if (levelWaveData.IsThemeBoss())
                value *= 4;
            else if (levelWaveData.IsBoss())
            {
                value *= 2;
            }
            Value = value;

        }

        public static GoldAssetData Create(string value)
        {
            var data = new GoldAssetData();
            data.ValueText = value;
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

        public System.Type UsedStatisticsType() => typeof(Statistics.GoldUsedAssetStatisticsData);

        public System.Type GetStatisticsType() => typeof(Statistics.GoldGetAssetStatisticsData);

        public System.Type AccumulateStatisticsType() => typeof(Statistics.GoldAccumulateAssetStatisticsData);

        public StorableData GetStorableData()
        {
            var data = new AssetDataStorableData();
            data.SetData(GetType().AssemblyQualifiedName, AssetValue.ToString());
            return data;
        }
#endif

    }


}