using UnityEngine;

public class MainState : GameStateBase
{
    public MainState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Main Scene Start");
        // b_Restart = false;
        machine.currentState.FadeIn(machine.fadeob, machine.googleAd.AdsPlay);
        // GooglePlayGPGS.instance.Save();

        
        machine.soundManager.MainBGM();

      
    }

    public override void UpdateBase()
    {
        base.UpdateBase();

       // var GameLoad = GameObject.FindObjectOfType<GoogleProcess>();
        //  GameLoad.LoadT(UserDataManager.instance.Player_Eqip);
    }

    public override void End(string Scenename)
    {
        Debug.Log("Main Scene End");

        FadeOut(machine.fadeob, Scenename);
    }
}