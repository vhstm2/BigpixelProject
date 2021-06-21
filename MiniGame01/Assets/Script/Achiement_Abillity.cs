using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Achiement_Abillity : MonoBehaviour
{
    public Achiement[] achiement;
    public Achiement_Compensation compensation;

    public Image PostIt;
    public Sprite[] postIt_sprites;
    public int selectInt;

    public static int checkint;

    public ParticleSystem noteAnim;

    public string[] complit_Text;

    private void Awake()
    {
        StartCoroutine(Choice(0));

        for (int i = 0; i < UserDataManager.instance.Player_Eqip.Achiement.Count; i++)
        {
            achiement[i].Achiement_success =
            UserDataManager.instance.Player_Eqip.Achiement[i];
        }

        Achiement_localize();
    }

    private void Achiement_setting(Achiement item, string text, float lenght)
    {
        item.Achiement_Text.text = text;
        var rect = item.Line.rectTransform.sizeDelta;
        rect = new Vector2(lenght, rect.y);
        item.Line.rectTransform.sizeDelta = rect;
    }

    private void Achiement_localize()
    {
        switch (Localizer.instance.localizer)
        {
            case localizer_Enum.Korean:
                foreach (var item in achiement)
                {
                    Achiement_setting(item, item.KO_Text, item.Ko_Lenght);
                }
                break;

            case localizer_Enum.English:
                foreach (var item in achiement)
                {
                    Achiement_setting(item, item.EN_Text, item.EN_Lenght);
                }
                break;

            case localizer_Enum.Japanese:
                foreach (var item in achiement)
                {
                    Achiement_setting(item, item.JA_Text, item.JA_Lenght);
                }
                break;
        }
    }

    private int c = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            checkint = 2;
            achiement[checkint].Achiement_success = true;
        }
    }

    private bool clicked = false;

    public void Click(int n)
    {
        if (selectInt == n) return;
        if (!clicked)
            StartCoroutine(Choice(n));
    }

    private bool emt;

    private void activetTue()
    {
        compensation.Compensation.gameObject.SetActive(true);
        // compensation.JA_Text.gameObject.SetActive(true);
        compensation.KO_Text.gameObject.SetActive(true);
        compensation.completed_Text.gameObject.SetActive(true);
    }

    private void activeFalse()
    {
        compensation.Compensation.gameObject.SetActive(false);
        // compensation.JA_Text.gameObject.SetActive(false);
        compensation.KO_Text.gameObject.SetActive(false);
        compensation.completed_Text.gameObject.SetActive(false);
    }

    private IEnumerator PostItActive(int n)
    {
        clicked = true;
        Invoke("activeFalse", 0.01f);

        foreach (var sprite in postIt_sprites)
        {
            PostIt.sprite = sprite;
            yield return new WaitForSeconds(0.05f);
        }
        PostIt.sprite = postIt_sprites[0];

        Invoke("activetTue", 0.05f);

        if (achiement[n].Achiement_success)
        {
            compensation.Check.gameObject.SetActive(true);
            //compensation.completed_Text.gameObject.SetActive(true);

            if (!emt)
            {
                emt = true;
                StartCoroutine(text_Tiping());
            }
        }
        else
        {
            compensation.Check.gameObject.SetActive(false);
            // compensation.completed_Text.gameObject.SetActive(false);
            compensation.completed_Text.text = "";
            emt = false;
        }
        clicked = false;
    }

    private IEnumerator Choice(int n)
    {
        if (selectInt != n) achiement[selectInt].Line.DOFillAmount(0, 0.5f);
        yield return null;
        achiement[n].Line.DOFillAmount(1, 0.5f);

        selectInt = n;
        yield return StartCoroutine(PostItActive(n));
        yield return StartCoroutine(compensations(n));
    }

    private IEnumerator compensations(int n)
    {
        yield return null;

        switch (Localizer.instance.localizer)
        {
            case localizer_Enum.Korean:
                compensation.KO_Text.text = achiement[n].achiement_Text.Ko_Text.Replace("\\n", "\n"); ;
                break;

            case localizer_Enum.English:
                compensation.KO_Text.text = achiement[n].achiement_Text.EN_Text.Replace("\\n", "\n"); ;
                break;

            case localizer_Enum.Japanese:
                compensation.KO_Text.text = achiement[n].achiement_Text.JA_Text.Replace("\\n", "\n");
                break;
        }
        // compensation.KO_Text.text = achiement[n].achiement_Text.Ko_Text;
        // compensation.JA_Text.text = achiement[n].achiement_Text.JA_Text.Replace("\\n", "\n");
        compensation.Compensation.sprite = achiement[n].achiement_Text.Compensation_Image;
        compensation.Compensation_Count.text = $"  x {achiement[n].achiement_Text.Compensation_Int}";
        achiement[n].Achiement_success = UserDataManager.instance.Player_Eqip.Achiement[n];

        if (!achiement[n].Achiement_success)
        {
            compensation.Check.gameObject.SetActive(false);
            // compensation.completed_Text.gameObject.SetActive(false);
            compensation.completed_Text.text = "";
        }
    }

    public IEnumerator text_Tiping()
    {
        yield return null;
        foreach (var item in complit_Text)
        {
            compensation.completed_Text.text += item;
            yield return new WaitForSeconds(0.1f);
        }
    }
}