namespace SEF.Manager
{
    using Data;
    using Entity;
    using System.Collections.Generic;
    using UnityEngine;
    using Storage;
    using Process;
    using Utility.IO;

    #region ##### Serialize #####

    [System.Serializable]
    public class MineManagerStorableData : StorableData
    {
        public void SetData(StorableData[] children)
        {
            Children = children;
        }
    }

    #endregion

    public class MineManager : MonoBehaviour
    {

        private List<MineLine> _list;

        public static MineManager Create()
        {
            return new MineManager();
        }

        public void Initialize()
        {
            _list = new List<MineLine>();
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

        private IAssetData _expandAssetData;
        public IAssetData ExpandAssetData
        {
            get
            {
                if (_expandAssetData == null)
                {
                    var assetData = NumberDataUtility.Create<GoldAssetData>();
                    assetData.ValueText = System.Numerics.BigInteger.Pow(1000, _list.Count + 1).ToString();
                    _expandAssetData = assetData;
                }
                return _expandAssetData;
            }
        }

        public int Expand()
        {
            CreateLine();
            _expandAssetData = null;
            return _list.Count;
        }


        public RewardAssetPackage RewardOffline(System.TimeSpan timeSpan)
        {
            var rewardAssetPackage = new RewardAssetPackage();
            for(int i = 0; i < _list.Count; i++)
            {
                rewardAssetPackage.AddAssetData(_list[i].RewardOffline(timeSpan));
            }
            return rewardAssetPackage;
        }

        private MineLine CreateLine()
        {
            var line = MineLine.Create();
            line.Initialize();
            line.SetIndex(_list.Count);
            line.SetOnRefreshListener(OnRefreshEvent);
            line.SetOnProcessEntityListener(OnProcessEntityEvent);
            _list.Add(line);


            //�⺻ ������ ����
            line.SetData(GetMineData());

            return line;
        }

        private MineData GetMineData()
        {
            var data = DataStorage.Instance.GetDataOrNull<MineData>("MineData_Mine", null, null);
            Debug.Log(data);
            return data;
        }


        #region ##### Listener #####


        private System.Action<int, MineEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<int, MineEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<int, MineEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, MineEntity entity)
        {
            _refreshEvent?.Invoke(index, entity);
        }


        private System.Action<IProcessProvider, ProcessEntity> _setProcessEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _setProcessEvent = act;
        private void OnProcessEntityEvent(IProcessProvider provider, ProcessEntity entity) => _setProcessEvent?.Invoke(provider, entity);

        #endregion





        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new MineManagerStorableData();

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
            var storableData = (MineManagerStorableData)data;
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
        /// �ʱ�ȭ �׽�Ʈ
        /// </summary>
        /// <param name="accountData"></param>
        public void Initialize_Test()
        {
            _list = new List<MineLine>();
            var line = CreateLine();
            line.SetIndex(Count);
        }

        //��� �׽�Ʈ

        //�Ѱ� �׽�Ʈ
#endif
    }
}