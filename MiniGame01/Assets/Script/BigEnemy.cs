using System.Collections;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    public GameManager gmr;
    public Animator Anim;
    public static int ArrayIdx;

    public enum Next { 이전, 다음, End }

    public Next next = Next.End;

    public static bool BigEnemyRelayStop = false;

    public virtual void stating()
    {
        Debug.Log("starting");

        if (UserDataManager.instance.state == State.GameReStart)
        {
            Debug.Log("씬 = Gamerestart 시작됨.");
        }

        BigEnemyManager.EndBigEnemy = false;
    }

    public virtual void Update()
    {
        if (UserDataManager.instance.state == State.GameEnd)
        {
            BigEnemyRelayStop = true;
        }
    }

    public IEnumerator StatingEnd()
    {
        yield return null;
        yield return new WaitForSeconds(4);
        if (!BigEnemyRelayStop)
            gmr.bigEnemyManager.invoke();
    }

    public virtual IEnumerator End()
    {
        yield return null;
        yield return new WaitForSeconds(4);
        BigEnemyManager.EndBigEnemy = true;
        print("waitfor 2초");
        if (!BigEnemyRelayStop)
        {
            gmr.bigEnemyManager.invoke();
            print("!BigEnemyRelayStop");
        }
        print("BigEnemyRelayStop = " + BigEnemyRelayStop);
    }
}