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
        /// <br>복리 계산식</br>
        /// <br>소숫점 0.001까지 사용가능</br>
        /// <br>Pow를 두번 사용 O(a^n)</br>
        /// </summary>
        /// <param name="startValue">초기값</param>
        /// <param name="nowValue">증가량</param>
        /// <param name="rate">증가율</param>
        /// <param name="length">기간</param>
        /// <returns></returns>
        public static BigInteger GetCompoundInterest(BigInteger startValue, int nowValue = 1, float rate = 0.1f, int length = 1)
        {
            var exponent = length;
            var nv = nowValue * 1000;
            var rt = (int)UnityEngine.Mathf.Round(rate * 1000f);
            return startValue * BigInteger.Pow(nv + rt, exponent) / BigInteger.Pow(1000, exponent);
        }



    }
}