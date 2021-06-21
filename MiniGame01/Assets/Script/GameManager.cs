using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameSceneState { GameStart, GameEnd, NONE }

    public GameSceneState gameSceneState = GameSceneState.NONE;

    public TextMeshProUGUI GameTimerText;
    public TextMeshProUGUI HpCountText;

    [System.NonSerialized]
    public float GameTimer = 0.0f;

    public RectTransform ReGamePanel;

    public Image Controller_land;

    [System.NonSerialized]
    public long timer = 0;

    public Image FadeOut_Ob;

    public Image DeadPanel;
    public Image DeadPanel2;

    public PlayerCtrl Player;

    public LazerSponer lazerSponer;
    public SponerManager billenSponer;

    public TextMeshProUGUI five;
    public TextMeshProUGUI resultTime;
    public TextMeshProUGUI result2Time;

    public StateMachine machine;

    public EnemyRobot robot;

    public Button[] DeadEndButtons;

    public MatchParent Billens;
    public MatchDrilled middleBillens;

    public bool b_Following = false;
    public GameRank rank;

    public float bestScore;

    public TextMeshProUGUI[] RankingText;
    public string[] LeaderBoardTexts;

    public ScreenFrame screenFrame;

    public BigEnemyManager bigEnemyManager;

    public tipClass tip_Panels;

    public void ButtonOff(bool flag)
    {
        foreach (var item in DeadEndButtons)
        {
            item.interactable = flag;
        }
    }

    public long EndTimer()
    {
        return timer;
    }

    public void OnEnable()
    {
        System.GC.Collect();
    }

    private bool tipEnable = false;

    private void Start3()
    {
        Player.Coliider_onoff(false);
        GameTimerText.text = "3";

        if (tip_Panels.tip_background.alpha < 1 && !tipEnable)
        {
            tip_Panels.enumChange(5);

            tip_Panels.OnInspector(tip_Panels);

            DOTween.To(() => tip_Panels.tip_background.alpha,
              x => tip_Panels.tip_background.alpha = x,
              1, 1f).OnComplete(() => { });
        }

        Invoke("Start2", 1);
    }

    private void Start2()
    {
        GameTimerText.text = "2";
        Invoke("Start1", 1);
    }

    private void Start1()
    {
        GameTimerText.text = "1";

        Invoke("Start0", 1);
    }

    private void Start0()
    {
        GameTimerText.text = "0";

        if (tip_Panels.tip_background.alpha >= 1 && !tipEnable)
        {
            DOTween.To(() => tip_Panels.tip_background.alpha,
              x => tip_Panels.tip_background.alpha = x,
              0, 1f).OnComplete(() => { tipEnable = true; });
        }

        if (Player.charcter_Info.rare_character == Rare_Character.기사)
            Player.sheld.setting();

        Invoke("GameStartButtonClicked", 1);
    }

    public void panelOnoff(Image deadpanel, bool flag)
    {
        if (flag)
        {
            deadpanel.gameObject.SetActive(true);
            GameTimerText.gameObject.SetActive(false);
        }
        else
        {
            deadpanel.gameObject.SetActive(false);
            GameTimerText.gameObject.SetActive(true);
        }
    }

    #region Tween_Panel

    public static void TweenSet(GameObject obj, Action action = null, Action action2 = null)
    {
        DeadPanelTweenAlpha(
               obj.gameObject,
               2.0f);

        DeadPaneTweenlScale(
            obj.gameObject,
            3.0f,
            Ease.InOutElastic, () => action?.Invoke());

        DeadPanelTween(
            obj.gameObject,
            0,//Screen.height,
            2.0f,
            Ease.InOutElastic, () => action2?.Invoke());
    }

    public static void DeadPanelTweenAlpha(GameObject obj, float delay, Action action = null)
    {
        var rect = obj.GetComponent<CanvasGroup>();

        rect.alpha = 0;

        DOTween.To(() => rect.alpha,
            x => rect.alpha = x,
            1, delay).OnComplete(() => { action?.Invoke(); });
    }

    public static void DeadPaneTweenlScale(GameObject obj, float delay, Ease ease, Action action = null)
    {
        obj.transform.localScale = Vector3.zero;

        DOTween.To(() => obj.transform.localScale,
            x => obj.transform.localScale = x,
            Vector3.one, delay).SetEase(ease).OnComplete(() => { action?.Invoke(); });
    }

    public static void DeadPanelTween(GameObject obj, float delta, float delay, Ease ease, Action action = null)
    {
        var rect = obj.GetComponent<RectTransform>();

        rect.DOLocalMoveY(delta, delay).
            SetEase(ease).OnComplete(() =>
            {
                action?.Invoke();
            });
    }

    #endregion Tween_Panel

    public void EndGameSceneChange(Action action = null)
    {
        //리더보드에 쓰기

        action?.Invoke();

        return;
    }

    [System.NonSerialized]
    public int Following = 1;

    public ITEM_SKILL eraser;

    public void ReGameButtonClicked()
    {
        Following--;
        // Player.HpCount = 1;
        UserDataManager.instance.Following_Counting = 5;
        b_Following = true;
        //적지우기
        //eraser.eraserItemSetup();

        machine.ChangeState("GameRestart");
    }

    public void Resurrectionbutton()
    {
        if (UserDataManager.instance.opalmoney <= 0)
        {
            MessagePopManager.instance.ShowPop("오팔부족... 이어서 진행불가...");
        }
        else
        {
            UserDataManager.instance.opalmoney -= 1;
            ReGameButtonClicked();
        }
    }



    public void Restart()
    {
        Debug.Log("리스타트실행");
        panelOnoff(DeadPanel, false);
        Player.sheld.setting();
        Start3();
        //3초뒤 GameStartButtonClicked() 실행됨
    }

    public void GameStartButtonClicked()
    {
        b_Following = false;
        //3.2.1초 후 게임시작
        gameSceneState = GameSceneState.GameStart;

        Player.Coliider_onoff(true);
        //if (GameStateBase.b_Restart)
        //    StartCoroutine(screenFrame.replay.FrameSave(screenFrame.Frame));

        //robot.CansleSystem = false;

        StartCoroutine(middleBillens.middleBillen());
        BigEnemy.BigEnemyRelayStop = false;
        //적 스타트임.
        //if (!bigEnemyManager.Is_BigEnemys)
        {
            // bigEnemyManager.invoke();
        }
        //StartCoroutine(robot.stating());
    }

    public void EnemyClear()
    {
        StartCoroutine(enemyclears());
    }

    private IEnumerator enemyclears()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < billenSponer.fullList.Count; i++)
        {
            billenSponer.fullList[i].SetActive(false);

            //스포너매니저 불
        }
        for (int i = 0; i < lazerSponer.LazerList.Count; i++)
        {
            lazerSponer.LazerList[i].SetActive(false);
        }
        for (int i = 0; i < lazerSponer.fulllList_Light.Length; i++)
        {
            lazerSponer.fulllList_Light[i].SetActive(false);
        }
    }

    public void Followings()
    {
        b_Following = true;
    }
}