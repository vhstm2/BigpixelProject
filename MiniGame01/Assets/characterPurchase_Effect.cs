using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterPurchase_Effect : MonoBehaviour
{

    public List<ParticleSystem> particles = new List<ParticleSystem>();

    private Vector3 particlePos = Vector3.zero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(purchase_effect_play());
    }

    public IEnumerator purchase_effect_play()
    {
        yield return null;
       
        foreach (var item in particles)
        {
          //  particlePos.Set(Random.Range(-0.5f , 6.0f) , Random.Range(-3.0f , 2.5f),0);
          //  item.transform.position =  particlePos;
            item.Play();
            yield return null;
        }
    }


}
