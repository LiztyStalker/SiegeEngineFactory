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

        private WorkshopManager _workshopManager;
        //BlackSmithManager
        //VillageManager
        //ResearchManager

        public static GameSystem Create()
        {
            return new GameSystem();
        }

        public void Initialize() 
        {
            _account = Account.Current;

            _workshopManager = WorkshopManager.Create();

            _workshopManager.Initialize(null);
            //BlackSmithManager
            //VillageManager
            //ResearchManager
        }

        public void CleanUp()
        {
            _workshopManager.CleanUp();
            //BlackSmithManager
            //VillageManager
            //ResearchManager
        }

        public void RunProcess(float deltaTime)
        {
            _workshopManager.RunProcess(deltaTime);
            //VillageManager
        }
        public void AddAsset(AssetData assetData) => _account.AddAsset(assetData);
        public void SubjectAsset(AssetData assetData) => _account.SubjectAsset(assetData);
        
        public AssetData GetAssetData()
        {
            return null;
        }

        public void UpgradeWorkshop(int index)
        {
            var assetData = _workshopManager.UpgradeWorkshop(index);
            _account.SubjectAsset(assetData);
        }

        public void ExpendWorkshop()
        {
            _workshopManager.ExpendWorkshop();
            //AssetData Subject(ExpendAssetData)
        }
        public void UpTechWorkshop(int index, UnitData unitData)
        {
            _workshopManager.UpTechWorkshop(index, unitData);
            //unitData TechAssetData ¼Òºñ
        }

        public void DestroyedActor(PlayActor playActor)
        {
            switch (playActor)
            {
                case UnitActor unitActor:
                    break;
                case EnemyActor enemyActor:
                    Debug.Log(enemyActor.GetRewardAssetData().Value);
                    _account.AddAsset(enemyActor.GetRewardAssetData());
                    break;
            }
        }


        #region ##### Listener #####
        public void AddRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        public void AddProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.AddProductUnitListener(act);
        public void RemoveProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.RemoveProductUnitListener(act);


        public void AddRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.AddRefreshAssetEntityListener(act);
        public void RemoveRefreshAssetEntityListener(System.Action<AssetEntity> act) => _account.RemoveRefreshAssetEntityListener(act);

        public void AddRefreshAssetDataListener(System.Action<AssetData> act) => _account.AddRefreshAssetDataListener(act);
        public void RemoveRefreshAssetDataListener(System.Action<AssetData> act) => _account.RemoveRefreshAssetDataListener(act);

        #endregion


        public void Save() { }

    }

}