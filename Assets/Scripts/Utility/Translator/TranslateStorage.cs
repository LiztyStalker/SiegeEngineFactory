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
                Debug.Log(arr[i].name);
                _dic.Add(arr[i].name, JsonMapper.ToObject(arr[i].text));
            }
        }

        public string GetTranslateData(string title, string key, string verb = null, int index = 0)
        {
            if (_dic.ContainsKey(title))
            {
                //Debug.Log(title);
                var dicTitle = _dic[title];
                if (dicTitle.ContainsKey(key))
                {
                    //Debug.Log(key);
                    var dicKey = dicTitle[key];
                    if (dicKey.IsArray)
                    {
                        //Debug.Log(index);
                        var dicValues = dicKey[index]["values"];
                        //verb = "Language_Verb" Gamesettings - CurrentLanguage
                        if (dicValues.ContainsKey(verb))
                        {
                            //Debug.Log(verb);
                            return dicValues[verb].ToString();
                        }
                    }
                }
            }
#if UNITY_EDITOR
            return "-";
#else
            return null;
#endif
        }
        public static void Dispose()
        {
            _instance = null;
        }


        #region ##### Listener #####

        private System.Action _changedTranslateEvent;
        public void AddOnChangedTranslateListener(System.Action act) => _changedTranslateEvent += act;
        public void RemoveOnChangedTranslateListener(System.Action act) => _changedTranslateEvent -= act;
        private void OnChangedTranslateEvent()
        {
            _changedTranslateEvent?.Invoke();
        }

        #endregion
    }
}