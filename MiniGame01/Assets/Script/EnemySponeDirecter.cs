using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponeDirecter : MonoBehaviour
{
    private float timer;

    public void sponnerRandomPostion()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-20.0f, 20.0f);
        pos.y = 0;
        pos.z = Random.Range(-10.0f, 10.0f);

        transform.position = pos;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (0.5f <= timer)
        {
            timer = 0;
            sponnerRandomPostion();
        }
    }
}