using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Sharing;
using UntitledCube.UI.Buttons;

public class ShareButton : MonoBehaviour
{
    [SerializeField] private Button _shareButton;
    [SerializeField] private Canvas _photoCanvas;

    private void Start() 
    {
        _shareButton.onClick.AddListener(AppShareManager.Instance.CallSharePopUp);
        AppShareManager.Instance.SetCanvas(_photoCanvas);
    }

}
