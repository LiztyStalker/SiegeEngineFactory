namespace SEF.UI.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.Experimental.Rendering.Universal;
    using UtilityManager.Test;
    using Entity;
    using Data;
    using Quest;
#if INCLUDE_UI_TOOLKIT
    using Toolkit;
#endif

    public class UIQuestTest
    {
        private Camera _camera;
        private Light2D _light;

        //private UIQuest _uiQuest;
        //private UIQuestTab _uiQuestTab;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();

//            _uiQuest = UIQuest.Create();
//            _uiQuestTab = UIQuestTab.Create();

            //_uiQuest.Initialize();
            //_uiQuestTab.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            //_uiQuest.CleanUp();
           // _uiQuestTab.CleanUp();

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }


        [UnityTest]
        public IEnumerator UIQuestTab_Initialize()
        {
            var _uiQuestTab = UIQuestTab.Create();
            _uiQuestTab.Initialize();
            yield return new WaitForSeconds(1f);
            _uiQuestTab.CleanUp();
        }


        [UnityTest]
        public IEnumerator UIQuestTab_Refresh()
        {

            var _uiQuestTab = UIQuestTab.Create();
            _uiQuestTab.Initialize();

            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Goal, typeof(CreateUnitConditionQuestData), 10, typeof(GoldAssetData), 100);

            var entity = new QuestEntity();
            entity.Initialize();
            entity.SetData(data);

            _uiQuestTab.RefreshQuest(entity);
            yield return new WaitForSeconds(1f);
            _uiQuestTab.CleanUp();
        }

        [UnityTest]
        public IEnumerator UIQuestTab_Refresh_Goal()
        {

            var _uiQuestTab = UIQuestTab.Create();
            _uiQuestTab.Initialize();

            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Goal, typeof(CreateUnitConditionQuestData), 10, typeof(GoldAssetData), 100);

            var entity = new QuestEntity();
            entity.Initialize();
            entity.SetData(data);

            while (true)
            {
                entity.AddQuestValue();
                _uiQuestTab.RefreshQuest(entity);
                if (entity.HasQuestGoal())
                    break;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            _uiQuestTab.CleanUp();
        }


        [UnityTest]
        public IEnumerator UIQuestTab_Refresh_Rewarded()
        {

            var _uiQuestTab = UIQuestTab.Create();
            _uiQuestTab.Initialize();

            var data = QuestData.Create_Test("Test", QuestData.TYPE_QUEST_GROUP.Goal, typeof(CreateUnitConditionQuestData), 10, typeof(GoldAssetData), 100);

            var entity = new QuestEntity();
            entity.Initialize();
            entity.SetData(data);

            entity.SetRewarded(true);

            _uiQuestTab.RefreshQuest(entity);

            yield return new WaitForSeconds(1f);
            _uiQuestTab.CleanUp();
        }


        //[UnityTest]
        //public IEnumerator UIHitTest_ShowHitContainer_10()
        //{
        //    int count = 0;

        //    yield return new WaitForSeconds(1f);

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Vector2 pos = new Vector2(UnityEngine.Random.Range(-2f, 2f), 2f);
        //        _uiHitContainer.ShowHit_Test("1.234A", pos, block =>
        //        {
        //            Debug.Log("End");
        //            count++;
        //        });
        //        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
        //    }

        //    while (true)
        //    {
        //        if (count >= 10)
        //            break;
        //        yield return null;
        //    }
        //}

    }
}