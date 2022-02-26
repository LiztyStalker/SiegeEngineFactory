namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class UnitTechDataEditor : VisualElement
    {
        public UnitTechDataEditor(SerializedProperty property)
        {
            UnityEngine.Debug.Log("Editor");

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uss");
            this.styleSheets.Add(styleSheet);


            var typeTechUnitField = this.Query<PropertyField>("unit-tech-type-field").First();
            typeTechUnitField.label = "테크타입";
            typeTechUnitField.BindProperty(property.FindPropertyRelative("_typeTechTree"));


            var techUnitKeyField = this.Query<PropertyField>("unit-tech-key-field").First();
            techUnitKeyField.label = "테크유닛키";
            techUnitKeyField.BindProperty(property.FindPropertyRelative("_techUnitKey"));


            var techAssetField = this.Query<PropertyField>("unit-tech-asset-field").First();
            techAssetField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));


        }


        //private UnitTechData _data;

        //public UnitTechDataEditor(UnitTechData data)
        //{
        //    _data = data;

        //    var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uxml");
        //    visualTree.CloneTree(this);

        //    var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uss");
        //    this.styleSheets.Add(styleSheet);



        //    var typeTechUnitField = this.Query<PropertyField>("unit-tech-type-field").First();
        //    typeTechUnitField.label = "테크타입";
        //    typeTechUnitField.BindProperty(property.FindPropertyRelative("_typeTechTree"));


        //    var techUnitKeyField = this.Query<PropertyField>("unit-tech-key-field").First();
        //    techUnitKeyField.label = "테크유닛키";
        //    techUnitKeyField.BindProperty(property.FindPropertyRelative("_techUnitKey"));


        //    var techAssetField = this.Query<PropertyField>("unit-tech-asset-field").First();
        //    techAssetField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));



        //    ObjectField modelField = this.Query<ObjectField>("model-field").First();
        //    modelField.label = "모델";
        //    modelField.objectType = typeof(Spine.Unity.SkeletonDataAsset);
        //    modelField.value = _attackerData.SkeletonDataAsset;
        //    modelField.RegisterCallback<ChangeEvent<Object>>(
        //        e =>
        //        {
        //            _attackerData.SkeletonDataAsset = e.newValue as Spine.Unity.SkeletonDataAsset;
        //        }
        //    );

        //    TextField modelKeyField = this.Query<TextField>("model-key-field").First();
        //    modelKeyField.label = "모델키";
        //    modelKeyField.value = _attackerData.SkeletonDataAssetKey;
        //    modelKeyField.SetEnabled(false);

        //    TextField modelSkinKeyField = this.Query<TextField>("model-skin-field").First();
        //    modelSkinKeyField.label = "스킨키";
        //    modelKeyField.value = _attackerData.SkeletonDataAssetSkinKey;
        //    modelKeyField.RegisterCallback<ChangeEvent<string>>(
        //        e =>
        //        {
        //            _attackerData.SkeletonDataAssetSkinKey = e.newValue;
        //        }
        //    );

        //    VisualElement attackData = this.Query<VisualElement>("attack-data-layout");
        //    attackData.Clear();
        //    attackData.Add(new AttackDataEditor(_attackerData.AttackData));

        //    Vector2Field positionField = this.Query<Vector2Field>("position-field").First();
        //    positionField.label = "위치";
        //    positionField.value = _attackerData.Position;
        //    positionField.RegisterCallback<ChangeEvent<Vector2>>(
        //        e =>
        //        {
        //            _attackerData.Position = e.newValue;
        //        }
        //    );

        //    FloatField scaleField = this.Query<FloatField>("scale-field").First();
        //    scaleField.label = "크기";
        //    scaleField.value = _attackerData.Scale;
        //    scaleField.RegisterCallback<ChangeEvent<float>>(
        //        e =>
        //        {
        //            _attackerData.Scale = e.newValue;
        //        }
        //    );

        //    Button removeButton = this.Query<Button>("remove-button").First();
        //    removeButton.clicked += delegate { _removeEvent?.Invoke(_attackerData); };
        //}

        //#region ##### Listener #####

        //private System.Action<AttackerData> _removeEvent;
        //public void SetOnRemoveListener(System.Action<AttackerData> act) => _removeEvent = act;

        //#endregion
    }
}