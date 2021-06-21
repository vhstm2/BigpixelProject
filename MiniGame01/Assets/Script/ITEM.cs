using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ITEM : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent Enter;

    public LayerMask layer;
    public Collider ItemCollider;
    public Animator anim;
    public float itemdelayfersecond = 0;
    public ParticleSystem smokeEx;

    protected int has_enterAnim;
    protected bool itemAnimExit = false;
    protected string AnimStateName;

    public virtual void Awake()
    {
        Settings();
    }

    public virtual void OnEnable()
    {
    }

    public virtual void Settings()
    {
    }

    public virtual void SmokeExpro()
    {
        smokeEx.transform.position = transform.position;
        smokeEx.Play();
    }


    public virtual void reset()
    {
        //풀로 들어감.
        ItemCollider.enabled = false;
        itemAnimExit = false;
        itemdelayfersecond = 0;
        anim.Play("None");
    }
}