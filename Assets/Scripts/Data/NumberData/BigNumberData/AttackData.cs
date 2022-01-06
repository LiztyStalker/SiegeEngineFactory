namespace SEF.Data
{
    using UnityEngine;

    [System.Serializable]
    public class AttackData : BigNumberData
    {
        public AttackData() : base() { }
        protected AttackData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new AttackData(this);
        }

        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartAttackValue.Value, unitData.IncreaseAttackValue, unitData.IncreaseAttackRate, upgradeData.Value - 1);
        }

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = (levelWaveData.GetWave() == 0) ? levelWaveData.GetLevel() - 1 : levelWaveData.GetLevel();
            var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartAttackValue.Value, enemyData.IncreaseAttackValue, enemyData.IncreaseAttackRate, level);
            //            var waveValue = (levelValue * (int)(UnityEngine.Mathf.Round((float)((levelWaveData.Value - 1) % 10 - enemyData.IncreaseWaveHealthValue) * enemyData.IncreaseWaveHealthRate * 100f))) / 100;
            var value = levelValue;// + waveValue;
            //            Debug.Log(levelValue + " " + waveValue + " " + value);
            if (levelWaveData.IsThemeBoss())
                value *= 3;
            else if (levelWaveData.IsBoss())
            {
                value *= 2;
            }
            Value = value;

        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackData Create_Test()
        {
            var data = new AttackData();
            data.ValueText = "30";
            return data;
        }
        public static AttackData Create_Test(int value)
        {
            var data = new AttackData();
            data.ValueText = value.ToString();
            return data;
        }
#endif
    }
}