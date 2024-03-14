using MarkUlrich.Utils;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Gravity;

namespace UntitledCube.Timer
{
    public class Stopwatch : SingletonInstance<Stopwatch>
    {
        [SerializeField] private Text _timerText;

        private float _startTime;
        private bool _timerRunning = false;
        private float _elapsedTime;
        private Coroutine _interactionRoutine;

        private bool _gravityChanged;

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
            print("STARTING!!!");
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

        /// <summary>
        /// Starts the stopwatch after a gravity change interaction.
        /// </summary>
        public void StartWithInteractionSync()
        {
            if (_interactionRoutine != null)
            {
                StopCoroutine(StartWithInteraction());
                GravityManager.Instance.OnGravityChanged -= OnGravityChangedHandler;
            }
            _interactionRoutine = StartCoroutine(StartWithInteraction());
        }

        private IEnumerator StartWithInteraction()
        {
            _gravityChanged = false;
            GravityManager.Instance.OnGravityChanged += OnGravityChangedHandler;
            print(_gravityChanged);
            yield return new WaitUntil(() => _gravityChanged);
            Stopwatch.Instance.StartStopWatch();
            GravityManager.Instance.OnGravityChanged -= OnGravityChangedHandler;
        }

        private void OnGravityChangedHandler(Vector3 newGravity) => _gravityChanged = true;
    }
}