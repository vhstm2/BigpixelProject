using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFactor : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    public void Awake()
    {
        resetScaler();
    }

    private void resetScaler()
    {
        // if (Application.isEditor)
        //    return;

        //#if ANDROID
        float dpi = Screen.dpi / 160;

        //canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        // canvasScaler.scaleFactor = dpi;

        // Screen.SetResolution(Screen.width, (Screen.width / 16) * 9, true);
        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
        //#endif
    }
}