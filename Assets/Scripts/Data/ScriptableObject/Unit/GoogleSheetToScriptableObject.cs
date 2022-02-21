#if UNITY_EDITOR
namespace SEF.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;
    using System.Linq;

    public class GoogleSheetToScriptableObject
    {


        private readonly static GSTU_Search gstuSearcher = new GSTU_Search("16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY", "Unit_Data", "A2", 2);



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
            int index = 0;
            for (int c = gstuSearcher.titleRow; c < sheet.RowCount + gstuSearcher.titleRow; c++, index++)
            {
                var row = sheet.rows[c];
                var key = row[0].value;
                try
                {
                    var data = AssetDatabase.LoadAssetAtPath<UnitData>($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");

                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<UnitData>();
                        data.SetData(index, row.Select(cell => cell.value).ToArray());
                        data.SetAssetBundle("data/units");
                        EditorUtility.SetDirty(data);
                        AssetDatabase.CreateAsset(data, $"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        data.SetData(index, row.Select(cell => cell.value).ToArray());
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

        private static void Upload()
        {

            var list = new List<UnitData>();
            var files = System.IO.Directory.GetFiles("Assets/Data/Units");
            for (int i = 0; i < files.Length; i++)
            {
                var data = AssetDatabase.LoadAssetAtPath<UnitData>(files[i]);
                if (data != null) list.Add(data);
            }
            list.Sort((d1, d2) => d1.Index - d2.Index);



            var saveList = new List<List<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                //Debug.Log(list[i].Index);
                saveList.Add(new List<string>(list[i].GetData()));
            }

            SpreadsheetManager.Write(gstuSearcher, new ValueRange(saveList), OnUploadedEvent);
        }

        private static void OnUploadedEvent()
        {
            Debug.Log("Uploaded End");
        }
    }
}
#endif