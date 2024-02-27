using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class AppShareManager : MonoBehaviour
{
    public Image screenshotImage; // Reference to your screenshot image
    public string messageToShare = "Check out this cool screenshot!"; // Text to share

    private bool isProcessing = false;

    public void ShareScreenshot()
    {
        if (!isProcessing)
            StartCoroutine(ShareScreenshotCoroutine());
    }

    private IEnumerator ShareScreenshotCoroutine()
    {
        isProcessing = true;

        // Capture a screenshot (you can replace this with your own screenshot logic)
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Convert the screenshot to a byte array
        byte[] screenshotBytes = screenshotTexture.EncodeToPNG();

        // Save the screenshot to a temporary file
        string screenshotPath = Application.persistentDataPath + "/screenshot.png";
        System.IO.File.WriteAllBytes(screenshotPath, screenshotBytes);

        // Create an intent for sharing
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        // Attach the screenshot
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenshotPath);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        // Attach the text
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), messageToShare);

        // Get the current activity and start the sharing intent
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share via");
        currentActivity.Call("startActivity", chooser);

        // Wait for a frame to allow the permission dialog to appear
        yield return new WaitForEndOfFrame();

        // Clean up
        System.IO.File.Delete(screenshotPath);
        isProcessing = false;
    }
}
/*    [SerializeField] private Image testRemoveLater;

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

        string screenshotName = "HighScore_" + DateTime.UtcNow.ToOADate();
        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        string seedNumber = "123456";
        string time = "21 secs";
        string message = $"I challenge you to beat my time of *{time}* in this seed: {seedNumber}" +
        "\nhttp://TotallyAReallyLinkThatGoesToOurApp";

        ScreenCapture.CaptureScreenshot(screenshotName, 1);

        yield return new WaitForSeconds(0.5f);

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");

        intent.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

        intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        intent.Call<AndroidJavaObject>("setType", "image/png");
        intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intent, "Share your Time");

        currentActivity.Call("startActivity", chooser);
        testRemoveLater.color = Color.blue;
        yield return new WaitUntil(() => _isFocus);

        testRemoveLater.color = Color.cyan;
        _isProcessing = false;*/
/*    }
}*/
