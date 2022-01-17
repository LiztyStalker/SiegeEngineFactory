namespace SEF.Data
{
    using UnityEngine;

    [System.Serializable]
    public class DamageData : BigNumberData
    {
        public DamageData() : base() { }
        protected DamageData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new DamageData(this);
        }

        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartAttackValue.Value, unitData.IncreaseAttackValue, unitData.IncreaseAttackRate, upgradeData.Value) + (upgradeData.Value);
        }

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = levelWaveData.GetLevel();
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

        public void SetAssetData(AttackData attackData, LevelWaveData levelWaveData)
        {
            var level = levelWaveData.GetLevel();
            var levelValue = NumberDataUtility.GetCompoundInterest(attackData.DamageValue.Value, attackData.IncreaseDamageValue, attackData.IncreaseDamageRate, level);
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

        public void SetAssetData(AttackData attackData, NumberData numberData)
        {
            var level = numberData.Value;
            var levelValue = NumberDataUtility.GetCompoundInterest(attackData.DamageValue.Value, attackData.IncreaseDamageValue, attackData.IncreaseDamageRate, level);
            var value = levelValue;
            Value = value;

        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static DamageData Create_Test()
        {
            var data = new DamageData();
            data.ValueText = "30";
            return data;
        }
        public static DamageData Create_Test(int value)
        {
            var data = new DamageData();
            data.ValueText = value.ToString();
            return data;
        }
#endif
    }
}