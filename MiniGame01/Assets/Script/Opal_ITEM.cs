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

        #region �ִ� �����ӽð�  0.8 Ÿ�ְ̹��

        ///������ �ְ� �����..
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

        #endregion �ִ� �����ӽð�  0.8 Ÿ�ְ̹��

        #region �ö��̴� Ȱ�� �ð����

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

        #endregion �ö��̴� Ȱ�� �ð����
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