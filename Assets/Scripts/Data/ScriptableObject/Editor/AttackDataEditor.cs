namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class AttackDataEditor : VisualElement
    {
        AttackData _attackData;

        public AttackDataEditor(AttackData attackData)
        {
            this._attackData = attackData;

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Editor/AttackDataEditor.uxml");
            visualTree.CloneTree(this);

            StyleSheet stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Editor/AttackDataEditor.uss");
            this.styleSheets.Add(stylesheet);


            TextField damageValueField = this.Query<TextField>("damage-value-field").First();
            damageValueField.label = "공격력";
            damageValueField.value = _attackData.DamageValue.GetValue();
            damageValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _attackData.DamageValue.ValueText = e.newValue;
                }
            );


            IntegerField increaseDamageValueField = this.Query<IntegerField>("increase-damage-value-field").First();
            increaseDamageValueField.label = "증가량";
            increaseDamageValueField.value = _attackData.IncreaseDamageValue;
            increaseDamageValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _attackData.IncreaseDamageValue = e.newValue;
                }
            );

            FloatField increaseDamageRateField = this.Query<FloatField>("increase-damage-rate-field").First();
            increaseDamageRateField.label = "증가율";
            increaseDamageRateField.value = _attackData.IncreaseDamageRate;
            increaseDamageRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _attackData.IncreaseDamageRate = e.newValue;
                }
            );


            IntegerField attackCountField = this.Query<IntegerField>("attack-count-field").First();
            attackCountField.label = "공격횟수";
            attackCountField.value = _attackData.AttackCount;
            attackCountField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _attackData.AttackCount = e.newValue;
                }
            );

            FloatField attackDelayField = this.Query<FloatField>("attack-delay-field").First();
            attackDelayField.label = "딜레이";
            attackDelayField.value = _attackData.IncreaseDamageRate;
            attackDelayField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _attackData.AttackDelay = e.newValue;
                }
            );
        }
    }
}