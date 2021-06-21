using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Vector3[] StartTweenPos;
    public Vector3[] EndTweenPos;

    public Vector3[] ResetPos;

    public ThrowSimulator throwSimulator;

    public Transform height;

    public Transform[] Endpos;

    private Tweener t;

    public Animator archerAnim;

    public static System.Action EndEvent = () => { };

    public int ArcherIndx;

    public Transform resetpos;

    public int EndCounting = 0;

    public AudioSource audioSource;
    public AudioClip fullingback;
    public AudioClip impact;


    public void SetArcher()
    {
        StartCoroutine(StartMover());
    }

    public void EndArcher()
    {
        StartCoroutine(EndMover());
    }

    //이동
    private IEnumerator StartMover()
    {
        yield return null;
        yield return new WaitForSeconds(2);
        t = transform.DOPath(StartTweenPos, 3, PathType.CatmullRom)
        .SetOptions(false).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(Attack());
        });
    }

    private IEnumerator Attack()
    {
        yield return null;
        yield return new WaitForSeconds(2);

        audioSource.clip = fullingback;
        audioSource.PlayOneShot(audioSource.clip);
        archerAnim.Play("shot");
    }

    /// <summary>
    /// 애니메이터의 이벤트로 실행
    /// </summary>
    public void ShootAtk()
    {
        int rnd = UnityEngine.Random.Range(0, Endpos.Length);
        audioSource.clip = impact;
        audioSource.PlayOneShot(audioSource.clip);

        throwSimulator.Shoot(throwSimulator.transform, throwSimulator.transform.position, Endpos[rnd].position, throwSimulator.g, height.position.z, onComplate, acher_end);
    }

    private void acher_end()
    {
    }

    public void onComplate()
    {
        //공격완료
        archerAnim.Play("None");
        StartCoroutine(EndMover());
    }

    public IEnumerator EndMover()
    {
        yield return null;
        yield return new WaitForSeconds(1);

        t = transform.DOPath(EndTweenPos, 3, PathType.CatmullRom)
       .SetOptions(false).SetEase(Ease.Linear).OnComplete(() =>
       {
           archerAnim.Play("None");
           throwSimulator.transform.position = resetpos.transform.position;
           throwSimulator.transform.rotation = resetpos.transform.rotation;
           ++Bear_Archer.bearIndx;
           //  Debug.Log("아처 = " + Bear_Archer.bearIndx + "명");

           if (6 == Bear_Archer.bearIndx)
           {
               EndEvent?.Invoke();
               Debug.Log("이벤트 실행");
               return;
           }
           //아처 공격종료
       });
    }
}