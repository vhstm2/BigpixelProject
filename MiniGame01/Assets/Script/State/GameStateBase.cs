using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateBase
{
    protected StateMachine machine;
    public static bool b_Restart = false;

    public GameStateBase(StateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void Enter()
    {
    }

    public virtual void UpdateBase()
    {
    }

    public virtual void End(string Scenename)
    {
    }

    public void FadeIn(Image fadeob, Action action = null)
    {
        // var fadeob2 = GameObject.FindObjectOfType<fadeIn>().GetComponent<Image>();
        fadeob.color = new Color(0, 0, 0, 1);

        DOTween.To(() => fadeob.color,
            x => fadeob.color = x,
            new Color(0, 0, 0, 0),
            UserDataManager.instance.SceneFadeOutSecond).OnComplete(() =>
            {
                //씬 이동후 화면이 밝아짐.
                action?.Invoke();
            });
    }

    public void FadeOut(Image fadeob, string SceneName)
    {
        machine.soundManager.LoadingSound();
        fadeob.color = new Color(0, 0, 0, 0);
        DOTween.To(() => fadeob.color,
           x => fadeob.color = x,
           new Color(0, 0, 0, 1),
          UserDataManager.instance.SceneFadeOutSecond).OnComplete(() =>
          {
              //씬이 어두워진 후 이동시킴
              SceneManager.LoadScene(SceneName);
          });
    }

    public void ff()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
    }
}