using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemy : MonoBehaviour
{
    // [System.NonSerialized]
    public LazerSponer lazersponer = null;

    public GameObject Enemy;
    public LazerMover lazerMover;

    public void OnEnable()
    {
        //SponerCreate();

        StartCoroutine(SponerCreate());
    }

    public void Awake()
    {
        lazersponer.LazerList.Add(Enemy);
    }

    private IEnumerator SponerCreate()
    {
        yield return new WaitForSeconds(0.5f);

        var pos = lazersponer.sponerList[Random.Range(0, lazersponer.sponerList.Length - 1)].transform.position;

        pos.y = 0;

        var viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0) viewPos.x = 0.05f;
        if (viewPos.x > 1.0f) viewPos.x = 0.95f;
        if (viewPos.y < 0) viewPos.y = 0.05f;
        if (viewPos.y > 1.0f) viewPos.y = 0.95f;

        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        // yield return new WaitForSeconds(2.0f);
        yield return new WaitForSeconds(2);

        if (Enemy != null && !Enemy.activeSelf)
        {
            Enemy.transform.position = transform.position;

            var euler = transform.rotation.eulerAngles;
            Enemy.transform.rotation = Quaternion.Euler(euler);

            Enemy.gameObject.SetActive(true);
        }
        yield return null;
        gameObject.SetActive(false);
    }
}