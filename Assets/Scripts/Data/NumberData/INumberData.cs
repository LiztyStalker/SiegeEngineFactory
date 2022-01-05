namespace SEF.Data
{
    using UnityEngine;
    using System.Numerics;

    public interface INumberData
    {
        string GetValue();
        INumberData Clone();
        void CleanUp();
    }

    public class NumberDataUtility
    {
        public static T Create<T>() where T : INumberData
        {
            return System.Activator.CreateInstance<T>();
        }

        public static T CreateAssetData<T>() where T : IAssetData
        {
            return System.Activator.CreateInstance<T>();
        }



        /// <summary>
        /// <br>���� ����</br>
        /// <br>�Ҽ��� 0.01���� ��밡��</br>
        /// <br>Pow�� �ι� ��� O(a^n)</br>
        /// </summary>
        /// <param name="startValue">�ʱⰪ</param>
        /// <param name="nowValue">������</param>
        /// <param name="rate">������</param>
        /// <param name="length">�Ⱓ</param>
        /// <returns></returns>
        public static BigInteger GetCompoundInterest(BigInteger startValue, int nowValue = 1, float rate = 0.1f, int length = 1)
        {
            var exponent = length;
            var nv = nowValue * 100;
            var rt = (int)UnityEngine.Mathf.Round(rate * 100f);
            return startValue * BigInteger.Pow(nv + rt, exponent) / BigInteger.Pow(100, exponent);
        }



    }
}