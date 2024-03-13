using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Sharing;
using UntitledCube.UI.Buttons;

public class ShareButton : MonoBehaviour
{
    [SerializeField] private Button _shareButton;

    private void Start() => _shareButton.onClick.AddListener(AppShareManager.Instance.CallSharePopUp);
}
