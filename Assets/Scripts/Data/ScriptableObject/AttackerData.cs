namespace SEF.Data
{
    using UnityEngine;
    using Spine.Unity;

    [System.Serializable]
    public class AttackerData
    {
        [SerializeField]
        private SkeletonDataAsset _skeletonDataAsset;
        public SkeletonDataAsset SkeletonDataAsset { get => _skeletonDataAsset; set => _skeletonDataAsset = value; }


        [SerializeField]
        private string _skeletonDataAssetKey;
        public string SkeletonDataAssetKey { get => _skeletonDataAssetKey; set => _skeletonDataAssetKey = value; }

        [SerializeField]
        private string _skeletonDataAssetSkinKey;
        public string SkeletonDataAssetSkinKey { get => _skeletonDataAssetSkinKey; set => _skeletonDataAssetSkinKey = value; }


        [SerializeField]
        private AttackData _attackData;
        public AttackData AttackData { get => _attackData; set => _attackData = value; }


        [SerializeField]
        private Vector2 _position;
        public Vector2 Position { get => _position; set => _position = value; }


        [SerializeField]
        private float _scale;
        public float Scale { get => _scale; set => _scale = value; }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public void SetPositionAndScale_Test(Vector2 position, float scale)
        {
            _position = position;
            _scale = scale;
        }

        public static AttackerData Create_Test()
        {
            return new AttackerData();
        }

        private AttackerData()
        {
            _skeletonDataAssetKey = "BowSoldier_SkeletonData";
            _attackData = AttackData.Create_Test();
            _position = Vector2.zero;
            _scale = 0.3f;
        }
#endif
    }
}