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
                OnCreateAndUpdateEvent<T>(sheet, dataPath, bundleName, gstuSearcher.titleRow); 
            });
        }

        private static void OnCreateAndUpdateEvent<T>(GstuSpreadSheet sheet, string dataPath, string bundleName, int startRow) where T : ScriptableObjectData
        {
            int index = 0;
            for (int c = startRow; c < sheet.RowCount + startRow; c++, index++)
            {
                var row = sheet.rows[c];
                var key = row[0].value; //0 = Key
                if (!string.IsNullOrEmpty(key))
                {
                    //try
                    //{
                    var data = AssetDatabase.LoadAssetAtPath<T>($"{dataPath}/{typeof(T).Name}_{key}.asset");

                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<T>();
                        data.SetSortIndex(index);
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        AssetDatabase.CreateAsset(data, $"{dataPath}/{typeof(T).Name}_{key}.asset");
                        data.SetAssetBundle(bundleName);
                        EditorUtility.SetDirty(data);
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        data.SetSortIndex(index);
                        data.SetData(row.Select(cell => cell.value).ToArray());
                        data.SetAssetBundle(bundleName);
                        EditorUtility.SetDirty(data);
                    }
                    //}
                    //catch (System.Exception e)
                    //{
                    //    Debug.LogWarning(e.Message);
                    //    AssetDatabase.DeleteAsset($"{dataPath}/{typeof(T).Name}_{key}.asset");
                    //}
                }
            }
            Debug.Log("Create And Update End");
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
            Upload<T>(sheetkey, worksheet, dataPath, endCallback);
        }

        private static void Upload<T>(string sheetkey, string worksheet, string dataPath, UnityEngine.Events.UnityAction endCallback = null) where T : ScriptableObjectData
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
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet, "A2");
            SpreadsheetManager.Write(gstuSearcher, new ValueRange(saveList), endCallback);
        }

        #endregion



    }
}
#endif