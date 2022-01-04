namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Numerics;

    [System.Serializable]
    public class GoldAssetData : BigNumberData, IAssetData
    {
        public BigInteger AssetValue { get => Value; set => Value = value; }

        public override INumberData Clone()
        {
            return new GoldAssetData();
        }

        public void SetAssetData(UnitData unitData, UpgradeData upgradeData)
        {
            var upgradeValue = upgradeData.Value - 1;
            var increaseUpgradeAssetValue = unitData.IncreaseUpgradeAssetValue.Value;
            var increaseUpgradeAssetRate = Mathf.RoundToInt(unitData.IncreaseUpgradeAssetRate * 100f);
            //Value = unitData.StartUpgradeAsset.Value * Pow(increaseUpgradeAssetValue + increaseUpgradeAssetRate, upgradeValue); // 복리
            Value = unitData.StartUpgradeAsset.Value + (increaseUpgradeAssetValue * upgradeValue) + (increaseUpgradeAssetValue * upgradeValue * increaseUpgradeAssetRate / 100); //단리
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static GoldAssetData Create_Test() 
        {
            var data = new GoldAssetData();
            data.ValueText = "100";
            return data;
        }

        public static GoldAssetData Create_Test(int value)
        {
            var data = new GoldAssetData();
            data.ValueText = value.ToString();
            return data;
        }
#endif

    }


}