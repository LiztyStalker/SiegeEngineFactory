namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SEF.UI.Toolkit;
    using Account;
    using Unit;

    public class GameManager : MonoBehaviour
    {
        private UIGame _uiGame;
        private GameSystem _gameSystem;
        private UnitManager _unitManager;

        public static GameManager Create()
        {
            var obj = new GameObject();
            obj.name = "GameManager";
            return obj.AddComponent<GameManager>();
        }

        private void Awake()
        {
            //Initialize
            _gameSystem = GameSystem.Create();
            _gameSystem.Initialize();

            _unitManager = UnitManager.Create();
            _unitManager.Initialize();

            _uiGame = UIGame.Create();
            _uiGame.Initialize();


            //Event 연결
            _gameSystem.AddRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.AddRefreshAssetEntityListener(_uiGame.RefreshAssetEntity);
            _gameSystem.AddRefreshAssetDataListener(_uiGame.RefreshAssetData);
            _gameSystem.AddProductUnitListener(_unitManager.ProductUnitActor);

            //_unitManager.AddOnHitListener();
            _unitManager.AddOnDestoryedListener(OnDestroyedEvent);
            _unitManager.AddOnNextEnemyListener(_uiGame.RefreshEnemyUnit);

            _uiGame.AddUpgradeListener(_gameSystem.UpgradeWorkshop);
            _uiGame.AddUpTechListener(_gameSystem.UpTechWorkshop);
            _uiGame.AddExpendListener(_gameSystem.ExpendWorkshop);
        }

        private void OnDestroy()
        {
            //Event 해제
            _gameSystem.RemoveRefreshUnitListener(_uiGame.RefreshUnit);
            _gameSystem.RemoveRefreshAssetEntityListener(_uiGame.RefreshAssetEntity);
            _gameSystem.RemoveRefreshAssetDataListener(_uiGame.RefreshAssetData);

            //_unitManager.RemoveOnHitListener();
            _unitManager.RemoveOnDestoryedListener(OnDestroyedEvent);
            _gameSystem.RemoveProductUnitListener(_unitManager.ProductUnitActor);

            _uiGame.RemoveUpgradeListener(_gameSystem.UpgradeWorkshop);
            _uiGame.RemoveUpTechListener(_gameSystem.UpTechWorkshop);
            _uiGame.RemoveExpendListener(_gameSystem.ExpendWorkshop);




            //CleanUp
            _gameSystem.CleanUp();
            _unitManager.CleanUp();
            _uiGame.CleanUp();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _gameSystem.RunProcess(deltaTime);
            _unitManager.RunProcess(deltaTime);
        }



        private void OnDestroyedEvent(PlayActor playActor) 
        {
            _gameSystem.DestroyedActor(playActor);
        }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void AddAssetData(Data.AssetData assetData)
        {
            _gameSystem.AddAsset(assetData);
        }
#endif
    }
}