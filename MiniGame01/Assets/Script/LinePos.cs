using UnityEngine;

public class LinePos : MonoBehaviour
{
    public LineRenderer line;
    public Transform lineCenter;
    public Transform maxpos;
    public Transform parents;
    public PlayerCtrl player;

    private float dis = 0;
    private bool LineMax = false;
    private RaycastHit hitInfo;

    public bool PlayerDead = false;

    private void Awake()
    {
        line.SetPosition(0, lineCenter.position);
    }

    private void OnEnable()
    {
        line.SetPosition(0, lineCenter.position);
        line.SetPosition(1, lineCenter.position);
        LineMax = false;
        PlayerDead = false;
    }

    private void OnDisable()
    {
        transform.position = lineCenter.position;
    }

    // Update is called once per frame
    private void Update()
    {
        float dis = Vector3.Distance(transform.position, maxpos.position);
        if (dis <= 0.6f) LineMax = true;
        line.SetPosition(1, transform.position);

        if (Physics.Linecast(lineCenter.position, transform.position, out hitInfo))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.collider.gameObject.name == "player" && !PlayerDead)
            {
                Debug.Log("플레이어 맞음.");
                PlayerDead = true;
                player.playerDeaded = PlayerDead;
            }
        }

        if (!LineMax)
            transform.position = Vector3.MoveTowards(transform.position, maxpos.position, Time.deltaTime * 100.0f);
    }
}