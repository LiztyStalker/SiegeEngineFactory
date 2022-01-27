#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Statistics;

    public class StatisticsTest
    {

        private StatisticsPackage _package;

        private class StatisticsTest1 : IStatisticsData { }
        private class StatisticsTest2 : IStatisticsData { }
        private class StatisticsTest3 : IStatisticsData { }

        [SetUp]
        public void SetUp()
        {
            _package = StatisticsPackage.Create();
            _package.Initialize(null);
        }

        [TearDown]
        public void TearDown()
        {
            _package.CleanUp();
        }

        [Test]
        public void StatisticsTest_Initialize()
        {

        }

        [Test]
        public void StatisticsTest_AddStatisticsData_AddStatisticsData()
        {
            _package.AddStatisticsData<StatisticsTest1>();
            Debug.Log(_package.GetStatisticsValue<StatisticsTest1>().Value);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest1>().Value == 1);
        }

        [Test]
        public void StatisticsTest_AddStatisticsData_AddStatisticsData_x2()
        {
            _package.AddStatisticsData<StatisticsTest1>();
            _package.AddStatisticsData<StatisticsTest1>();
            Debug.Log(_package.GetStatisticsValue<StatisticsTest1>().Value);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest1>().Value == 2);
        }

        [Test]
        public void StatisticsTest_AddStatisticsData_AddStatisticsData_Test1_Test2()
        {
            _package.AddStatisticsData<StatisticsTest1>();
            _package.AddStatisticsData<StatisticsTest2>();
            Debug.Log(_package.GetStatisticsValue<StatisticsTest1>().Value);
            Debug.Log(_package.GetStatisticsValue<StatisticsTest2>().Value);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest1>().Value == 1);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest2>().Value == 1);
        }

        [Test]
        public void StatisticsTest_SetStatisticsData_SetStatistics_x5()
        {
            _package.SetStatisticsData<StatisticsTest3>(1);
            Debug.Log(_package.GetStatisticsValue<StatisticsTest3>().Value);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest3>().Value == 1);
            _package.SetStatisticsData<StatisticsTest3>(5);
            Debug.Log(_package.GetStatisticsValue<StatisticsTest3>().Value);
            Assert.IsTrue(_package.GetStatisticsValue<StatisticsTest3>().Value == 5);
        }


        [Test]
        public void StatisticsTest_HasType() 
        {
            var test = new StatisticsTest1();
            Debug.Log(test.GetType().FullName);
            var type1 = System.Type.GetType($"SEF.Test.StatisticsTest+StatisticsTest1");
            Debug.Log(type1);
            var type2 = System.Type.GetType($"SEF.Test.StatisticsTest+StatisticsTest4");
            Debug.Log(type2);

        }











    }
}
#endif