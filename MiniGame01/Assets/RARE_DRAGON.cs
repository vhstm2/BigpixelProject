using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum fire_info
{
    bullet, tonaido
}
public class RARE_DRAGON : RARE_CHARACTER
{
    public PlayerCtrl playerDir;

    [SerializeField]
    private float FireTimer = 3f;

    [SerializeField]
    private float FIredeltaTimer = 0f;

    public List<fire> fireList = new List<fire>();

    public UnityEngine.UI.Slider firegage;

    public override void setting()
    {
        base.setting();
        foreach (var ob in fireList)
        {
            ob.transform.position = transform.position;
        }
        firegage.gameObject.SetActive(true);
    }
    public fire_info fireinfo = fire_info.bullet;

    public override void play()
    {
        //게임씬에있을때만
        if (UserDataManager.instance.stateMachine.gmr.gameSceneState !=
             GameManager.GameSceneState.GameStart) return;

        //발사타이밍 시간계산
        FIredeltaTimer += Time.deltaTime;

        float value = FIredeltaTimer % FireTimer;
        firegage.value = value;

        //발사할 시간이 됬다.
        if (FireTimer <= FIredeltaTimer)
        {
            //발사
            Debug.Log("드래곤 불꽃생성");
            if (fireinfo == fire_info.bullet)
                StartCoroutine(DragonFire());
            else
                StartCoroutine(toneido_Fire());

            FIredeltaTimer = 0;
        }
    }

    private fire[] fires = new fire[5];

    //공격생성? 발사?
    private IEnumerator DragonFire()
    {
        yield return null;

        for (int x = 0; x < 2; x++)
        { 
            int rnd = Random.Range(70, 360);
        
            //5방향
            for (int i = 0; i < 5; i++)
            {
                var fire = fireone();
                fires[i] = fire;
                fires[i].fireinfo = fire_info.bullet;
                fires[i].dirNumber = i * 72 + rnd;

                fires[i].transform.position = transform.position;
                fires[i].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    private IEnumerator toneido_Fire()
    {
        yield return null;
        for (int i = 0; i < 1; i++)
        {
            var fire = fireone();
            if (fire == null)
            {
                var f = Instantiate(fires[0],transform.position , Quaternion.identity);
                fires[i] = f;
            }
            else
            {
                fires[i] = fire;
            }

            fires[i].fireinfo = fire_info.tonaido;
            fires[i].dirNumber = 0;

            fires[i].transform.position = transform.position;
            fires[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private fire fireone()
    {
        foreach (var item in fireList)
        {
            if (item.gameObject.activeSelf == false)
            {
                return item;
            }
        }



        return new fire();
    }
}