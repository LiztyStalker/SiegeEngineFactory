namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class PopulationData : NumberData
    {

        public PopulationData() {}

        private PopulationData(PopulationData data)
        {
            Value = data.Value;
        }

        public override INumberData Clone() => new PopulationData(this);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static PopulationData Create(int value)
        {
            var data = NumberDataUtility.Create<PopulationData>();
            data.Value = value;
            return data;
        }
#endif
    }
}