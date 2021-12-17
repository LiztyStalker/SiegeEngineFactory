namespace SEF.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class BigNumberData : INumberData
    {
        private Dictionary<string, int> _value;
        protected Dictionary<string, int> Value => _value;

        protected BigNumberData()
        {
            _value = new Dictionary<string, int>();
            _value.Add("0", 0);
        }

        public void Add(BigNumberData bigNumberData)
        {
            //합산 연산식
        }
        public void Subject(BigNumberData bigNumberData)
        {
            //차산 연산식
        }
        public string GetValue()
        {
            //데이터 출력
            return null;
        }
        public INumberData Clone()
        {
            //복사본 출력
            return null;
        }
        public void CleanUp()
        {
            _value = null;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        protected void Clear()
        {
            _value.Clear();
        }
#endif

    }
}