namespace System.Numerics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Unity.Mathematics;

    [System.Serializable]
    public struct BigDecimal : IComparable, IComparable<BigDecimal>, IEquatable<BigDecimal>, IFormattable
    {

        //private static Dictionary<byte, int> _powDic = new Dictionary<byte, int>();

        private const byte COUNT_DECIMALPOINT = 5;

        [SerializeField]
        private BigInteger _value;

        [SerializeField]
        private byte _decimalPoint;

        public bool IsZero => (_value.IsZero && _decimalPoint == 0);

        public BigInteger Value { get => _value; set => _value = value; }

        public BigDecimal(int value)
        {
            _decimalPoint = 0;
            _value = ConvertToBigInteger((decimal)value);
        }
        public BigDecimal(double value)
        {
            _decimalPoint = 0;
            _value = ConvertToBigInteger((decimal)value);
        }
        public BigDecimal(float value)
        {
            _decimalPoint = 0;
            _value = ConvertToBigInteger((decimal)value);
        }
        public BigDecimal(decimal value)
        {
            _decimalPoint = 0;
            _value = ConvertToBigInteger(value);
        }

        public BigDecimal(BigDecimal bigdec)
        {
            _value = bigdec._value;
            _decimalPoint = bigdec._decimalPoint;
        }

        public void Clear()
        {
            _value = 0;
        }


        public decimal GetDecimalValue()
        {
            //반올림
            return decimal.Round(((decimal)_value) * (decimal)Mathf.Pow(0.1f, _decimalPoint), COUNT_DECIMALPOINT);
        }

        public override string ToString()
        {
            return null;
        }

        private BigInteger ConvertToBigInteger(decimal value)
        {
            CalculateDecimalPoint((decimal)value, out _decimalPoint, out int integer);
            return new BigInteger(integer);
        }

        public static void CalculateDecimalPoint(decimal value, out byte decimalPoint, out int integer)
        {
            decimal nowValue = value;
            decimalPoint = 0;
            while (true)
            {
                if (nowValue % 1 == 0)
                {
                    break;
                }
                nowValue *= 10;
                decimalPoint++;
            }
            integer = (int)nowValue;
        }

        public void SetValue(BigInteger value, byte decimalPoint)
        {
            _value = value;
            _decimalPoint = decimalPoint;
        }



        private BigDecimal(BigInteger value, byte decimalPoint)
        {
            _value = value;
            _decimalPoint = decimalPoint;
        }

        private BigDecimal CorrectDecimalPoint(byte decimalPoint)
        {
            var data = new BigDecimal();
            byte gapDecimalPoint = 0;
            if (_decimalPoint > decimalPoint)
                gapDecimalPoint = (byte)(_decimalPoint - decimalPoint);
            else if (_decimalPoint < decimalPoint)
                gapDecimalPoint = (byte)(decimalPoint - _decimalPoint);

            int pow = (int)Mathf.Pow(10, gapDecimalPoint);
            data.SetValue(_value * pow, (byte)(_decimalPoint + gapDecimalPoint));
            return data;
        }


        public static BigDecimal operator +(BigDecimal a, BigDecimal b)
        {
            CorrectDecimalPoint(ref a, ref b);
            return new BigDecimal(a._value + b._value, a._decimalPoint);
        }
        public static BigDecimal operator -(BigDecimal a, BigDecimal b)
        {
            CorrectDecimalPoint(ref a, ref b);
            return new BigDecimal(a._value - b._value, a._decimalPoint);
        }

        public static BigDecimal operator *(BigDecimal a, BigDecimal b)
        {
            return new BigDecimal(BigInteger.Multiply(a._value, b._value), (byte)(a._decimalPoint + b._decimalPoint));
        }
        
        public static BigDecimal operator /(BigDecimal a, BigDecimal b)
        {
            if (b._decimalPoint == 0 && b._value == 0)
            {
                throw new System.DivideByZeroException();
            }

            //소숫점 자리수
            CorrectDecimalPoint(ref a, ref b);

            var result = new BigDecimal();
            var nowValue = a._value;
            var decimalPoint = 0;
            while (true)
            {
                result += new BigDecimal(nowValue / b._value, (byte)decimalPoint);
                var moduler = nowValue % b._value;

                if (moduler == 0 || decimalPoint >= COUNT_DECIMALPOINT)
                    break;

                decimalPoint++;
                nowValue = moduler * 10;
            }
            return result;
        }


        public static BigDecimal operator %(BigDecimal a, BigDecimal b)
        {
            CorrectDecimalPoint(ref a, ref b);
            return new BigDecimal(a._value % b._value, a._decimalPoint);
        }

        public static BigDecimal operator ++(BigDecimal value)
        {
            value._value++;
            return value;
        }
        public static BigDecimal operator --(BigDecimal value)
        {
            value._value--;
            return value;
        }

        //public static BigInteger operator &(BigInteger left, BigInteger right);
        //public static BigInteger operator |(BigInteger left, BigInteger right);
        //public static BigInteger operator ^(BigInteger left, BigInteger right);
        //public static BigInteger operator <<(BigInteger value, int shift);
        //public static BigInteger operator >>(BigInteger value, int shift);


        public static bool operator ==(BigDecimal a, BigDecimal b) => (a._value == b._value && a._decimalPoint == b._decimalPoint);
        public static bool operator !=(BigDecimal a, BigDecimal b) => (a._value != b._value || a._decimalPoint != b._decimalPoint);
        //public static bool operator >(BigDecimal a, BigDecimal b) => false;
        //public static bool operator <(BigDecimal a, BigDecimal b) => false;
        //public static bool operator >=(BigDecimal a, BigDecimal b) => false;
        //public static bool operator <=(BigDecimal a, BigDecimal b) => false;


//        public static implicit operator BigDecimal(long value);
//        public static implicit operator BigDecimal(int value);
//        public static implicit operator BigDecimal(short value);
//        public static implicit operator BigDecimal(float value);
//        public static implicit operator BigDecimal(double value);
//        public static implicit operator BigDecimal(decimal value);
        

        //public static explicit operator BigDecimal(decimal value) => new BigDecimal(value);
        //public static explicit operator BigDecimal(double value) => new BigDecimal(value);
        //public static explicit operator byte(BigDecimal value);
        //public static explicit operator decimal(BigDecimal value);
        //public static explicit operator double(BigDecimal value);
        //public static explicit operator short(BigDecimal value);
        //public static explicit operator long(BigDecimal value);
        //[CLSCompliant(false)]
        //public static explicit operator sbyte(BigDecimal value);
        //[CLSCompliant(false)]
        //public static explicit operator ushort(BigDecimal value);
        //[CLSCompliant(false)]
        //public static explicit operator uint(BigDecimal value);
        //[CLSCompliant(false)]
        //public static explicit operator ulong(BigDecimal value);
        //public static explicit operator BigDecimal(float value);
        //public static explicit operator int(BigDecimal value);
        //public static explicit operator float(BigDecimal value);


        private static void CorrectDecimalPoint(ref BigDecimal a, ref BigDecimal b)
        {
            if (b._decimalPoint > a._decimalPoint)
            {
                a = a.CorrectDecimalPoint(b._decimalPoint);
            }
            else if (a._decimalPoint > b._decimalPoint)
            {
                b = b.CorrectDecimalPoint(a._decimalPoint);
            }
        }

        public override bool Equals(object obj)
        {
            var bigDec = (BigDecimal)obj;
            return (_value == bigDec._value && _decimalPoint == bigDec._decimalPoint);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(BigDecimal other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(BigDecimal other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}