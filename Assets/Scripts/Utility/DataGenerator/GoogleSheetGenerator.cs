#if UNITY_EDITOR
namespace Utility.Generator
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using GoogleSheetsToUnity;
    using System.Linq;
    using Utility.Data;
    using LitJson;
    using System.Text;

    public class GoogleSheetGenerator
    {



        #region ##### CreateAndUpdate #####

        public static void CreateAndUpdateTextAsset(string sheetkey, string worksheet, string dataPath, string bundleName = null)
        {
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet);
            SpreadsheetManager.Read(gstuSearcher, sheet =>
            {
                OnCreateAndUpdateTextAssetEvent(sheet, worksheet, dataPath, bundleName, gstuSearcher.titleRow);
            });
        }


        private static void OnCreateAndUpdateTextAssetEvent(GstuSpreadSheet sheet, string worksheet, string dataPath, string bundleName, int startRow)
        {
            Dictionary<string, List<Dictionary<string, Dictionary<string, string>>>> _dic = new Dictionary<string, List<Dictionary<string, Dictionary<string, string>>>>(); 

            //헤더에 맞춰서 데이터 구조 제작


            string[] arr = new string[1];

            string tmpkey = "";




            //{"data":
            //    {"accupgradeunit" : Dictionary
            //		[{"values" : List, Dictionary
            //            {"Korean_Name : 0}, Dictionary
            //            { Korean_Description: 0},						
            //	  		  {"English_Name : 0}, 
            //            { English_Description: 0}
            //      }
            //		],
            //		[
            //      ...
            //      ]
            //    }
            //{



            ////첫줄은 헤더 및 키 라인
            ///Key_2 Kor Eng
            ////두번째 줄부터 데이터
            for (int c = startRow; c < sheet.RowCount + startRow; c++)
            {
                if (sheet.rows.ContainsKey(c))
                {
                    var row = sheet.rows[c];
                    var key = row[0].value; //0 = Header

                    if (!string.IsNullOrEmpty(key))
                    {
                        //첫라인이면 헤더 설정
                        if (c == startRow)
                        {
                            var split = key.Split('_');

                            //키라인을 제외하고 적용
                            arr = new string[row.Count - 1];
                            for(int i = 0; i < row.Count - 1; i++)
                            {
                                arr[i] = row[i + 1].value;
                            }
                        }
                        //두번째부터 데이터라인
                        else
                        {
                            //key가 같으면
                            if(tmpkey == key)
                            {
                                _dic[key].Add(new Dictionary<string, Dictionary<string, string>>());

                                _dic[key][_dic[key].Count - 1].Add("values", new Dictionary<string, string>());

                                for (int i = 0; i < row.Count - 1; i++)
                                {
                                    _dic[key][_dic[key].Count - 1]["values"].Add(arr[i], row[i + 1].value);
                                }
                            }
                            //key가 다르면
                            else
                            {
                                if (!_dic.ContainsKey(key)) 
                                {
                                    _dic.Add(key, new List<Dictionary<string, Dictionary<string, string>>>());
                                }

                                _dic[key].Add(new Dictionary<string, Dictionary<string, string>>());

                                _dic[key][_dic[key].Count - 1].Add("values", new Dictionary<string, string>());

                                for (int i = 0; i < row.Count - 1; i++)
                                {
                                    _dic[key][_dic[key].Count - 1]["values"].Add(arr[i], row[i + 1].value);
                                }
                            }
                        }
                    }
                }
            }
            //AssetDatabase.WriteTextAsset();

            Debug.Log(JsonMapper.ToJson(_dic));

            TextAsset textAsset = new TextAsset(JsonMapper.ToJson(_dic));

            System.IO.File.WriteAllText($"{dataPath}/{worksheet}.txt", textAsset.text);

            //바로 적용되지 않음
            UnityEditor.AssetImporter importer = UnityEditor.AssetImporter.GetAtPath($"{dataPath}/{worksheet}.txt");
            importer.SetAssetBundleNameAndVariant(bundleName, "");
            AssetDatabase.SaveAssets();


            Debug.Log("Create And Update TextAsset End");
        }



        /// <summary>
        /// 생성 및 갱신
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataPath"></param>
        /// <param name="bundleName"></param>
        public static void CreateAndUpdateAllData<T>(string sheetkey, string worksheet, string dataPath, string bundleName = null) where T : ScriptableObjectData
        {
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet, "A2", 2);
            SpreadsheetManager.Read(gstuSearcher, sheet =>
            { 
                OnCreateAndUpdateEvent<T>(sheet, dataPath, bundleName, gstuSearcher.titleRow); 
            });
        }

        private static void OnCreateAndUpdateEvent<T>(GstuSpreadSheet sheet, string dataPath, string bundleName, int startRow) where T : ScriptableObjectData
        {
            T tmpData = null;
            int index = 0;
            for (int c = startRow; c < sheet.RowCount + startRow; c++, index++)
            {

                if (sheet.rows.ContainsKey(c))
                {
                    var row = sheet.rows[c];
                    var key = row[0].value; //0 = Key

                    if (!string.IsNullOrEmpty(key))
                    {
                        //연계 퀘스트 - 키가 같음
                        if (tmpData != null && tmpData.Key == key)
                        {
                            tmpData.AddData(row.Select(cell => cell.value).ToArray());
                            EditorUtility.SetDirty(tmpData);
                        }
                        else
                        {
                            tmpData = null;
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

                            tmpData = data;
                        }
                        //}
                        //catch (System.Exception e)
                        //{
                        //    Debug.LogWarning(e.Message);
                        //    AssetDatabase.DeleteAsset($"{dataPath}/{typeof(T).Name}_{key}.asset");
                        //}
                    }
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
        public static void UploadAllData<T>(string sheetkey, string worksheet, string dataPath, UnityEngine.Events.UnityAction endCallback = null) where T : ScriptableObjectData
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
                if (data != null && !data.IsTest) list.Add(data);
            }
            list.Sort((d1, d2) => d1.SortIndex - d2.SortIndex);

            var saveList = new List<List<string>>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].HasDataArray())
                {
                    var arr = list[i].GetDataArray();

                    for(int j = 0; j < arr.Length; j++)
                    {
                        saveList.Add(new List<string>(arr[j]));
                    }
                }
                else
                {
                    saveList.Add(new List<string>(list[i].GetData()));
                }
            }
            GSTU_Search gstuSearcher = new GSTU_Search(sheetkey, worksheet, "A2");
            SpreadsheetManager.Write(gstuSearcher, new ValueRange(saveList), endCallback);
        }

        private class T
        {
        }

        #endregion



    }
}
#endif

