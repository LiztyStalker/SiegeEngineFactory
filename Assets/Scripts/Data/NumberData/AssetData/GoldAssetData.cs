namespace SEF.Data
{
    using System.Numerics;

    [System.Serializable]
    public class GoldAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { get => Value; set => Value = value; }

        public GoldAssetData() { }
        private GoldAssetData(GoldAssetData data) : base(data) { }

        public override INumberData Clone()
        {
            return new GoldAssetData(this);
        }
        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value - 1);
        }

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, levelWaveData.GetLevel());
            var waveValue = (int)UnityEngine.Mathf.Round(((float)levelWaveData.GetWave() * enemyData.IncreaseWaveRewardAssetRate + (float)levelWaveData.Value)) * 100;

            var value = (levelValue * 2) + (levelValue * waveValue);
            value /= 100;
            Value = value;
//            Value = NumberDataUtility.GetCompoundInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, levelWaveData.GetLevel());
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static GoldAssetData Create_Test() 
        {
            var data = new GoldAssetData();
            data.ValueText = "100";
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