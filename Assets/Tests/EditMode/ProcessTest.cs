#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Process;

    public class ProcessTest
    {

        private ProcessPackage _package;

        private class Test1 : IProcessData
        {
            public float ProcessTime => 1f;
        }
        private class Test2 : IProcessData {
            public float ProcessTime => 1.5f;

        }
        private class Test3 : IProcessData {
            public float ProcessTime => 3f;
        }




        [SetUp]
        public void SetUp()
        {
            _package = ProcessPackage.Create();
            _package.Initialize(null);
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
            _package.SetProcessData(new Test1(), 1);
            int count = 0;
            _package.AddOnProcessEvent(delegate
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
            _package.SetProcessData(new Test2(), 1);
            int count = 0;
            _package.AddOnProcessEvent(delegate
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
            _package.SetProcessData(new Test3(), 1);
            int count = 0;
            _package.AddOnProcessEvent(delegate
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
            _package.SetProcessData(new Test1(), 1);
            _package.SetProcessData(new Test2(), 1);
            _package.SetProcessData(new Test3(), 1);
            int count = 0;
            _package.AddOnProcessEvent(delegate
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