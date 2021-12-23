namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SEF.UI.Toolkit;
    using Account;

    public class GameManager : MonoBehaviour
    {
        private UIGame _uiGame;
        private GameSystem _gameSystem;

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

            _uiGame = UIGame.Create();
            _uiGame.Initialize();



            //Event 연결
            _gameSystem.AddRefreshUnitListener(_uiGame.RefreshUnit);
        }

        private void OnDestroy()
        {
            //Event 해제
            _gameSystem.RemoveRefreshUnitListener(_uiGame.RefreshUnit);



            //CleanUp
            _gameSystem.CleanUp();
            _uiGame.CleanUp();
        }

        private void Update()
        {
            _gameSystem.RunProcess(Time.deltaTime);
        }
    }
}