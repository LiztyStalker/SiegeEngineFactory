namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;
    using DG.Tweening;
    

    [RequireComponent(typeof(UIDocument))]
    public class UIHitBlock : MonoBehaviour, IPoolElement
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private Label _label;

        private float _opacity = 1f;

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHitBlock.uxml";

        public void Initialize() {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _label = _uiDocument.rootVisualElement.Q<Label>("hit_label");

            Inactivate();
        }

        public void CleanUp() 
        {
            _retrieveEvent = null;
            Inactivate();
        }

        private Sequence AssembleSequence()
        {
            var scaleT = transform.DOScale(2f, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCirc).OnUpdate(SetScale);
            var opacityT = DOTween.To(() => _opacity, o => _opacity = o, 0, 1f).SetEase(Ease.OutCirc).From(1, true).OnUpdate(SetOpercity);


            return DOTween.Sequence()
                .Append(scaleT)
                .Join(opacityT)
                .OnComplete(() => {
                    _retrieveEvent?.Invoke(this);
                    Inactivate();
                });
        }

        private Sequence AssembleJumpSequence(Vector3 startPosition)
        {
            var endPosition = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), -0.5f);
            var jumpPow = UnityEngine.Random.Range(0.5f, 1f);
            var jumpT = transform.DOJump(startPosition + endPosition, jumpPow, 1, 1f).SetEase(Ease.OutCirc).OnUpdate(SetPosition);
            return DOTween.Sequence().Append(jumpT);
        }


        public void ShowHit(string value, Vector2 position)
        {
            transform.name = value;
            transform.position = position;
        }

        public void Activate()
        {
            _root.style.display = DisplayStyle.Flex;
            transform.localScale = Vector3.one;
            SetPosition();
            SetLabel();

            var seq = AssembleSequence();
            var jumpSeq = AssembleJumpSequence(transform.position);
            jumpSeq.Insert(0f, seq);
            jumpSeq.Restart();
        }

        public void Inactivate()
        {
            _root.style.display = DisplayStyle.None;
        }


        private void SetPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
            _uiDocument.rootVisualElement.transform.position = screenPos;
        }

        private void SetScale()
        {
            _uiDocument.rootVisualElement.transform.scale = transform.localScale;
        }

        private void SetOpercity()
        {
            var opercity = _root.style.opacity;
            opercity.value = _opacity;
            _root.style.opacity = opercity;
        }

        private void SetLabel()
        {
            //Inactivate되면 기존에 연결한 VisualElement가 해제되므로 
            //Activate후 다시 연결할 필요 있음
            _label.text = transform.name;


        }

        //private float nowTime = 0;
        //private void Update()
        //{
        //    nowTime += Time.deltaTime;
        //    if (nowTime > 1f) 
        //    {
        //        nowTime = 0;
        //        _retrieveEvent?.Invoke(this);
        //        Inactivate();
        //    }
        //}

        #region ##### Listener #####

        private System.Action<UIHitBlock> _retrieveEvent;
        public void SetOnRetrieveBlockListener(System.Action<UIHitBlock> act) => _retrieveEvent = act;

        #endregion



        public static UIHitBlock Create()
        {
            var obj = new GameObject();
            obj.name = "UIHitBlock";
            var block = obj.AddComponent<UIHitBlock>();
            var root = UIUXML.GetVisualElement(obj, PATH_UI_UXML);
            //Debug.Log(root.name);
            return block;
        }
    }
}