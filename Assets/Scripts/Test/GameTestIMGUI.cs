namespace SEF.Manager {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameTestIMGUI : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        private bool _isStatistics;

        private List<Statistics.StatisticsEntity> _list = new List<Statistics.StatisticsEntity>();

        private Vector2 _scrollPos;

        private void OnEnable()
        {
            _gameManager.SetOnRefreshStatisticsListener(ShowLabel);
        }

        public void OnGUI()
        {

            if (GUI.Button(new Rect(0, 0, 100, 30), "Save"))
            {
                _gameManager.SaveData();
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Load"))
            {
                _gameManager.LoadData_Test();
            }
            if (GUI.Button(new Rect(0, 60, 100, 30), "Add GoldAssetData"))
            {
                var data = new Data.GoldAssetData();
                data.AssetValue = 1000;
                _gameManager.AddAssetData_Test(data);
            }
            DrawLabel();
        }

        private void ShowLabel(Statistics.StatisticsEntity entity)
        {
            bool isCheck = false;
            for(int i = 0; i < _list.Count; i++)
            {
                if(_list[i].GetStatisticsType() == entity.GetStatisticsType())
                {
                    _list[i].SetStatisticsData(entity.GetStatisticsValue());
                    isCheck = true;
                    break;
                }
            }
            if (!isCheck)
            {
                _list.Add(entity);
            }
        }

        private void DrawLabel()
        {
            GUILayout.BeginArea(new Rect(0, 90, 200, 500));
            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
            GUILayout.BeginVertical();
            for (int i = 0; i < _list.Count; i++)
            {
                var entity = _list[i];
                GUILayout.Label(entity.GetStatisticsType() + " " + entity.GetStatisticsValue());
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}