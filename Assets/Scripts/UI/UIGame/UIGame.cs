namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;

    [RequireComponent(typeof(UIDocument))]
    public class UIGame : MonoBehaviour
    {

        private readonly string PATH_UI_GAME_UXML = "Assets/Scripts/UI/UIGame/UIGameUXML.uxml";


        private VisualElement _root = null;

        private UIAsset _uiAsset;
        private UISystem _uiSystem;
        //private UIPlay _uiPlay;
        //private UIOfflineReward _uiOfflineReward;

        private void Awake()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_GAME_UXML);            
        }
        private void Start()
        {

            _uiAsset = UIAsset.Create();
            _uiSystem = _root.Q<UISystem>();

            Debug.Assert(_uiAsset != null, "_uiAsset 이 등록되지 않았습니다");
            Debug.Assert(_uiSystem != null, "_uiSystem 이 등록되지 않았습니다");

            _uiAsset.Initialize(_root.Q<VisualElement>("UIAssetUXML"));
            _uiSystem.Initialize();
            
        }

        private void OnDestroy()
        {
            _uiAsset.CleanUp();
            _uiSystem.CleanUp();
        }



        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime) => _uiSystem.RefreshUnit(index, unitEntity, nowTime);



    }
}