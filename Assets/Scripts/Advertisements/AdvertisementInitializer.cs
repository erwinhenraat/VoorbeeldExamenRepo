using UnityEngine.Advertisements;
using UnityEngine;

namespace UntitledCube.Advertisements
{
    public class AdvertisementInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] bool _testMode = true;
        
        private const string ANDROID_GAME_ID = "5562758";
        private const string IOS_GAME_ID = "5562759";

        private string _gameId;

        private void Awake() => InitializeAds();

        /// <summary>
        /// Initializes the Unity Ads service. Selects the appropriate game ID based on 
        /// the current platform.
        /// </summary>
        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
            _gameId = ANDROID_GAME_ID;
#elif UNITY_EDITOR
            _gameId = _androidGameId;
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
                Advertisement.Initialize(_gameId, _testMode, this);
        }

        /// <summary>
        /// Callback executed when Unity Ads initialization is successfully completed.  
        /// </summary>
        public void OnInitializationComplete() => Debug.Log("Unity Ads initialization complete.");

        /// <summary>
        /// Callback executed when Unity Ads initialization fails.
        /// </summary>
        /// <param name="error">The type of initialization error.</param>
        /// <param name="message">A descriptive error message.</param>
        public void OnInitializationFailed(UnityAdsInitializationError error, string message) 
            => Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }
}