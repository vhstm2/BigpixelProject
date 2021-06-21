//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEndFederationAuth : MonoBehaviour
{
    private void Awake()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        //    .Builder()
        //    //.RequestServerAuthCode(false)
        //    //.RequestEmail()                 // 이메일 요청
        //    //.RequestIdToken()               // 토큰 요청
        //    .Build();

        ////커스텀된 정보로 GPGS 초기화
        //PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.DebugLogEnabled = true;

        ////GPGS 시작.
        //PlayGamesPlatform.Activate();

        GoogleAuth();
    }

    // 구글에 로그인하기
    private void GoogleAuth()
    {
        //if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        //{
        //    Social.localUser.Authenticate(success =>
        //    {
        //        if (success == false)
        //        {
        //            Debug.Log("구글 로그인 실패");
        //            return;
        //        }

        //        // 로그인이 성공되었습니다.
        //        Debug.Log("로그인성공");
        //        //Debug.Log("GetIdToken - " + PlayGamesPlatform.Instance.GetIdToken());
        //        //Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
        //        //Debug.Log("GoogleId - " + Social.localUser.id);
        //        //Debug.Log("UserName - " + Social.localUser.userName);
        //        //Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());
        //    });
        //}
    }

    // 구글 토큰 받아오기
    private string GetTokens()
    {
        //if (PlayGamesPlatform.Instance.localUser.authenticated)
        //{
        //    // 유저 토큰 받기 첫번째 방법
        //    string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
        //    // 두번째 방법
        //    // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        //    return _IDtoken;
        //}
        //else
        //{
        //    Debug.Log("접속되어있지 않습니다. 잠시 후 다시 시도하세요.");
        //    GoogleAuth();
        return null;
        //}
    }
}