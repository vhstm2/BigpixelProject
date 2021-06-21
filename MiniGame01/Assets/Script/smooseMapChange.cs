using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smooseMapChange : MonoBehaviour
{
    public GameManager gameManager;

    public int MapNumber = 0;

    public SpriteRenderer[] maps;

    public float MapChangeTimer;

    public bool flag = false;
    public bool change = false;

    [Tooltip("에너미카운트를 0으로 바꿀용도")]
    public BigEnemyManager bigenemyManager;

    // Update is called once per frame
    private void Update()
    {
        if (flag) return;
        if (MapNumber >= maps.Length - 1) return;

        //게임시간이 1분30초 마다 뭔가 실행한다.
        if (gameManager.timer >= MapChangeTimer * (MapNumber + 1) && gameManager.gameSceneState == GameManager.GameSceneState.GameStart)
        {
            bigenemyManager.EnemyCount = 0;
            change = true;

            //컬러Alpha값 변경. 1 ~ 0

            //maps[MapNumber]부터 차례대로
        }

        if (change)
        {
            var color = maps[MapNumber].color;

            color.a -= Time.deltaTime * 0.5f;
            maps[MapNumber].color = color;

            if (color.a <= 0f)
            {
                ++MapNumber;
                UserDataManager.instance.StageNumber = MapNumber;
                change = false;
                flag = true;
                return;
            }
        }
    }
}