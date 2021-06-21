using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CVersionCheckMgr : MonoBehaviour
{
    public bool isTestMode = false;

    public GooglePlayGPGS gpgs;
    private void Start()
    {
        if (isTestMode == false)
        {
            StartCoroutine(PlayStoreVersionCheck());
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            // ----- GPGS -----

       //     gpgs.GPGSLogin();
        }
        else
        {
            StateMachine.StateEnter = true;
            UserDataManager.instance.stateMachine.EnterState();
            // playnanooLoad("bigpixel");
        }


    }

    private IEnumerator PlayStoreVersionCheck()
    {
        string uri = "https://play.google.com/store/apps/details?id=" + Application.identifier;

        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                MessagePopManager.instance.ShowPop("인터넷에러");
                // 인터넷 연결 안내 팝업 필요
            }
            else
            {
                // Debug.Log(www.downloadHandler.text);

                string _Pattern = @"<span class=""htlgb"">([0-9]{1,3}[.][0-9]{1,3})<";
                Regex _Regex = new Regex(_Pattern, RegexOptions.IgnoreCase);
                Match _Match = _Regex.Match(www.downloadHandler.text);

                if (_Match != null)
                {
                    string currentVersion = _Match.Groups[1].ToString();

                    if (Application.version.Equals(currentVersion))
                    {
                        MessagePopManager.instance.ShowPop("최신 버전임");
                        Debug.Log("최신 버전임");
                        

                        if (Application.platform == RuntimePlatform.Android)
                        {
                            // ----- GPGS -----

                            gpgs.GPGSLogin();
                        }
                        else
                        {
                            StateMachine.StateEnter = true;
                            UserDataManager.instance.stateMachine.EnterState();
                            // playnanooLoad("bigpixel");
                        }
                    }
                    else
                    {
                        MessagePopManager.instance.ShowPop("스토어로이동");
                        Debug.Log(string.Format("App Version = {0}, Current Version = {1}", Application.version, currentVersion));

                        //스토어로 이동
                        Application.OpenURL("market://details?id=com.Mini.Game");

                    }
                }
            }
        }
    }
}