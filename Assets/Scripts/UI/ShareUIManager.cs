using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Player.Coins;
using UntitledCube.Sharing;

namespace UntitledCube.UI
{
    public class ShareUIManager : MonoBehaviour
    {
        [SerializeField] private Button _shareButton;
        [SerializeField] private Canvas _disableCanvas;
        [SerializeField] private RawImage _cubePlacementImage;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _timerText;

        private void Start()
        {
            _shareButton.onClick.AddListener(AppShareManager.Instance.CallSharePopUp);
            AppShareManager.Instance.SetCanvas(_disableCanvas);
            _cubePlacementImage.texture = AppShareManager.Instance.GetTexture();
            _coinsText.text = "Coins:" + CoinPurse.Coins.ToString();
            _timerText.text = "Time:" + AppShareManager.Instance.GetTimer();
        }
    }
}
