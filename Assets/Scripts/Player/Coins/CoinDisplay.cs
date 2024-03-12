using MarkUlrich.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.Player.Coins
{
    public class CoinDisplay : SingletonInstance<CoinDisplay>
    {
        [SerializeField] private Text _coinText;

        protected override void OnEnable()
        {
            base.OnEnable();
            CoinPurse.OnCoinAdded += UpdateDisplay;
        }

        private void OnDisable() => CoinPurse.OnCoinAdded -= UpdateDisplay;

        private void UpdateDisplay(int coins) => _coinText.text = $"Coins: {coins}";
    }
}