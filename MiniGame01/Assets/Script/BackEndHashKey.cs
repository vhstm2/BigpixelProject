using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackEndHashKey : MonoBehaviour
{
    private void Start()
    {
        //초기화 되지않는 경우
        //if (!Backend.IsInitialized)
        //{
        //    Backend.Initialize(BackEndCallBack);
        //}
    }

    //private void BackEndCallBack(BackendReturnObject bro)
    //{
    //    if (bro.IsSuccess())
    //    {
    //        Debug.Log("뒤끝 초기화 성공");
    //    }
    //    else
    //    {
    //        Debug.Log("뒤끝 초기화 실패");
    //    }
    //}

    //public void GoogleHashKey()
    //{
    //    //구글 해시키 획득
    //    string HashKey = Backend.Utils.GetGoogleHash();
    //        Debug.Log(HashKey);
    //    inputField.text = HashKey;
    //    //if (!string.IsNullOrEmpty(HashKey))
    //    //{
    //    //    if (inputField != null) inputField.text = HashKey;
    //    //}
    //}
}