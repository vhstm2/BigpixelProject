using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MessagePopManager : MonoBehaviour
{
    public static MessagePopManager instance;
    public GameObject popBoard;
    public Text txt;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // HidePop();
    }

    public void ShowPop(string _str, float _time = 2f)
    {
        txt.text = _str;
        popBoard.SetActive(true);
         CancelInvoke("HidePop");
        Invoke("HidePop", _time);
    }

    public void HidePop()
    {
        popBoard.SetActive(false);
    }
}