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
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartUpgradeAsset.Value, unitData.IncreaseUpgradeAssetValue, unitData.IncreaseUpgradeAssetRate, upgradeData.Value - 1) + (upgradeData.Value - 1);
        }

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = (levelWaveData.GetWave() == 0) ? levelWaveData.GetLevel() - 1 : levelWaveData.GetLevel();
            var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartRewardAssetValue.Value, enemyData.IncreaseLevelRewardAssetValue, enemyData.IncreaseLevelRewardAssetRate, level);
            var waveValue = (levelValue * (int)(UnityEngine.Mathf.Round((float)(((levelWaveData.Value - 1) % 10) - enemyData.IncreaseWaveRewardAssetValue) * enemyData.IncreaseWaveRewardAssetRate * 100f))) / 100;
            var value = levelValue + waveValue;
            UnityEngine.Debug.Log(levelValue + " " + waveValue + " " + value);
            if (levelWaveData.IsThemeBoss())
                value *= 4;
            else if (levelWaveData.IsBoss())
            {
                value *= 2;
            }
            Value = value;

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