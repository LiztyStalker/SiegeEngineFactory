namespace SEF.Data
{
    using UnityEngine;

    public abstract class NumberData : INumberData
    {
        private int _value;
        public int Value { get => _value; protected set => _value = value; }

        protected NumberData()
        {
            Initialize();
        }

        public abstract INumberData Clone();

        public virtual string GetValue()
        {
            return Value.ToString();
        }

        public void IncreaseNumber()
        {
            _value++;
        }

        public virtual void Initialize() { }

        public void CleanUp()
        {
            _value = 0;
        }

        public void SetCompoundInterest(float nowValue = 1, float rate = 0.1f, int length = 1) { }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        /// <summary>
        /// Test¿ë
        /// </summary>
        /// <param name="value"></param>
        public void SetValue_Test(int value)
        {
            Value = value;
        }
#endif
    }
}