using System;
using UnityEngine;

public class GameEndState : GameStateBase
{
    private bool success;

    public GameEndState(StateMachine machine) : base(machine)
    { }

    public override void Enter()
    {
        Debug.Log("GameEnd Start");
        // GooglePlayGPGS.instance.LeaderBoardLead();
        machine.gmr.gameSceneState = GameManager.GameSceneState.GameEnd;
        //====================================================

        //====================================================
        machine.gmr.Player.DeadEffect.SetActive(true);
        //====================================================

        machine.gmr.Controller_land.raycastTarget = false;
        //machine.gmr.ButtonOff(true);
        UserDataManager.instance.Following_Counting = 5;

        //적들 한번 싹다 밀기

        long i = machine.gmr.timer / 60;

        if (i == 0)
        {
            if (!b_Restart) machine.gmr.resultTime.text = "00 : " + machine.gmr.GameTimer.ToString("00");
            else machine.gmr.result2Time.text = "00 : " + machine.gmr.GameTimer.ToString("00");
        }
        else
        {
            long a = machine.gmr.timer % 60;
            if (!b_Restart) machine.gmr.resultTime.text = i.ToString() + " : " + a.ToString();
            else machine.gmr.result2Time.text = i.ToString() + " : " + a.ToString();
        }

        if (b_Restart == false)
        {
            //게임씬에서 넘어왔을떄
            machine.gmr.EnemyClear();
            machine.gmr.panelOnoff(machine.gmr.DeadPanel, true);

            GameManager.TweenSet
            (
                machine.gmr.DeadPanel.gameObject,
                () => success = true
            );
        }
        else
        {
            UserDataManager.instance.Player_Eqip.StageNumber = 0;
            //리스타트씬에서 넘어왔을때
            machine.gmr.screenFrame.Play();
            machine.gmr.EnemyClear();
            GooglePlayGPGS.instance.RankingLoad();
            machine.gmr.panelOnoff(
                      machine.gmr.DeadPanel2, true);

            GameManager.TweenSet
            (
                machine.gmr.DeadPanel2.gameObject
            // play
            );
        }
    }

    private void play()
    {
        machine.StartCoroutine(machine.gmr.screenFrame.replay.ChangeFrame(machine.gmr.screenFrame.Frame));
    }

    public override void UpdateBase()
    {
        if (b_Restart == false && !machine.gmr.b_Following && success)
        {
            machine.gmr.five.text = (UserDataManager.instance.Following_Counting).ToString("0");

            UserDataManager.instance.Following_Counting -= Time.deltaTime;
            //5초안에 선택안하면 씬변경
            if (UserDataManager.instance.Following_Counting <= 0.25f)
            {
                UserDataManager.instance.Following_Counting = 0f;
                MainScene();
                return;
            }
        }
    }

    public override void End(string Scenename)
    {
        Debug.Log("GameEnd End");

        machine.gmr.bigEnemyManager.Is_BigEnemys = false;

        machine.gmr.Controller_land.raycastTarget = true;

        if (b_Restart == false && machine._isHomeBotton) //홈가는 버튼 눌럿을떄
        {
            Debug.Log("goHome");
            FadeOut(machine.fadeob, Scenename);
        }

        //이미 한번 이어하기 썻으면 씬 화면 변경
        if (b_Restart == true || UserDataManager.instance.Following_Counting == 0f)
        {
            UserDataManager.instance.Player_Eqip.rankScore = (int)machine.gmr.timer;
            //버튼 클릭안되게 막기비활성화
            FadeOut(machine.fadeob, Scenename);
        }
        else
        {
            machine.gmr.panelOnoff(machine.gmr.DeadPanel, false);
            UserDataManager.instance.BillenSpeed = 0;
        }

        success = false;
        //machine.gmr.ButtonOff(false);

        machine.gmr.Player.DeadEffect.SetActive(false);
        //GooglePlayGPGS.instance.UpdateScore2(machine.gmr.GameTimer);
    }

    public void MainScene(Action common = null)
    {
        UserDataManager.instance.BillenSpeed = 0;
        machine.gmr.EnemyClear();
        Debug.Log("리스타트로 이동");
        machine.ChangeState("Main");
    }
}