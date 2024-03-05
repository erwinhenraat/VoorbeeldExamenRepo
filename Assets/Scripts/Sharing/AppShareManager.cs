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
            "I challenge you to beat my time of 1 in this IMPOSSIBLE seed: \n 23456 \n",
            "Think you can beat my time? I clocked 1 in this seed. Let's see what you've got! \n 23456 \n",
            "23456 \n Seed is mine – beaten in 1 . But I dare you to try and top it 😉 \n",
            "Okay, hotshots. I just blazed through this seed in 1 . Your turn!  \n 23456 \n",
            "My fingers were FLYING. I nailed a 1 run in this seed. Show me your skills! \n 23456 \n",
            "1 seconds in this seed! It's your time to shine... or get crushed 😏 \n 23456 \n"
        };

        private void Start()
        {
            _stopwatch = FindObjectOfType<Stopwatch>();
            _stopwatch.OnTimerStopped += SetScoreTimer;
        }

        /// <summary>
        /// Checks if sharing isn't in procces, clears the temp folder and calls share screenshot
        /// </summary>
        public void CallSharePopUp()
        {
            if (!_isProcessing)
            {
                RemoveAllTempFiles();
                StartCoroutine(ShareScreenshotInAnroid());
            }
        }

        private void SetScoreTimer(string timer) => _scoreTimer = timer;

        private void RemoveAllTempFiles()
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(Application.persistentDataPath));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

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
            .SetSubject("Untitled Cube Highscore").SetText(message).SetUrl("\n\n https://github.com/swzwij/Proeve-van-Bekwaamheid-2023-2024/releases/tag/Sprint_1")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

            yield return new WaitForEndOfFrame();
            _isProcessing = false;
        }
    }
}