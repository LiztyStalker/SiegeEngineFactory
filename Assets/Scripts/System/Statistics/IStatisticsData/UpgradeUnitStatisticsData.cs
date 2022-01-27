namespace SEF.Statistics
{
    public class UpgradeUnitStatisticsData : IStatisticsData { }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class TestUpgradeUnitStatisticsData : IStatisticsData { }
#endif
}