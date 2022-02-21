#if UNITY_EDITOR
namespace SEF.Data
{
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;
    using System.Linq;

    public class GoogleSheetToScriptableObject
    {


        private readonly static GSTU_Search gstuSearcher = new GSTU_Search("16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY", "Unit_Data");



        [MenuItem("Data/Create And Update All Units")]
        private static void CreateAndUpdateAllUnits()
        {
            CreateAndUpdate();
        }

        private static void CreateAndUpdate()
        {
            SpreadsheetManager.Read(gstuSearcher, OnCreateAndUpdateEvent);
        }


        [MenuItem("Data/Upload All Units")]
        private static void UploadAllUnits()
        {
            Upload();
        }

        private static void Upload()
        {
            SpreadsheetManager.Write(gstuSearcher, "", OnUploadEvent);
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

        private static void OnCreateAndUpdateEvent(GstuSpreadSheet sheet)
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
                        data.SetAssetBundle("data/units");
                        EditorUtility.SetDirty(data);
                        AssetDatabase.CreateAsset(data, $"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        data.SetAssetBundle("data/units");
                        EditorUtility.SetDirty(data);
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                    AssetDatabase.DeleteAsset($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
                }
            }
        }

        private static void OnUploadEvent()
        {
            Debug.Log("Uploaded Success");
        }
    }
}
#endif