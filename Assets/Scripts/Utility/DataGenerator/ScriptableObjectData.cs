namespace Utility.Data
{
    using UnityEngine;

    public abstract class ScriptableObjectData : ScriptableObject
    {
        [SerializeField]
        private int _sortIndex;
        public int SortIndex { get => _sortIndex; protected set => _sortIndex = value; }

        public void SetSortIndex(int index) => SortIndex = index;
        public abstract void SetData(string[] arr);
        public abstract string[] GetData();

        public virtual void SetAssetBundle(string bundleName)
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(this);
            UnityEditor.AssetImporter importer = UnityEditor.AssetImporter.GetAtPath(path);
            importer.SetAssetBundleNameAndVariant(bundleName, "");
        }
    }
}