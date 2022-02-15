namespace SEF.Manager
{
    using System.Collections.Generic;
    using Account;
    using Entity;
    using Data;
    using System.Linq;
    using Utility.IO;

    #region ##### Serialize #####

    [System.Serializable]
    public class WorkshopManagerStorableData : StorableData
    {
        public void LoadData(StorableData data)
        {
        }
        public void SaveData(StorableData[] children)
        {
            Children = children;
        }
    }

    #endregion

    public class WorkshopManager
    {

        private List<WorkshopLine> _list;

        private WorkshopManagerStorableData _storableData;

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

        private IAssetData _expendAssetData;

        public IAssetData ExpendAssetData
        {
            get
            {
                if(_expendAssetData == null)
                {
                    var assetData = NumberDataUtility.Create<GoldAssetData>();
                    assetData.ValueText = System.Numerics.BigInteger.Pow(1000, _list.Count).ToString();
                    _expendAssetData = assetData;
                }
                return _expendAssetData;
            }
        }

        public int ExpendWorkshop() 
        {
            CreateLine();
            _expendAssetData = null;
            return _list.Count;
            //return ExpendAssetData to Count
        }

        public void UpTechWorkshop(int index, UnitData unitData)
        {
            _list[index].UpTech(unitData);
        }

        private WorkshopLine CreateLine()
        {
            var workshopLine = WorkshopLine.Create();
            workshopLine.Initialize();
            workshopLine.SetIndex(_list.Count);
            workshopLine.SetOnProductUnitListener(OnProductUnitEvent);
            workshopLine.SetOnRefreshListener(OnRefreshEvent);
            workshopLine.SetOnConditionProductUnitListener(OnConditionProductUnitEvent);
            //workshopLine.SetOnStatusPackageListener(GetStatusPackage);
            _list.Add(workshopLine);

            //기본 유닛 적용
            workshopLine.UpTech(Storage.DataStorage.Instance.GetDataOrNull<UnitData>("UnitData_Test", null, null)); 
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


        //private System.Func<StatusPackage> _statusPackageEvent;
        //public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        //private StatusPackage GetStatusPackage() => _statusPackageEvent();

        //LineEvent

        #endregion



        #region ##### StorableData #####

        public StorableData GetStorableData()
        {
            if (_storableData == null)
                _storableData = new WorkshopManagerStorableData();

            _storableData.SaveData(GetChildren());
            return _storableData;
        }

        public void SetStorableData(StorableData data)
        {

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
        /// 초기화 테스트
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
        /// 증축 테스트
        /// </summary>
        public void ExpendWorkshop_Test()
        {
            var line = CreateLine();
            line.UpTech(UnitData.Create_Test());
            
        }
#endif
    }
}