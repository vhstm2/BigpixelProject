using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerAnim : MonoBehaviour
{
    private float time;

    private bool scaletigger = false;

    // Update is called once per frame
    private void Update()
    {
        StateAnim();
    }

    public void StateAnim()
    {
        if (!scaletigger)
        {
            transform.localScale = Vector3.one * (1 + time);
            time += Time.deltaTime * 0.3f;
            if (time > 0.3f)
            {
                transform.localScale = Vector3.one;

                time = 0;
            }
        }
    }
}