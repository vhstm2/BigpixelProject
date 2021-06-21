using UnityEngine;
using UnityEngine.UI;

public class ScreenFrame : MonoBehaviour
{
    public Replay replay;

    public Image Frame;

    public void Play()
    {
        StartCoroutine(replay.Shot(Frame));
    }
}