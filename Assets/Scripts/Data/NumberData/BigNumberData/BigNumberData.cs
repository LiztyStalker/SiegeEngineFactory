namespace SEF.Data
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public abstract class BigNumberData : INumberData
    {
        private BigInteger _value;

        public BigInteger Value { get {return _value; } set {_value = value; } }

        public BigNumberData() { }

        protected BigNumberData(BigNumberData data)
        {
            _value = data.Value;
        }

        public string GetValue()
        {
            var str = _value.ToString();
            int capacity = GetDigitCapacity();

            if(capacity > 0)
            {
                //자리수적용
                string digit = GetDigit(capacity);

                //소숫점 적용
                int dot = (str.Length % 3);
                int length = (dot == 0) ? 3 : dot;
                //1/000
                //10/000
                //100/000
                //1.000A 1 1
                //10.00A 2 2
                //100.0A 3 0
                
                var str1 = str.Substring(0, length);
                var str2 = str.Substring(length, 4 - length);
                return str1 + "." + str2 + digit;
            }

            //출력
            return str;
        }

        private int GetDigitCapacity()
        {
            var value = _value;
            var capacity = 0;
            while (true)
            {
                value /= 1000;
                if (value == 0)
                {
                    break;
                }
                capacity++;
            }
            return capacity;
        }

        public string GetDigitValue()
        {
            Stack<string> stack = new Stack<string>();
            var value = _value;
            while (true) 
            {
                var digit = GetDigit(stack.Count);
                var str = value % 1000;
                stack.Push(string.Format("{0:d3}{1} ", str, digit));
                value /= 1000;
                if(value == 0)
                    break;
            }

            StringBuilder builder = new StringBuilder();

            while (true)
            {
                builder.Append(stack.Pop());
                if (stack.Count == 0)
                    break;
            }
            return builder.ToString();
        }

        private string GetDigit(int capacity)
        {
            if(capacity == 0)
            {
                return "";
            }

            capacity--;

            StringBuilder builder = new StringBuilder();

            var digit = capacity;

            Debug.Log(digit);

            while (true)
            {
                if (digit / 26 == 0)
                {
                    builder.Append(GetAlphabet(digit));
                    break;
                }
                else
                {
                    digit = (digit / 26) - 1;
                    builder.Append(GetAlphabet(digit % 26));
                    //Debug.Log(digit);
                }
            }
            return builder.ToString();
        }

        private string GetAlphabet(int capacity)
        {
            return char.ConvertFromUtf32('A' + capacity).ToString();
        }

        public abstract INumberData Clone();

        public void CleanUp()
        {
            Value = 0;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        protected void Clear()
        {
            Value = 0;
        }
#endif

    }
}