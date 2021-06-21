using UnityEngine;

public class settingState : GameStateBase
{
    public settingState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Setting Scene Start");
        machine.currentState.FadeIn(machine.fadeob);

        machine.soundManager.MainBGM();
    }

    public override void End(string Scenename)
    {
        Debug.Log("Setting Scene End");

        FadeOut(machine.fadeob, Scenename);
    }
}