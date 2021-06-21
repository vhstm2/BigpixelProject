using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class charcter_infomation
{
    /// <summary>
    /// 노말
    /// 레어
    /// </summary>
    [Tooltip("노말/레어")]
    public Character E_charcter;

    /// <summary>
    /// 레어케릭터
    /// </summary>
    [Tooltip("레어케릭터")]
    public Rare_Character rare_character;

    [Tooltip("케릭터 번호?")]
    public int charcterindx;

    [Tooltip("케릭터 네임?")]
    public string name;

    [Tooltip("케릭터 어빌리티")]
    public string Abillity;

    [Tooltip("케릭터 설명?")]
    public string text;

    public Sprite GamePlayCharacter;

    /// <summary>
    /// true = 사용할수있다
    /// false = 구매해야된다 사용불가
    /// </summary>

    public bool is_purchase;
}

public enum Rare_Character
{
    NONE, 톱, 기사, 돌고래, 드래곤, 고스트, 롱노즈, 매그니토, ㅁ6, ㅁ7
}

public class UserDataManager : MonoBehaviour
{
    private static UserDataManager Instance;

    public static UserDataManager instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = (UserDataManager)FindObjectOfType(typeof(UserDataManager));
                if (Instance == null)
                {
                    // Debug.Log("There's no active UserDataManager object");
                }
            }
            return Instance;
        }
    }

    public State state = State.Main;

    public StateMachine stateMachine;

    public int ch_number = 0;

    /// <summary>
    /// 사용가능한 케릭터 입력
    /// </summary>

    public void Awake()

    {
        if (Instance == null)
            DontDestroyOnLoad(this);

        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        // 델리게이트 체인을 걸어준다. Awake -> OnEnable -> OnLevelLoaded -> Start 순이라서 첫 씬도
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        // 레벨 변경시에 호출됨.
        if (scene.name == "Main")
        {
            if (main_reference == null)
            {
                main_reference = FindObjectOfType<MainSceneReference>();
            }
            main_reference.GoldText.text = Player_Eqip.Money.ToString();
            main_reference.OpalText.text = Player_Eqip.OpalCount.ToString();
        }
        else if (scene.name == "Shop")
        {
            if (shop_reference == null)
            {
                shop_reference = FindObjectOfType<shop_ref>();
            }
            shop_reference.GameMoney.text = Player_Eqip.Money.ToString();
            shop_reference.OpalMoney.text = Player_Eqip.OpalCount.ToString();
        }
        else if (scene.name == "Charactor")
        {
            if (character_Ref == null)
                character_Ref = FindObjectOfType<UserDataContent>();

            character_Ref.P_Money.text = Player_Eqip.Money.ToString();
            character_Ref.P_OPAL.text = Player_Eqip.OpalCount.ToString();
        }

        stateMachine = FindObjectOfType<StateMachine>();
        MapChanger = FindObjectOfType<StageChange>();
    }

    public StageChange MapChanger;

    public Character character;
    public Sprite[] UserSprites;

    public Eqip Player_Eqip;

    [Header("레퍼런스 Main")]
    public MainSceneReference main_reference;

    [Header("레퍼런스 Shop")]
    public shop_ref shop_reference;

    [Header("레퍼런스 Character")]
    public UserDataContent character_Ref;

    public Dictionary<string, Localizing> locallizingDic = new Dictionary<string, Localizing>();
    //=================================================//

    #region MapIndex

    private int MapIndex;

    public int mapindex
    {
        get => MapIndex;
        set
        {
            MapIndex = value;
            Player_Eqip.idx = MapIndex;
        }
    }

    #endregion MapIndex

    //=================================================//

    #region GameMoney

    private int Gamemoney;

    public int gameMoney
    {
        get => Gamemoney;
        set
        {
            Gamemoney = value;
            Player_Eqip.Money = Gamemoney;
            if (main_reference != null)
            {
                main_reference.GoldText.text = Player_Eqip.Money.ToString();
            }
            else if (shop_reference != null)
            {
                shop_reference.GameMoney.text = Player_Eqip.Money.ToString();
            }
            else if (character_Ref != null)
            {
                character_Ref.P_Money.text = Player_Eqip.Money.ToString();
            }
        }
    }

    #endregion GameMoney

    //=================================================//

    #region StageNuber

    private int stagenumber;

    public int StageNumber
    {
        get => stagenumber;
        set
        {
            stagenumber = value;
            Player_Eqip.StageNumber = stagenumber;
            //   MapChanger.Change();
        }
    }

    #endregion StageNuber

    //=================================================//

    #region OpalMoney

    private int OpalMoney;

    public int opalmoney
    {
        get => Player_Eqip.OpalCount;
        set
        {
            OpalMoney = value;

            Player_Eqip.OpalCount = OpalMoney;
            if (main_reference != null)
            {
                main_reference.OpalText.text = Player_Eqip.OpalCount.ToString();
            }
            if (shop_reference != null)
            {
                shop_reference.OpalMoney.text = Player_Eqip.OpalCount.ToString();
            }
            if (character_Ref != null)
            {
                character_Ref.P_OPAL.text = Player_Eqip.OpalCount.ToString();
            }
        }
    }

    #endregion OpalMoney

    //=================================================//

    #region 슬로우

    private int item_slow;

    public int Item_Slow
    {
        get => item_slow;
        set
        {
            item_slow = value;
            Player_Eqip.Slow = item_slow;
        }
    }

    #endregion 슬로우

    //=================================================//

    #region 투명

    private int item_Transparency;          //투명

    public int Item_Transparency
    {
        get => item_Transparency;
        set
        {
            item_Transparency = value;
            Player_Eqip.Transparenc = item_Transparency;
        }
    }

    #endregion 투명

    //=================================================//

    #region 지우개

    private int item_eraser;

    public int Item_Eraser
    {
        get => item_eraser;
        set
        {
            item_eraser = value;
            Player_Eqip.Eraser = item_eraser;
        }
    }

    #endregion 지우개

    //=================================================//

    [System.NonSerialized]
    public float Following_Counting = 5;

    [System.NonSerialized]
    public int BillenSpeed = 1;

    [System.NonSerialized]
    public float SceneFadeOutSecond = 1f;

    [System.NonSerialized]
    public float timer = 1800f;

    public void QuitGame()
    {
        Application.Quit();
    }

    private int ac_pur = 0;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Scene GetScene = SceneManager.GetActiveScene();

            if (GetScene.name == "Main")
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    //var eqip = Player_Eqip;

                    //string data = JsonUtility.ToJson(eqip);
                    //GooglePlayGPGS.instance.playnanooSave(Social.localUser.id, data, QuitGame);
                    QuitGame();
                }
                else
                {
                    var eqip = Player_Eqip;
                    eqip.StageNumber = 0;
                    eqip.rankScore = (int)UserDataManager.instance.stateMachine.gmr.timer;
                    string data = JsonUtility.ToJson(eqip);
                    GooglePlayGPGS.instance.playnanooSave("bigpixel", data);
                    QuitGame();
                }
            }
            //if (GetScene.name == "Shop"
            //    || GetScene.name == "Charactor"
            //    || GetScene.name == "Stage")
            if (GetScene.name == "Game")
            {
                return;
            }
            else
            {
                stateMachine.ChangeState("Main");
            }
        }

        if (timer <= 0f)
        {
            //보상줌
            //1. 오팔
            //2. 골드
            //3. 아이템

            timer = 0f;
        }
        else
        {
            timer -= Time.deltaTime;
            Player_Eqip.timer = timer;
        }

        if (!Player_Eqip.achiement_Secces)
        {
            var list = Player_Eqip.Achiement.FindAll(x => x == false);

            if (list == null || list.Count == 0)
            {
                Player_Eqip.achiement_Secces = true;
                Achiement_Secces(10);
            }
        }

        if (!Player_Eqip.Allcharacter_Secces)
        {
            for (int i = 0; i < Player_Eqip.user_Characters.Length; i++)
            {
                if (Player_Eqip.user_Characters[i].is_purchase == false)
                {
                    ac_pur = 0;
                    break;
                }

                ac_pur++;
            }
         
            if (ac_pur >= 11)
            {
                Player_Eqip.Allcharacter_Secces = true;
                Achiement_Secces(9);
            }
        }

        
    }

    public void Achiement_Secces(int index)
    {
        if (Player_Eqip.Achiement[index]) return;

        Player_Eqip.Achiement[index] = true;
        opalmoney += Player_Eqip.Achiement_int[index];
    }
}

