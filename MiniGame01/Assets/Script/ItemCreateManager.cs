using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gmr;

    public EnemySponeDirecter enemySponeDirecter;
    private int resulttimer = 1;
    public ITEM[] items;
    public int ItemCreateTimer;

    private int itemnum = 0;

    private void Update()
    {   //현재시간 >= 진행해야될시간 
        if (gmr.timer >= ItemCreateTimer * resulttimer && gmr.gameSceneState == GameManager.GameSceneState.GameStart)
        {
            //아이템생성
            int i = Random.Range(0, items.Length);
            if (i == itemnum) return;
            items[i].transform.position = enemySponeDirecter.transform.position;
            items[i].gameObject.SetActive(true);

            itemnum = i;
            resulttimer++;
            return;
        }
    }
}