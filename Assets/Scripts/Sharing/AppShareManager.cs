using System;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class AppShareManager : MonoBehaviour
{
    [SerializeField] private Image testRemoveLater;
    [SerializeField] private TextMeshProUGUI removeLater;

    private bool _isFocus;
    private bool _isProcessing;

    private void OnApplicationFocus(bool focus) => _isFocus = focus;

    public void CallSharePopUp()
    {
        if (!_isProcessing)
            StartCoroutine(ShareScreenshotInAnroid());
    }

    private IEnumerator ShareScreenshotInAnroid()
    {
        _isProcessing = true;
        yield return new WaitForEndOfFrame();

        string screenshotName = $"HighScore_{DateTime.UtcNow.ToOADate()}.jpg";
        string screenShotPath = Path.Combine(Application.persistentDataPath, screenshotName);
        string seedNumber = "123456";
        string time = "21 secs";
        string message = $"I challenge you to beat my time of *{time}* in this seed: {seedNumber}" +
        "\nhttp://TotallyAReallyLinkThatGoesToOurApp";

        ScreenCapture.CaptureScreenshot(screenshotName, 1);

        removeLater.text = screenShotPath;

        yield return new WaitForEndOfFrame();

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "TEST");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TEST");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", screenShotPath);
        AndroidJavaObject uriList = new AndroidJavaObject("java.util.ArrayList");

        uriList.Call<bool>("add", uriObject);
        intentObject.Call<AndroidJavaObject>("putParcelableArrayListExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriList);
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        currentActivity.Call("startActivity", intentObject);
        testRemoveLater.color = Color.blue;

        yield return new WaitUntil(() => _isFocus);

        testRemoveLater.color = Color.cyan;
        _isProcessing = false;
    }
}

    /*
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");

            AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", screenShotPath);
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Image");
            currentActivity.Call("startActivity", jChooser);*/

