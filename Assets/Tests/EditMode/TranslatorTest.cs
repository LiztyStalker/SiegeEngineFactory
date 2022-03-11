#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using NUnit.Framework;
    using Storage;
    using SEF.Data;
    using SEF.Entity;
    using SEF.Manager;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Statistics;
    using SEF.Quest;
    using SEF.Unit;

    public class TranslatorTest
    {

        [TearDown]
        public void TearDown()
        {
            TranslateStorage.Dispose();
        }


        [Test]
        public void TranslatorTest_Initialize()
        {
            Assert.IsNotNull(TranslateStorage.Instance);
        }

        [Test]
        public void TranslatorTest_GetTranslateData()
        {
            Debug.Log(TranslateStorage.Instance.GetTranslateData("Quest_Challenge_Data_Tr", "AccUpgradeUnit", "Name", 0));
        }

        [Test]
        public void TranslatorTest_GetTranslateData_System()
        {
            Debug.Log(TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Ok"));
        }

    }
}
#endif