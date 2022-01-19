namespace SEF.Data
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;


    [System.Serializable]
    public abstract class BigNumberData : INumberData
    {
        [SerializeField]
        private string _valueText;
        public string ValueText { get { return _valueText; } set { _valueText = value; } }

        private BigInteger? _value;
        public BigInteger Value 
        { 
            get {
                if (_value == null)
                {
                    SetValue();
                }
                return _value.Value; 
            } 
            set {
                _value = value; 
            } 
        }

        public BigNumberData() {}

        protected BigNumberData(BigNumberData data)
        {
            _value = data.Value;
        }
        public bool IsZero()
        {
            return Value.IsZero;
        }

        public void SetValue()
        {
            if (!string.IsNullOrEmpty(_valueText))
                SetValue(_valueText);
            else
                _value = new BigInteger();
        }

        public void SetValue(string value)
        {

            var str = value;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            int digit = 0;
            string letter = null;
            for(int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                //숫자 - 배수 곱하기 1, 10, 100...
                //문자 - 단위 넣기 A-Z 0-25 AA 26 ZZ 701

                if (char.IsLetter(ch))
                {
                    //첫 위치에 문자가 들어오면 무조건 에러
                    if (i == 0)
                    {
                        Debug.LogError("첫 위치에 문자가 들어올 수 없습니다");
                        break;
                    }

                    letter += ch;
                }
                else if (char.IsDigit(ch))
                {
                    //조립
                    if(digit > 0 && !string.IsNullOrEmpty(letter))
                    {
                        if (!dic.ContainsKey(letter))
                            dic.Add(letter, digit);
                        else
                        {
                            Debug.LogError($"{letter} 같은 자리수의 문자가 있습니다");
                            break;
                        }
                        digit = 0;
                        letter = null;
                    }

                    var tmpDigit = int.Parse(ch.ToString());
                    if (digit == 0)
                        digit = tmpDigit;
                    else
                    {
                        digit *= 10;
                        digit += tmpDigit;
                    }
                }
            }

            if(digit > 0 && !string.IsNullOrEmpty(letter))
            {
                dic.Add(letter, digit);
            }
            else if (digit > 0)
            {
                dic.Add("0", digit);
            }


            BigInteger bigint = new BigInteger();

            foreach (var key in dic.Keys)
            {
                var result = BigInteger.Multiply(dic[key], GetDigit(key));
                bigint = BigInteger.Add(bigint, result);
            }

            Value = bigint;
        }

        private BigInteger GetDigit(string letter)
        {
            int power = 1;
            for(int i = 0; i < letter.Length; i++)
            {
                if (letter[i] == '0')
                    power = 0;
                else
                    power *= ((int)letter[i] - 'A') * 3 + 3;
            }

            return BigInteger.Pow(10, power);
        }

        public string GetValue() => GetValue(Value);

        private string GetValue(BigInteger bigInt)
        {
            var str = bigInt.ToString();
            int capacity = GetDigitCapacity(bigInt);

            if (capacity > 0)
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

        private int GetDigitCapacity(BigInteger bigInt)
        {
            var value = bigInt;
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

        public string GetDigitValue() => GetDigitValue(Value);

        public string GetDigitValue(BigInteger bigInt)
        {
            Stack<string> stack = new Stack<string>();
            var value = bigInt;
            while (true)
            {
                var digit = GetDigit(stack.Count);
                var str = value % 1000;
                stack.Push(string.Format("{0:d3}{1} ", str, digit));
                value /= 1000;
                if (value == 0)
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

//            Debug.Log(digit);

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

        protected BigInteger Pow(BigInteger value, int exponent) => BigInteger.Pow(value, exponent);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        protected void Clear()
        {
            Value = 0;
        }
#endif




    }
}