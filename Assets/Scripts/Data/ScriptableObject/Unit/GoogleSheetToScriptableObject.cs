#if UNITY_EDITOR
namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;
    using System.Linq;

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

        public enum TYPE_UNIT_SHEET_COLUMNS { 
            Key, 
            Group, 
            StartHealthValue, 
            IncreaseHealthValue,
            IncreaseHealthRate,
            ProductTime,
            StartDamageValue,
            IncreaseDamageValue,
            IncreaseDamageRate,
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
            //1 = Title
            for (int c = 2; c < sheet.RowCount + 1; c++)
            {
                var row = sheet.rows[c];
                var key = row[0].value;
                try
                {
                    var data = AssetDatabase.LoadAssetAtPath<UnitData>($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");

                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<UnitData>();
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        EditorUtility.SetDirty(data);
                        AssetDatabase.CreateAsset(data, $"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        EditorUtility.SetDirty(data);
                        AssetDatabase.SaveAssets();
                    }
                }
                catch(System.Exception e)
                {
                    Debug.Log(e.Message);
                    AssetDatabase.DeleteAsset($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
                }
            }
        }
    }
}
#endif