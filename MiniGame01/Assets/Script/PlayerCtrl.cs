using System;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public GameManager gameManager;

    //[System.NonSerialized]
    public int HpCount = 1;

    public VariableJoystick variableJoystick;

    public Rigidbody rb;

    [SerializeField]
    private float speed = 0f;

    //public NavMeshAgent agent;
    public Collider coll;

    public StateMachine stateMachine;

    public GameObject DeadEffect;

    [NonSerialized]
    public Vector3 dir = Vector3.zero;

    public Action PlayerDeadEvent;

    private bool playerdeaded;

    public bool playerDeaded
    {
        get => playerdeaded;
        set
        {
           
            dead_Particles[UnityEngine.Random.Range(0, dead_Particles.Length)].Play();
            playerDead();
        }
    }

    public charcter_infomation charcter_Info;

    public RARE_CHARACTER[] _rarecharcter;

    public RARE_SHELD sheld;

    public ITEM_SKILL items_skill;

    public ParticleSystem[] dead_Particles;

    public ParticleSystem heal_effect;

    //아이템습득
    [Header("아이템습득 이펙트")]
    public ParticleSystem itemAcquire;
    [Header("아이템생성 이펙트")]
    public ParticleSystem[] itemCreate;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charcter_Info = UserDataManager.instance.Player_Eqip.select_Character;

        SelectRARECharacterSetting(charcter_Info);
    }

    private void SelectRARECharacterSetting(charcter_infomation info)
    {
        switch (info.rare_character)
        {
            case Rare_Character.NONE:
                //일반케릭터
                break;

            case Rare_Character.톱:
                _rarecharcter[0].setting();
                speed = _rarecharcter[0].speedSet();
                break;

            case Rare_Character.기사:
                sheld.gameObject.SetActive(true);
                _rarecharcter[1].setting();
                speed = _rarecharcter[1].speedSet();

                break;

            case Rare_Character.돌고래:
                _rarecharcter[2].setting();
                speed = _rarecharcter[2].speedSet();
                break;

            case Rare_Character.드래곤:
                _rarecharcter[3].setting();
                speed = _rarecharcter[3].speedSet();
                break;

            case Rare_Character.고스트:
                _rarecharcter[4].setting();
                speed = _rarecharcter[4].speedSet();
                break;

            case Rare_Character.롱노즈:
                _rarecharcter[5].setting();
                speed = _rarecharcter[5].speedSet();
                break;

            case Rare_Character.매그니토:
                _rarecharcter[6].setting();
                speed = _rarecharcter[6].speedSet();
                break;
        }
    }

    public void Coliider_onoff(bool on)
    {
        coll.enabled = on;
    }

    private void Dead()
    {
        stateMachine.ChangeState("GameEnd");
    }

    public void playerDead()
    {
        Dead();
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (UserDataManager.instance.state == State.GameEnd) return;

        if (coll.gameObject.layer == LayerMask.GetMask("Item") || coll.CompareTag("Item"))
        {

            var obj = coll.GetComponentInParent<ITEM>();
            obj.Enter?.Invoke();
            
            itemAcquire.transform.position = obj.transform.position; 
            itemAcquire.Play();
            obj.gameObject.SetActive(false);
            items_skill.item_Acquisition++;
        }

        //if (sheld.sheldOn) return;

        if (charcter_Info.rare_character == Rare_Character.기사)
        {
            if (sheld.sheldOn) return;
        }

        if ((coll.CompareTag("Enemy") || coll.CompareTag("EnemyLazer") || coll.CompareTag("RobotLazer") || coll.CompareTag("RobotBody") ||
            coll.CompareTag("Archer_weapon") || coll.CompareTag("nife") || coll.CompareTag("tongs") || coll.CompareTag("shrimp")))
        {
            gameManager.HpCountText.text = "HP : " + HpCount.ToString();

            playerDeaded = true;
        }
    }

    private bool MapOut = false;

    private float min = 0.02f;
    private float max = 0.98f;

    private void MapOutViewPosition()
    {
        //화면 밖으로 나가라면 포지션 고정시킴.
        var viewPos = Camera.main.WorldToViewportPoint(transform.position);
        //뷰포트연산 화면을 x = 0~1 y = 0~1 로 변환해서 고정

        if (viewPos.x < 0f) viewPos.x = min;
        if (viewPos.x > 1.0f) viewPos.x = max;
        if (viewPos.y < 0f) viewPos.y = min;
        if (viewPos.y > 1.0f) viewPos.y = max;

        if (viewPos.x > min || viewPos.x < max || viewPos.y > min || viewPos.y < max)
        {
            //고정후 다시 월드포지션으로 변환에서 위치변환.
            MapOut = true;
            transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        }
        else
        {
            MapOut = false;
        }
    }

    private Vector3 dirD()
    {
        return dir;
    }

    private void Update()
    {
        sheld.transpaOnoff = items_skill.transparencyOnOff;

        dir = (transform.forward * variableJoystick.Vertical) +
                 (transform.right * variableJoystick.Horizontal);

        transform.Translate(dir * (speed * Time.deltaTime) * UserDataManager.instance.BillenSpeed);

        //rb.velocity = dir * (speed * Time.deltaTime) * UserDataManager.instance.BillenSpeed;

        MapOutViewPosition();

        SelectRARECharacterprosess(charcter_Info);
    }

    private float prosessTime = 0f;

    private void SelectRARECharacterprosess(charcter_infomation info)
    {
        switch (info.rare_character)
        {
            case Rare_Character.NONE:
                items_skill.Plus_OPAL = 3;
                break;

            case Rare_Character.톱:

                _rarecharcter[0].play();
                //_rarecharcter[0].colliderOverlap();
                break;

            case Rare_Character.기사:
                if (!sheld.sheldOn) return;
                //if (sheld.sheldOn)
                coll.enabled = false;

                _rarecharcter[1].colliderOverlap();
                break;

            case Rare_Character.돌고래:
                break;

            case Rare_Character.드래곤:
                _rarecharcter[3].play();
                break;

            case Rare_Character.고스트:
                _rarecharcter[4].play();
                break;

            case Rare_Character.롱노즈:
                break;

            case Rare_Character.매그니토:
                _rarecharcter[6].colliderOverlap();
                break;
        }
    }
}