using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Sound;
using System;

public enum State
{
    Main,
    Shop,
    Character,
    Game,
    GameReStart,
    GameEnd,
    Stage,
    Achievement,
    Setting,
    Zero
}

public class StateMachine : MonoBehaviour
{
    [System.NonSerialized]
    public GameStateBase currentState = null;

    // public StateMachine Machine;

    public Image fadeob;

    public GameManager gmr;

    public static bool StateEnter = false;

    public SoundManager soundManager;

    public GoogleAdMob googleAd;

    private void OnEnable()
    {
        soundManager = FindObjectOfType<SoundManager>();
        if (StateEnter == true)
        {
            EnterState();
        }
    }

    public void EnterState()
    {
        currentState = CreateStateInstance(UserDataManager.instance.state);
        currentState.Enter();
    }

    public void ChangeState(string Scenename)
    {
        if (currentState != null)
            currentState.End(Scenename);     //페이드 아웃 => 씬이동

        UserDataManager.instance.state = StringReturnState(Scenename);
        // currentState = CreateStateInstance(state);

        if ((UserDataManager.instance.state == State.GameEnd)
         || (UserDataManager.instance.state == State.GameReStart))
        {
            currentState = CreateStateInstance(UserDataManager.instance.state);
            Debug.Log("ChangeState =" + UserDataManager.instance.state);
            currentState.Enter();
        }
    }

    public State StringReturnState(string Scenename)
    {
        switch (Scenename)
        {
            case "Main": return State.Main;
            case "Shop": return State.Shop;
            case "Charactor": return State.Character;
            case "Game": return State.Game;
            case "GameEnd": return State.GameEnd;
            case "GameRestart": return State.GameReStart;
            case "Stage": return State.Stage;
            case "Achievement": return State.Achievement;
            case "Setting": return State.Setting;
        }

        return State.Zero;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateBase();
        }
    }

    public GameStateBase CreateStateInstance(State nextState)
    {
        switch (nextState)
        {
            case State.Main: return new MainState(this);
            case State.Shop: return new ShopState(this);
            case State.Character: return new CharacterState(this);
            case State.Game: return new GameState(this);
            case State.GameEnd: return new GameEndState(this);
            case State.GameReStart: return new GameRestartState(this);
            case State.Stage: return new StageState(this);
            case State.Achievement: return new AchievementState(this);
            case State.Setting: return new settingState(this);
        }

        return null;
    }

    public bool _isHomeBotton = false;

    public void HomeButton()
    {
        gmr.gameSceneState = GameManager.GameSceneState.GameEnd;
        _isHomeBotton = true;
    }
}