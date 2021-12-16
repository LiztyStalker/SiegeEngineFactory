namespace UtilityManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Storage;

    [CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData")]
    public class BulletData : ScriptableObject //IComponentData
    {
        public enum TYPE_BULLET_ACTION { Move, Curve, Drop, Direct }

        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private string _bulletPrefabKey;

        [SerializeField]
        private TYPE_BULLET_ACTION _typeBulletAction;

        [SerializeField]
        private EffectData _arriveEffectData;

        [SerializeField]
        private string _arriveEffectDataKey;

        [SerializeField]
        private float _movementSpeed = 1f;

        [SerializeField]
        private bool _isRotate = false;

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static BulletData CreateTest()
        {
            return new BulletData();
        }

        public void SetData(TYPE_BULLET_ACTION typeBulletAction, float movementSpeed, bool isRotate)
        {
            _typeBulletAction = typeBulletAction;
            _movementSpeed = movementSpeed;
            _isRotate = isRotate;
        }

        private static Sprite _instanceSprite;

        private BulletData()
        {
            var obj = new GameObject();
            obj.name = "Data@Bullet";
            var sprite = obj.AddComponent<SpriteRenderer>();


            if (_instanceSprite == null)
            {
                Texture2D texture = new Texture2D(100, 100);

                for (int y = 0; y < texture.height; y++)
                {
                    for (int x = 0; x < texture.width; x++)
                    {
                        texture.SetPixel(x, y, Color.white);
                    }
                }
                _instanceSprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), Vector2.one * 0.5f);
            }

            sprite.sprite = _instanceSprite;
            _bulletPrefab = obj;
            _bulletPrefab.gameObject.SetActive(false);
        }
#endif


        #region ##### Getter Setter #####
        public GameObject prefab
        {
            get
            {
                if (_bulletPrefab == null && !string.IsNullOrEmpty(_bulletPrefabKey))
                    _bulletPrefab = DataStorage.Instance.GetDataOrNull<GameObject>(_bulletPrefabKey, "Bullet", null);
                return _bulletPrefab;
            }
        }
        public EffectData ArriveEffectData
        {
            get
            {
                if (_arriveEffectData == null && !string.IsNullOrEmpty(_arriveEffectDataKey))
                    _arriveEffectData = DataStorage.Instance.GetDataOrNull<EffectData>(_arriveEffectDataKey);
                return _arriveEffectData;
            }
        }
        public bool IsRotate => _isRotate;
        public float MovementSpeed => _movementSpeed;
        public TYPE_BULLET_ACTION TypeBulletAction => _typeBulletAction;

        #endregion

    }
}