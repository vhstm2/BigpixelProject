using DG.Tweening;
using UnityEngine;

public class RARE_MAGNITO : RARE_CHARACTER
{
    public ParticleSystem circle;

    private bool itemChecked = false;

    private Collider item;

    public override void setting()
    {
        base.setting();
        circle.gameObject.SetActive(true);
        circle.Play();
    }

    public override void colliderOverlap()
    {
        //주변에 아이템이 있으면 빨아들인다.
        var itemover = Physics.OverlapSphere(transform.position, 3.5f, 1 << LayerMask.NameToLayer("Item"));

        if (itemover.Length >= 1)
        {
            item = itemover[0];
            itemChecked = true;
        }
        else return;

        if (itemChecked)
        {
            var dir = (item.transform.position - transform.position).normalized;
            //   item.transform.DOMove(dir, 0.2f);
            item.transform.DOMove(transform.position, 0.25f);
            itemChecked = false;
            return;
        }
    }
}