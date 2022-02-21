namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;

    public class GoogleSheetToScriptableObject
    {
        [MenuItem("Data/Unit All Update")]
        private static void Update()
        {
            UpdateData();
        }

        private static void UpdateData()
        {
            GSTU_Search search = new GSTU_Search("16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY", "Unit_Data");
            SpreadsheetManager.Read(search, Callback);
        }

        private enum TYPE_UNIT_SHEET_COLUMNS { 
            Key, 
            Group, 
            HealthValue, 
            IncreaseHealthValue,
            IncreaseHealthRate,
            ProductTime,
            AttackValue,
            IncreaseAttackValue,
            IncreaseAttackRate,
            TypeAttackRange,
            TypeAttackAction,
            AttackPopulation,
            AttackCount,
            AttackDelay,
            BulletDataKey,
            BulletScale,
            AttackDataKeys,
            StartUpgradeAsset,
            IncreaseUpgradeAssetValue,
            IncreaseUpgradeAssetRate,
            ConditionTechTree,
            ConditionTechTreeValue,
            TechTreeAsset,
            TechTreeKeys
        }

        private static void Callback(GstuSpreadSheet sheet)
        {
            for (int c = 1; c < sheet.RowCount + 1; c++)
            {
                var row = sheet.rows[c];
                for (int r = 0; r < row.Count; r++) {
                    Debug.Log(sheet.rows[c][r].value);
                }
            }
        }
    }
}