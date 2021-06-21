using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyRobot : BigEnemy
{
    public Vector3[] tweenerPosition;

    public Vector3[] tweenerEndPosition;

    public Vector3[] resetpos;
    public PathType pathType = PathType.CatmullRom;

    public GameObject[] launcher;

    public bool RobotStarting;

    public enum RobotState { 등장, 레이저발사, 퇴장 }

    public RobotState robotState = RobotState.퇴장;

    public Tween t;
    public AudioSource leftLazersource;
    public AudioSource rightLazersource;
    public AudioClip beamClip;

   

    [System.NonSerialized]
    public bool CansleSystem = false;

    private void Awake()
    {
        Anim.speed = 0;
    }

    public override void stating()
    {
        base.stating();
        StartCoroutine(Robot_Stating());
    }

    public IEnumerator Robot_Stating()
    {
        Anim.speed = 0;
        //yield return new WaitForSeconds(5);

        if (CansleSystem) yield return null;
        yield return new WaitForSeconds(3.0f);

        StartCoroutine("StartPathMove");
        //Invoke("StartPathMove", 3.0f);

        yield return null;
    }

    private IEnumerator StartPathMove()
    {
        robotState = RobotState.등장;
        yield return new WaitForSeconds(3);
        if (CansleSystem) yield return null;
        t = transform.DOPath(tweenerPosition, 3, pathType)
        .SetOptions(false).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
         
            //로봇이 등장위치까지 왔다.
            //1. 고개 까딱까딱하기

            Anim.speed = 1;
            //2. 로봇 레이저 발사

            StartCoroutine("RocatLazer");

            //3. 화면 밖까지 나가면 레이저 비활성
            //4. 로봇 후퇴

            //5. 후퇴 후 수초후 로봇 다시 등장
        });
    }

    private IEnumerator RocatLazer()
    {
        yield return new WaitForSeconds(3);
        Anim.speed = 0.0f;
        robotState = RobotState.레이저발사;
        // for (int i = 0; i < launcher.Length; i++)
        {
            // if (CansleSystem) yield return null;
            StartCoroutine(Leftlauncher());
            //Invoke("Leftlauncher", 0.1f);
            // if (CansleSystem) yield return null;
            StartCoroutine(Rightlauncher());
            //Invoke("Rightlauncher", 0.5f);

            //launcher[i].SetActive(true);
        }
        if (CansleSystem) yield return null;

        yield return new WaitForSeconds(2);

        StartCoroutine(e());
    }

    private IEnumerator Leftlauncher()
    {
        yield return new WaitForSeconds(0.1f);
        leftLazersource.PlayOneShot(beamClip);
        launcher[0].SetActive(true);
    }

    private IEnumerator Rightlauncher()
    {
        yield return new WaitForSeconds(0.5f);
        rightLazersource.PlayOneShot(beamClip);
        launcher[1].SetActive(true);
    }

    /// <summary>
    /// 로봇 퇴장
    /// </summary>
    /// <returns></returns>
    //public override IEnumerator End()
    //{
    //    yield return null;
    //    robotState = RobotState.퇴장;
    //    // if (CansleSystem) return;
    //    t = transform.DOPath(tweenerEndPosition, 4, pathType)
    //    .SetOptions(false).OnComplete(() =>
    //    {
    //        Tween_Stop();
    //    });
    //    yield return base.End();
    //}
    public override IEnumerator End()
    {
        yield return base.End();
    }

    private IEnumerator e()
    {
        yield return null;
        robotState = RobotState.퇴장;
        // if (CansleSystem) return;
        t = transform.DOPath(tweenerEndPosition, 4, pathType)
        .SetOptions(false).OnComplete(() =>
        {
            endCommt();
        });
    }

    private void endCommt()
    {
        StartCoroutine("End");
    }

    public void Tween_Stop()
    {
        CansleSystem = true;
        switch (robotState)
        {
            case RobotState.등장:
                //  CancelInvoke("RocatLazer");
                // t.Kill();
                End();
                break;

            case RobotState.레이저발사:
                //  CancelInvoke("Leftlauncher");
                //  CancelInvoke("Rightlauncher");
                //  CancelInvoke("Robot_End");
                // t.Kill();
                End();
                break;

            case RobotState.퇴장:
                break;
        }
    }

    public override void Update()
    {
        base.Update();
    }

    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.CompareTag("Enemy"))
    //    {
    //        coll.transform.parent.gameObject.SetActive(false);
    //    }
    //}
}

//public Transform target;
//public PathType pathType = PathType.CatmullRom;

//public Vector3[] waypoints = new[] {
//        new Vector3(4, 2, 6),
//        new Vector3(8, 6, 14),
//        new Vector3(4, 6, 14),
//        new Vector3(0, 6, 6),
//        new Vector3(-3, 0, 0)
//    };

//private Tween t;

//private void Start()
//{
//    // Create a path tween using the given pathType, Linear or CatmullRom (curved).
//    // Use SetOptions to close the path
//    // and SetLookAt to make the target orient to the path itself
//    t = target.DOPath(waypoints, 4, pathType)
//        .SetOptions(true);
//    //.SetLookAt(0.001f);
//    // Then set the ease to Linear and use infinite loops
//    //t.SetEase(Ease.Linear).SetLoops(-1);
//}