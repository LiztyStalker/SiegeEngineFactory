namespace SEF.UI.Toolkit
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor;
    using Storage;
    using SEF.Account;

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
            InitializeAssetBundle();
        }

        private void InitializeAssetBundle()
        {
            //AssetBundle 불러오기
            DataStorage.Initialize(_uiLoad.ShowLoad, result =>
            {
                if (result == TYPE_IO_RESULT.Success)
                {
                    InitializeAccount();
                }
                else
                {
                    //Success가 아니면 메시지 출력
                    ShowPopup(result);
                }

            });
        }

        private void InitializeAccount()
        {
            //데이터 불러오기
            Account.Current.Load(_uiLoad.ShowLoad, result =>
            {
                if (result == TYPE_IO_RESULT.Success)
                {
                    _uiLoad.Hide();
                    _uiStart.ShowStart(GameStart);
                }
                else
                {
                    //Success가 아니면 메시지 출력
                    ShowPopup(result);
                }
            });
        }

        private void ShowPopup(TYPE_IO_RESULT result)
        {
            UICommon.Current.ShowPopup(result.ToString(), Quit);
        }

        public void Initialize()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_LOAD_UXML);

            var parent = _root.Q<VisualElement>("window");

            _uiStart = GetComponentInChildren<UIStart>(true);

            if (_uiStart == null)
            {
                _uiStart = UIStart.Create();
                _uiStart.transform.SetParent(transform);
            }

            _uiStart.Initialize(parent);



            _uiLoad = GetComponentInChildren<UILoad>(true);

            if (_uiLoad == null)
            {
                _uiLoad = UILoad.Create();
                _uiLoad.transform.SetParent(transform);
            }
            _uiLoad.Initialize(parent);
        }

     
        private void GameStart()
        {
            Debug.Log("GameStart");
        }

        private void Quit()
        {
            Application.Quit();
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