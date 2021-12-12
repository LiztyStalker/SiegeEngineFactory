namespace Storage
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using System.IO;

    [ExecuteAlways]
    public class DataStorage
    {
        [ExecuteAlways]
        private static DataStorage _instance = null;

        public static DataStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataStorage();
                return _instance;
            }
        }

        private Dictionary<string, Dictionary<string, Object>> _dataDic = new Dictionary<string, Dictionary<string, Object>>();

        private DataStorage()
        {
            InitializeDataFromAssetBundle<TextAsset>();

            //InitializeDataFromAssetBundle<SkeletonDataAsset>("spine", "data");
            InitializeDataFromAssetBundle<AudioClip>("sfx", "sound");
            InitializeDataFromAssetBundle<AudioClip>("bgm", "sound");
            InitializeDataFromAssetBundle<Sprite>("unit", "sprite");
            InitializeDataFromAssetBundle<Sprite>("type", "sprite");

            //InitializeDataFromAssetBundle<TribeData>("data");
            //InitializeDataFromAssetBundle<BulletData>("data");
            //InitializeDataFromAssetBundle<EffectData>("data");
            //InitializeDataFromAssetBundle<SkillData>("data");
            //InitializeDataFromAssetBundle<StatusData>("data");
            //InitializeDataFromAssetBundle<UnitData>("data");
            //InitializeDataFromAssetBundle<CommanderData>("data");
            //InitializeDataFromAssetBundle<BattleFieldData>("data");

            InitializeDataFromAssetBundle<GameObject>("prefab", null);
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static void Initialize(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            var loader = DataLoader.Create();
            loader.LoadTest(loadCallback, result => 
            {
                endCallback?.Invoke(result);
                loader.Dispose();
            });
        }
#else
        public static void Initialize(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            var loader = DataLoader.Create();
            loader.Load(loadCallback, result => 
            {
                endCallback?.Invoke(result);
                loader.Dispose();
            });
        }
#endif

        public static void Dispose()
        {
            _instance = null;
        }

        private void InitializeDataFromAssetBundle<T>(string path, string directory = null) where T : Object
        {
            string bundlePath = path;
            if (directory != null)
            {
                bundlePath = Path.Combine(directory, path);
            }

            bundlePath = Path.Combine(Application.streamingAssetsPath, bundlePath);

            var assetbundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundlePath));
            if (assetbundle == null)
            {
                Debug.LogError($"{bundlePath} AssetBundle�� ã�� �� �����ϴ�");
                return;
            }

            var files = assetbundle.LoadAllAssets<T>();
            for (int i = 0; i < files.Length; i++)
            {
                var data = files[i];
                //Debug.Log(files[i]);
                if (data != null)
                {
                    AddDirectoryInData(data.name, data);
                }
            }
            assetbundle.Unload(false);
        }


        /// <summary>
        /// ������ �ʱ�ȭ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        private void InitializeDataFromAssetBundle<T>(string directory = null) where T : Object
        {
            InitializeDataFromAssetBundle<T>(typeof(T).Name.ToLower(), directory);
        }



#if UNITY_EDITOR
        public T[] GetDataArrayFromAssetDatabase<T>(string path) where T : Object
        {
            var list = new List<T>();
            var files = System.IO.Directory.GetFiles($"Assets/{path}");
            for (int j = 0; j < files.Length; j++)
            {
                var data = AssetDatabase.LoadAssetAtPath<T>(files[j]);
                //Debug.Log(files[j]);
                if (data != null)
                {
                    list.Add(data);
                }
            }
            return list.ToArray();
        }

        public T GetDataFromAssetDatabase<T>(string path) where T : Object
        {
            var data = AssetDatabase.LoadAssetAtPath<T>($"Assets/{path}");
            return data;

        }
