using UnityEngine;

public class RARE_JSON : RARE_CHARACTER
{
    //제이슨 주위로 톱이 돌아나닌다. 적을 1회막아준다.

    public enum jsonHP
    {
        ZERO,
        HP1
    }

    public jsonHP jsonhp = jsonHP.HP1;

    public int JsonHP = 1;
    public blade[] topblade = new blade[3];

    public override void setting()
    {
        base.setting();

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = transform.position;
            topblade[i].rot = (i * 120);
            topblade[i].gameObject.SetActive(true);
        }
    }

    public override void play()
    {
        transform.rotation = Quaternion.AngleAxis(Time.time * 90f, Vector3.up);
    }
}