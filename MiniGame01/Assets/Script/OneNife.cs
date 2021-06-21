using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OneNife : MonoBehaviour
{
    public Transform nife;
    public ParticleSystem targetingEffect;
    public Vector3 updownRndposition = Vector3.zero;

    private float AttackSpeed = 0.5f;

    public enum enemy_Nife{none , patton1 , patton2 , patton3};
    public enemy_Nife e_nife;


    public void nifeStating()
    {
        //위아래 포지션 위치 세팅.
        updown_rnd();
        
        positionSetting(targetingEffect.transform , updownRndposition);
        StartCoroutine(nifeMoving());
    }

    private void OnEnable() 
    {
       nifeStating();
    }

   
    
    private void positionSetting(Transform tr , Vector3 output)
    {
        tr.position = output;
        targetingEffect.Play();
    }

    private IEnumerator nifeMoving()
    {
        yield return null;
        
        nifeReset();
        yield return new WaitForSeconds(3.45f);
        nife.DOMoveZ(updownRndposition.z, AttackSpeed ,false).SetOptions(true).SetEase(Ease.Linear).OnComplete(() =>
        {
           StartCoroutine(SleepingSecond(nife.gameObject,2));
        });

    }

    private IEnumerator SleepingSecond(GameObject obj , float second)
    {
        yield return new WaitForSeconds(second);
        obj.SetActive(false);
    }
    


    private void updown_rnd()
    {
        Vector3 rnd = new Vector3();
        rnd.x = transform.position.x;
        rnd.y = 0;
        rnd.z = UnityEngine.Random.Range(-8.0f , 14.0f);

        updownRndposition = rnd;
    }


    private void nifeReset()
    {
        nife.localPosition = Vector3.zero;
        if(!nife.gameObject.activeSelf)
        {
            nife.gameObject.SetActive(true);
        }
    }

}
