namespace SEF.UI.Toolkit
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using SEF.Entity;

    public class UIQuestLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIQuestLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIQuest/UIQuestLine.uxml";


        private int _index;

        private VisualElement _activatePanel;

        private VisualElement _icon;

        private Label _nameLabel;

        private Label _contentLabel;

        private UIFillable _fillable;

        private Button _rewardButton;
        private VisualElement _rewardAssetIcon;
        private Label _rewardValueLabel;

        private VisualElement _inactivatePanel;
        private Label _inactivateLabel;

        public void SetIndex(int index) => _index = index;

        public static UIQuestLine Create()
        {
            return UIUXML.GetVisualElement<UIQuestLine>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _activatePanel = this.Q<VisualElement>("activate-panel");

            _icon = this.Q<VisualElement>("line-icon");

            _nameLabel = this.Q<Label>("name-label");

            _contentLabel = this.Q<Label>("content-label");

            _fillable = this.Q<UIFillable>();

            _rewardButton = this.Q<Button>("upgrade-button");
            _rewardAssetIcon = this.Q<VisualElement>("upgrade-asset-icon");
            _rewardValueLabel = this.Q<Label>("upgrade-asset-value-label");

            _inactivatePanel = this.Q<VisualElement>("inactivate_panel");

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nameLabel != null, "_nameLabel element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");

            Debug.Assert(_fillable != null, "_fillable element 를 찾지 못했습니다");

            Debug.Assert(_rewardButton != null, "_rewardButton element 를 찾지 못했습니다");
            Debug.Assert(_rewardAssetIcon != null, "_rewardAssetIcon element 를 찾지 못했습니다");
            Debug.Assert(_rewardValueLabel != null, "_rewardValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element 를 찾지 못했습니다");


            //_icon

            _nameLabel.text = "이름";
            _contentLabel.text = "설명";

            _rewardButton.RegisterCallback<ClickEvent>(OnRewardClickedEvent);

            _activatePanel.style.display = DisplayStyle.None;
            _inactivatePanel.style.display = DisplayStyle.Flex;
        }


        public void RefreshQuestLine()
        {
            if (_activatePanel.style.display == DisplayStyle.None)
            {
                _activatePanel.style.display = DisplayStyle.Flex;
                _inactivatePanel.style.display = DisplayStyle.None;
            }
        }

        public void RefreshAssetEntity(AssetEntity assetEntity)
        {
            //var isEnough = assetEntity.IsEnough(_unitEntity.UpgradeAssetData);
            //_upgradeButton.SetEnabled(isEnough);
        }

        public void CleanUp()
        {
            _rewardButton.UnregisterCallback<ClickEvent>(OnRewardClickedEvent);
            _icon = null;
        }



        #region ##### Listener #####


        private System.Action<int> _rewardEvent;
        public void AddOnRewardListener(System.Action<int> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<int> act) => _rewardEvent -= act;
        private void OnRewardClickedEvent(ClickEvent e)
        {
            _rewardEvent?.Invoke(_index);
        }

        #endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIQuestLine_Test : MonoBehaviour
    {
        private UIQuestLine _instance;
        public UIQuestLine Instance => _instance;

        public static UIQuestLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIQuestLine_Test";
            return obj.AddComponent<UIQuestLine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIQuestLine.PATH_UI_UXML);
            _instance = root.Q<UIQuestLine>();
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