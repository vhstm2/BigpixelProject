using UnityEngine;
using UnityEngine.Events;

public class DrilledMovers : EnemyMoves
{
    public UnityEvent DrilledSkill;

    private void OnEnable()
    {
        time = 0;
        EnemyRot();
        EScale = (scaleLv)Random.Range((int)scaleLv.a, (int)scaleLv.max);
        transform.localScale = Vector3.one * (int)scaleLv.c;
    }

    public override void Update()
    {
        base.Update();
        DrilledSkill?.Invoke();
    }
}