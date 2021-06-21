using System.Collections;
using UnityEngine;

public class Mat : MatchParent
{
    public GameManager gameManager;
    public SponerManager sponerManager;

    //생성할 Enemy;
    private GameObject[] obj = new GameObject[5];

    public enum EnemyKinds { 포크삼지창, 스파게티레이저 };

    public EnemyKinds enemyKinds = EnemyKinds.포크삼지창;

    public override IEnumerator commot()
    {
        while (true)
        {
            //Debug.Log("빌런1들 나와라");
            if (gameManager.gameSceneState == GameManager.GameSceneState.GameStart)
            {
                var pos = sponerManager.sponerList[Random.Range(0, sponerManager.sponerList.Length - 1)].transform.position;

                pos.y = 0;
                transform.position = pos;

                //기본
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[i] = sponerManager.GetObj();

                    if (obj[i] != null)
                    {
                        obj[i].transform.position = transform.position;
                        obj[i].gameObject.SetActive(true);
                    }
                }
            }

            yield return new WaitForSeconds(Random.Range(0.3f, 1.0f));
            yield return null;
        }
    }
}