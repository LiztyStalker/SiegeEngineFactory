namespace SEF.Manager
{
    using SEF.Account;
    using SEF.Data;
    using SEF.Entity;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Storage;
    using SEF.Process;
    using Utility.IO;


    #region ##### Serialize #####

    [System.Serializable]
    public class VillageManagerStorableData : StorableData
    {
        public void SetData(StorableData[] children)
        {
            Children = children;
        }
    }

    #endregion

    public class VillageManager : MonoBehaviour
    {

        private List<VillageLine> _list;

        public static VillageManager Create()
        {
            return new VillageManager();
        }

        public void Initialize()
        {
            _list = new List<VillageLine>();
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<VillageData>();

            for (int i = 0; i < arr.Length; i++)
            {
                var line = CreateLine();
                line.SetData(arr[i]);
            }
        }

        public void CleanUp()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].CleanUp();
            }
            _list.Clear();
        }

        public void Refresh()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Refresh();
            }
        }

        public IAssetData Upgrade(int index)
        {
            return _list[index].Upgrade();
        }

        public IAssetData UpTech(int index)
        {
            return _list[index].UpTech();
        }

        private VillageLine CreateLine()
        {
            var line = VillageLine.Create();
            line.Initialize();
            line.SetIndex(_list.Count);
            line.SetOnRefreshListener(OnRefreshEvent);
            _list.Add(line);
            return line;
        }


        #region ##### Listener #####


        private System.Action<int, VillageEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<int, VillageEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<int, VillageEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, VillageEntity entity)
        {
            _refreshEvent?.Invoke(index, entity);
        }

        #endregion





        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new VillageManagerStorableData();

            var list = new List<StorableData>();
            for (int i = 0; i < _list.Count; i++)
            {
                var storableData = _list[i].GetStorableData();
                list.Add(storableData);
            }
            data.SetData(list.ToArray());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (VillageManagerStorableData)data;
            for (int i = 0; i < storableData.Children.Length; i++)
            {
                var children = storableData.Children[i];
                var index = _list.FindIndex(data => data.Contains(children));
                if (index >= 0)
                {
                    _list[index].SetStorableData(children);
                }
            }
        }
        #endregion



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public int Count => _list.Count;

        /// <summary>
        /// 초기화 테스트
        /// </summary>
        /// <param name="accountData"></param>
        public void Initialize_Test()
        {
            _list = new List<VillageLine>();
            var line = CreateLine();
            line.SetIndex(Count);
        }

        //언락 테스트

        //한계 테스트
#endif
    }
}