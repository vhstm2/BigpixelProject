using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tongsRots : MonoBehaviour
{
    public Animator anim;
    public tongs tong;
    public Vector3[] movePostions;
    public Tween t;

    public string animHasName;
    public int hasAnim;

    public string animlastEndHasName;
    public int hasLastAnim;

    private void OnEnable()
    {
        tongs.tong += tongsAnimAttack;
        hasAnim = Animator.StringToHash(animHasName);
        hasLastAnim = Animator.StringToHash(animlastEndHasName);
    }

    private void OnDisable()
    {
        tongs.tong -= tongsAnimAttack;
    }

    public void tongsAnimAttack()
    {
        t = transform.DOLocalPath(movePostions, 0.7f, PathType.CatmullRom)
         .SetOptions(false).SetEase(tong.ease).OnComplete(() =>
        {
            angang();
        });
    }

    private void angang()
    {
        anim.SetTrigger(hasAnim);
    }

    public void angangAnimEndEvent()
    {
        this.anim.Play("None");
        transform.DOLocalMove(movePostions[0], 0.5f)
            .SetOptions(false)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                anim.SetTrigger(hasLastAnim);
            });
    }

    private static int n = 0;
    public void LastEndEvent()
    {
        print("n = " + n);
        if(++n >= 2)
        {   
            anim.Play("None");
            StartCoroutine(tong.End());
            n = 0;
        }
    }
}