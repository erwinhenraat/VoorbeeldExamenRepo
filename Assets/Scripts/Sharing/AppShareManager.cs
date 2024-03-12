﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UntitledCube.Maze.Generation;
using UntitledCube.Player.Coins;
using UntitledCube.Timer;

namespace UntitledCube.Sharing
{
    public class AppShareManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RenderTexture _photoCanvasTexture;

        private bool _isProcessing;
        private string _scoreTimer;

        private Stopwatch _stopwatch;
        private readonly List<string> challengeTexts = new()
        {
            "I challenge you to beat my time of $  with * coins in this IMPOSSIBLE seed: \n # \n",
            "Think you can beat my time? I clocked $ in this seed with * coins. Let's see what you've got! \n # \n",
            "# \n Seed is mine – beaten in $ with * coins. But I dare you to try and top it 😉 \n",
            "Okay, hotshot. I just blazed through this seed in $  with * coins. Your turn!  \n # \n",
            "My fingers were FLYING. I nailed a $ run in this seed with * coins. Show me your skills! \n # \n",
            "$ seconds in this seed with * coins! It's your time to shine... or get crushed 😏 \n # \n"
        };

        private void Start()
        {
            _stopwatch = Stopwatch.Instance;
            _stopwatch.OnTimerStopped += SetScoreTimer;
        }

        /// <summary>
        /// Checks if sharing isn't in procces, clears the temp folder and calls share screenshot.
        /// </summary>
        public void CallSharePopUp()
        {
            if (_isProcessing) 
                return;
        
            RemoveAllTempFiles();
            StartCoroutine(ShareScreenshotInAnroid());
        }

        private void SetScoreTimer(string timer) => _scoreTimer = timer;

        private void RemoveAllTempFiles()
        {
            DirectoryInfo directoryInfo = new(Path.Combine(Application.persistentDataPath));

            foreach (FileInfo file in directoryInfo.GetFiles())
                file.Delete();
        }

        private IEnumerator ShareScreenshotInAnroid()
        {
            _isProcessing = true;
            yield return new WaitForEndOfFrame();

            CaptureScreenShot(out string screenshotName); 

            string screenShotPath = Path.Combine(Application.persistentDataPath, screenshotName);
            string shareText = RandomiseShareText();

            yield return new WaitForEndOfFrame();

            SetSharePopUp(screenShotPath, shareText);

            yield return new WaitForEndOfFrame();
            _isProcessing = false;
        }

        private string RandomiseShareText()
        {
            string message = challengeTexts[UnityEngine.Random.Range(0, challengeTexts.Count)];
            string seedNumber = MazeGenerator.Seed;

            return message.Replace("$", _scoreTimer).Replace("#", seedNumber).Replace("*", $"{CoinPurse.Coins}");
        }

        private void CaptureScreenShot(out string screenshotName)
        {
            _camera.targetTexture = _photoCanvasTexture;
            _camera.Render();

            Texture2D screenshotTexture = new Texture2D(_photoCanvasTexture.width, _photoCanvasTexture.height);
            RenderTexture.active = _photoCanvasTexture;

            screenshotTexture.ReadPixels(new Rect(0, 0, _photoCanvasTexture.width, _photoCanvasTexture.height), 0, 0);
            screenshotTexture.Apply();

            byte[] screenshotBytes = screenshotTexture.EncodeToPNG();
            screenshotName = $"HighScore_{DateTime.UtcNow.ToOADate()}.jpg";

            File.WriteAllBytes(Application.persistentDataPath + screenshotName, screenshotBytes);

            RenderTexture.active = null;
            _camera.targetTexture = null;
        }

        private void SetSharePopUp(string screenShotPath, string message)
        {
            new NativeShare().AddFile(screenShotPath)
                .SetSubject("Untitled Cube Highscore").SetText(message).SetUrl("\n\n https://github.com/swzwij/Proeve-van-Bekwaamheid-2023-2024/releases/tag/Sprint_1")
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
                .Share();
        }
    }
}