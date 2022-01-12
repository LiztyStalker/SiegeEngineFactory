namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using Unity.Mathematics;
    using Unity.Collections;

    public class UIProgressbar : UIFillable
    {

        public new class UxmlFactory : UxmlFactory<UIProgressbar, UxmlTraits> { }

        //속성 정의
        //
        public new class UxmlTraits : VisualElement.UxmlTraits {

        }
        internal static readonly new string PATH_UI_UXML = "Assets/Scripts/UI/UIUtility/UIProgressbar.uxml";
    }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

    [RequireComponent(typeof(UIDocument))]
    public class UIProgressbar_Test : MonoBehaviour
    {
        private UIProgressbar _instance;
        public UIProgressbar Instance => _instance;

        public static UIProgressbar_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIProgressbar_Test";
            return obj.AddComponent<UIProgressbar_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIProgressbar.PATH_UI_UXML);
            _instance = root.Q<UIProgressbar>();            
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}