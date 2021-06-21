using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lazer_launch : MonoBehaviour
{
    private Vector3 firstPosition = new Vector3();

    private void OnEnable()
    {
        firstPosition = transform.localPosition;
        transform.localPosition = firstPosition;
        transform.DOMove(transform.position + transform.forward * 150.0f, 5.0f).OnComplete(Secc);
    }

    private void Secc()
    {
        gameObject.SetActive(false);
        transform.localPosition = firstPosition;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            coll.transform.parent.gameObject.SetActive(false);
        }
        else if (coll.CompareTag("EnemyLazer"))
        {
            coll.transform.parent.parent.parent.gameObject.SetActive(false);
        }
    }
}