using UnityEngine;

public class RARE_GHOST : RARE_CHARACTER
{
    public ITEM_SKILL skill;

    [Header("기능시작타이머")]
    [SerializeField]
    private float ghost_deltaTime = 0;

    [SerializeField]
    private float ghost_Time = 5;

    [Header("기능대기타이머")]
    [SerializeField]
    private float prosessTimer = 0f;

    [SerializeField]
    private float ghost_prosessTime = 4;

    [SerializeField]
    private bool ghostProsess = false;

    public override void setting()
    {
        base.setting();
    }

    public override void play()
    {
        if (!ghostProsess)
        {
            if (ghost_Time < (ghost_deltaTime += Time.deltaTime))
            {
                //
                skill.transparencyItemSetup();

                ghostProsess = true;
            }
        }
        else
        {
            prosessTimer += Time.deltaTime;
            if (prosessTimer >= ghost_prosessTime)
            {
                //4초 후 timer =0;
                ghostProsess = false;
                prosessTimer = 0;
                ghost_deltaTime = 0;
            }
        }
    }
}