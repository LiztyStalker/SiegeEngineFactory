namespace SEF.Manager
{
    using System.Collections.Generic;
    using SEF.Account;
    using Data;

    public class WorkshopManager
    {

        private List<WorkshopLine> _list;

        public static WorkshopManager Create()
        {
            return new WorkshopManager();
        }

        public void Initialize(IAccountData accountData)
        {
            if (accountData == null)
            {
                //null이면 초기화
                //null이 아니면 accountData 적용
                _list = new List<WorkshopLine>();
                var line = CreateLine();
                //line 기본 유닛 적용
                _list.Add(CreateLine());
            }
            else
            {
                UnityEngine.Debug.Log("AccountData Load");
            }
        }

        public void CleanUp()
        {
            for(int i = 0; i < _list.Count; i++)
            {
                _list[i].CleanUp();
            }
            _list.Clear();
        }

        public void RunProcess(float deltaTime)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RunProcess(deltaTime);
            }
        }


        public void UpgradeWorkshop(int index) 
        {
            _list[index].Upgrade();
        }

        public void ExpendWorkshop() 
        {
            CreateLine();
        }

        public void UpTechWorkshop(int index, UnitData unitData)
        {
            _list[index].UpTech(unitData);
        }

        private WorkshopLine CreateLine()
        {
            var workshopLine = WorkshopLine.Create();
            workshopLine.SetOnProductUnitListener(OnProductUnitEvent);
            workshopLine.SetOnRefreshListener(OnRefreshEvent);
            _list.Add(workshopLine);
            //기본 유닛 적용 workshopLine.UpTech()
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



        private System.Action<UnitEntity> _refreshEvent;
        public void AddRefreshListener(System.Action<UnitEntity> act) => _refreshEvent += act;
        public void RemoveRefreshListener(System.Action<UnitEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(UnitEntity unitEntity)
        {
            _refreshEvent?.Invoke(unitEntity);
        }

        #endregion


        #region ##### Data #####
        public IAccountData GetAccountData()
        {
            return null;
        }
        #endregion



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public int Count => _list.Count;

        public void Initialize_Test(IAccountData accountData)
        {
            if (accountData == null)
            {
                _list = new List<WorkshopLine>();
                var line = CreateLine();
                line.UpTech(UnitData.Create_Test());
            }
            else
            {
                UnityEngine.Debug.Log("AccountData Load Test");
            }
        }

        public void ExpendWorkshop_Test()
        {
            var line = CreateLine();
            line.UpTech(UnitData.Create_Test());
            
        }
#endif
    }
}