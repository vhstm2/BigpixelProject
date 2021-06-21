using UnityEngine;
using UnityEngine.Events;

public class EnemyMoves : MonoBehaviour
{
    public enum scaleLv { a = 1, b, c, max }

    public scaleLv EScale;

    private ITEM_SKILL item_Skill;

    public UnityEvent<float> Item_Speed;

    public GameObject[] BillenOb;
    protected GameObject Center;

    public virtual void EnemyRot()
    {
        var currpos = transform.position;

        Vector3 dir = (Center.transform.position - currpos).normalized;
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir);

        transform.rotation = rot;
    }

    protected float time = 0;

    public virtual void Awake()
    {
        Center = FindObjectOfType<PlayerCtrl>().gameObject;
        item_Skill = FindObjectOfType<ITEM_SKILL>();
    }

    private void OnEnable()
    {
        time = 0;
        EnemyRot();
        EScale = (scaleLv)Random.Range((int)scaleLv.a, (int)scaleLv.max);
        transform.localScale = Vector3.one * (int)EScale * 0.5f;

        BillenChangeOf();
    }

    private void BillenChangeOf()
    {
        BillenOb[UserDataManager.instance.Player_Eqip.StageNumber].SetActive(true);
        for (int i = 0; i < BillenOb.Length; i++)
        {
            if (i == UserDataManager.instance.Player_Eqip.StageNumber)
                continue;
            else if (BillenOb[i].activeSelf)
                BillenOb[i].SetActive(false);
        }
    }

    public Vector2 po = new Vector2();
    private float min = -0.5f;
    private float max = 1.5f;

    public virtual void Update()
    {
        if (this.gameObject.activeSelf)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 10.0f * item_Skill.slow.SlowSpeed
                * UserDataManager.instance.BillenSpeed);
            //  time += Time.deltaTime;
        }
        po = Camera.main.WorldToViewportPoint(transform.position);

        if (po.x <= min || po.x >= max || po.y < min || po.y > max)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        time += Time.deltaTime;
    }

    public virtual float Distance()
    {
        Vector3 Dpos = transform.position;
        float dis = (Dpos - Center.transform.position).magnitude;

        return dis;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("EnemyLazer"))
        //{
        //    transform.parent.gameObject.SetActive(false);
        //}
    }
}