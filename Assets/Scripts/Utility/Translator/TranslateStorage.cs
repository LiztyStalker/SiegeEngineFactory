namespace Storage
{
    using UnityEngine;
    using LitJson;
    using System.Collections.Generic;

    public class TranslateStorage
    {
        private static TranslateStorage _instance;

        private Dictionary<string, JsonData> _dic;

        public static TranslateStorage Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new TranslateStorage();
                }
                return _instance;
            }
        }

        private TranslateStorage() 
        {
            _dic = new Dictionary<string, JsonData>();
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<TextAsset>();

            for(int i = 0; i < arr.Length; i++)
            {
                _dic.Add(arr[i].name, JsonMapper.ToObject(arr[i].text));
            }
        }

        public string GetTranslateData(string title, string key, string verb, int index = 0)
        {
            if (_dic.ContainsKey(title))
            {
                var dicTitle = _dic[title];
                if (dicTitle.ContainsKey(key))
                {
                    var dicKey = dicTitle[key];
                    if (dicKey.IsArray)
                    {
                        var dicValues = dicKey[index]["values"];
                        //verb = "Language_Verb"
                        if (dicValues.ContainsKey(verb))
                        {
                            //Debug.Log(verb);
                            return dicValues[verb].ToString();
                        }
                    }
                }
            }
            return null;
        }
        public static void Dispose()
        {
            _instance = null;
        }
    }
}