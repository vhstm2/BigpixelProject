using UnityEngine;

public class AchievementState : GameStateBase
{
    public AchievementState(StateMachine machine) : base(machine)
    { }

    public override void Enter()
    {
        Debug.Log("Achievement Scene Start");
        machine.currentState.FadeIn(machine.fadeob);
    }

    public override void End(string Scenename)
    {
        FadeOut(machine.fadeob, Scenename);
        Debug.Log("Achievement Scene End");
    }
}