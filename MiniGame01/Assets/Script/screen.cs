using UnityEngine;

public class screen : MonoBehaviour
{
    #region screen

    //public float aspectWH = 1.6f;
    //public float aspectAdd = 0.05f;

    //public bool StartScreedAdjest = true;
    //public bool UpdateScreedAdjust = false;

    //Vector3 localScale;

    //private void Start()
    //{
    //    localScale = transform.localScale;
    //    if (StartScreedAdjest)
    //        ScreedAdjust();
    //}

    //private void Update()
    //{
    //    if (UpdateScreedAdjust)
    //        ScreedAdjust();
    //}

    //private void ScreedAdjust()
    //{
    //    float wh = (float)Screen.width / (float)Screen.height;

    //    if (wh < aspectWH)
    //    {
    //        transform.localScale = new Vector3
    //            (
    //                localScale.x - (aspectWH - wh) + aspectAdd,
    //                localScale.y,
    //                localScale.z
    //            );
    //    }
    //    else
    //    {
    //        transform.localScale = localScale;
    //    }
    //}

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    //Screen.SetResolution(Screen.width, Screen.width /  16  * 9, true);
    //    //Screen.SetResolution(1920, 1080, true);
    //    //Camera.main.aspect = 16f / 9f;
    //    //Screen.sleepTimeout = SleepTimeout.NeverSleep;
    //    //Screen.SetResolution(Screen.width, Screen.height, true);
    //    //Screen.SetResolution(Screen.width, Screen.width /  16  * 9, true);
    //    //Screen.SetResolution(Screen.width, (Screen.width /  16) * 9, true);

    //    // 2:3 비율로 개발 한다면
    //    // SetResolution(Screen.width, Screen.width * 3 / 2, ture)로 한다.
    //    // SetResolution(Screen.width, Screen.width / 2 * 3, true)
    //    // SetResolution( Screen.width, Screen.width * 3 / 2, ture )
    //}
    //    public CanvasScaler canvasScaler;

    //    private void Awake()
    //    {
    //        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
    //        Resize();
    //    }

    //    private void Resize()
    //    {
    //        if (Application.isEditor)
    //        {
    //            return;
    //        }

    //        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
    //#if UNITY_ANDROID
    //        canvasScaler.scaleFactor = Screen.dpi / 170;
    //#elif UNITY_IOS
    //        _canvasScaler.scaleFactor = MyApplePlugin.GetNativeScaleFactor();
    //#endif
    //    }

    #endregion screen

    public Camera camera;
    public bool settings;



    public Rect rt;
    
    

    private void Awake()
    {
        Setting();
    }

    private void Setting()
    {
        // if (settings)
        {
            rt = camera.rect;

            //// 현재 세로 모드 9:16, 반대로 하고 싶으면 16:9를 입력.
            float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
            float scale_width = 1f / scale_height;

            if (scale_height < 1)
            { 
                rt.height = scale_height; 
                rt.y = (1f - scale_height) / 2f; 
            }
            else 
            { 
                rt.width = scale_width;
                rt.x = (1f - scale_width) / 2f; 
            }
            camera.rect = rt;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //  else
        {
            //float baseRatio = 1920.0f / 1080.0f;
            //float currentRatio = Screen.width / Screen.height;

            //camera.orthographicSize = camera.orthographicSize * baseRatio / currentRatio;
        }
    }
}