#if UNITY_EDITOR
namespace SEF.UI.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(UIAssetButton))]
    [CanEditMultipleObjects]
    public class UIAssetButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
#endif