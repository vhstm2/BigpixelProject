using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMissileCar : BigEnemy
{
    public Vector3[] tweenerPosition;

    public Vector3[] tweenerEndPosition;

    public SpriteRenderer dirRoot;

    public enum missileCarType { 정지, 이동, 조준, 발사, 후퇴 }

    public missileCarType type = missileCarType.정지;

    public Missile[] Missles;

    private string Missile;
    private string tire;

    private void Awake()
    {
        Missile = "missileAiming";
        tire = "MissileTire";
        type = missileCarType.정지;
    }

    public override void stating()
    {
        base.stating();
        foreach (var item in Missles)
        {
            item.gameObject.SetActive(true);
        }
        Anim.speed = 1;
        dirRoot.transform.localRotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine(Car_start());
    }

    private IEnumerator Car_start()
    {
        yield return null;
        yield return new WaitForSeconds(3);
        Anim.Play(tire);
        type = missileCarType.이동;
        Tween t = transform.DOPath(tweenerPosition, 4, PathType.CatmullRom)
        .SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            StartCoroutine(Attacekraiming());
        });
    }

    private IEnumerator Attacekraiming()
    {
        yield return null;
        type = missileCarType.조준;
        yield return new WaitForSeconds(0.5f);
        Anim.Play(Missile);
        //Anim.speed = 1f;
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));

        StartCoroutine(missileAttack());
    }

    private IEnumerator missileAttack()
    {
        yield return null;
        type = missileCarType.발사;
        //미사일 발사 3개
        yield return new WaitForSeconds(0.5f);
        Missles[0].Fire();
        yield return new WaitForSeconds(1.5f);
        Missles[1].Fire();
        yield return new WaitForSeconds(1.5f);
        Missles[2].Fire();
        Anim.speed = 0;

        //미사일 발사완료
        yield return new WaitForSeconds(3);
        StartCoroutine(e());
    }

    public override IEnumerator End()
    {
        //yield return null;
        //Anim.Play(tire);
        //Anim.speed = 1;
        //type = missileCarType.후퇴;
        //Tween t = transform.DOPath(tweenerEndPosition, 2, PathType.CatmullRom)
        //.SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
        //{
        //    Anim.speed = 0;
        //    transform.position = tweenerPosition[0];
        //    Anim.Play("None");
        //});

        yield return base.End();
    }

    private IEnumerator e()
    {
        yield return null;
        //dirRoot.transform.localRotation = Quaternion.Euler(Vector3.up * 180);
        Anim.Play(tire);
        Anim.speed = 1;
        type = missileCarType.후퇴;
        Tween t = transform.DOPath(tweenerEndPosition, 4, PathType.CatmullRom)
        .SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            Anim.speed = 0;
            transform.position = tweenerPosition[0];
            Anim.Play("None");
            endComm();
        });
    }

    private void endComm()
    {
        StartCoroutine("End");
    }

    public override void Update()
    {
        base.Update();
    }
}