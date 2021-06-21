using UnityEngine;

public class RARE_SHELD : RARE_CHARACTER
{
    public ParticleSystem sheld_effect;

    public ParticleSystem sheld_die;

    public bool sheldOn = false;

    public bool transpaOnoff = false;

    public override void setting()
    {
        base.setting();
        sheldOn = true;
        
        sheld_effect.Play();

    }

    private int layer;

    public override void colliderOverlap()
    {

         layer = (
            (1 << 8) |
            (1 << 10) |
            (1 << 11) |
            (1 << 12) |
            (1 << 13) |
            (1 << 16) |
            (1 << 17) |
            (1 << 18) |
            (1 << 19) |
            (1 << 20)
            );

        if (transpaOnoff) return;

        var collider = Physics.OverlapSphere(
            transform.position,
            0.5f, layer);

        if (collider.Length > 0)
        {
            var ob = collider[0].GetComponentInParent<ITEM>();
            if (ob != null)
            {
                ob.Enter?.Invoke();
                ob.gameObject.SetActive(false);
            }
            else
            {
                if (player.coll == false) return;
                sheld_effect.Clear(true);
                sheld_effect.Stop(true);
                sheld_die.Play();

                Invoke("collOn", 0.8f);
            }
        }
    }

    private void collOn()
    {
        player.coll.enabled = true;
        sheldOn = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}