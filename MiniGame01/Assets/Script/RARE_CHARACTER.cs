using UnityEngine;

public class RARE_CHARACTER : MonoBehaviour
{
    protected PlayerCtrl player = null;
    public float speed;

    public virtual void setting()
    {
        player = FindObjectOfType<PlayerCtrl>();
        player.items_skill.Plus_OPAL = 2;
    }

    public virtual float speedSet() => speed;

    public virtual void play()
    {
    }

    public virtual void colliderOverlap()
    { }
}