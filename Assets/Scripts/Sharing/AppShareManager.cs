using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppShareManager : MonoBehaviour
{
    private bool _isProcessing;
    private List<string> challengeTexts = new List<string>()
    {
        "I challenge you to beat my time of 1 secs in this IMPOSSIBLE seed: 23456", 
        "Think you can beat my time? I clocked 1 seconds in seed: 23456 . Let's see what you've got!", 
        "Seed 23456 is mine – 1 seconds flat. But I dare you to try and top it 😉", 
        "Okay, hotshots. I just blazed through seed 23456 in 1 seconds. Your turn!", 
        "My fingers were FLYING. I nailed a 1 -second run in seed 23456 . Show me your skills!", 
        "1 seconds in seed 23456 . It's your time to shine... or get crushed 😏" 
    };

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

        string message = challengeTexts[UnityEngine.Random.Range(0, challengeTexts.Count)];

        string seedNumber = "123456";
        string time = "21 secs";

        message = message.Replace("1", time);
        message = message.Replace("23456", seedNumber);

        ScreenCapture.CaptureScreenshot(screenshotName, 1);

        yield return new WaitForEndOfFrame();

        new NativeShare().AddFile(screenShotPath)
        .SetSubject("Untitled Cube Highscore").SetText(message).SetUrl("\n\n https://TotallyARealLinkThatGoesToOurApp")
        .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
        .Share();

        yield return new WaitForEndOfFrame();
        _isProcessing = false;
    }
}