using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppShareManager : MonoBehaviour
{
    public void CallSharePopUp()
    {
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("setAction", "android.intent.action.SEND");
        intent.Call<AndroidJavaObject>("setType", "text/plain");
        intent.Call<AndroidJavaObject>("putExtra", "android.intent.extra.TEXT", "HEY CHECK OUT MY HIGHSCORE ON THIS SEEDS");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intent);
    }
}
