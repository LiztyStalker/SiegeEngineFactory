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

        public static INumberData Create(System.Type type)
        {
            return (INumberData)System.Activator.CreateInstance(type);
        }

        public static IAssetData Create(AssetDataStorableData data)
        {
            var type = System.Type.GetType(data.Type);
            if(type != null)
            {
                var assetData = (IAssetData)System.Activator.CreateInstance(type);
                assetData.SetValue(data.Value);
                return assetData;
            }
            return null;
        }


        /// <summary>
        /// <br>���� ����</br>
        /// <br>�Ҽ��� 0.001���� ��밡��</br>
        /// <br>Pow�� �ι� ��� O(a^n)</br>
        /// </summary>
        /// <param name="startValue">�ʱⰪ</param>
        /// <param name="nowValue">������</param>
        /// <param name="rate">������</param>
        /// <param name="length">�Ⱓ</param>
        /// <returns></returns>
        //public static BigInteger GetCompoundInterest(BigInteger startValue, int nowValue = 1, float rate = 0.1f, int length = 1)
        //{
        //    var exponent = length;
        //    var nv = nowValue * 1000;
        //    var rt = (int)UnityEngine.Mathf.Round(rate * 1000f);
        //    return startValue * BigInteger.Pow(nv + rt, exponent) / BigInteger.Pow(1000, exponent);
        //}



        /// <summary>
        /// <br>���� ����</br>
        /// </summary>
        /// <param name="startValue">�ʱⰪ</param>
        /// <param name="nowValue">������</param>
        /// <param name="rate">������</param>
        /// <param name="length">�Ⱓ</param>
        /// <returns></returns>
        public static BigDecimal GetCompoundInterest(BigDecimal startValue, float nowValue = 1, float rate = 0.1f, int length = 1)
        {
            var exponent = length;
            var nv = nowValue;
            var rt = rate;
            var value = startValue * BigDecimal.Pow(nv + rt, exponent);
            //Debug.Log(value.GetDecimalValue());
            return value;
        }

    }
}