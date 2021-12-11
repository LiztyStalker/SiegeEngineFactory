using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SEF.UI;
using SEF.UI.Toolkit;

public class UICommonTest
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


    private UIPopup CreateUIPopup()
    {
        var _uiPopup = UIPopup.Create();
        _uiPopup.Initialize();
        return _uiPopup;
    }

    private void DestoryUIPopup(UIPopup uiPopup)
    {
        uiPopup.CleanUp();
    }

    private UISettings CreateUISettings()
    {
        var _uiSettings = UISettings.Create();
        _uiSettings.Initialize();
        return _uiSettings;
    }

    private void DestoryUISettings(UISettings uiSettings)
    {
        uiSettings.CleanUp();
    }

    private UICredits CreateUICredits()
    {
        var uiCredits = UICredits.Create();
        uiCredits.Initialize();
        return uiCredits;
    }

    private void DestoryUICredits(UICredits uiCredits)
    {
        uiCredits.CleanUp();
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
    public IEnumerator UICommonTest_UIPopup_Normal()
    {
        bool isClicked = false;
        var uiPopup = CreateUIPopup();
        uiPopup.ShowPopup("UIPopup Normal Test", delegate
        {
            Debug.Log("Normal ClosedEvent Callback");
            isClicked = true;
        });

        while (!isClicked) {
            yield return null;
        }
        DestoryUIPopup(uiPopup);
    }

    [UnityTest]
    public IEnumerator UICommonTest_UIPopup_Apply()
    {
        bool isClicked = false;
        var uiPopup = CreateUIPopup();
        uiPopup.ShowPopup("UIPopup Apply Test", "확인", delegate
        {
            Debug.Log("Apply Callback");
            isClicked = true;
        });
        while (!isClicked)
        {
            yield return null;
        }
        DestoryUIPopup(uiPopup);
    }

    [UnityTest]
    public IEnumerator UICommonTest_UIPopup_ApplyCancel()
    {
        bool isClicked = false;
        var uiPopup = CreateUIPopup();
        uiPopup.ShowPopup("UIPopup ApplyCancel Test", "확인", "취소", delegate {
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
        DestoryUIPopup(uiPopup);
    }

    [UnityTest]
    public IEnumerator UICommonTest_UISettings_Show()
    {
        bool isClicked = false;
        var uiSettings = CreateUISettings();
        uiSettings.Initialize();
        uiSettings.Show(delegate {
            Debug.Log("closed Callback");
            isClicked = true;
        });
        while (!isClicked)
        {
            yield return null;
        }
        DestoryUISettings(uiSettings);

    }

    [UnityTest]
    public IEnumerator UICommonTest_UICredits_Show()
    {
        bool isClicked = false;
        var uiCredits = CreateUICredits();
        uiCredits.Initialize();
        uiCredits.Show(delegate {
            Debug.Log("closed Callback");
            isClicked = true;
        });
        while (!isClicked)
        {
            yield return null;
        }
        DestoryUICredits(uiCredits);

    }


    [UnityTest]
    public IEnumerator UICommonTest_UICommonToPopupNormal()
    {

        bool isClicked = false;
        UICommon.Current.ShowPopup("UIPopup Normal Test", delegate
        {
            Debug.Log("Normal ClosedEvent Callback");
            isClicked = true;
        });

        while (!isClicked)
        {
            yield return null;
        }
        UICommon.Current.CleanUp();
    }

    [UnityTest]
    public IEnumerator UICommonTest_UICommonToPopupApply()
    {

        bool isClicked = false;
        UICommon.Current.ShowPopup("UIPopup Apply Test", "확인", delegate
        {
            Debug.Log("Apply Callback");
            isClicked = true;
        });

        while (!isClicked)
        {
            yield return null;
        }
        UICommon.Current.CleanUp();
    }

    [UnityTest]
    public IEnumerator UICommonTest_UICommonToPopupApplyCancel()
    {

        bool isClicked = false;
        UICommon.Current.ShowPopup("UIPopup ApplyCancel Test", "확인", "취소", delegate {
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
        UICommon.Current.CleanUp();
    }



    [UnityTest]
    public IEnumerator UICommonTest_UICommonToSettings()
    {

        bool isClicked = false;
        UICommon.Current.ShowSettings(delegate {
            Debug.Log("closed Callback");
            isClicked = true;
        });

        while (!isClicked)
        {
            yield return null;
        }
        UICommon.Current.CleanUp();
    }

    [UnityTest]
    public IEnumerator UICommonTest_UICommonToCredits()
    {

        bool isClicked = false;
        UICommon.Current.ShowCredits(delegate {
            Debug.Log("closed Callback");
            isClicked = true;
        });

        while (!isClicked)
        {
            yield return null;
        }
        UICommon.Current.CleanUp();
    }
}
