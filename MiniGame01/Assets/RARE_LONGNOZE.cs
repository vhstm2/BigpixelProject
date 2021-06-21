using UnityEngine;

public class RARE_LONGNOZE : RARE_CHARACTER
{
    public ParticleSystem longnoze_efx;

    public override void setting()
    {
        if (player == null)
            player = FindObjectOfType<PlayerCtrl>();

        player.items_skill.Plus_OPAL = 3;
    }

    public void efx()
    {
        //위치 동기화
        longnoze_efx.transform.position = transform.position;

        //플레이
        longnoze_efx.Play();
    }
}