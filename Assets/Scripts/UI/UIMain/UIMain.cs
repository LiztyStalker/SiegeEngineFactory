namespace SEF.UI.Toolkit
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor;
    using Storage;

    [RequireComponent(typeof(UIDocument))]
    public class UIMain : MonoBehaviour
    {

        private readonly string PATH_UI_LOAD_UXML = "Assets/Scripts/UI/UIMain/UIMainUXML.uxml";


        private UIStart _uiStart;
        private UILoad _uiLoad;

        private VisualElement _root;

        public static UIMain Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Main";
            return obj.AddComponent<UIMain>();
        }


        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            //AssetBundle 불러오기
            DataStorage.Initialize(_uiLoad.ShowLoad, delegate
            {
                _uiLoad.Hide();
                _uiStart.ShowStart(GameStart);
            });


            //데이터 불러오기
            //Start 띄우기

        }


        public void Initialize()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_LOAD_UXML);

            _uiStart = GetComponentInChildren<UIStart>(true);

            if (_uiStart == null)
            {
                _uiStart = UIStart.Create();
                _uiStart.transform.SetParent(transform);
            }

            _uiStart.Initialize(_root);



            _uiLoad = GetComponentInChildren<UILoad>(true);

            if (_uiLoad == null)
            {
                _uiLoad = UILoad.Create();
                _uiLoad.transform.SetParent(transform);
            }
            _uiLoad.Initialize(_root);
        }

     
        private void GameStart()
        {
            Debug.Log("GameStart");
        }


        public void CleanUp()
        {
            _uiStart.CleanUp();
            _uiLoad.CleanUp();

        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}