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

        public bool IsZero()
        {
            return Value.IsZero;
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
            Value = NumberDataUtility.GetCompoundInterest(unitData.StartHealthValue.Value, unitData.IncreaseHealthValue, unitData.IncreaseHealthRate, upgradeData.Value - 1);
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static HealthData Create_Test()
        {            
            var data = new HealthData();
            data.ValueText = "1000";
            return data;
        }

        
#endif
    }
}