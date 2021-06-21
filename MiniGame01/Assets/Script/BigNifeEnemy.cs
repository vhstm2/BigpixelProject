using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BigNifeEnemy : BigEnemy
{
    public Animator Animator;
    public GameObject CrackEffect;
    public GameObject CreckAlpha;
    public SpriteRenderer nifeImage;
    public LinePos[] pos;
    public Transform EffectRot;
    public Transform EffectCenter;
    public Vector3 firstPos = Vector3.zero;
    public Vector3 AttackPos = Vector3.zero;
    public Transform effectpos;

    public SpriteRenderer[] linealpha;

    public override void stating()
    {
        base.stating();
        tweenMove(true);
    }

    private void tweenMove(bool isAttacked)
    {
        //트윈으로 등장
        if (isAttacked)
        {
            Tween t = transform.parent.DOMove(AttackPos, 1f, false)
            .SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
            {
                Attacked();
            });
        }
        //트윈으로 퇴장
        else
        {
            Tween t = transform.parent.DOMove(firstPos, 0.7f, false)
            .SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
            {
            });
        }
    }

    public void Attacked()
    {
        //공격시작 시작전에 방향회전
        EffectRot.rotation = Quaternion.Euler(0, Random.Range(0.0f, 359.0f), 0);
        StartCoroutine(AlphaCreck());
    }

    private IEnumerator AlphaCreck()
    {
        yield return null;

        Color color1 = new Color(linealpha[0].color.r, linealpha[0].color.g, linealpha[0].color.b, 0.4f);
        Color color2 = new Color(linealpha[0].color.r, linealpha[0].color.g, linealpha[0].color.b, 0.0f);
        EffectRot.transform.position = EffectCenter.position;
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                lineAlphaControll(linealpha, color2);
            }
            else
            {
                lineAlphaControll(linealpha, color1);
            }
            yield return new WaitForSeconds(0.2f);
        }

        lineAlphaControll(linealpha, color2);
        yield return new WaitForSeconds(0.5f);
        Animator.Play("Attack");
    }

    private void lineAlphaControll(SpriteRenderer[] renderers, Color color)
    {
        renderers[0].color = color;
        renderers[1].color = color;
        renderers[2].color = color;
        renderers[3].color = color;
        renderers[4].color = color;
        renderers[5].color = color;
    }

    public void CrackEvent()
    {
        //for (int i = 0; i < pos.Length; i++)
        //{
        //    pos[i].transform.position = pos[i].lineCenter.position;
        //    pos[i].line.SetPosition(0, pos[i].lineCenter.position);
        //    pos[i].line.SetPosition(1, pos[i].lineCenter.position);
        //}
        CrackEffect.gameObject.SetActive(true);
        StartCoroutine(NifeEnd());
    }

    private IEnumerator NifeEnd()
    {
        yield return new WaitForSeconds(1f);
        Animator.Play("None");
        yield return new WaitForSeconds(.5f);
        tweenMove(false);

        yield return null;
        yield return new WaitForSeconds(3);

        CrackEffect.gameObject.SetActive(false);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i].transform.position = pos[i].lineCenter.position;
            pos[i].line.SetPosition(0, pos[i].lineCenter.position);
            pos[i].line.SetPosition(1, pos[i].lineCenter.position);
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(End());
    }

    public override IEnumerator End()
    {
        return base.End();
    }
}