using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ITEM_SKILL : MonoBehaviour
{
    [Header("거북이")]
    public Slow slow;

    [Header("지우개")]
    public Eraser eraser;

    [Header("투명화")]
    public Transparency transparency;

    public Player_Eqip_Sprite player_Eqip_Alpha;

    public ParticleSystem transparency_Effect;

    public CanvasGroup text;
    public TextMeshProUGUI opaltext;

    public RARE_LONGNOZE longnoze;

    public int item_Acquisition;

    private void Awake()
    {
        slow.SlowSpeed = 1f;
    }

    #region Slow

    private IEnumerator DefultSpeedSetup()
    {
        //슬로우 효과
        yield return new WaitForSeconds(3);
        DefultSpeed();
    }

    private void DefultSpeed()
    {
        slow.SlowSpeed = 1f;
    }

    public void SlowItemSetUP()
    {
        slow.SlowSpeed = 0.1f;
        StartCoroutine(DefultSpeedSetup());
    }

    #endregion Slow

    #region eraser

    public void eraserItemSetup()
    {
        eraser.EraserSystem?.Invoke();
    }

    #endregion eraser

    #region transparecy

    public bool transparencyOnOff;
    public void transparencyItemSetup()
    {
        var pColorAlpha = player_Eqip_Alpha.ColorAlpha_transparecy(true);
        transparencyOnOff = true;

        var t = player_Eqip_Alpha.particlePos();
        t.position = new Vector3(999, 999, 999);
        transparency.colorAlpha = pColorAlpha.color;

        //collider 조절

        playerAlphaSetup();
        StartCoroutine(TransparencyALpha());
    }

    private IEnumerator TransparencyALpha()
    {
        yield return null;
        var NoNpColorAlpha = player_Eqip_Alpha.ColorAlpha_transparecy(false);
        transparency.colorAlpha = NoNpColorAlpha.color;
        NoNpColorAlpha.coll.enabled = false;
        transparency_Effect.Play();
        yield return new WaitForSeconds(4);
        var t = player_Eqip_Alpha.particlePos();
        t.position = player_Eqip_Alpha.transform.position;
        NoNpColorAlpha.coll.enabled = true;
        transparencyOnOff = false;
        playerAlphaSetup();
    }

    private void playerAlphaSetup()
    {
        player_Eqip_Alpha.spriteRenderer.color = transparency.colorAlpha;
       
    }

    #endregion transparecy

    private int plus_opal;

    public int Plus_OPAL
    {
        get { return plus_opal; }
        set
        {
            //롱노즈 사용시 plus_opal +=1;
            //else plus_opal = (3);
            plus_opal = value;
        }
    }

    public void OpalSetUP(GameObject obj)
    {
        if (UserDataManager.instance.Player_Eqip.
            select_Character.rare_character == Rare_Character.롱노즈)
        {
            longnoze.efx();
        }

        opaltext.text = Plus_OPAL.ToString();
        text.alpha = 1;
        UserDataManager.instance.opalmoney += Plus_OPAL;
        text.transform.position = obj.transform.position + Vector3.forward * 0.5f;
        //text획득 Text 추가
        text.transform.DOMove(text.transform.position + Vector3.forward * 3f, 1f);

        DOTween.To(() => text.alpha,
           x => text.alpha = x,
           0, 1f).OnComplete(() => { });
    }
}

[System.Serializable]
public class Slow
{
    public float SlowSpeed;
}

[System.Serializable]
public class Eraser
{
    public UnityEvent EraserSystem;
}

[System.Serializable]
public class Transparency
{
    public Color colorAlpha;
}