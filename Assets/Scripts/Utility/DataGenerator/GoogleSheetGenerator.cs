#if UNITY_EDITOR
namespace Utility.Generator
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;
    using System.Linq;
    using Utility.Data;

    public class GoogleSheetGenerator
    {
        private readonly static GSTU_Search gstuSearcher = new GSTU_Search("16Ps885lj_8ZSY6Gv_WQgehpCxusaz2l9J50JBXKq2DY", "Unit_Data", "A2", 2);



        //[MenuItem("Data/Create And Update All Units")]
        //private static void CreateAndUpdateAllUnits()
        //{
        //    CreateAndUpdate();
        //}

        //private static void CreateAndUpdate()
        //{
        //    SpreadsheetManager.Read(gstuSearcher, OnCreateAndUpdateEvent);
        //}


        //[MenuItem("Data/Upload All Units")]
        //private static void UploadAllUnits()
        //{
        //    Upload();
        //}

        //private static void OnCreateAndUpdateEvent(GstuSpreadSheet sheet)
        //{
        //    int index = 0;
        //    for (int c = gstuSearcher.titleRow; c < sheet.RowCount + gstuSearcher.titleRow; c++, index++)
        //    {
        //        var row = sheet.rows[c];
        //        var key = row[0].value;
        //        try
        //        {
        //            var data = AssetDatabase.LoadAssetAtPath<UnitData>($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");

        //            if (data == null)
        //            {
        //                data = ScriptableObject.CreateInstance<UnitData>();
        //                data.SetData(index, row.Select(cell => cell.value).ToArray());
        //                data.SetAssetBundle("data/units");
        //                EditorUtility.SetDirty(data);
        //                AssetDatabase.CreateAsset(data, $"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
        //                AssetDatabase.SaveAssets();
        //            }
        //            else
        //            {
        //                data.SetData(index, row.Select(cell => cell.value).ToArray());
        //                data.SetAssetBundle("data/units");
        //                EditorUtility.SetDirty(data);
        //            }
        //        }
        //        catch(System.Exception e)
        //        {
        //            Debug.LogWarning(e.Message);
        //            AssetDatabase.DeleteAsset($"Assets/Data/Units/{typeof(UnitData).Name}_{key}.asset");
        //        }
        //    }
        //}



        #region ##### CreateAndUpdate #####

        /// <summary>
        /// 생성 및 갱신
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataPath"></param>
        /// <param name="bundleName"></param>
        public static void CreateAndUpdateAllUnits<T>(string sheetkey, string worksheet, string dataPath, string bundleName = null) where T : ScriptableObjectData
        {
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet, "A2", 2);
            SpreadsheetManager.Read(gstuSearcher, sheet =>
            { 
                OnCreateAndUpdateEvent<T>(sheet, dataPath, bundleName); 
            });
        }

        private static void OnCreateAndUpdateEvent<T>(GstuSpreadSheet sheet, string dataPath, string bundleName) where T : ScriptableObjectData
        {
            int index = 0;
            for (int c = gstuSearcher.titleRow; c < sheet.RowCount + gstuSearcher.titleRow; c++, index++)
            {
                var row = sheet.rows[c];
                var key = row[0].value; //0 = Key
                try
                {
                    var data = AssetDatabase.LoadAssetAtPath<T>($"{dataPath}/{typeof(T).Name}_{key}.asset");

                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<T>();
                        data.SetSortIndex(index);
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        data.SetAssetBundle(bundleName);
                        EditorUtility.SetDirty(data);
                        AssetDatabase.CreateAsset(data, $"{dataPath}/{typeof(T).Name}_{key}.asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        data.SetSortIndex(index);
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        data.SetAssetBundle(bundleName);
                        EditorUtility.SetDirty(data);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                    AssetDatabase.DeleteAsset($"{dataPath}/{typeof(T).Name}_{key}.asset");
                }
            }
        }

        #endregion



        #region ##### Upload #####


        /// <summary>
        /// 업로드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataPath"></param>
        public static void UploadAllUnits<T>(string sheetkey, string worksheet, string dataPath, UnityEngine.Events.UnityAction endCallback = null) where T : ScriptableObjectData
        {
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet);
            Upload<T>(dataPath, endCallback);
        }


        private static void Upload<T>(string dataPath, UnityEngine.Events.UnityAction endCallback = null) where T : ScriptableObjectData
        {
            var list = new List<T>();
            var files = System.IO.Directory.GetFiles(dataPath);
            for (int i = 0; i < files.Length; i++)
            {
                var data = AssetDatabase.LoadAssetAtPath<T>(files[i]);
                if (data != null) list.Add(data);
            }
            list.Sort((d1, d2) => d1.SortIndex - d2.SortIndex);

            var saveList = new List<List<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                saveList.Add(new List<string>(list[i].GetData()));
            }
            SpreadsheetManager.Write(gstuSearcher, new ValueRange(saveList), endCallback);
        }

        #endregion



        //private static void Upload()
        //{
        //    var list = new List<UnitData>();
        //    var files = System.IO.Directory.GetFiles("Assets/Data/Units");
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        var data = AssetDatabase.LoadAssetAtPath<UnitData>(files[i]);
        //        if (data != null) list.Add(data);
        //    }
        //    list.Sort((d1, d2) => d1.SortIndex - d2.SortIndex);



        //    var saveList = new List<List<string>>();
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        //Debug.Log(list[i].Index);
        //        saveList.Add(new List<string>(list[i].GetData()));
        //    }

        //    SpreadsheetManager.Write(gstuSearcher, new ValueRange(saveList), OnUploadedEvent);
        //}

        //private static void OnUploadedEvent()
        //{
        //    Debug.Log("Uploaded End");
        //}
    }
}
#endif