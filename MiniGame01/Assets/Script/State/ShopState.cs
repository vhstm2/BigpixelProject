using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ShopState : GameStateBase
{
    public ShopState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Shop Scene Start");
        machine.currentState.FadeIn(machine.fadeob);
    }

    public override void End(string Scenename)
    {
        Debug.Log("Shop Scene End");
        FadeOut(machine.fadeob, Scenename);
    }
}