[Serializable]
public class Eqip
{
    public charcter_infomation select_Character;

    public charcter_infomation[] user_Characters;
    public int idx;
    public int Money;
    public int StageNumber;
    public int OpalCount;
    public int Slow;                //슬로우
    public int Transparenc;         //투명
    public int Eraser;              //지우개
    public float timer;
    public int rewardIndx;
    public int rankScore;

    public List<bool> Achiement;

    public List<int> Achiement_int;

    public int AdsConut;

    public bool achiement_Secces;
    public bool Allcharacter_Secces;

    public Eqip()
    {
    }

    public Eqip
        (
        int idx,
        int Money,
        int StageNumber,
        int OpalCount,
        int Slow,
        int Transparenc,
        int Eraser,
        float timer,
        int rewardindex,
        int rankscore,
        List<bool> Achiement,
        List<int> Achiement_int
        )
    {
        this.idx = idx;
        this.Money = Money;
        this.StageNumber = StageNumber;
        this.OpalCount = OpalCount;
        this.Slow = Slow;
        this.Transparenc = Transparenc;
        this.Eraser = Eraser;
        this.timer = timer;
        this.rewardIndx = rewardindex;
        this.rankScore = rankscore;
        this.Achiement = Achiement;
        this.Achiement_int = Achiement_int;
    }
}