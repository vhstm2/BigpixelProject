using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigEnemyManager : MonoBehaviour
{
    [SerializeField]
    public List<UnityEvent> BigEnemys_Stage1 = new List<UnityEvent>();

    public List<UnityEvent> BigEnemys_Stage2 = new List<UnityEvent>();

    [SerializeField]
    public Dictionary<int, List<UnityEvent>> BigEnemy_All = new Dictionary<int, List<UnityEvent>>();

    public PlayerCtrl player;

    public bool Is_BigEnemys = false;

    public UnityEvent Enemy_Attacked;

    public int EnemyCount = 0;

    public static bool EndBigEnemy = true;

    public void OnEnable()
    {
    }

    public void Setting()
    {
        EnemyCount = 0;
        //스테이지 빅에너미 세팅!
        BigEnemy_All.Add(0, BigEnemys_Stage1);
        BigEnemy_All.Add(1, BigEnemys_Stage2);
    }

    public void invoke()
    {
        Is_BigEnemys = true;

        // if (!EndBigEnemy) return;
        if (UserDataManager.instance.state == State.Game || UserDataManager.instance.state == State.GameReStart)
        {
            var BigEnemyNumber = EnemyCount % BigEnemy_All[UserDataManager.instance.StageNumber].Count;
            // Debug.Log($"BigEnemyNumber = {BigEnemyNumber}");
            Enemy_Attacked = BigEnemy_All[UserDataManager.instance.StageNumber][BigEnemyNumber];
            EnemyCount++;

            Enemy_Attacked.Invoke();
        }
        else
        {
            invoke();
        }
    }
}