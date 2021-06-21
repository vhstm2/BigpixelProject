using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum pingpong
{
    none,
    ping_pong
}

public class Shrimp : BigEnemy
{
    public event Action shrimp_Event = () => { };

    public Action shrimp_EndEvent = () => { };

    private void OnEnable()
    {
        shrimp_EndEvent += eventEnd;
    }

    private void OnDisable()
    {
        shrimp_EndEvent -= eventEnd;
    }

    public override void stating()
    {
        base.stating();

        shrimp_Event?.Invoke();
    }

    public override IEnumerator End()
    {
        return base.End();
    }

    public void eventEnd()
    {
        StartCoroutine(End());
    }
}