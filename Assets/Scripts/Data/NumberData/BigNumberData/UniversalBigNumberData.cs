namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class UniversalBigNumberData : BigNumberData
    {
        public UniversalBigNumberData() : base() { }
        protected UniversalBigNumberData(BigNumberData value) : base(value) { }

        public override INumberData Clone()
        {
            return new UniversalBigNumberData(this);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static UniversalBigNumberData Create_Test()
        {
            var data = new UniversalBigNumberData();
            data.ValueText = "100";
            return data;
        }

        public static UniversalBigNumberData Create_Test(int value)
        {
            var data = new UniversalBigNumberData();
            data.ValueText = value.ToString();
            return data;
        }
#endif
    }
}