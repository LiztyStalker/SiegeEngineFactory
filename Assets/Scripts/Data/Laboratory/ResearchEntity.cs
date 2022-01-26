namespace SEF.Research
{
    public struct ResearchEntity
    {
        private System.Type _type;
        private int _value;

        private ResearchEntity(System.Type type)
        {
            _type = type;
            _value = 0;
        }

        internal void SetResearchData(int value)
        {
            _value = value;
        }
        internal int GetResearchValue() => _value;
        internal System.Type GetResearchType() => _type;

        internal static ResearchEntity Create<T>() where T : IResearchData
        {
            return new ResearchEntity(typeof(T));
        }
    }
}