using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoogleProcess : MonoBehaviour
{
    public Text LoginText = null;

    public Text Idx;
    public Text money;
    public Text opal;
    public Text stagenum;
    public Text item1;
    public Text item2;
    public Text item3;

    public void Update()
    {
        //if (LoginText != null)
        {
            if (GooglePlayGPGS.instance.Authenticated)
                LoginText.text = "로그인성공.";
            else
                LoginText.text = "로그인실패.";
        }
    }

    public void Readerboard()
    {
       // if (GooglePlayGPGS.instance.Authenticated)
        {
            GooglePlayGPGS.instance.GoogleLederBoardUI();
            Debug.Log("ReaderBoardUI OPen");
            MessagePopManager.instance.ShowPop("ReaderBoardUI");
        }
        //else
        {
            MessagePopManager.instance.ShowPop("로그인실패");
            GooglePlayGPGS.instance.GPGSLogin();
        }
    }

    public void SaveUI()
    {
    }

    public void LoadT(Eqip player)
    {
        this.Idx.text = player.idx.ToString();
        this.money.text = player.Money.ToString();
        this.opal.text = player.OpalCount.ToString();
        this.stagenum.text = player.StageNumber.ToString();
        this.item1.text = player.Transparenc.ToString();
        this.item2.text = player.Slow.ToString();
        this.item3.text = player.timer.ToString();
    }
}