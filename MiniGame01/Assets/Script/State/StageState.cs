using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageState : GameStateBase
{
    public StageState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Stage Scene Start");
        //UserDataManager.instance.MapChanger = GameObject.FindObjectOfType<StageChange>();
        machine.currentState.FadeIn(machine.fadeob);
    }

    public override void End(string Scenename)
    {
        Debug.Log("Stage Scene End");
        //케릭터 넘버 저장

        FadeOut(machine.fadeob, Scenename);
    }
}