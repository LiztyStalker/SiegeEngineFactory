#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Process;
    using SEF.Data;

    public class ProcessTest
    {

        private ProcessPackage _package;

        private class Provider_Test : IProcessProvider { }

        private class Test1 : IProcessData
        {
            public float ProcessTime => 1f;

            public void SetValue(IAssetData data, float increaseValue, float processTime)
            {
            }
        }
        private class Test2 : IProcessData {
            public float ProcessTime => 1.5f;
            public void SetValue(IAssetData data, float increaseValue, float processTime)
            {
            }

        }
        private class Test3 : IProcessData {
            public float ProcessTime => 3f;
            public void SetValue(IAssetData data, float increaseValue, float processTime)
            {
            }
        }




        [SetUp]
        public void SetUp()
        {
            _package = ProcessPackage.Create();
            _package.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _package.CleanUp();
        }
                
        [Test]
        public void ProcessTest_Initialize()
        {

        }

        [UnityTest]
        public IEnumerator ProcessTest_SetProcessData_Test1()
        {
            var entity = new ProcessEntity(new Test1(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(),  entity);
            int count = 0;
            _package.AddOnCompleteProcessEvent(delegate
            {
                Debug.Log("End");
                count++;
            });
            while (true)
            {
                _package.RunProcess(Time.deltaTime);
                yield return null;
                if(count == 3)
                {
                    break;
                }
            }
            yield return null;
        }
        [UnityTest]
        public IEnumerator ProcessTest_SetProcessData_Test2()
        {
            var entity = new ProcessEntity(new Test2(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(), entity);

            int count = 0;
            _package.AddOnCompleteProcessEvent(delegate
            {
                Debug.Log("End");
                count++;
            });
            while (true)
            {
                _package.RunProcess(Time.deltaTime);
                yield return null;
                if (count == 2)
                {
                    break;
                }
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator ProcessTest_SetProcessData_Test3()
        {
            var entity = new ProcessEntity(new Test3(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(), entity);
            int count = 0;
            _package.AddOnCompleteProcessEvent(delegate
            {
                Debug.Log("End");
                count++;
            });
            while (true)
            {
                _package.RunProcess(Time.deltaTime);
                yield return null;
                if (count == 1)
                {
                    break;
                }
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator ProcessTest_SetProcessData_Test1_Test2_Test3()
        {
            var entity1 = new ProcessEntity(new Test1(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(), entity1);

            var entity2 = new ProcessEntity(new Test2(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(), entity2);

            var entity3 = new ProcessEntity(new Test3(), NumberDataUtility.Create<UpgradeData>());
            _package.SetProcessEntity(new Provider_Test(), entity3);

            int count = 0;
            _package.AddOnCompleteProcessEvent(delegate
            {
                Debug.Log("End");
                count++;
            });
            while (true)
            {
                _package.RunProcess(Time.deltaTime);
                yield return null;
                if (count == 6)
                {
                    break;
                }
            }
            yield return null;
        }

    }
}
#endif