#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Research;

    public class LaboratoryTest
    {

        private LaboratoryPackage _package;

        private class Test1 : IResearchData { }
        private class Test2 : IResearchData { }
        private class Test3 : IResearchData { }




        [SetUp]
        public void SetUp()
        {
            _package = LaboratoryPackage.Create();
            _package.Initialize(null);
        }

        [TearDown]
        public void TearDown()
        {
            _package.CleanUp();
        }
                
        [Test]
        public void LaboratoryTest_Initialize()
        {

        }

        [Test]
        public void LaboratoryTest_SetResearchData_Test1()
        {
            _package.SetResearchData<Test1>(1);
            Assert.IsTrue(_package.HasResearchData<Test1>());
        }

        [Test]
        public void LaboratoryTest_SetResearchData_Test2_x2()
        {
            _package.SetResearchData<Test2>(2);
            Assert.IsTrue(_package.HasResearchData<Test2>(2));
        }

        [Test]
        public void LaboratoryTest_SetResearchData_Test1_Test2()
        {
            _package.SetResearchData<Test1>(1);
            _package.SetResearchData<Test2>(1);
            Assert.IsTrue(_package.HasResearchData<Test1>(1));
            Assert.IsTrue(_package.HasResearchData<Test2>(1));
        }

        [Test]
        public void LaboratoryTest_SetResearchData_Test3_5()
        {
            _package.SetResearchData<Test3>(5);
            Assert.IsTrue(_package.HasResearchData<Test3>(5));
        }

        private void Initialize_Test()
        {
            _package.SetResearchData<Test1>(1);
            _package.SetResearchData<Test2>(2);
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test1_1()
        {
            Initialize_Test();
            Assert.IsTrue(_package.HasResearchData<Test1>(1));
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test1_2()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasResearchData<Test1>(2));
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test2_1()
        {
            Initialize_Test();
            Assert.IsTrue(_package.HasResearchData<Test2>(1));
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test2_3()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasResearchData<Test2>(3));
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test3_1()
        {
            Initialize_Test();
            Assert.IsFalse(_package.HasResearchData<Test3>(1));
        }

        [Test]
        public void LaboratoryTest_HasResearchData_Test1_n1()
        {
            Initialize_Test();
            try
            {
                _package.HasResearchData<Test1>(-1);
                Assert.Fail("음수 적용 불가");
            }
            catch
            {
                Assert.Pass();
            }
        }













    }
}
#endif