namespace SEF.Data
{
    using UnityEngine;

    public abstract class NumberData : INumberData
    {
        private int _value;
        public int Value { get => _value; set => _value = value; }

        protected NumberData()
        {
            Initialize();
        }

        public abstract INumberData Clone();

        public virtual string GetValue()
        {
            return Value.ToString();
        }

        public void SetValue(int value)
        {
            Value = value;
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

        //public void SetCompoundInterest(float nowValue = 1, float rate = 0.1f, int length = 1) { Debug.LogWarning("계산되지 않음"); }
        public void SetIsolationInterest(float increaseValue, float increaseRate, int length = 1) 
        {
            _value = (int)NumberDataUtility.GetIsolationInterest(_value, increaseValue, increaseRate, length);
        }

    }
}