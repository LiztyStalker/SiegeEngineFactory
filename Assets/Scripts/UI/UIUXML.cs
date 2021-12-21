namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Storage;

    public class UIUXML
    {

        private readonly static string PATH_DEFAULT_SETTING = "Assets/UIToolkit/DefaultPanelSettings.asset";

        public static T GetVisualElement<T>(string uxmlPath) where T : VisualElement
        {
            var element = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var container = element.CloneTree();
            return container.Q<T>();
        }

        public static VisualElement GetVisualElement(GameObject gameObject, string uxmlPath)
        {
            var uiDocument = gameObject.GetComponent<UIDocument>();
            if(uiDocument == null)
            {
                uiDocument = gameObject.AddComponent<UIDocument>();
            }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
            uiDocument.panelSettings = DataStorage.LoadAssetAtPath<PanelSettings>(PATH_DEFAULT_SETTING);
            var asset = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
#else
            //AssetBundle 적용
#endif
            uiDocument.visualTreeAsset = asset;

            var root = uiDocument.rootVisualElement;

            Debug.Assert(root != null, $"{uxmlPath} UI를 구성하지 못했습니다");

            return root;
        }
    }
}