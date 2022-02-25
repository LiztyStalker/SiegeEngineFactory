namespace SEF.Manager
{
    using Data;
    using Entity;
    using System.Collections.Generic;
    using Storage;
    using Utility.IO;




    #region ##### StorableData #####

    [System.Serializable]
    public class SmithyManagerStorableData : StorableData
    {
        public void SetData(StorableData[] children)
        {
            Children = children;
        }
    }

    #endregion




    public class SmithyManager
    {

        private List<SmithyLine> _list;

        public static SmithyManager Create()
        {
            return new SmithyManager();
        }

        public void Initialize()
        {
            _list = new List<SmithyLine>();
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<SmithyData>();

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

        private SmithyLine CreateLine()
        {
            var line = SmithyLine.Create();
            line.Initialize();
            line.SetIndex(_list.Count);
            line.SetOnRefreshListener(OnRefreshEvent);
            _list.Add(line);
            return line;
        }


        #region ##### Listener #####


        private System.Action<int, SmithyEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<int, SmithyEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<int, SmithyEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, SmithyEntity unitEntity)
        {
            _refreshEvent?.Invoke(index, unitEntity);
        }

        #endregion



        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new SmithyManagerStorableData();

            var list = new List<StorableData>();
            for(int i = 0; i < _list.Count; i++)
            {
                var storableData = _list[i].GetStorableData();
                list.Add(storableData);
            }
            data.SetData(list.ToArray());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (SmithyManagerStorableData)data;

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
        public void Initialize_Test(StorableData accountData)
        {
            if (accountData == null)
            {
                _list = new List<SmithyLine>();
                var line = CreateLine();
                line.SetIndex(Count);
            }
            else
            {
                UnityEngine.Debug.Log("AccountData Load Test");
            }
        }

        //언락 테스트

        //한계 테스트
#endif
    }
}