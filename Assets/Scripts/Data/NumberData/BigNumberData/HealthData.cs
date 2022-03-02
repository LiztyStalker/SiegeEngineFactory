namespace SEF.Data
{
    using UnityEngine;

    [System.Serializable]
    public class HealthData : BigNumberData
    {
        
        public HealthData() : base(){ }
        protected HealthData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new HealthData(this);
        }


        public void Add(BigNumberData addNumberData, BigNumberData maxNumberData)
        {
            if (Value + addNumberData.Value > maxNumberData.Value)
                Value = maxNumberData.Value;
            else
                Value += addNumberData.Value;
        }

        public void Subject(BigNumberData subjectNumberData) 
        {
            if (Value - subjectNumberData.Value < 0)
                Value = 0;
            else
                Value -= subjectNumberData.Value;
        }

        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartHealthValue.Value, unitData.IncreaseHealthValue, unitData.IncreaseHealthRate, upgradeData.Value) + (upgradeData.Value);
        }

        public void SetAssetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            var level = levelWaveData.GetLevel();
            var levelValue = NumberDataUtility.GetCompoundInterest(enemyData.StartHealthValue.Value, enemyData.IncreaseLevelHealthValue, enemyData.IncreaseLevelHealthRate, level);
            var waveValue = (levelValue * (levelWaveData.GetWave() - enemyData.IncreaseWaveHealthValue) * enemyData.IncreaseWaveHealthRate);
            var value = levelValue + waveValue;
            //Debug.Log(levelValue.GetDecimalValue() + " " + waveValue.GetDecimalValue() + " " + value.GetDecimalValue());
            if (levelWaveData.IsThemeBoss())
                value *= 5;
            else if (levelWaveData.IsBoss())
            {
                value *= 3;
            }
            Value = value;
        }

        public static HealthData Create(string value)
        {
            var data = new HealthData();
            data.ValueText = value;
            return data;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static HealthData Create_Test()
        {            
            var data = new HealthData();
            data.ValueText = "100";
            return data;
        }

        public static HealthData Create_Test(int value)
        {
            var data = new HealthData();
            data.ValueText = value.ToString();
            return data;
        }        
#endif
    }
}