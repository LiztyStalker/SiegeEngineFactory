namespace SEF.Statistics
{
    using System.Numerics;

    internal struct StatisticsEntity
    {
        private System.Type _type;
        private BigInteger _value;

        private StatisticsEntity(System.Type type)
        {
            _type = type;
            _value = new BigInteger();
        }

        internal void AddStatisticsData(BigInteger value)
        {
            _value += value;
        }
        internal void SetStatisticsData(BigInteger value)
        {
            _value = value;
        }
        internal BigInteger GetStatisticsValue() => _value;
        internal System.Type GetStatisticsType() => _type;

        internal static StatisticsEntity Create<T>() where T : IStatisticsData
        {
            return new StatisticsEntity(typeof(T));
        }
    }
}