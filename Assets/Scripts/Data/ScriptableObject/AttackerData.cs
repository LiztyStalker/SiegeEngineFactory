namespace SEF.Data
{
    using UnityEngine;
    using Spine.Unity;

    [CreateAssetMenu(fileName = "AttackerData", menuName = "ScriptableObjects/AttackerData")]

    public class AttackerData : ScriptableObject
    {
        [SerializeField]
        private SkeletonDataAsset _skeletonDataAsset;
        public SkeletonDataAsset SkeletonDataAsset => _skeletonDataAsset;


        [SerializeField]
        private string _skeletonDataAssetKey;
        public string SkeletonDataAssetKey => _skeletonDataAssetKey;


        [SerializeField]
        private AttackData _attackData;
        public AttackData AttackData => _attackData;


        [SerializeField]
        private Vector2 _position;
        public Vector2 Position => _position;


        [SerializeField]
        private float _scale;
        public float Scale => _scale;

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
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