#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;

    public class UIVillage : VisualElement, ISystemPanel
    {
        public new class UxmlFactory : UxmlFactory<UIVillage, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIVillage/UIVillage.uxml";
        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UISystem/UIVillage/UIVillage.uss";

        private Dictionary<int, UIVillageLine> _dic = new Dictionary<int, UIVillageLine>();

        private ScrollView _scrollView;

        public static UIVillageLine Create()
        {
            return UIUXML.GetVisualElement<UIVillageLine>(PATH_UI_UXML, PATH_UI_USS);
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
                value.RemoveOnUpTechListener(OnUpTechEvent);
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
        public void RefreshVillage(int index, VillageEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIVillageLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddOnUpTechListener(OnUpTechEvent);
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshVillageLine(entity);
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


        private System.Action<int> _uptechEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _uptechEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index)
        {
            _uptechEvent?.Invoke(index);
        }
#endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIVillage_Test : MonoBehaviour
    {
        private UIVillage _instance;
        public UIVillage Instance => _instance;

        public static UIVillage_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIVillage_Test";
            return obj.AddComponent<UIVillage_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIVillage.PATH_UI_UXML);
            _instance = root.Q<UIVillage>();
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