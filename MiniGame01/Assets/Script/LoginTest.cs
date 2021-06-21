using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class LoginTest : MonoBehaviour
{
    private bool bWait = false;
    public Text text;

    public void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       .RequestEmail()
       .RequestServerAuthCode(false)
       .RequestIdToken()
       .Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();

        text.text = "no Login";
    }

    public void Start()
    {
        OnLogin();
    }

    public void Update()
    {
        if (Social.localUser.authenticated)
        {
            text.text = Social.localUser.userName;
        }
    }

    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                bWait = bSuccess;
                if (bWait)
                {
                    Debug.Log("Success : " + Social.localUser.userName);

                    text.text = Social.localUser.userName;
                }
                else
                {
                    Debug.Log("Fall");
                    text.text = "Fail";
                }
            });
        }
    }

    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        text.text = "Logout";
    }
}