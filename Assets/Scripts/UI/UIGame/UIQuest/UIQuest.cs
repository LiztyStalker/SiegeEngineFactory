namespace SEF.UI.Toolkit
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;

    public class UIQuest : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIQuest, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIQuest/UIQuest.uxml";

        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UIQuest/UIQuest.uss";

        private Dictionary<int, UIQuestLine> _dic = new Dictionary<int, UIQuestLine>();

        private ScrollView _scrollView;

        private Button _exitButton;

        private QuestData.TYPE_QUEST_GROUP _typeQuestGroup;

        public static UIQuest Create()
        {
            return UIUXML.GetVisualElement<UIQuest>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
            _exitButton = this.Q<Button>("exit-button");

            _exitButton.RegisterCallback<ClickEvent>(e => { Hide(); });

            Hide();
        }

        public void CleanUp()
        {
            foreach (var value in _dic.Values)
            {
                value.RemoveOnRewardListener(OnRewardEvent);
                value.CleanUp();
            }

            _dic.Clear();
            _scrollView = null;

            _exitButton.UnregisterCallback<ClickEvent>(e => { Hide(); });
        }

        public void Show()
        {
            this.parent.style.display = DisplayStyle.Flex;
            this.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            this.parent.style.display = DisplayStyle.None;
            this.style.display = DisplayStyle.None;
        }

        public void RefreshQuest(QuestEntity entity)
        {
            //if (entity.TypeQuestGroup == _typeQuestGroup)
            //{
            Debug.Log(entity.Key);
                var hashcode = entity.GetHashCode();
                if (!_dic.ContainsKey(hashcode))
                {
                    var line = UIQuestLine.Create();
                    line.Initialize();
                    line.AddOnRewardListener(OnRewardEvent);
                    _scrollView.Add(line);
                    _dic.Add(hashcode, line);
                }
                _dic[hashcode].RefreshQuestLine(entity);
            //}
        }

        #region ##### Listener #####


        private System.Action<QuestData.TYPE_QUEST_GROUP, string> _rewardEvent;
        public void AddOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string> act) => _rewardEvent -= act;
        private void OnRewardEvent(string key)
        {
            _rewardEvent?.Invoke(_typeQuestGroup, key);
        }

        #endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIQuest_Test : MonoBehaviour
    {
        private UIQuest _instance;
        public UIQuest Instance => _instance;

        public static UIQuest_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIQuest_Test";
            return obj.AddComponent<UIQuest_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIQuest.PATH_UI_UXML);
            _instance = root.Q<UIQuest>();
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