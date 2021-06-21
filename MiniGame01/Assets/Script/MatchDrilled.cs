using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MatchDrilled : MatchParent
{
    public GameManager gameManager;
    public LazerSponer sponerManager;

    public float appear_Timer;

    //생성할 Enemy;
    private GameObject obj;

    public enum EnemyKinds { 포크삼지창, 스파게티레이저 };

    public EnemyKinds enemyKinds = EnemyKinds.스파게티레이저;
    public EnemySponeDirecter enemySponeDirecter;

    public Quaternion forword_rotation(Vector3 posision, Vector3 position2)
    {
        var pos = (posision - position2).normalized;
        pos.y = 0;
        //Quaternion rot = Quaternion.LookRotation(pos);
        return Quaternion.LookRotation(pos);
    }

    public override IEnumerator middleBillen()
    {
        while (gameManager.gameSceneState == GameManager.GameSceneState.GameStart)
        {
            if (gameManager.gameSceneState != GameManager.GameSceneState.GameStart) yield break;

            yield return new WaitForSeconds(appear_Timer);

            {
                SponerCreate();
                yield return new WaitForSeconds(1f);
                SponerCreate();
                yield return new WaitForSeconds(1f);
                SponerCreate();
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }

    public void SponerCreate()
    {
        var pos = sponerManager.sponerList[Random.Range(0, sponerManager.sponerList.Length)].transform.position;

        pos.y = 0;
        transform.position = pos;

        obj = sponerManager.fulllList_Light[sponerManager.GetObj_Light()];

        if (obj != null && !obj.GetComponent<LazerEnemy>().Enemy.activeSelf)
        {
            obj.transform.position = transform.position;

            obj.transform.rotation = forword_rotation(
                                        enemySponeDirecter.transform.position,
                                        obj.transform.position);

            obj.gameObject.SetActive(true);
        }
    }
}