namespace SEF.Statistics
{
    public class TechUnitStatisticsData : IStatisticsData { }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class TestTechUnitStatisticsData : IStatisticsData { }
#endif
}