#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;


    public class UIEnemyHealthBar : UIProgressbar
    {
        public new class UxmlFactory : UxmlFactory<UIEnemyHealthBar, UxmlTraits> { }

        //속성 정의
        //
        public new class UxmlTraits : VisualElement.UxmlTraits  {   }



        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIEnemyHealthBar.uxml";

        public UIEnemyHealthBar() : base()
        {
//            ProgressLabel.style.display = DisplayStyle.None;
        }


        public void ShowHealth(string value, float rate)
        {
            FillAmount = rate;
            ProgressLabel.text = value;
        }


    }
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

    [RequireComponent(typeof(UIDocument))]
    public class UIEnemyHealthBar_Test : MonoBehaviour
    {
        private UIEnemyHealthBar _instance;
        public UIEnemyHealthBar Instance => _instance;

        public static UIEnemyHealthBar_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIEnemyHealthBar_Test";
            return obj.AddComponent<UIEnemyHealthBar_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIEnemyHealthBar.PATH_UI_UXML);
            _instance = root.Q<UIEnemyHealthBar>();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}
#endif