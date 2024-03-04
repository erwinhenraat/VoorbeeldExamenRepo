using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UntitledCube.Maze.Generation;
using UntitledCube.Timer;

namespace UntitledCube.Sharing
{
    public class AppShareManager : MonoBehaviour
    {
        private bool _isProcessing;
        private string _scoreTimer;

        private Stopwatch _stopwatch;
        private List<string> challengeTexts = new List<string>()
        {
            "I challenge you to beat my time of 1 secs in this IMPOSSIBLE seed: \n 23456",
            "Think you can beat my time? I clocked 1 seconds in seed: \n 23456 \n Let's see what you've got!",
            "Seed \n 23456 \n is mine – 1 seconds flat. But I dare you to try and top it 😉",
            "Okay, hotshots. I just blazed through seed \n 23456 \n in 1 seconds. Your turn!",
            "My fingers were FLYING. I nailed a 1 -second run in seed \n 23456 \n Show me your skills!",
            "1 seconds in seed \n 23456 \n It's your time to shine... or get crushed 😏"
        };

        private void Start()
        {
            _stopwatch = FindObjectOfType<Stopwatch>();
            _stopwatch.OnTimerStopped += SetScoreTimer;
        }

        public void CallSharePopUp()
        {
            if (!_isProcessing)
                StartCoroutine(ShareScreenshotInAnroid());
        }

        private void SetScoreTimer(string timer) => _scoreTimer = timer;

        private IEnumerator ShareScreenshotInAnroid()
        {
            _isProcessing = true;
            yield return new WaitForEndOfFrame();

            string screenshotName = $"HighScore_{DateTime.UtcNow.ToOADate()}.jpg";
            string screenShotPath = Path.Combine(Application.persistentDataPath, screenshotName);

            string message = challengeTexts[UnityEngine.Random.Range(0, challengeTexts.Count)];

            string seedNumber = MazeGenerator.Seed;

            message = message.Replace("1", _scoreTimer);
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
}