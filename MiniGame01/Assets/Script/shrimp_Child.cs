using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class shrimp_Child : MonoBehaviour
{
    private Vector3 firstPos = Vector3.zero;
    public Vector3 shrimp_root;

    public Shrimp shrimp;

    private Tween t;

    public Animator anim;
    public string AnimTriggername;
    public int AnimHashInt;

    private float angle;
    public float animspeed;
    public float sin_Speed;
    public pingpong p2ong = pingpong.none;

    private void OnEnable()
    {
        firstPos = transform.position;
        shrimp.shrimp_Event += shrimp_Start;
    }

    private void OnDisable()
    {
        shrimp.shrimp_Event -= shrimp_Start;
    }

    public void Update()
    {
        if (p2ong == pingpong.ping_pong)
        {
            anim.speed = animspeed;
            angle += Time.deltaTime * sin_Speed;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Mathf.Sin(angle));
        }
    }

    private static int n = 0;

    private void shrimp_Start()
    {
        p2ong = pingpong.ping_pong;
        anim.Play("Shrimp");
        //move
        transform.DOMoveX(shrimp_root.x, 3f, false)
       .SetOptions(false).SetEase(Ease.Linear).OnComplete(() =>
       {
           p2ong = pingpong.none;
           anim.Play("None");
           transform.position = firstPos;
           
           if (++n == 2)
           {
               shrimp.shrimp_EndEvent?.Invoke();
               n = 0;
           }
       });
    }
}