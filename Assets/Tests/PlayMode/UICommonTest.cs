using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SEF.UI.Toolkit;

public class UICommonTest
{
    private Camera _camera;
    private UIPopup _uiPopup;


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

    [SetUp]
    public void SetUp()
    {
        CreateCamera();
        _uiPopup = UIPopup.Create();
        _uiPopup.Initialize();
    }

    [TearDown]
    public void TearDown()
    {
        DestoryCamera();
        _uiPopup.CleanUp();
        _uiPopup = null;
    }

    [UnityTest]
    public IEnumerator UICommonTest_UIPopup_Normal()
    {
        bool isClicked = false;
        _uiPopup.ShowPopup("UIPopup Normal Test", delegate
        {
            Debug.Log("Normal ClosedEvent Callback");
            isClicked = true;
        });

        while (!isClicked) {
            yield return null;
        }
    }

    [UnityTest]
    public IEnumerator UICommonTest_UIPopup_Apply()
    {
        bool isClicked = false;
        _uiPopup.ShowPopup("UIPopup Apply Test", "확인", delegate
        {
            Debug.Log("Apply Callback");
            isClicked = true;
        });
        while (!isClicked)
        {
            yield return null;
        }
    }

    [UnityTest]
    public IEnumerator UICommonTest_UIPopup_ApplyCancel()
    {
        bool isClicked = false;
        _uiPopup.ShowPopup("UIPopup ApplyCancel Test", "확인", "취소", delegate {
            Debug.Log("Apply Callback");
            isClicked = true;
        }, delegate {
            Debug.Log("Cancel Callback");
            isClicked = true;
        });
        while (!isClicked)
        {
            yield return null;
        }
    }


}
