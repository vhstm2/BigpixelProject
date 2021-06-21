using UnityEngine;
using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ThrowSimulator : MonoBehaviour

{
    public AudioSource audioSource = null;
    public AudioClip throwFlying = null;
    private Transform bullet;   // 포물체

    private float tx;

    private float ty;

    private float tz;

    private float v;

    public float g = 9.8f;

    private float elapsed_time;

    public float max_height;

    private float t;

    private Vector3 start_pos;

    private Vector3 end_pos;

    private float dat;  //도착점 도달 시간

    public float speed = 0;

    public void Shoot(Transform bullet, Vector3 startPos, Vector3 endPos, float g, float max_height, System.Action onComplete , System.Action disable = null)

    {
        // target = endPos;
        start_pos = startPos;

        end_pos = endPos;

        this.g = g;

        this.max_height = max_height;

        this.bullet = bullet;

        this.bullet.position = start_pos;

        //var dh = endPos.y - startPos.y;
        var dh = endPos.z - startPos.z;

        //var mh = max_height - startPos.y;
        var mh = max_height - startPos.z;

        //ty = Mathf.Sqrt(2 * this.g * mh);
        tz = Mathf.Sqrt(2 * this.g * mh);

        float a = this.g;

        //float b = -2 * ty;
        float b = -2 * tz;

        float c = 2 * dh;

        dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        tx = -(startPos.x - endPos.x) / dat;

        // tz = -(startPos.z - endPos.z) / dat;
        ty = -(startPos.y - endPos.y) / dat;

        this.elapsed_time = 0;

        StartCoroutine(this.ShootImpl(onComplete, disable));
    }

    private Vector3 p;

    private IEnumerator ShootImpl(System.Action onComplete , System.Action disble =null)
    {
        if (audioSource != null)
        {
            audioSource.clip = throwFlying;
            audioSource.PlayOneShot(audioSource.clip);
        }
        while (true)
        {
            this.elapsed_time += Time.deltaTime * speed;

            var tx = start_pos.x + this.tx * elapsed_time;

            var ty = start_pos.y + this.ty * elapsed_time;// - 0.5f * g * elapsed_time * elapsed_time;

            var tz = start_pos.z + this.tz * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;

            var tpos = new Vector3(tx, transform.position.y, tz);

            bullet.transform.LookAt(tpos);

            bullet.transform.position = tpos;

            if (this.elapsed_time >= this.dat)
                break;

            yield return null;
        }

        onComplete();
        disble();
    }
}