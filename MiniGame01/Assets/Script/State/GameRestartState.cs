using System.Collections;
using UnityEngine;

public class GameRestartState : GameStateBase
{
    public GameRestartState(StateMachine machine) : base(machine)
    { }

    public override void Enter()
    {
        Debug.Log("Gamerestart Start");
        UserDataManager.instance.BillenSpeed = 1;
        machine.gmr.GameTimerText.text = "3";
        machine.gmr.panelOnoff(machine.gmr.DeadPanel, false);
        b_Restart = true;
        machine.gmr.Restart();
        
        //machine.StartCoroutine(bigEnemy());
        machine.gmr.Player.heal_effect.gameObject.SetActive(true);
        machine.gmr.Player.heal_effect.Play();
    }

    private IEnumerator bigEnemy()
    {
        yield return new WaitForSeconds(3);
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
    }

    public override void End(string Scenename)
    {
        Debug.Log("Gamerestart End");

        //GooglePlayGPGS.instance.UpdateScore2(machine.gmr.GameTimer);

        machine.gmr.gameSceneState = GameManager.GameSceneState.GameEnd;
        GooglePlayGPGS.instance.LeaderBoardPostring(machine.gmr.timer);

        //machine.gmr.robot.RobotStarting = false;
        machine.gmr.robot.Tween_Stop();

        // FadeOut(machine.fadeob, Scenename);
    }
}