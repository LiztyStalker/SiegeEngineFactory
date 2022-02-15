
#if UNITY_EDITOR && UNITY_INCLUDE_TESTS
namespace SEF.UI.Test
{

    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using SEF.UI.Toolkit;
    using Storage;
    using SEF.Account;

    public class UIMainTest
    {

        private Camera _camera;


        private void CreateCamera()
        {
            var obj = new GameObject();
            obj.name = "Camera";
            obj.transform.position = Vector3.back * 10f;
            _camera = obj.AddComponent<Camera>();

        }

        private void DestoryCamera()
        {
            Object.DestroyImmediate(_camera.gameObject);
        }

        private UIMain CreateUIMain()
        {
            var uiMain = UIMain.Create();
            uiMain.Initialize();
            return uiMain;
        }

        private void DestoryUIMain(UIMain uiMain)
        {
            uiMain.CleanUp();
        }

        private UIStart CreateUIStart()
        {
            var uiStart = UIStart.Create();
            uiStart.Initialize(null);
            return uiStart;
        }

        private void DestoryUIStart(UIStart uiStart)
        {
            uiStart.CleanUp();
        }

        private UILoad CreateUILoad()
        {
            var uiLoad = UILoad.Create();
            uiLoad.Initialize(null);
            return uiLoad;
        }

        private void DestoryUILoad(UILoad uiLoad)
        {
            uiLoad.CleanUp();
        }



        [SetUp]
        public void SetUp()
        {
            CreateCamera();
        }

        [TearDown]
        public void TearDown()
        {
            DestoryCamera();
        }

        [UnityTest]
        public IEnumerator UIMainTest_UIMain()
        {
            bool isClosed = true;
            var uiMain = CreateUIMain();
            yield return null;
        }

        [UnityTest]
        public IEnumerator UIMainTest_DataLoader_LoadTest()
        {
            bool isClosed = true;
            var loader = DataLoader.Create();
            loader.LoadTest(progress => Debug.Log(progress), result => {
                isClosed = false;
                Debug.Log(result); 
            });
            while (isClosed)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator UIMainTest_DataLoader_LoadAssetBundle()
        {
            bool isClosed = true;
            var loader = DataLoader.Create();
            loader.Load(progress => Debug.Log(progress), result => {
                isClosed = false;
                Debug.Log(result);
            });
            while (isClosed)
            {
                yield return null;
            }
            loader.Dispose();
        }

        [UnityTest]
        public IEnumerator UIMainTest_Account_Load()
        {            
            bool isClosed = true;
            Account.Current.LoadData(progress => Debug.Log(progress), result => {
                isClosed = false;
                Debug.Log(result);
            });
            while (isClosed)
            {
                yield return null;
            }
        }


        [UnityTest]
        public IEnumerator UIMainTest_Account_Save()
        {
            bool isClosed = true;
            Account.Current.SaveData(null, delegate
            {
                Debug.Log("Save End");
                isClosed = false;
            });
            while (isClosed)
            {
                yield return null;
            }
        }

        //게임 시작


        [UnityTest]
        public IEnumerator UIMainTest_UIStart()
        {
            bool isClosed = true;
            var uiStart = CreateUIStart();
            uiStart.ShowStart(delegate
            {
                Debug.Log("Start Callback");
                isClosed = false;
            });

            while (isClosed)
            {
                yield return null;
            }
            uiStart.Hide();
            DestoryUIStart(uiStart);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UIMainTest_UILoad()
        {
            bool isClosed = true;
            var uiLoad = CreateUILoad();
            float nowTime = 0f;

            while (isClosed)
            {
                uiLoad.ShowLoad(nowTime);
                nowTime += Time.deltaTime;
                if(nowTime > 1f)
                {
                    isClosed = false;
                }
                yield return null;
            }
            uiLoad.Hide();
            DestoryUILoad(uiLoad);

            yield return null;
        }

    }
}
#endif