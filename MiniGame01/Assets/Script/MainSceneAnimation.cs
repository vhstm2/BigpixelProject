using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainSceneAnimation : MonoBehaviour
{
    public SpriteRenderer mushiroom;
    public SpriteRenderer herEffect;
    public SpriteRenderer bear;
    public Sprite[] bears;
    public enum bearState{stop,Tracking};
    private bearState bearstate = bearState.stop;
    private int spriteNum =0;
    private float time = 0f;

    private bool isleft =false;


    private void Start() 
    {
        StartCoroutine(stating());   
    }
         
    
    IEnumerator stating()
    {
        yield return new WaitForSeconds(3);
        AnimationStarting(isleft);
    }


    private void AnimationStarting(bool isLeft)
    {
        int n = 0;
        
        n = isLeft ? 2 : -2;
        
        this.isleft = !isLeft;
        mushiroom.flipX = isLeft;

        mushiroom.transform.DOMoveX(n,3f).SetOptions(false).SetEase(Ease.Linear).OnComplete(() =>
        {
           mushiroom.flipX = isleft;
           bear.flipX = !isleft;
           this.isleft = !this.isleft;
           herEffect.gameObject.SetActive(true);
           Invoke("jump",0.5f);
        });
    }
    private void jump()
    {
        int n = 0;
        
        n =  isleft ? 13 : -13;
        
        bearstate = bearState.Tracking;
        herEffect.gameObject.SetActive(false);
        bear.transform.DOMoveX(n,5).OnComplete(() =>{
            bearstate = bearState.stop;

            AnimationStarting(!this.isleft);
        });
        mushiroom.transform.DOJump(mushiroom.transform.position , 1f, 1, 0.5f,false).SetEase(Ease.Linear).OnComplete(() =>{
            mushiroom.flipX = isleft;
            mushiroom.transform.DOMoveX(n,3f).SetOptions(false);
        });
    }

    private void Update() 
    {
        if(bearstate == bearState.Tracking)
        {
            time += Time.deltaTime;
            if(time >= 0.2f)
            {
                time =0;
                bear.sprite = bears[spriteNum++];
                if(spriteNum == 3) spriteNum = 0;
            }
        }
    }
}