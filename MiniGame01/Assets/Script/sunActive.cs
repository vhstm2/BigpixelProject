using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunActive : MonoBehaviour
{
    public GameObject[] ActiveObjs;

    public static int ActiveInt;

    public enum ActiveMiddleEnemy{none, 선,스파게티};
    public ActiveMiddleEnemy activeEnemy = ActiveMiddleEnemy.none;

    public void enabledFuns()
    {
        EnumSwithing(UserDataManager.instance.StageNumber);

        if(activeEnemy == ActiveMiddleEnemy.선)
        {
            //선일때는 차례대로 출격
            ActiveObjs[ActiveInt % ActiveObjs.Length].SetActive(true);
            ActiveInt++;
        }
        else if(activeEnemy == ActiveMiddleEnemy.스파게티)
        {
            //선이 아닐때는 스파게티 랜덤..
            var i = Random.Range(0,ActiveObjs.Length);
            ActiveObjs[i].SetActive(true);
        }

        
    }


    private void OnDisable()
    {
        foreach (var item in ActiveObjs)
        {
            item.SetActive(false);
        }
    }


    private void EnumSwithing(int n)
    {
        switch(n)
        {
            case 0:
            activeEnemy = ActiveMiddleEnemy.선;
            break;
            
            case 1:
            activeEnemy = ActiveMiddleEnemy.스파게티;
            break;

        }
    }


}
