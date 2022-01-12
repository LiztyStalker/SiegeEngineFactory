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
            bool isRun = true;
            _uitest.Instance.SetOnClosedEvent(delegate
            {
                Debug.Log("End");
                isRun = false;
            });

            _uitest.Instance.ShowAlarm(Data.TYPE_ENEMY_GROUP.Boss);


            while (isRun)
            {
                _uitest.Instance.RunProcess_Test(Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIBossAlarmTest_ShowThemeBoss()
        {
            bool isRun = true;
            _uitest.Instance.SetOnClosedEvent(delegate
            {
                Debug.Log("End");
                isRun = false;
            });

            _uitest.Instance.ShowAlarm(Data.TYPE_ENEMY_GROUP.ThemeBoss);


            while (isRun)
            {
                _uitest.Instance.RunProcess_Test(Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator UIBossAlarmTest_ShowNormal()
        {
            _uitest.Instance.ShowAlarm(Data.TYPE_ENEMY_GROUP.Normal);
            yield return new WaitForSeconds(1f);
        }

    }
}