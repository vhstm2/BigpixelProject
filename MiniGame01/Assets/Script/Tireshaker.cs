using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tireshaker : MonoBehaviour
{
    public Transform[] Tires;

    public Vector3[] poss;
    public Vector3[] poss1;
    public Vector3[] poss2;

    public Transform Car;

    public Vector3[] Carposs;

    private Tweener t;
    private Tweener t1;
    private Tweener t2;
    private Tweener Car1;

    public void Shake()
    {
        t = Tires[0].DOPath(poss, poss.Length, PathType.CatmullRom)
         .SetOptions(true)
         .SetLoops(-1, LoopType.Incremental)
         .SetEase(Ease.Linear)
         .SetDelay(0.1f)
         .OnComplete(() =>
         {
         });

        t1 = Tires[1].DOPath(poss1, poss1.Length, PathType.CatmullRom)
        .SetOptions(true)
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.Linear)
        .SetDelay(0.3f)
        .OnComplete(() =>
        {
        });
        t2 = Tires[2].DOPath(poss2, poss2.Length, PathType.CatmullRom)
        .SetOptions(true)
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.Linear)
        .SetDelay(0.5f)
        .OnComplete(() =>
        {
        });

        Car1 = Car.DOPath(Carposs, Carposs.Length, PathType.CatmullRom)
       .SetOptions(true)
       .SetLoops(-1, LoopType.Incremental)
       .SetEase(Ease.Linear)
       .SetDelay(0.2f)
       .OnComplete(() =>
       {
       });
    }

    public void shakeEnd()
    {
        t.Kill();
        t1.Kill();
        t2.Kill();
        Car1.Kill();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        // Tires[0].DOShakePosition(0.5f, 1, 1, 180, true, false);
    }
}