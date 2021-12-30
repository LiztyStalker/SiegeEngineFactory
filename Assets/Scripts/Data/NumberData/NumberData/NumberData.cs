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

        public void Initialize()
        {
            _value = 1;
        }

        public void CleanUp()
        {
            _value = 0;
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetValue(int value)
        {
            Value = value;
        }
#endif
    }
}