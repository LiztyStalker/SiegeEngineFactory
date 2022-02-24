namespace SEF.Data
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;


    //type을 찾지 못하는 버그 있음
    #region ##### Serialize #####

    [System.Serializable]
    public struct SerializeBigNumberData
    {
        [SerializeField] private string _type;
        [SerializeField] private string _value;

        public string @Type => _type;
        public string @Value => _value; //ex) 1B1A

        internal void SetData(string type, string value)
        {
//            Debug.Log(type);
            _type = type;
            _value = value;
        }

        public BigNumberData GetDeserializeData()
        {
            try
            {
                var type = System.Type.GetType(_type, true);
                if (type != null)
                {
                    var data = (BigNumberData)NumberDataUtility.Create(type);
                    data.ValueText = _value;
                    return data;
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Deserialize에 실패했습니다 : {e.Message}");
#endif
            }
            return null;
        }
    }
    #endregion






    [System.Serializable]
    public abstract class BigNumberData : INumberData
    {
        private const int NUMBER_ALPHABET = 'Z' - 'A'; //26

        [SerializeField]
        private string _valueText;
        public string ValueText { get { return _valueText; } set { _valueText = value; } }

        private BigDecimal? _value;
        public BigDecimal Value 
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
                _value = new BigDecimal();
        }


        private BigDecimal GetStringToBigDecimal(string value)
        {
            var str = value;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            int digit = 0;
            string letter = null;
            for (int i = 0; i < str.Length; i++)
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
                    if (digit > 0 && !string.IsNullOrEmpty(letter))
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

            if (digit > 0 && !string.IsNullOrEmpty(letter))
            {
                dic.Add(letter, digit);
            }
            else if (digit > 0)
            {
                dic.Add("0", digit);
            }


            //var decimalPoint = s

            BigDecimal bigdec = new BigDecimal();

            foreach (var key in dic.Keys)
            {
                var result = new BigDecimal(BigInteger.Multiply(dic[key], GetDigit(key)));
                bigdec += result;
            }
            return bigdec;
        }


        public void SetValue(string value)
        {
            BigDecimal bigdec;
            var str = value;

            if (int.TryParse(str, out int intTmp))
            {
                //숫자로만 이루어짐
                bigdec = new BigDecimal(intTmp);
            }
            else if(double.TryParse(str, out double doubleTmp))
            {
                bigdec = new BigDecimal(doubleTmp);
                //소수로만 이루어짐 
            }
            else
            {
                //문자 1.000A
                bigdec = GetStringToBigDecimal(str);
            }
            Value = bigdec;
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

        private string GetValue(BigDecimal bigInt)
        {
            var str = bigInt.Value.ToString();
            //Debug.Log(str);
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
                return $"{str1}.{str2}{digit}";
            }
            //출력
            return str;
        }

        private int GetDigitCapacity(BigDecimal bigInt)
        {
            var value = bigInt.Value;
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

        public string GetDigitValue(BigDecimal bigInt)
        {
            Stack<string> stack = new Stack<string>();
            var value = bigInt.Value;
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
                if (digit / NUMBER_ALPHABET == 0)
                {
                    builder.Append(GetAlphabet(digit));
                    break;
                }
                else
                {
                    digit = (digit / NUMBER_ALPHABET) - 1;
                    builder.Append(GetAlphabet(digit % NUMBER_ALPHABET));
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

        public void SetCompoundInterest(float nowValue, float rate, int length = 1)
        {
            _value = NumberDataUtility.GetCompoundInterest(_value.Value, nowValue, rate, length);
        }

        public void SetIsolationInterest(float nowValue, int length = 1)
        {
            _value = NumberDataUtility.GetIsolationInterest(_value.Value, nowValue, length);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        protected void Clear()
        {
            Value = 0;
        }
#endif



        #region ##### SerializeData #####

        public SerializeBigNumberData GetSerializeData()
        {
            var serializeData = new SerializeBigNumberData();
            serializeData.SetData(GetType().FullName, GetDigitValue());
            return serializeData;
        }

        #endregion


    }
}