#endif

        /// <summary>
        /// ��� �����͸� �����ɴϴ�
        /// ������ 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] GetAllDataArrayOrZero<T>() where T : Object
        {
            List<T> list = new List<T>();
            if (IsHasDataType<T>())
            {
                foreach (var data in _dataDic[ToTypeString<T>()].Values)
                {
                    list.Add((T)data);
                }
            }
            return list.ToArray();
        }

        public static string ToTypeString<T>() => typeof(T).Name.ToString();


        /// <summary>
        /// ������ �������� 
        /// ex) GetDataOrNull<UnitData>() => return [UnitData_Data]
        /// ������ null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetDataOrNull<T>(string key) where T : Object => GetDataOrNull<T>(key, ToTypeString<T>(), null);

        /// <summary>
        /// ������ ��������
        /// ������ null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetDataOrNull<T>(string key, string firstVerb, string lastVerb) where T : Object
        {
            if (IsHasDataType<T>())
            {
                var dic = _dataDic[ToTypeString<T>()];
                var cKey = GetConvertKey(key, firstVerb, lastVerb);
                //Debug.Log(ToTypeString<T>() + " " + cKey);
                return GetDataOrNull<T>(dic, cKey);
            }
            return null;
        }

        /// <summary>
        /// ù ������ ��������
        /// ������ null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="firstVerb"></param>
        /// <param name="lastVerb"></param>
        /// <returns></returns>
        public T GetFirstDataOrNull<T>() where T : Object
        {
            var arr = GetAllDataArrayOrZero<T>();
            if (arr.Length > 0)
                return arr[0];
            return null;
        }

        /// <summary>
        /// ������ ������ ��������
        /// ������ null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="firstVerb"></param>
        /// <param name="lastVerb"></param>
        /// <returns></returns>
        public T GetLastDataOrNull<T>() where T : Object
        {
            var arr = GetAllDataArrayOrZero<T>();
            if (arr.Length > 0)
                return arr[arr.Length - 1];
            return null;
        }



        /// <summary>
        /// �����͸���Ʈ �������� 
        /// ex) GetDataArrayOrZero<UnitData>() => return [UnitData_Data]
        /// ������ 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public T[] GetDataArrayOrZero<T>(string[] keys) where T : Object => GetDataArrayOrZero<T>(keys, ToTypeString<T>(), null);

        /// <summary>
        /// ������ ����Ʈ ��������
        /// ������ 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public T[] GetDataArrayOrZero<T>(string[] keys, string firstVerb, string lastVerb) where T : Object
        {
            List<T> list = new List<T>();
            if (keys != null && keys.Length > 0)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (IsHasDataType<T>())
                    {
                        var dic = _dataDic[ToTypeString<T>()];
                        if (dic.ContainsKey(GetConvertKey(keys[i], ToTypeString<T>())))
                        {
                            var cKey = GetConvertKey(keys[i], firstVerb, lastVerb);
                            var data = GetDataOrNull<T>(dic, cKey);
                            if (data != null)
                            {
                                list.Add(data);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// ������ ���� ��������
        /// ������ 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        public T[] GetRandomDatasOrZero<T>(int count) where T : Object
        {
            if (count <= 0)
            {
                Debug.LogWarning($"������ ������ 0 ���ϰ� �� �� �����ϴ�. 1�� ������ �� �����մϴ�");
                count = 1;
            }

            var dataArray = GetAllDataArrayOrZero<T>();
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(dataArray[Random.Range(0, dataArray.Length)]);
            }
            return list.ToArray();
        }



        /// <summary>
        /// �����Ͱ� �ִ��� Ȯ��
        /// ������ true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsHasData<T>(string key) where T : Object => IsHasData<T>(key, ToTypeString<T>(), null);


        /// <summary>
        /// �����Ͱ� �ִ��� Ȯ��
        /// ������ true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsHasData<T>(string key, string frontVerb, string lastVerb) where T : Object
        {
            if (IsHasDataType<T>())
            {
                var dic = _dataDic[ToTypeString<T>()];
                var cKey = GetConvertKey(key, frontVerb, lastVerb);
                return dic.ContainsKey(cKey);
            }
            return false;
        }




        private int GetDataCount<T>() where T : Object
        {
            if (IsHasDataType<T>())
            {
                return _dataDic[ToTypeString<T>()].Count;
            }
            return 0;
        }

        private bool IsHasDataType<T>() where T : Object => _dataDic.ContainsKey(ToTypeString<T>());
        private T GetDataOrNull<T>(Dictionary<string, Object> dic, string key) where T : Object
        {
            if (dic.ContainsKey(key))
            {
                //Debug.Log("GetDataOrNull " + key);
                //Debug.Log("GetDataOrNull " + (T)dic[key]);
                return (T)dic[key];
            }
            return null;
        }


        private void AddDirectoryInData<T>(string key, T data) where T : Object
        {
            if (!IsHasDataType<T>())
                _dataDic.Add(ToTypeString<T>(), new Dictionary<string, Object>());

            if (!IsHasData<T>(key))
                _dataDic[ToTypeString<T>()].Add(key, data);
        }


        private string GetConvertKey(string key, string frontVerb = null, string backVerb = null)
        {
            if (frontVerb != null) frontVerb += "_";
            if (backVerb != null) backVerb = "_" + backVerb;
            return $"{frontVerb}{key}{backVerb}";
        }



#if UNITY_EDITOR
        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
#endif
    }
}