namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Entity;
    using Account;
    using Unit;


    public class GameSystem
    {

        private Account _account;

        private StatusPackage _statusPackage;

        private WorkshopManager _workshopManager;
        private BlacksmithManager _blacksmithManager;
        private VillageManager _villageManager;
        //ResearchManager

        public static GameSystem Create()
        {
            return new GameSystem();
        }

        public void Initialize() 
        {
            _account = Account.Current;

            _workshopManager = WorkshopManager.Create();

            _workshopManager.SetOnConditionProductUnitListener(IsConditionProductUnitEvent);
            _workshopManager.SetOnStatusPackageListener(GetStatusPackage);
            _workshopManager.Initialize(null);

            _blacksmithManager = BlacksmithManager.Create();
            _blacksmithManager.SetOnStatusPackageListener(GetStatusPackage);
            _blacksmithManager.Initialize(null);

            _villageManager = VillageManager.Create();
            _villageManager.SetOnStatusPackageListener(GetStatusPackage);
            _villageManager.Initialize(null);

            //ResearchManager

            _statusPackage = StatusPackage.Create();
            _statusPackage.Initialize();
            _statusPackage.AddOnProductListener(OnStatusProductEvent);


        }

        public void CleanUp()
        {
            _statusPackage.RemoveOnProductListener(OnStatusProductEvent);
            _statusPackage.CleanUp();

            _workshopManager.CleanUp();
            _blacksmithManager.CleanUp();
            _villageManager.CleanUp();
            //ResearchManager
        }

        public void RunProcess(float deltaTime)
        {
            _workshopManager.RunProcess(deltaTime);
            _blacksmithManager.RunProcess(deltaTime);
            //VillageManager

            _statusPackage.RunProcess(deltaTime);
        }

        public void Refresh()
        {
            _workshopManager.Refresh();
            _blacksmithManager.Refresh();
            _villageManager.Refresh();

            //UI는 나중에 갱신 필요 - Data -> UI
            _account.RefreshAssetEntity();
        }

        #region ##### StatusEntity #####

        //        public U GetStatusDataToBigNumberData<T, U>(U data) where T : IStatusData where U : BigNumberData => _statusEntity.GetStatusDataToBigNumberData<T, U>(data);

        public StatusPackage GetStatusPackage() => _statusPackage;

        private void OnStatusProductEvent(IAssetData[] assetDataArr)
        {
            for(int i = 0; i < assetDataArr.Length; i++)
            {
                AddAsset(assetDataArr[i]);
            }
        }

        #endregion


        #region ##### AssetEntity #####

        public void AddAsset(IAssetData assetData) => _account.AddAsset(assetData);
        public void SubjectAsset(IAssetData assetData) => _account.SubjectAsset(assetData);
        public void SetAsset(IAssetData assetData) => _account.SetAsset(assetData);

        #endregion

              

        public IAssetData GetAssetData()
        {
            return null;
        }


        //DestroyedActor가 여기 있을 필요가 있는지 의문
        [System.Obsolete("StatusPackage 적용 보상")]
        public void DestroyedActor(PlayActor playActor)
        {
            switch (playActor)
            {
                case UnitActor unitActor:
                    break;
                case EnemyActor enemyActor:
                    //[System.Obsolete("StatusPackage 적용 보상")]
                    _account.AddAsset(enemyActor.GetRewardAssetData());
                    break;
            }
        }




        #region ##### Workshop #####
        public void UpgradeWorkshop(int index)
        {
            var assetData = _workshopManager.Upgrade(index);
            _account.SubjectAsset(assetData);
        }

        public void UpgradeBlacksmith(int index)
        {
            var assetData = _blacksmithManager.Upgrade(index);
            _account.SubjectAsset(assetData);
        }
        public void UpgradeVillage(int index)
        {
            var assetData = _villageManager.Upgrade(index);
            _account.SubjectAsset(assetData);
        }




        public void ExpendWorkshop()
        {
            _workshopManager.ExpendWorkshop();
        }
        public void UpTechWorkshop(int index, UnitData unitData)
        {
            _workshopManager.UpTechWorkshop(index, unitData);
            //unitData TechAssetData 소비
        }

        private bool IsConditionProductUnitEvent(UnitEntity unitEntity)
        {
            var population = new PopulationAssetData(unitEntity.Population);
            return !_account.IsOverflow(population);
        }

        //BlacksmithCondition 적용
        //VillageCondition적용
        

        #endregion




        #region ##### Listener #####
        public void AddRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        public void AddOnRefreshBlacksmithListener(System.Action<int, BlacksmithEntity> act) => _blacksmithManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshBlacksmithListener(System.Action<int, BlacksmithEntity> act) => _blacksmithManager.RemoveOnRefreshListener(act);
        public void AddOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.AddOnRefreshListener(act);
        public void RemoveOnRefreshVillageListener(System.Action<int, VillageEntity> act) => _villageManager.RemoveOnRefreshListener(act);



        public void AddProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.AddProductUnitListener(act);
        public void RemoveProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.RemoveProductUnitListener(act);
        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.AddRefreshAssetEntityListener(act);
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.RemoveRefreshAssetEntityListener(act);
        public void AddRefreshAssetDataListener(System.Action<IAssetData> act) => _account.AddRefreshAssetDataListener(act);
        public void RemoveRefreshAssetDataListener(System.Action<IAssetData> act) => _account.RemoveRefreshAssetDataListener(act);


        #endregion




        #region ##### Data #####

        public void Save() { }

        #endregion 
    }

}