using GooglePlayGames;
using GooglePlayGames.OurUtils;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using PlayNANOO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;

 public class GooglePlayGPGS : MonoBehaviour
 {
     private static GooglePlayGPGS Instance;

    public static GooglePlayGPGS instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = (GooglePlayGPGS)FindObjectOfType(typeof(GooglePlayGPGS));
                if (Instance == null)
                {
                    Debug.Log("There's no active GooglePlayGPGS object");
                }
            }
            return Instance;
        }
    }

//     //private bool isSuccess = false;
//     //private BackendReturnObject bro = new BackendReturnObject();

//     //private string GoogleNickName = "google";

    public bool Authenticated { get { return Social.Active.localUser.authenticated; } }

    [NonSerialized]
    public Plugin plugin;

    private const string tableCode = "bigpixel966550157665-RANK-41CD7AB1-1B67F1C7";

    public string GetTokens()
    {
        if (Authenticated)
        {
            // 유저 토큰 받기 첫번째 방법
           string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            MessagePopManager.instance.ShowPop("토큰받기승인");
            // 두번째 방법
            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            return _IDtoken;
        }
        else
        {
            Debug.Log("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            MessagePopManager.instance.ShowPop("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            return null;
        }
    }

    public void GoogleLederBoardUI()
    {
        if (Authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void LeaderBoardPostring(long score, Action puncA = null)
    {
        if (Authenticated)
        {
            GoogleLederBoardPostingScore(GPGSIds.leaderboard, score);
        }

        {
            #region 플레이나누 랭킹서버에 저장

            if (Application.platform == RuntimePlatform.WindowsEditor) return;

            //랭킹점수도 저장시켯슴.
            if (UserDataManager.instance.Player_Eqip.rankScore >= UserDataManager.instance.stateMachine.gmr.timer) return;

            plugin = playnanooStart();

            plugin.RankingRecord(tableCode, (int)score, score.ToString(), (state, message, rawData, dictionary) =>
            {
                if (state.Equals(Configure.PN_API_STATE_SUCCESS))
                {
                    Debug.Log("랭킹저장Success");
                    MessagePopManager.instance.ShowPop("랭킹점수저장성공");
                    puncA?.Invoke();
                }
                else
                {
                    Debug.Log("랭킹저장Fail");
                    MessagePopManager.instance.ShowPop("랭킹점수저장실패");
                }
            });

            #endregion 플레이나누 랭킹서버에 저장
        }
    }

    public void GoogleLederBoardPostingScore(string LeaderBoardID, long PostingScore)
    {
        Social.ReportScore(PostingScore, LeaderBoardID, (bool success) =>
        {
            if (success)
            {
                //LoadDataText.text = $"리더보드 {PostingScore}게시 성공";
                MessagePopManager.instance.ShowPop(PostingScore + "점 게시 ");
            }
            else
            {
                // LoadDataText.text = $"리더보드 게시 Error";
            }
        });
    }

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

    public Plugin playnanooStart()
    {
        plugin = Plugin.GetInstance();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            plugin.SetUUID("bigpixel");
            plugin.SetNickname("hyun");
            plugin.SetLanguage(Configure.PN_LANG_KO);
        }
        else
        {
            plugin.SetUUID(Social.localUser.id);
            plugin.SetNickname(Social.localUser.userName);
            plugin.SetLanguage(Configure.PN_LANG_KO);
        }
        return plugin;
    }

    public Plugin PlayNanooreceipt_Start(string id)
    {
        plugin = Plugin.GetInstance();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            plugin.SetUUID("bigpixel_receipt");
            plugin.SetNickname("hyun_receipt");
            plugin.SetLanguage(Configure.PN_LANG_KO);
        }
        else
        {
            plugin.SetUUID(id + "_receipt");
            plugin.SetNickname(Social.localUser.userName + "_receipt");
            plugin.SetLanguage(Configure.PN_LANG_KO);
        }
        return plugin;
    }

    public void PlayNanooSave_receipt(string id, string key, string data)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) return;
        plugin = PlayNanooreceipt_Start(id);

        plugin.StorageSave(key, data, true, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void playnanooSave(string key, string data)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) return;
        plugin = playnanooStart();

        plugin.StorageSave(key, data, true, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void playnanooLoad(string key)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) return;
        plugin = playnanooStart();

        plugin.StorageLoad(key, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                var data = dictionary["value"].ToString();

                var myData = JsonUtility.FromJson<Eqip>(data);

                UserDataManager.instance.mapindex = myData.idx;
                UserDataManager.instance.gameMoney = myData.Money;
                UserDataManager.instance.StageNumber = myData.StageNumber;
                UserDataManager.instance.opalmoney = myData.OpalCount;
                UserDataManager.instance.Item_Slow = myData.Slow;
                UserDataManager.instance.Item_Transparency = myData.Transparenc;
                UserDataManager.instance.Item_Eraser = myData.Eraser;
                UserDataManager.instance.timer = myData.timer;

                UserDataManager.instance.Player_Eqip.rankScore = myData.rankScore;
                UserDataManager.instance.Player_Eqip.Achiement = myData.Achiement;
                UserDataManager.instance.Player_Eqip.user_Characters = myData.user_Characters;
                UserDataManager.instance.Player_Eqip.select_Character = myData.select_Character;

                // if (UserDataManager.instance.MapChanger != null)
                //     UserDataManager.instance.MapChanger.Change();

                MessagePopManager.instance.ShowPop("로드완료");
            }
            else
            {
                Debug.Log("Fail");
                MessagePopManager.instance.ShowPop("로드실패");

                ////로드할때 데이터가 없으면 저장해서 데이터 추가
                //string data = JsonUtility.ToJson(UserDataManager.instance.Player_Eqip);
                //if (Application.platform == RuntimePlatform.Android)
                //    playnanooSave(Social.localUser.id, data);
            }
            StateMachine.StateEnter = true;
            UserDataManager.instance.stateMachine.EnterState();
        });
    }

    public void playnanoo_EventLog(string str)
    {
        plugin = playnanooStart();
        var messages = new PlayNANOO.Monitor.LogMessages();
        messages.Add(Configure.PN_LOG_DEBUG, str);

        plugin.LogWrite(new PlayNANOO.Monitor.LogWrite()
        {
            EventCode = "EVENT_CODE",
            EventMessages = messages
        });
    }

    public void playnanoo_ReceiptVerificationAOS(string productId,
    string receipt, string signiture, double price)
    {
        plugin = playnanooStart();

        plugin.ReceiptVerificationAOS(productId, "{RECEIPT}",
        "{SIGNITURE}", "{CURRENCY}", price, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["package"]);
                Debug.Log(dictionary["product_id"]);
                Debug.Log(dictionary["order_id"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    private RankItem rankItem = new RankItem();

    public void RankingLoad()
    {
        plugin = playnanooStart();

        plugin.Ranking(tableCode, 10, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];

                //  Debug.Log($"list 갯수 = {list.Count}");
                foreach (Dictionary<string, object> rank in list)
                {
                    rankItem.score = rank["score"].ToString();
                    rankItem.nickname = rank["nickname"].ToString();
                    rankItem.rank = rank["ranking"].ToString();

                    UserDataManager.instance.stateMachine.gmr.RankingText[int.Parse(rankItem.rank) - 1].text =
                    $"{rankItem.rank}. {rankItem.nickname} {rankItem.score}점";
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void Start()
    {
        Application.targetFrameRate = 60;

        if (Application.platform == RuntimePlatform.Android)
        {
            // ----- GPGS -----

            GPGSLogin();
        }
        else
        {
            StateMachine.StateEnter = true;
            UserDataManager.instance.stateMachine.EnterState();
            // playnanooLoad("bigpixel");
        }
    }

    public void GPGSLogin()
    {

         PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        .Builder()
        .EnableSavedGames()
        .RequestServerAuthCode(false)
        .Build();

        //커스텀된 정보로 GPGS 초기화
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        //GPGS 시작.
        PlayGamesPlatform.Activate();
        if (Social.localUser.authenticated)
        {
            StateMachine.StateEnter = true;
            // Initialize();

            UserDataManager.instance.stateMachine.EnterState();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("로그인성공");
                    // Load();

                    //업적 구글 로그인 클리어
                    UserDataManager.instance.Achiement_Secces(0);

                    if (Application.platform == RuntimePlatform.Android)
                    {
                        playnanooLoad("new" + Social.localUser.id);
                    }
                    //Initialize();
                }
                else
                {
                    StateMachine.StateEnter = true;
                    Debug.Log("로그인 안함.");
                    UserDataManager.instance.stateMachine.EnterState();
                }
            });
        }
    }

    #region 뒤끝코드

    //private void Initialize()
    //{
    //    Backend.Initialize(() =>
    //    {
    //        // 초기화 성공한 경우 실행
    //        if (Backend.IsInitialized)
    //        {
    //            // example
    //            // 버전체크 -> 업데이트
    //            Debug.Log("뒤끝 초기화");
    //            MessagePopManager.instance.ShowPop("뒤끝초기화");
    //            federationToken();
    //        }
    //        // 초기화 실패한 경우 실행
    //        else
    //        {
    //        }
    //    });
    //}

    //public void federationToken()
    //{
    //    Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, callback =>
    //    {
    //        // 페더레이션 인증 이후 처리
    //        isSuccess = true;
    //        bro = callback;
    //        if (bro.IsSuccess())
    //        {
    //            Debug.Log("페더성공");
    //            MessagePopManager.instance.ShowPop("페더성공");
    //            NickNameUpdate();
    //            DataLoad();
    //            StateMachine.StateEnter = true;
    //            UserDataManager.instance.stateMachine.EnterState();
    //        }
    //        else
    //        {
    //            Debug.Log("페더실패");
    //            MessagePopManager.instance.ShowPop("페더 실패");
    //            StateMachine.StateEnter = true;
    //            UserDataManager.instance.stateMachine.EnterState();
    //        }
    //    });
    //    //BackendAsync(Backend.BMember.AuthorizeFederation, GetTokens(), FederationType.Google, "GoogleToken", (callback) =>
    //    // {
    //    //     isSuccess = true;
    //    //     bro = callback;
    //    //     if (bro.IsSuccess())
    //    //     {
    //    //         Debug.Log("페더성공");
    //    //         MessagePopManager.instance.ShowPop("페더성공");
    //    //         NickNameUpdate();
    //    //         DataLoad();
    //    //         StateMachine.StateEnter = true;
    //    //         UserDataManager.instance.stateMachine.EnterState();
    //    //     }
    //    //     else
    //    //     {
    //    //         Debug.Log("페더실패");
    //    //         MessagePopManager.instance.ShowPop("페더 실패");
    //    //         StateMachine.StateEnter = true;
    //    //         UserDataManager.instance.stateMachine.EnterState();
    //    //     }
    //    // });
    //}

    //private void NickNameUpdate()
    //{
    //    if (!Authenticated) return;

    //    GoogleNickName = Social.Active.localUser.userName;

    //    Backend.BMember.CreateNickname(GoogleNickName, (callback) =>
    //    {
    //        bro = callback;
    //        if (bro.IsSuccess())
    //        {
    //            var name = callback.GetInDate();
    //            MessagePopManager.instance.ShowPop(name);
    //        }
    //    });

    //    //BackendAsync(Backend.BMember.CreateNickname, GoogleNickName, (callback) =>
    //    //{
    //    //    bro = callback;
    //    //    if (bro.IsSuccess())
    //    //    {
    //    //        var name = callback.GetInDate();
    //    //        MessagePopManager.instance.ShowPop(name);
    //    //    }
    //    //});

    //    // string Email = " ";
    //    /*((PlayGamesLocalUser)Social.localUser).Email;*/

    //    Backend.BMember.UpdateFederationEmail(GetTokens(), FederationType.Google, (callback) =>
    //    {
    //        bro = callback;
    //        if (bro.IsSuccess())
    //        {
    //            var name = callback.GetInDate();
    //            MessagePopManager.instance.ShowPop(name);
    //        }
    //    });

    //    //BackendAsync(Backend.BMember.UpdateFederationEmail, GetTokens(), FederationType.Google, (callback) =>
    //    //{
    //    //    bro = callback;
    //    //    if (bro.IsSuccess())
    //    //    {
    //    //        var name = callback.GetInDate();
    //    //        MessagePopManager.instance.ShowPop(name);
    //    //    }
    //    //});
    //}

    //private string tableName = "character";

    //private void DataLoad()
    //{
    //    Backend.GameInfo.GetPrivateContents(tableName, (callback) =>
    //    {
    //        bro = callback;
    //        if (bro.IsSuccess())
    //        {
    //            var row = bro.GetReturnValuetoJSON();
    //            DataInfogame(row);
    //        }
    //        else
    //        {
    //            MessagePopManager.instance.ShowPop("LoadTableOpen false");
    //        }
    //    });

    //    //테이블 열기
    //    //BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, tableName, (callback) =>
    //    //{
    //    //    bro = callback;
    //    //    if (bro.IsSuccess())
    //    //    {
    //    //        var row = bro.GetReturnValuetoJSON();
    //    //        DataInfogame(row);
    //    //    }
    //    //    else
    //    //    {
    //    //        MessagePopManager.instance.ShowPop("LoadTableOpen false");
    //    //    }
    //    //});
    //}

    //private void DataInfogame(JsonData returnData)
    //{
    //    if (returnData != null)
    //    {
    //        Debug.Log("데이터가 존재합니다.");

    //        if (returnData.Keys.Contains("rows"))
    //        {
    //            JsonData rows = returnData["rows"];

    //            Debug.Log("rows = " + rows.Count);
    //            DataInfo(rows[0]);
    //        }

    //        if (returnData.Keys.Contains("row"))
    //        {
    //            JsonData row = returnData["row"];
    //            Debug.Log(row[0]);
    //            Debug.Log("rows = " + row.Count);
    //            DataInfo(row[0]);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("데이터가 없습니다");
    //        MessagePopManager.instance.ShowPop("데이터가 없습니다");
    //    }
    //}

    //private void DataInfo(JsonData data)
    //{
    //    if (data.Keys.Contains(tableName))
    //    {
    //        JsonData infoData = new JsonData();
    //        infoData = data[tableName][0];

    //        var LoadD = JsonUtility.FromJson<Eqip>(infoData.ToString());

    //        //var Load = new Eqip()
    //        //{
    //        //    idx = LoadD.idx,
    //        //    Money = LoadD.Money,
    //        //    OpalCount = LoadD.OpalCount,
    //        //    StageNumber = LoadD.StageNumber,
    //        //    Slow = LoadD.Slow,
    //        //    Eraser = LoadD.Eraser,
    //        //    Transparenc = LoadD.Transparenc
    //        //};

    //        //UserDataManager.instance.Player_Eqip = Load;
    //        print("로드완료");
    //        MessagePopManager.instance.ShowPop("로드완료");

    //        UserDataManager.instance.mapindex = LoadD.idx;
    //        UserDataManager.instance.gameMoney = LoadD.Money;
    //        UserDataManager.instance.StageNumber = LoadD.StageNumber;
    //        UserDataManager.instance.opalmoney = LoadD.OpalCount;
    //        UserDataManager.instance.Item_Slow = LoadD.Slow;
    //        UserDataManager.instance.Item_Transparency = LoadD.Transparenc;
    //        UserDataManager.instance.Item_Eraser = LoadD.Eraser;
    //        UserDataManager.instance.timer = LoadD.timer;

    //        if (UserDataManager.instance.MapChanger != null)
    //            UserDataManager.instance.MapChanger.Change();
    //    }
    //}

    // private void Update()
    //{
    //if (isSuccess)
    //{
    //    Backend.BMember.IsAccessTokenAlive((callback) =>
    //    {
    //        // 이후 처리
    //        if (callback.IsSuccess())
    //        {
    //            NickNameUpdate();
    //            StateMachine.StateEnter = true;
    //        }
    //        isSuccess = false;
    //        callback.Clear();
    //    });
    //}

    //if (isSuccess)
    //{
    //    var saveToken = Backend.BMember.SaveToken(bro);
    //    if (saveToken.IsSuccess())
    //    {
    //        NickNameUpdate();
    //        StateMachine.StateEnter = true;
    //    }
    //    isSuccess = false;
    //    bro.Clear();
    //}
    //}

    #endregion 뒤끝코드

    //============================================================================

    #region 리더보드 읽어오기

    private List<string> userIDs = new List<string>();
    private List<IScore> userID_Scores = new List<IScore>();

    public void LeaderBoardLead()
    {
        if (Authenticated == false)
        {
            //로그인 안됨
            for (int i = 0; i < 10; i++)
            {
                UserDataManager.instance.stateMachine.gmr.LeaderBoardTexts[i] = "구글로그인시 보여집니다.";
            }
        }
        //else
        //{
        //    Social.LoadScores(GPGSIds.leaderboard, scores =>
        //    {
        //        userIDs.Clear();
        //        userID_Scores.Clear();
        //        if (scores.Length > 0)
        //        {
        //            for (int i = 0; i < scores.Length; i++)
        //            {
        //                userIDs.Add(scores[i].userID);
        //                userID_Scores.Add(scores[i]);
        //            }
        //        }
        //    });

        //    Social.LoadUsers(userIDs.ToArray(), profiles =>
        //    {
        //        if (profiles.Length > 0)
        //        {
        //            var Length = profiles.Length;

        //            for (int i = 0; i < Length; i++)
        //            {
        //                var username = profiles[i].userName;
        //                var score = userID_Scores[i].value;

        //                UserDataManager.instance.stateMachine.gmr.LeaderBoardTexts[i] =
        //                /*i + 1*/  userID_Scores[i].rank + ". " + username + " : " + score;
        //            }
        //        }
        //    });
        //}
    }

    #endregion 리더보드 읽어오기

    //============================================================================

    // 게임 상단 메시지 띄우기
    public void ShowMessage(string msg)
    {
        DispatcherAction(() => MessagePopManager.instance.ShowPop(msg));
    }

    private void ShowMessage(string msg, float time)
    {
        DispatcherAction(() => MessagePopManager.instance.ShowPop(msg, time));
    }

    // Dispatcer에서 action 실행 (메인스레드)
    private void DispatcherAction(Action action)
    {
        action?.Invoke();
    }

    //####################################################################

    //#########################################################################################

    #region 데이터 로드

    public delegate void LoadDele();

    public static event LoadDele DeleEvent;

    #endregion 데이터 로드

    //###############################################

    public void GoogleLogOut()
    {
        var eqip = UserDataManager.instance.Player_Eqip;
        eqip.StageNumber = 0;
        //eqip.rankScore = (int)UserDataManager.instance.stateMachine.gmr.timer;
        string data = JsonUtility.ToJson(eqip);
        if (Application.platform == RuntimePlatform.Android)
        {
            playnanooSave("new"+Social.localUser.id, data);
        }
        //PlayGamesPlatform.Instance.SignOut();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            var eqip = UserDataManager.instance.Player_Eqip;
            eqip.StageNumber = 0;
            //eqip.rankScore = (int)UserDataManager.instance.stateMachine.gmr.timer;
            string data = JsonUtility.ToJson(eqip);
            if (Application.platform == RuntimePlatform.Android)
            {
                playnanooSave("new" + Social.localUser.id, data);
            }
            else
            {
                //  playnanooSave("bigpixel", data);
            }
        }
        // Save();
    }

    private void OnApplicationQuit()
    {
        var eqip = UserDataManager.instance.Player_Eqip;
        eqip.StageNumber = 0;

        //eqip.rankScore = (int)UserDataManager.instance.stateMachine.gmr.timer;
        string data = JsonUtility.ToJson(eqip);
        if (Application.platform == RuntimePlatform.Android)
        {
            playnanooSave("new" + Social.localUser.id, data);
        }
        else
        {
            //  playnanooSave("bigpixel", data);
        }
    }

    #region 저장

    private bool isSaving = false;

    //private void BackEndSaved(Eqip data)
    //{
    //    Param SaveData = new Param();

    //    var str = JsonUtility.ToJson(data);

    //    SaveData.Add(tableName, str);
    //    Backend.GameInfo.Insert(tableName, SaveData, (callback) =>
    //    {
    //        bro = callback;
    //        if (bro.IsSuccess())
    //        {
    //            Debug.Log("저장");
    //            MessagePopManager.instance.ShowPop("저장");
    //        }
    //    });
    //}

    #endregion 저장

    #region 구글저장로드 불량품

    private string myData_String;
    private byte[] myData_Binary;
  //  private ISavedGameMetadata myGame;

    public void Load()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
           // ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("mysave", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);
        }
    }

    // private void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    // {
    //     if (status == SavedGameRequestStatus.Success)
    //     {
    //         myGame = game;
    //         LoadData(myGame);
    //     }
    // }

    // private void LoadData(ISavedGameMetadata game)
    // {
    //    // ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, LoadDataCallBack);
    // }

    // private void LoadDataCallBack(SavedGameRequestStatus status, byte[] LoadedData)
    // {
    //     if (status == SavedGameRequestStatus.Success)
    //     {
    //         myData_String = Encoding.UTF8.GetString(LoadedData);
    //         var myData = JsonUtility.FromJson<Eqip>(myData_String);

    //         // UserDataManager.instance.Player_Eqip = myData;

    //         UserDataManager.instance.mapindex = myData.idx;
    //         UserDataManager.instance.gameMoney = myData.Money;
    //         UserDataManager.instance.StageNumber = myData.StageNumber;
    //         UserDataManager.instance.opalmoney = myData.OpalCount;
    //         UserDataManager.instance.Item_Slow = myData.Slow;
    //         UserDataManager.instance.Item_Transparency = myData.Transparenc;
    //         UserDataManager.instance.Item_Eraser = myData.Eraser;
    //         UserDataManager.instance.timer = myData.timer;

    //         // if (UserDataManager.instance.MapChanger != null)
    //         //     UserDataManager.instance.MapChanger.Change();

    //         var GameLoad = GameObject.FindObjectOfType<GoogleProcess>();

    //         // GameLoad.LoadT(UserDataManager.instance.Player_Eqip);
    //     }
    // }

    public void Save()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //SaveData(myGame);
        }
    }

    // public void SaveData(ISavedGameMetadata game)
    // {
    //     var myData = UserDataManager.instance.Player_Eqip;

    //     myData_String = JsonUtility.ToJson(myData);
    //     myData_Binary = Encoding.UTF8.GetBytes(myData_String);

    //     SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
    //     ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, myData_Binary, SaveCallBack);
    // }

    // private void SaveCallBack(SavedGameRequestStatus status, ISavedGameMetadata game)
    // {
    //     if (status == SavedGameRequestStatus.Success)
    //     {
    //     }
    //     else
    //     {
    //     }
    // }

    #endregion 구글저장로드 불량품

    // 공지사항 클래스
    public class Notice
    {
        internal string Title { get; set; }          // 공지사항 제목
        internal string Content { get; set; }        // 공지사항 내용
        internal string ImageKey { get; set; }       // 공지사항 이미지 (존재할 경우)
        internal string LinkButtonName { get; set; } // 공지사항 버튼 (누르면 공지사항 UI를 띄우기 위한)
        internal string LinkUrl { get; set; }        // 공지사항 URL (존재할 경우)
    }

    // 랭크 클래스
    [Serializable]
    public class RankItem
    {
        public string nickname { get; set; } // 닉네임
        public string score { get; set; }    // 점수
        public string rank { get; set; }     // 랭크
    }

    public class Serializer
    {
        #region serialize

        //오브젝트 시리얼라이즈 후 결과 값을 스트링으로 변환하여 반환
        public static string ObjectToStringSerialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memoryStream, obj);
                memoryStream.Flush();
                memoryStream.Position = 0;

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        //오브젝트를 시리얼라이즈 후 바이트 배열 형태로 반환
        public static byte[] ObjectToByteArraySerialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memoryStream, obj);
                memoryStream.Flush();
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
        }

        #endregion serialize

        #region Deserialize

        //스트링 타입의 시리얼라이즈된 데이타를 디시리얼라이즈 후 해당 타입으로 변환하여 반환
        public static T Deserialize<T>(string xmlText)
        {
            if (xmlText != null && xmlText != String.Empty)
            {
                byte[] b = Convert.FromBase64String(xmlText);
                using (var stream = new MemoryStream(b))
                {
                    var formatter = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
            else
            {
                return default(T);
            }
        }

        //바이트 배열 형태의 시리얼라이즈된 데이타를 디시리얼라이즈 후 해당 타입으로 변환하여 반환
        public static T Deserialize<T>(byte[] byteData)
        {
            using (var stream = new MemoryStream(byteData))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion Deserialize
    }
 }

// //    private void Start()
// //    {
// //        if (!Backend.IsInitialized)
// //        {
// //            Backend.Initialize(BackEndCallBack);
// //        }
// //        GoogleServicesInit();
// //    }

// //    public bool Authenticated { get { return Social.Active.localUser.authenticated; } }

// //    private TestSaveData TestData = new TestSaveData();

// //    public void GoogleServicesInit()
// //    {
// //        //debugText.text = "이닛들어옴";
// //        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
// //        .Builder()
// //        //.RequestServerAuthCode(false)
// //        //.RequestIdToken()
// //        .RequestEmail()
// //        .EnableSavedGames()
// //        .Build();

// //        PlayGamesPlatform.InitializeInstance(config);
// //        PlayGamesPlatform.DebugLogEnabled = false;
// //        // Google Play 게임 플랫폼 활성화
// //        PlayGamesPlatform.Activate();

// //        GoogleLogin();
// //    }

// //    public void GoogleLogin()
// //    {
// //        if (PlayGamesPlatform.Instance.localUser.authenticated)
// //        {
// //            Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
// //            Debug.Log("GoogleId - " + Social.localUser.id);
// //            Debug.Log("UserName - " + Social.localUser.userName);
// //            Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());
// //        }
// //        else
// //        {
// //            Social.localUser.Authenticate((bool success) =>
// //            {
// //                if (success)
// //                {
// //                    Debug.Log("구글 로그인 성공");
// //                    ////초기화 되지않는 경우
// //                    //서버에 아이디가 없으면 가입
// //                    //서버에 아이디가 있으면 로그인
// //                    //ID = Email , Password = id

// //                    string Email = ((PlayGamesLocalUser)Social.localUser).Email;
// //                    string id = Social.localUser.id;
// //                    BackendReturnObject bro = Backend.BMember.CustomSignUp
// //                    (
// //                            Email,
// //                            id,
// //                            "SignUp"
// //                    );

// //                    if (bro.IsSuccess())
// //                    {
// //                        Debug.Log("회원가입 완료");
// //                        //로그인 작업
// //                        BackEndCustomLogin(Email, id);
// //                    }
// //                    else
// //                    {
// //                        Debug.Log("이미 가입된 유저");
// //                        //로그인
// //                        BackEndCustomLogin(Email, id);
// //                    }

// //                    Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
// //                    Debug.Log("GoogleId - " + Social.localUser.id);
// //                    Debug.Log("UserName - " + Social.localUser.userName);
// //                    Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());
// //                }
// //                else
// //                {
// //#if UNITY_EDITOR
// //                    string Email = "Test";
// //                    string id = "1234";
// //                    BackendReturnObject bro = Backend.BMember.CustomSignUp
// //                    (
// //                            Email,
// //                            id,
// //                            "SignUp"
// //                    );

// //                    if (bro.IsSuccess())
// //                    {
// //                        Debug.Log("회원가입 완료");
// //                        //로그인 작업
// //                        BackEndCustomLogin(Email, id);
// //                    }
// //                    else
// //                    {
// //                        Debug.Log("이미 가입된 유저");
// //                        //로그인
// //                        BackEndCustomLogin(Email, id);
// //                    }
// //#endif
// //                }
// //            });
// //        }
// //    }

// //    private void BackEndCustomLogin(string Email, string id)
// //    {
// //        BackendReturnObject Loginbro = Backend.BMember.CustomLogin
// //        (
// //            Email,
// //            id
// //        );

// //        if (Loginbro.IsSuccess())
// //        {
// //            Debug.Log("로그인 성공");
// //            BackEndLoadInfo();
// //        }
// //        else
// //        {
// //            Debug.Log("로그인 실패");
// //        }
// //    }

// //    private void BackEndCallBack(BackendReturnObject bro)
// //    {
// //        if (bro.IsSuccess())
// //        {
// //            Debug.Log("뒤끝 초기화 성공");
// //        }
// //        else
// //        {
// //            Debug.Log("뒤끝 초기화 실패");
// //        }
// //    }

// //    private string GetTokens()
// //    {
// //        if (Authenticated)
// //        {
// //            // 유저 토큰 받기 첫번째 방법
// //            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
// //            // 두번째 방법
// //            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
// //            return _IDtoken;
// //        }
// //        else
// //        {
// //            Debug.Log("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
// //            return null;
// //        }
// //    }

// //    //서버 정보 저장
// //    private void BackEndSaveInfo()
// //    {
// //        Param param = new Param();
// //        JsonDataClass data = new JsonDataClass
// //            (
// //                UserDataManager.instance.Player_Eqip.idx,
// //                UserDataManager.instance.Player_Eqip.Money,
// //                UserDataManager.instance.Player_Eqip.StageNumber
// //            );

// //        string str = JsonUtility.ToJson(data);

// //        param.Add("character", str);

// //        BackendReturnObject broSave = Backend.GameInfo.Insert("character", param);

// //        if (broSave.IsSuccess())
// //        {
// //            Debug.Log(broSave.GetInDate());
// //        }
// //        else
// //        {
// //            ErrorCode(broSave);
// //        }
// //    }

// //    private void BackEndLoadInfo()
// //    {
// //        TableList();
// //        //true 는 저장
// //        //false 는 검색
// //        GetLoadData("character", true);
// //    }

// //    private void TableList()
// //    {
// //        BackendReturnObject bro = Backend.GameInfo.GetTableList();

// //        if (bro.IsSuccess())
// //        {
// //            //퍼블릭된 테이블 가져오기
// //            JsonData data = bro.GetReturnValuetoJSON()["privateTables"];
// //            //Backend.GameInfo.GetPublicContents("character", 1);
// //            foreach (var item in data)
// //            {
// //                Debug.Log(item.ToString());
// //            }
// //        }
// //        else
// //        {
// //            Debug.Log(bro.GetMessage());
// //        }
// //    }

// //    private void GetLoadData(string dbTable, bool bLogin)
// //    {
// //        BackendReturnObject bro = Backend.GameInfo.GetPrivateContents(dbTable);

// //        if (bro.IsSuccess())
// //        {
// //            Debug.Log(bro.GetReturnValue());
// //            GetGameInfo(bro.GetReturnValuetoJSON(), dbTable, bLogin);
// //        }
// //        else
// //        {
// //            ErrorCode(bro);
// //        }
// //    }

// //    private string firstKey = string.Empty;

// //    private void GetPublicLoadDataNext()
// //    {
// //        BackendReturnObject bro;

// //        if (firstKey == null)
// //        {
// //            bro = Backend.GameInfo.GetPublicContents("character", 1);
// //        }
// //        else
// //        {
// //            //이미 불러온 데이터의 다음데이터를 불러온다.1개
// //            bro = Backend.GameInfo.GetPublicContents("character", firstKey, 1);
// //        }

// //        if (bro.IsSuccess())
// //        {
// //            firstKey = bro.FirstKeystring();
// //            GetGameInfo(bro.GetReturnValuetoJSON(), "character", true);
// //        }
// //        else
// //        {
// //            ErrorCode(bro);
// //        }
// //    }

// //    private void GetGameInfo(JsonData returnData, string dbTable, bool bLogin)
// //    {
// //        if (returnData != null)
// //        {
// //            Debug.Log("데이터가 존재합니다.");

// //            if (returnData.Keys.Contains("rows"))
// //            {
// //                JsonData rows = returnData["rows"];

// //                Debug.Log("rows = " + rows.Count);
// //                GetData(rows[0], dbTable, bLogin);
// //            }

// //            if (returnData.Keys.Contains("row"))
// //            {
// //                JsonData row = returnData["row"];
// //                Debug.Log(row[0]);
// //                Debug.Log("rows = " + row.Count);
// //                GetData(row[0], dbTable, bLogin);
// //            }
// //        }
// //        else
// //        {
// //            Debug.Log("데이터가 없습니다");
// //        }
// //    }

// //    public void GetData(JsonData Data, string dbTable, bool bLogin)
// //    {
// //        if (Data.Keys.Contains(dbTable))
// //        {
// //            JsonData data = new JsonData();
// //            data = Data[dbTable][0];

// //            var a = JsonUtility.FromJson<JsonDataClass>(data.ToString());

// //            if (bLogin)
// //            {
// //                UserDataManager.instance.Player_Eqip.idx = a.UserCharacteridx;
// //                UserDataManager.instance.gameMoney = a.money;
// //                UserDataManager.instance.StageNumber = a.StageMapNumber;
// //                // 스테이지 넘버
// //            }
// //        }
// //    }

// //    private void ErrorCode(BackendReturnObject bro)
// //    {
// //        switch (bro.GetErrorCode())
// //        {
// //            case "404":
// //                Debug.Log("존재하지 않는 테이블");
// //                break;

// //            case "412":
// //                Debug.Log("비활성화된 테이블");
// //                break;

// //            case "413":
// //                Debug.Log("한줄의 정보가 400kb가 넘을때");
// //                break;

// //            default:
// //                Debug.Log("서버공통 에러 " + bro.GetMessage());
// //                break;
// //        }
// //    }

// //    public void OnApplicationQuit()
// //    {
// //        //if(Authenticated)
// //        BackEndSaveInfo();

// //        GoogleLogOut();
// //    }

// //    public Texture2D GetUserImage()
// //    {
// //        if (Authenticated)
// //            return Social.localUser.image;
// //        else
// //            return null;
// //    }

// //    public void GoogleLogOut()
// //    {
// //        PlayGamesPlatform.Instance.SignOut();
// //        Backend.BMember.Logout();
// //    }

// //    #region 업적 UI

// //    public void AchievementsUI()
// //    {
// //        if (Authenticated)
// //            Social.ShowAchievementsUI();
// //    }

// //    #endregion 업적 UI

// //    #region 업적열림

// //    /// <summary>
// //    /// 튜토리얼 업적 열림
// //    /// </summary>
// //    //public void Starter_AchievementPosting()
// //    //{
// //    //    Social.ReportProgress(GPGSIds., 100f, (bool success) =>
// //    //    {
// //    //        if (success)
// //    //        {
// //    //            Debug.Log(" 업적 열림");
// //    //        }
// //    //    });
// //    //}

// //    #endregion 업적열림

// //    #region 업적에 점수게시 -> 업적 진행도 표시 (0.0 ~ 100.0f)

// //    public void AchievementsReportProgress(string AchievementsID, int progressvalue)
// //    {
// //        PlayGamesPlatform.Instance.IncrementAchievement(AchievementsID, progressvalue, (bool success) =>
// //        {
// //        });
// //    }

// //    #endregion 업적에 점수게시 -> 업적 진행도 표시 (0.0 ~ 100.0f)

// //    #region 리더보드 UI

// //    public void GoogleLederBoardUI()
// //    {
// //        //debugText.text = "리더보드유아이눌림";

// //        if (Authenticated)
// //        {
// //            //debugText.text = "리더보드유아이호출";
// //            Social.ShowLeaderboardUI();
// //        }
// //        else
// //            GoogleLogin();
// //    }

// //    public void GoogleLederBoardUITarget(string LeaderBoardID)
// //    {
// //        if (Authenticated)
// //            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoardID);
// //    }

// //    #endregion 리더보드 UI

// //    #region 리더보드에 점수게시

// //    public void LeaderBoardPostring(long score)
// //    {
// //        if (Authenticated)
// //            GoogleLederBoardPostingScore(GPGSIds.leaderboard, score);
// //    }

// //    public void GoogleLederBoardPostingScore(string LeaderBoardID, long PostingScore)
// //    {
// //        Social.ReportScore(PostingScore, LeaderBoardID, (bool success) =>
// //        {
// //            if (success)
// //            {
// //                //LoadDataText.text = $"리더보드 {PostingScore}게시 성공";
// //            }
// //            else
// //            {
// //                //LoadDataText.text = $"리더보드 게시 Error";
// //            }
// //        });
// //    }

// //    #endregion 리더보드에 점수게시

// //    #region 리더보드 정보 가져오기

// //    public void LeaderBoardScores()
// //    {
// //        List<string> userIDs = new List<string>();

// //        if (Social.Active.localUser.authenticated)
// //        {
// //            Social.LoadScores(GPGSIds.leaderboard, scores =>
// //            {
// //                if (scores.Length > 0)
// //                {
// //                    for (int i = 0; i < scores.Length; i++)
// //                    {
// //                        userIDs.Add(scores[i].userID);
// //                    }

// //                    Social.LoadUsers(userIDs.ToArray(), profiles =>
// //                    {
// //                        if (profiles.Length > 0)
// //                        {
// //                            var Length = 10;

// //                            for (int i = 0; i < Length; i++)
// //                            {
// //                                var username = profiles[i].userName;
// //                                var score = scores[i].value;

// //                                UserDataManager.instance.stateMachine.gmr.RankingText[i].text =
// //                                 i + 1 + ". " + username + " : " + score;
// //                            }
// //                        }
// //                    });

// //                    //for (int i = 0; i < scores.Length; i++)
// //                    //{
// //                    //    UserDataManager.instance.stateMachine.gmr.RankingText[i].text
// //                    //    = i + 1 + ". " + scores[i].leaderboardID + " Score = " + scores[i].value;

// //                    //    Debug.Log($"Id = {scores[i].userID} , " +
// //                    //              $"Score = {scores[i].value} , " +
// //                    //              $"Rank = {scores[i].rank}");
// //                    //}
// //                }
// //            });
// //        }
// //    }

// //    public void LeaderBoardRank(string mStatus)
// //    {
// //        PlayGamesPlatform.Instance.LoadScores(
// //            GPGSIds.leaderboard,
// //            LeaderboardStart.PlayerCentered,
// //            100,
// //            LeaderboardCollection.Public,
// //            LeaderboardTimeSpan.AllTime,
// //            (data) =>
// //            {
// //                mStatus = "Leaderboard data valid: " + data.Valid;
// //                mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
// //            });

// //        Debug.Log(mStatus);
// //    }

// //    #endregion 리더보드 정보 가져오기

// //    private string SaveFindName = "Game";

// //    public void SaveUIOpen()
// //    {
// //        if (Authenticated)
// //            ShowSelectUI();
// //    }

// //    private void ShowSelectUI()
// //    {
// //        uint maxNumToDisplay = 5;
// //        bool allowCreateNew = true;
// //        bool allowDelete = true;

// //        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
// //        savedGameClient.ShowSelectSavedGameUI("Select saved game",
// //            maxNumToDisplay,
// //            allowCreateNew,
// //            allowDelete,
// //            OnSavedGameSelected);
// //    }

// //    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
// //    {
// //        if (status == SelectUIStatus.SavedGameSelected)
// //        {
// //            if (Authenticated)
// //            {
// //                OpenLoadGame(SaveFindName);
// //            }

// //            //SaveGame(game);
// //        }
// //        else
// //        {
// //            // handle cancel or error
// //        }
// //    }

// //    #region 데이터 클라우드Save(저장할 데이터 지정해야됨)

// //    //데이터 저장
// //    public void SaveButtonClick()
// //    {
// //        if (Authenticated)
// //            OpenSavedGame(SaveFindName);
// //    }

// //    public void OpenSavedGame(string filename)
// //    {
// //        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

// //        savedGameClient.OpenWithAutomaticConflictResolution(filename,
// //                                                            DataSource.ReadCacheOrNetwork,
// //                                                            ConflictResolutionStrategy.UseLongestPlaytime,
// //                                                            OnSavedGameOpened);
// //    }

// //    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
// //    {
// //        if (status == SavedGameRequestStatus.Success)
// //        {
// //            //SaveGame(game);
// //            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
// //            SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

// //            #region 클라우드에 저장할 데이터

// //            // savedata1 v = new savedata1();

// //            var data = new GameObject();

// //            #endregion 클라우드에 저장할 데이터

// //            var stringToSave = JsonUtility.ToJson(data);
// //            byte[] bytes = Encoding.UTF8.GetBytes(stringToSave);
// //            savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
// //        }
// //        else
// //        {
// //            // handle error
// //            //  LoadDataText.text = "SaveError";
// //        }
// //    }

// //    private void SaveGame(ISavedGameMetadata game)
// //    {
// //        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
// //        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

// //        #region 클라우드에 저장할 데이터

// //        // savedata1 v = new savedata1();

// //        var data = new GameObject();

// //        #endregion 클라우드에 저장할 데이터

// //        var stringToSave = JsonUtility.ToJson(data);
// //        byte[] bytes = Encoding.UTF8.GetBytes(stringToSave);
// //        savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
// //    }

// //    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
// //    {
// //        if (status == SavedGameRequestStatus.Success)
// //        {
// //            // handle reading or writing of saved game.
// //            //string text = "Save_성공";
// //            //LoadDataText.text = text;
// //        }
// //        else
// //        {
// //            // handle error
// //            //string text = "Save_실패";
// //            //LoadDataText.text = text;
// //        }
// //    }

// //    #endregion 데이터 클라우드Save(저장할 데이터 지정해야됨)

// //    #region 데이터 클라우드Load

// //    //저장된 데이터 읽기
// //    public void LoadButtonClick()
// //    {
// //        OpenLoadGame(SaveFindName);
// //    }

// //    public void OpenLoadGame(string filename)
// //    {
// //        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

// //        savedGameClient.OpenWithAutomaticConflictResolution(filename,
// //                                                            DataSource.ReadCacheOrNetwork,
// //                                                            ConflictResolutionStrategy.UseLongestPlaytime,
// //                                                            OnLoadGameRead);
// //    }

// //    public void OnLoadGameRead(SavedGameRequestStatus status, ISavedGameMetadata game)
// //    {
// //        if (status == SavedGameRequestStatus.Success)
// //        {
// //            // handle reading or writing of saved game.
// //            LoadGameData(game);
// //        }
// //        else
// //        {
// //            // handle error
// //        }
// //    }

// //    private void LoadGameData(ISavedGameMetadata game)
// //    {
// //        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
// //    }

// //    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
// //    {
// //        if (status == SavedGameRequestStatus.Success)
// //        {
// //            string dd = Encoding.UTF8.GetString(data);
// //            // LoadData = JsonUtility.FromJson<savedata1>(dd);

// //            //UserDataManager.Instance.userData.Money = LoadData.Money - UserDataManager.Instance.randomValue;
// //        }
// //        else
// //        {
// //            // handle error
// //            // LoadDataText.text = "LoadError";
// //        }
// //    }

// //    #endregion 데이터 클라우드Load

// //    public Texture2D getScreenshot()
// //    {
// //        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
// //        // extracted
// //        Texture2D screenShot = new Texture2D(1024, 700);

// //        // Takes the screenshot from top left hand corner of screen and maps to top
// //        // left hand corner of screenShot texture
// //        screenShot.ReadPixels(
// //            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
// //        return screenShot;