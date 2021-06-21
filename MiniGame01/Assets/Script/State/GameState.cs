using UnityEngine;

public class GameState : GameStateBase
{
    public GameState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Game Scene Start");
      
        b_Restart = false;
        UserDataManager.instance.BillenSpeed = 1;
        UserDataManager.instance.StageNumber = 0;
        machine.gmr.GameTimerText.text = "3";
        machine.gmr.panelOnoff(machine.gmr.DeadPanel, false);
        //GooglePlayGPGS.instance.LeaderBoardLead();
        machine.currentState.FadeIn(machine.fadeob, GameStart);
        //게임사운드 (BGM)
        machine.soundManager.GameBGM();

        //빅에너미 세팅
        machine.gmr.bigEnemyManager.Setting();
        //빅에너미 출현
        machine.gmr.bigEnemyManager.invoke();
    }

    public override void UpdateBase()
    {
        if (machine.gmr.gameSceneState == GameManager.GameSceneState.GameStart)
        {
            machine.gmr.GameTimer += Time.deltaTime;
            machine.gmr.GameTimerText.text = machine.gmr.GameTimer.ToString("0.00");
            machine.gmr.timer = Mathf.RoundToInt(machine.gmr.GameTimer);
        }

        if (machine.gmr.GameTimer >= 30.0f && !UserDataManager.instance.Player_Eqip.Achiement[1])
        {
            //1번
            UserDataManager.instance.Achiement_Secces(1);
        }
        else if (machine.gmr.GameTimer >= 90.0f && !UserDataManager.instance.Player_Eqip.Achiement[2])
        {
            //2번
            UserDataManager.instance.Achiement_Secces(2);
        }
        else if (machine.gmr.GameTimer >= 150.0f && !UserDataManager.instance.Player_Eqip.Achiement[3])
        {
            //3번
            UserDataManager.instance.Achiement_Secces(3);
        }

        if (machine.gmr.GameTimer >= 120.0f && machine.gmr.Player.items_skill.item_Acquisition == 0)
        {
            //5번
            UserDataManager.instance.Achiement_Secces(5);
        }
    }

    public override void End(string Scenename)
    {
        Debug.Log("Game Scene End");
        //게임씬에서 죽을때 다시시작안할수도 있으니까. 요기 추가.
        GooglePlayGPGS.instance.LeaderBoardPostring(machine.gmr.timer);
        UserDataManager.instance.BillenSpeed = 0;
        //  GooglePlayGPGS.instance.LeaderBoardLead();
        machine.gmr.Billens.Endcommot();
        //machine.gmr.robot.RobotStarting = false;
        machine.gmr.robot.Tween_Stop();
    }

    public void GameStart()
    {
        UserDataManager.instance.Following_Counting = 5;
        machine.gmr.Controller_land.raycastTarget = true;
        machine.gmr.robot.CansleSystem = false;
        machine.gmr.Restart();  //로봇도 등장
    }
}