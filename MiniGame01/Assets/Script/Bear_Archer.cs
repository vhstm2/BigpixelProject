using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bear_Archer : BigEnemy
{
    public Archer[] archers;
    public static int bearIndx = 0;

    private void OnEnable()
    {
        Archer.EndEvent += EndEventComplate;
    }

    private void OnDisable()
    {
        Archer.EndEvent -= EndEventComplate;
    }

    public override void stating()
    {
        base.stating();
        bearIndx = 0;

        StartCoroutine(ArchersSetting());
    }

    public PlayerCtrl player;

    private IEnumerator ArchersSetting()
    {
        for (int i = 0; i < archers.Length; i++)
        {
            yield return null;
            yield return new WaitForSeconds(0.4f);
            archers[i].SetArcher();
        }
    }

    private void EndEventComplate()
    {
        StartCoroutine(End());
    }

    public override IEnumerator End()
    {
        return base.End();
    }

    public override void Update()
    {
        base.Update();
    }
}