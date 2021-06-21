using UnityEngine;

public class Opal_ITEM : ITEM
{
    private float itemDelay = 4f;

    public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        anim.Play(AnimStateName);
    }

    public override void Settings()
    {
        has_enterAnim = Animator.StringToHash("OpalEnter");
        AnimStateName = "OpalAnimation";
    }

    private void OnDisable()
    {
        reset();
    }

    public void Update()
    {
        if (!gameObject.activeSelf) return;

        #region 애님 프레임시간  0.8 타이밍계산

        ///딜레이 주고 사라짐..
        if (!itemAnimExit)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimStateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                itemAnimExit = true;
                int rn = Random.Range(0, UserDataManager.instance.stateMachine.gmr.Player.itemCreate.Length);
                UserDataManager.instance.stateMachine.gmr.Player.itemCreate[rn].transform.position = transform.position;
                UserDataManager.instance.stateMachine.gmr.Player.itemCreate[rn].Play();
            }
        }

        #endregion 애님 프레임시간  0.8 타이밍계산

        #region 컬라이더 활성 시간계산

        if (itemAnimExit)
        {
            ItemCollider.enabled = true;
            itemdelayfersecond += Time.deltaTime;
            if (itemDelay <= itemdelayfersecond)
            {
                gameObject.SetActive(false);
                SmokeExpro();
            }
        }

        #endregion 컬라이더 활성 시간계산
    }
    public override void SmokeExpro()
    {
        base.SmokeExpro();
    }
    public override void reset()
    {
        base.reset();
    }
}