using UnityEngine.Advertisements;
using UntitledCube.Utils;
using System.Collections;
using MarkUlrich.Utils;
using UnityEngine;
using System;
using UntitledCube.Loading;

namespace UntitledCube.Advertisements
{
    public class Advertising : SingletonInstance<Advertising>, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_UNITY_ID = "Interstitial_Android";
        private const string IOS_AD_UNITY_ID = "Interstitial_iOS";
        private const float COUNTDOWN_DURATION = 120f;
        
        private string _adUnitId;
        private bool _isCountdownRunning = false;

        /// <summary>
        /// Called when an advertisement is about to be loaded.
        /// </summary>
        public Action OnShowAdvertisement;

        /// <summary>
        /// Called when an advertisement has finished loading.
        /// </summary>
        public Action OnAdvertisementShown;

        private void Awake()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) 
                ? IOS_AD_UNITY_ID 
                : ANDROID_AD_UNITY_ID;
        }

        /// <summary>
        /// Initiates the loading of an advertisement from the Unity Ads service.
        /// </summary>
        public void LoadAd() 
        {
            if (!NetworkStatus.IsConnected)
                return;

            Advertisement.Load(_adUnitId, this);
        }

        /// <summary>
        /// Displays an advertisement if the device has a network connection. 
        /// </summary>
        public void ShowAd()
        {
            if (_isCountdownRunning || !NetworkStatus.IsConnected)
                return;

            LoadingScreen.Instance.Load(true);

            OnShowAdvertisement?.Invoke();
            Advertisement.Show(_adUnitId, this);
            Advertisement.Load(_adUnitId, this);
        }

        /// <summary>
        /// Logs a success message when an ad unit is loaded.
        /// </summary>
        /// <param name="adUnitId">The ID of the loaded ad unit.</param>
        public void OnUnityAdsAdLoaded(string adUnitId) => Advertisement.Show(_adUnitId, this);

        /// <summary>
        /// Logs an error message if an ad unit fails to load.
        /// </summary>
        /// <param name="_adUnitId">The ID of the ad unit that failed to load.</param>
        /// <param name="error">The type of error that occurred.</param>
        /// <param name="message">Additional error details.</param>
        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message) 
            => Debug.LogError($"Error loading Ad Unit: {_adUnitId} - {error} - {message}");

        /// <summary>
        /// Logs an error message if a loaded ad fails to display. 
        /// </summary>
        /// <param name="_adUnitId">The ID of the ad unit that failed to display.</param>
        /// <param name="error">The type of error that occurred.</param>
        /// <param name="message">Additional error details.</param>
        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message) 
            => Debug.LogError($"Error showing Ad Unit {_adUnitId}: {error} - {message}");

        /// <summary>
        /// Called when an advertisement starts playing. (Currently empty)
        /// </summary>
        /// <param name="placementId">The ID of the ad unit.</param>
        public void OnUnityAdsShowStart(string placementId)
        {
            LoadingScreen.Instance.Load(false);
            OnAdvertisementShown?.Invoke();
        }
        
        /// <summary>
        /// Called when the user clicks on an advertisement. (Currently empty)
        /// </summary>
        /// <param name="placementId">The ID of the ad unit.</param>
        public void OnUnityAdsShowClick(string placementId) { }

        /// <summary>
        /// Called when an advertisement is displayed successfully. (Currently empty)
        /// </summary>
        /// <param name="placementId">The ID of the ad unit.</param>
        /// <param name="showCompletionState">Indicates if the ad was watched in its entirety.</param>
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) 
            => StartCoroutine(AdCountdown());

        private IEnumerator AdCountdown()
        {
            _isCountdownRunning = true;
            yield return new WaitForSeconds(COUNTDOWN_DURATION);
            _isCountdownRunning = false;
        }
    }
}