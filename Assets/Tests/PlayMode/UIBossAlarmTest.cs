namespace SEF.UI.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;
    using SEF.UI.Toolkit;

    public class UIBossAlarmTest
    {
        private Camera _camera;
        private Light2D _light;

        private UIBossAlarm_Test _uitest;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _uitest = UIBossAlarm_Test.Create();
            Assert.IsNotNull(_uitest);
            _uitest.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _uitest.Dispose();
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIBossAlarmTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }



        [UnityTest]
        public IEnumerator UIBossAlarmTest_ShowBoss()
        {
            _uitest.Instance.ShowBoss();
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIBossAlarmTest_ShowThemeBoss()
        {
            _uitest.Instance.ShowThemeBoss();
            yield return new WaitForSeconds(1f);
        }

    }
}