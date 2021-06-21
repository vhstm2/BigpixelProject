using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongs : BigEnemy
{
    public delegate void tongStatings();

    public static tongStatings tong;

    public Ease ease = Ease.Linear;

    public override void stating()
    {
        base.stating();
        tong?.Invoke();
    }

    public override IEnumerator End()
    {
        return base.End();
    }
}