#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;
    using Utility.Address;

    public class UIMine : VisualElement, ISystemPanel
    {
        public new class UxmlFactory : UxmlFactory<UIMine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIMine/UIMine.uxml";

        private Dictionary<int, UIMineLine> _dic = new Dictionary<int, UIMineLine>();

        private int _lineCount = 0;

        private ScrollView _scrollView;

        public static UIMineLine Create()
        {
            return UIUXML.GetVisualElement<UIMineLine>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
        }

        public void CleanUp()
        {
            foreach (var value in _dic.Values)
            {
                value.RemoveUpgradeListener(OnUpgradeEvent);
                value.CleanUp();
            }
            _dic.Clear();
            _scrollView = null;
        }

        public void Show()
        {
            this.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            this.style.display = DisplayStyle.None;
        }

        //BlacksmithEntity
        public void RefreshMine(int index, MineEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIMineLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshMineLine(entity);
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach (var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }

#endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIMine_Test : MonoBehaviour
    {
        private UIMine _instance;
        public UIMine Instance => _instance;

        public static UIMine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIMine_Test";
            return obj.AddComponent<UIMine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIMine.PATH_UI_UXML);
            _instance = root.Q<UIMine>();
            _instance.Initialize();
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