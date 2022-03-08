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
            return value;
        }

        /// <summary>
        /// �ܸ� ����
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="nowValue"></param>
        /// <param name="rate"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        //public static BigDecimal GetIsolationInterest(BigDecimal startValue, float nowValue = 1, int length = 1)
        //{
        //    var value = startValue + nowValue * length;
        //    return value;
        //}


        /// <summary>
        /// �ܸ� ����
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="increaseValue"></param>
        /// <param name="increaseRate"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static BigDecimal GetIsolationInterest(BigDecimal startValue, float increaseValue = 1, float increaseRate = 0.1f, int length = 1)
        {
            var value = startValue + increaseValue * (float)length;
            value += value * increaseRate * (float)length;
            return value;
        }

        /// <summary>
        /// �ܸ� ����
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="increaseValue"></param>
        /// <param name="increaseRate"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float GetIsolationInterest(float startValue, float increaseValue = 1, float increaseRate = 0.1f, int length = 1)
        {
            var value = startValue + increaseValue * (float)length;
            value += value * increaseRate * (float)length;
            return value;
        }
    }
}