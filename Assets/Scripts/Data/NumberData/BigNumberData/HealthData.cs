namespace SEF.Data
{
    using UnityEngine;

    public class HealthData : BigNumberData
    {
        public bool IsZero()
        {
            return false;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static HealthData Create_Test()
        {
            var data = new HealthData();
            data.Clear();
            data.Value.Add("A", 1);
            return data;
        }

        
#endif
    }
}