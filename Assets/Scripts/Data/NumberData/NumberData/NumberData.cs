namespace SEF.Data
{
    using UnityEngine;

    public abstract class NumberData : INumberData
    {
        private int _value;

        protected int Value => _value;

        protected NumberData()
        {
            Initialize();
        }

        public INumberData Clone()
        {
            return null;
        }

        public string GetValue()
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
    }
}