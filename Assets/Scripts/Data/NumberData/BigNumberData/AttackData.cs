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

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackData Create_Test()
        {
            var data = new AttackData();
            data.Value = 100;
            return data;
        }

       
#endif
    }
}