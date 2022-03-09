namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;


    public class UIEnemyHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Text _name;

        [SerializeField]
        private Text _value;

        [SerializeField]
        private Slider _slider;


        public void ShowName(string name)
        {
            _name.text = name;
        }
        public void ShowHealth(string value, float rate)
        {
            _value.text = value;
            _slider.value = rate;
        }
    }
}