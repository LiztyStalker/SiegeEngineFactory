namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Entity;
    using Account;

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
        public void Save() { }
        public void AddAsset(AssetData assetData) { }
        public void SubjectAsset(AssetData assetData) { }
        public AssetData GetAssetData()
        {
            return null;
        }

        public void UpgradeWorkshop(WorkshopLine line) { }
        public void ExpendWorkshop(WorkshopLine line) { }
        public void UpTechWorkshop(WorkshopLine line) { }


        #region ##### Listener #####
        public void AddRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.AddRefreshListener(act);
        public void RemoveRefreshUnitListener(System.Action<int, UnitEntity, float> act) => _workshopManager.RemoveRefreshListener(act);
        public void AddProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.AddProductUnitListener(act);
        public void RemoveProductUnitListener(System.Action<UnitEntity> act) => _workshopManager.RemoveProductUnitListener(act);
        #endregion
    }
}