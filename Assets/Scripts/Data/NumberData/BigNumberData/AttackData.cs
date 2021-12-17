namespace SEF.Data
{
    using UnityEngine;

    public class AttackData : BigNumberData
    {

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackData Create_Test()
        {
            var data = new AttackData();
            data.Clear();
            data.Value.Add("0", 100);
            return data;
        }
#endif
    }
}