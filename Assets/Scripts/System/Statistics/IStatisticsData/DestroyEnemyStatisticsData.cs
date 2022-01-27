namespace SEF.Statistics
{
    public class DestroyEnemyStatisticsData : IStatisticsData { }

#if UNITY_EDITOR || UNITY_INCLUDE_TEST
    public class TestDestroyEnemyStatisticsData : IStatisticsData { }
#endif
}