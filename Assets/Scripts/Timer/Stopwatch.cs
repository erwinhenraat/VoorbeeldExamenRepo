using MarkUlrich.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.Timer
{
    public class Stopwatch : SingletonInstance<Stopwatch>
    {
        [SerializeField] private Text _timerText; // TODO: convert to TMP

        private float _startTime;
        private bool _timerRunning = false;
        private float _elapsedTime;

        public Action<string> OnTimerStopped;

        private void Update()
        {
            if (!_timerRunning)
                return;

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            _elapsedTime = Time.time - _startTime;           
            _timerText.text = FormatTime(_elapsedTime);
        }

        /// <summary>
        /// Starts the stopwatch by resetting elapsed time, recording the start time.
        /// </summary>
        public void StartStopWatch()
        {
            _elapsedTime = 0;
            _startTime = Time.time;
            _timerRunning = true;
        }

        /// <summary>
        /// Stops the stopwatch.
        /// </summary>
        public void Stop()
        {
            _timerRunning = false;
            OnTimerStopped?.Invoke(FormatTime(_elapsedTime));
        }

        /// <summary>
        /// Reset and stops the stopwatch.
        /// </summary>
        public void ResetStopWatch()
        {
            _timerRunning = false;
            _elapsedTime = 0;
            _timerText.text = FormatTime(_elapsedTime);
        }

        private string FormatTime(float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);

            string formatString = timeSpan.TotalMinutes >= 1 ? @"m\:ss\.ff" : @"s\.ff";
            return timeSpan.ToString(formatString);
        }
    }
}