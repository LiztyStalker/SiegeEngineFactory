namespace Utility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    public struct BigDecimal
    {
        private BigInteger _value;

        private byte _decimalPoint;


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

        public decimal GetDecimalValue()
        {
            return ((decimal)_value) * (decimal)Mathf.Pow(0.1f, _decimalPoint);
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
            if (a._decimalPoint > b._decimalPoint)
            {
                b = b.CorrectDecimalPoint(a._decimalPoint);
            }
            else if (a._decimalPoint < b._decimalPoint)
            {
                a = a.CorrectDecimalPoint(b._decimalPoint);
            }

            return new BigDecimal(a._value + b._value, a._decimalPoint);
        }
        public static BigDecimal operator -(BigDecimal a, BigDecimal b)
        {
            if (a._decimalPoint > b._decimalPoint)
            {
                b = b.CorrectDecimalPoint(a._decimalPoint);
            }
            else if (a._decimalPoint < b._decimalPoint)
            {
                a = a.CorrectDecimalPoint(b._decimalPoint);
            }
            return new BigDecimal(a._value - b._value, a._decimalPoint);
        }

        public static BigDecimal operator *(BigDecimal a, BigDecimal b)
        {
            return new BigDecimal(System.Numerics.BigInteger.Multiply(a._value, b._value), (byte)(a._decimalPoint + b._decimalPoint));
        }


        /// <summary>
        /// °³¹ßÁß
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigDecimal operator /(BigDecimal a, BigDecimal b)
        {
            if (b._decimalPoint == 0 && b._value == 0)
            {
                throw new System.DivideByZeroException();
            }

            if (b._decimalPoint > a._decimalPoint)
            {
                a = a.CorrectDecimalPoint(b._decimalPoint);
            }
            return new BigDecimal(System.Numerics.BigInteger.Divide(a._value, b._value), a._decimalPoint);
        }


        public static BigDecimal operator %(BigDecimal a, BigDecimal b)
        {

            return new BigDecimal(a._value - b._value, a._decimalPoint);
        }
    }
}