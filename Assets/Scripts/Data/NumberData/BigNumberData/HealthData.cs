namespace SEF.Data
{
    using UnityEngine;

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

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static HealthData Create_Test()
        {
            var data = new HealthData();
            data.Value = 1000;
            return data;
        }

        
#endif
    }
}