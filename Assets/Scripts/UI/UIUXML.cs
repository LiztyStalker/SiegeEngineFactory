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

        /// <summary>
        /// path�� ������ container�� �����ϰ� VisualElement�� �����ɴϴ�
        /// </summary>
        /// <typeparam name="T">VisualElement</typeparam>
        /// <param name="uxmlPath">UxmlPath</param>
        /// <returns></returns>
        public static T GetVisualElement<T>(string uxmlPath) where T : VisualElement
        {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
            var element = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var container = element.CloneTree();            
            return container.Q<T>();
#else
            Debug.Assert(false, "GetVisualElement ReleaseMode�� ������� �ʾҽ��ϴ�");
            return null;
#endif
        }

        public static T GetVisualElement<T>(string uxmlPath, string ussPath) where T : VisualElement
        {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
            var element = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var container = element.CloneTree();

            var styleSheet = DataStorage.LoadAssetAtPath<StyleSheet>(ussPath);
            container.styleSheets.Add(styleSheet);
            return container.Q<T>();

#else
            Debug.Assert(false, "GetVisualElement ReleaseMode�� ������� �ʾҽ��ϴ�");
            return null;
#endif
        }

        public static VisualElement GetVisualElement(GameObject gameObject, string uxmlPath)
        {

            var uiDocument = gameObject.GetComponent<UIDocument>();
            if(uiDocument == null)
            {
                uiDocument = gameObject.AddComponent<UIDocument>();
            }

            if (uiDocument.visualTreeAsset == null)
            {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
                uiDocument.panelSettings = DataStorage.LoadAssetAtPath<PanelSettings>(PATH_DEFAULT_SETTING);
                var asset = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
#else
                //AssetBundle ����
                Debug.Assert(false, "GetVisualElement AssetBundle�� ������� �ʾҽ��ϴ�");
                VisualTreeAsset asset = null;
#endif
                uiDocument.visualTreeAsset = asset;
            }

            var root = uiDocument.rootVisualElement;

            Debug.Assert(root != null, $"{uxmlPath} UI�� �������� ���߽��ϴ�");

            return root;
        }


        public static VisualElement GetVisualElement(GameObject gameObject, string uxmlPath, string ussPath)
        {

            var uiDocument = gameObject.GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                uiDocument = gameObject.AddComponent<UIDocument>();
            }

            if (uiDocument.visualTreeAsset == null)
            {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
                uiDocument.panelSettings = DataStorage.LoadAssetAtPath<PanelSettings>(PATH_DEFAULT_SETTING);
                var asset = DataStorage.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);

#else
                //AssetBundle ����
                Debug.Assert(false, "GetVisualElement AssetBundle�� ������� �ʾҽ��ϴ�");
                VisualTreeAsset asset = null;
#endif
                uiDocument.visualTreeAsset = asset;
            }

            var root = uiDocument.rootVisualElement;

            var styleSheet = DataStorage.LoadAssetAtPath<StyleSheet>(ussPath);
            root.styleSheets.Add(styleSheet);

            Debug.Assert(root != null, $"{uxmlPath} UI�� �������� ���߽��ϴ�");

            return root;
        }
    }
}