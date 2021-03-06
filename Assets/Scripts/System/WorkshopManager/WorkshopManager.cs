namespace SEF.Manager
{
    using System.Collections.Generic;
    using Entity;
    using Data;
    using System.Linq;
    using Utility.IO;

    #region ##### StorableData #####

    [System.Serializable]
    public class WorkshopManagerStorableData : StorableData
    {
        public void SaveData(StorableData[] children)
        {
            Children = children;
        }
    }

    #endregion

    public class WorkshopManager
    {
        private List<WorkshopLine> _list;

        public static WorkshopManager Create()
        {
            return new WorkshopManager();
        }

        public void Initialize()
        {
            _list = new List<WorkshopLine>();
            CreateLine();
        }

        public void CleanUp()
        {
            for(int i = 0; i < _list.Count; i++)
            {
                _list[i].CleanUp();
            }
            _list.Clear();
        }

        public void Refresh()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RunProcess(0f);
            }
        }

        public void RunProcess(float deltaTime)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RunProcess(deltaTime);
            }
        }

        public IAssetData Upgrade(int index, out string key)
        {
            return _list[index].Upgrade(out key);
        }

        private IAssetData _expandAssetData;

        public IAssetData ExpandAssetData
        {
            get
            {
                if(_expandAssetData == null)
                {
                    var assetData = NumberDataUtility.Create<GoldAssetData>();
                    assetData.ValueText = System.Numerics.BigInteger.Pow(1000, _list.Count).ToString();
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

        public IAssetData UpTechWorkshop(int index, UnitTechData data)
        {
            var unitData = Storage.DataStorage.Instance.GetDataOrNull<UnitData>(data.TechUnitKey);
             _list[index].UpTech(unitData);
            return data.TechAssetData;

        }

        private WorkshopLine CreateLine()
        {
            var workshopLine = WorkshopLine.Create();
            workshopLine.Initialize();
            workshopLine.SetIndex(_list.Count);
            workshopLine.SetOnProductUnitListener(OnProductUnitEvent);
            workshopLine.SetOnRefreshListener(OnRefreshEvent);
            workshopLine.SetOnConditionProductUnitListener(OnConditionProductUnitEvent);
            _list.Add(workshopLine);

            //???? ???? ????
            workshopLine.UpTech(Storage.DataStorage.Instance.GetDataOrNull<UnitData>("UnitData_ThrowerStone", null, null)); 
            return workshopLine;
        }





        #region ##### Listener #####

        private System.Action<UnitEntity> _productUnitEvent;
        public void AddProductUnitListener(System.Action<UnitEntity> act) => _productUnitEvent += act;
        public void RemoveProductUnitListener(System.Action<UnitEntity> act) => _productUnitEvent -= act;                
        private void OnProductUnitEvent(UnitEntity unitEntity)
        {
            _productUnitEvent?.Invoke(unitEntity);
        }



        private System.Action<int, UnitEntity, float> _refreshEvent;
        public void AddRefreshListener(System.Action<int, UnitEntity, float> act) => _refreshEvent += act;
        public void RemoveRefreshListener(System.Action<int, UnitEntity, float> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, UnitEntity unitEntity, float nowTime)
        {
            _refreshEvent?.Invoke(index, unitEntity, nowTime);
        }

        private System.Func<UnitEntity, bool> _conditionProductUnitEvent;
        public void SetOnConditionProductUnitListener(System.Func<UnitEntity, bool> act) => _conditionProductUnitEvent = act;
        private bool OnConditionProductUnitEvent(UnitEntity unitEntity)
        {
            return _conditionProductUnitEvent(unitEntity);
        }

        #endregion



        #region ##### StorableData #####

        public StorableData GetStorableData()
        {
            var _storableData = new WorkshopManagerStorableData();
            _storableData.SaveData(GetChildren());
            return _storableData;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = data;
            for(int i = 0; i < storableData.Children.Length; i++)
            {
                var children = storableData.Children[i];

                if (_list.Count <= i)
                {
                    var line = CreateLine();
                    line.SetStorableData(children);
                }
                else
                {
                    var line = _list[i];
                    line.SetStorableData(children);
                }
            }
        }


        private StorableData[] GetChildren()
        {
            StorableData[] arr = new StorableData[_list.Count];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = _list[i].GetStorableData();
            }
            return arr.ToArray();
        }

        #endregion



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public int Count => _list.Count;

        /// <summary>
        /// ?????? ??????
        /// </summary>
        /// <param name="accountData"></param>
        public void Initialize_Test()
        {
            _list = new List<WorkshopLine>();
            var line = CreateLine();
            line.UpTech(UnitData.Create_Test());
            line.SetIndex(Count);
        }

        /// <summary>
        /// ???? ??????
        /// </summary>
        public void ExpandWorkshop_Test()
        {
            var line = CreateLine();
            line.UpTech(UnitData.Create_Test());
            
        }
#endif
    }
}