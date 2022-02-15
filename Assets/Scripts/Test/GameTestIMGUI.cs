namespace SEF.Manager {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameTestIMGUI : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        public void OnGUI()
        {

            if (GUI.Button(new Rect(0, 0, 100, 30), "Save"))
            {
                _gameManager.SaveData();
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Load"))
            {
                _gameManager.LoadData();
            }
        }
    }
}