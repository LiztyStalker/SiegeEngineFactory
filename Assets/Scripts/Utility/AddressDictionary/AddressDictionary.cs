namespace Utility.Address
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class AddressDictionary
    {
        private Dictionary<string, System.Action> _dic;


        public static AddressDictionary Create()
        {
            return new AddressDictionary();
        }

        public void Initialize()
        {
            _dic = new Dictionary<string, System.Action>();
        }

        public void CleanUp()
        {
            _dic.Clear();
            _dic = null;
        }

        public void AddAddresses(KeyValuePair<string, System.Action>[] pairs)
        {
            for(int i = 0; i < pairs.Length; i++)
            {
                AddAddress(pairs[i].Key, pairs[i].Value);
            }
        }

        public void AddAddress(string key, System.Action value)
        {
            if (!_dic.ContainsKey(key))
                _dic.Add(key, value);
#if UNITY_EDITOR
            else
                Debug.LogWarning($"{key} 가 이미 등록되어 있습니다");
#endif
        }

        public void RemoveAddress(string key)
        {
            if (_dic.ContainsKey(key))
                _dic.Remove(key);
        }

        public void ShowAddress(string key)
        {
            if (_dic.ContainsKey(key))
                _dic[key]?.Invoke();
        }

    }
}