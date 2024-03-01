using MarkUlrich.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.Loading
{
    public class LoadingScreen : SingletonInstance<LoadingScreen>
    {
        [SerializeField] private Image _loadScreen;

        private void Awake() => Load(false);

        /// <summary>
        /// Controls the visibility of a loading screen visual element.
        /// </summary>
        /// <param name="showLoading">Whether the loading screen should be shown.</param>
        public void Load(bool showLoading) => _loadScreen.gameObject.SetActive(showLoading);
    }
}