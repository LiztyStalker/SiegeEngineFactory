namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using PoolSystem;
    using DG.Tweening;
    

    public class UIHitBlock : MonoBehaviour, IPoolElement
    {
        [SerializeField]
        private Text _label;

        private float _opacity = 1f;

        public void Initialize() 
        {
            _label = GetComponentInChildren<Text>();
            Inactivate();
        }

        public void CleanUp() 
        {
            _retrieveEvent = null;
            Inactivate();
        }

        //private Sequence AssembleSequence()
        //{
        //    var scaleT = transform.DOScale(2f, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCirc).OnUpdate(SetScale);
        //    var opacityT = DOTween.To(() => _opacity, o => _opacity = o, 0, 1f).SetEase(Ease.OutCirc).From(1, true).OnUpdate(SetOpercity);


        //    return DOTween.Sequence()
        //        .Append(scaleT)
        //        .Join(opacityT)
        //        .OnComplete(() => {
        //            _retrieveEvent?.Invoke(this);
        //            Inactivate();
        //        });
        //}

        //private Sequence AssembleJumpSequence(Vector3 startPosition)
        //{
        //    var endPosition = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), -0.5f);
        //    var jumpPow = UnityEngine.Random.Range(0.5f, 1f);
        //    var jumpT = transform.DOJump(startPosition + endPosition, jumpPow, 1, 1f).SetEase(Ease.OutCirc).OnUpdate(SetPosition);
        //    return DOTween.Sequence().Append(jumpT);
        //}


        public void ShowHit(string value, Vector2 position)
        {
            transform.name = value;
            transform.position = position;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            SetPosition();
            SetLabel();

            //var seq = AssembleSequence();
            //var jumpSeq = AssembleJumpSequence(transform.position);
            //jumpSeq.Insert(0f, seq);
            //jumpSeq.Restart();
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
        }


        private void SetPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
            transform.position = screenPos;
//            _uiDocument.rootVisualElement.transform.position = screenPos;
        }

        //private void SetScale()
        //{
            //transform.localScale = 
            //_uiDocument.rootVisualElement.transform.scale = transform.localScale;
        //}

        //private void SetOpercity()
        //{
            //var opercity = _root.style.opacity;
            //opercity.value = _opacity;
            //_root.style.opacity = opercity;
        //}

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
//            var root = UIUXML.GetVisualElement(obj, PATH_UI_UXML);
            //Debug.Log(root.name);
            return block;
        }
    }
}