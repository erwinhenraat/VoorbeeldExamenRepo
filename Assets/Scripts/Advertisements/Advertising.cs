using UnityEngine.Advertisements;
using UnityEngine;
using swzwij.Singletons;

namespace UntitledCube.Advertisements
{
    public class Advertising : SingletonBehaviour<Advertising>, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_UNITY_ID = "Interstitial_Android";
        private const string IOS_AD_UNITY_ID = "Interstitial_iOS";

        private string _adUnitId;

        private void Awake()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) 
                ? IOS_AD_UNITY_ID 
                : ANDROID_AD_UNITY_ID;
        }

        /// <summary>
        /// Initiates the loading of an advertisement from the Unity Ads service.
        /// </summary>
        public void LoadAd() => Advertisement.Load(_adUnitId, this);

        /// <summary>
        /// Logs a success message when an ad unit is loaded.
        /// </summary>
        /// <param name="adUnitId">The ID of the loaded ad unit.</param>
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log($"Successfully loaded Ad Unit: {adUnitId}");
            Advertisement.Show(_adUnitId, this);
        }

        /// <summary>
        /// Logs an error message if an ad unit fails to load.
        /// </summary>
        /// <param name="_adUnitId">The ID of the ad unit that failed to load.</param>
        /// <param name="error">The type of error that occurred.</param>
        /// <param name="message">Additional error details.</param>
        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message) 
            => Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error} - {message}");

        /// <summary>
        /// Logs an error message if a loaded ad fails to display. 
        /// </summary>
        /// <param name="_adUnitId">The ID of the ad unit that failed to display.</param>
        /// <param name="error">The type of error that occurred.</param>
        /// <param name="message">Additional error details.</param>
        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message) 
            => Debug.Log($"Error showing Ad Unit {_adUnitId}: {error} - {message}");

        /// <summary>
        /// Called when an advertisement starts playing. (Currently empty)
        /// </summary>
        /// <param name="placementId">The ID of the ad unit.</param>
        public void OnUnityAdsShowStart(string placementId) { }

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
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
    }
}