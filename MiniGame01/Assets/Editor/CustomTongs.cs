using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(tipClass))]
public class CustomTongs : Editor
{
    private tipClass _this;//수정할 스크립트를 선언

    public Sprite sprite;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    private void OnEnable()
    {
        _this = (tipClass)target;
    }

    public void spriteset(Sprite sprite, string text)
    {
        //sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), true);
        _this.img1 = sprite;

        _this.image1.sprite = _this.img1;
        _this.text.text = text.Replace("\\n", "\n");

        _this.image1.SetNativeSize();
    }

    public override void OnInspectorGUI()
    {
    //    base.OnInspectorGUI();

      //  _this.OnInspector(_this);

        //switch (_this.lang_tip.tip)
        //{
        //    case tipNumber.팁:
        //        _this.korea = "15초마다 나타나는 이 느낌표는 적이 아냐!\n아이템이 떨어지는 위치라구!\n어디서 나타날지 모르니까 눈 똑바로 뜨고 봐야 할걸?";
        //        _this.Japan = "15秒ごとに現れる、 このビックリマークは敵じゃない！\nアイテムが落ちる位置なんだ！\nどこから現われるか分からないから目をしっかり開けて見なければならないよ。";
        //        _this.English = "This exclamation mark that appears every 15 seconds is not an enemy!\nIt's where the items fall!\n You don't know where it's coming from, so you'll have to keep your eyes open.";
        //        _this.image2.gameObject.SetActive(false);
        //        spriteset(_this.sprite[0], _this.korea);

        //        //sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), true);
        //        //_this.img1 = sprite;

        //        break;
        //    case tipNumber.투명화:
        //        _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n짧은시간 투명화되어 모든 충돌을 회피할 수있는 효과를 지니고 있다.";
        //        _this.Japan = "ゲーム進行中、ビックリマークで現れるアイテムの一つ。\n 短時間、透明化され、あらゆる衝突を回避できる効果を持っている。";
        //        _this.English = "One of the items that appears on the exclamation point during the game.\nIt has the effect of being transparent for a short time to avoid all conflicts.";
        //        _this.image2.gameObject.SetActive(true);
        //        spriteset(_this.sprite[1], _this.korea);
        //        //sprite2 = (Sprite)EditorGUILayout.ObjectField(sprite2, typeof(Sprite), true);
        //        //_this.img1 = sprite2;

        //        break;
        //    case tipNumber.슬로우:
        //        _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n일정 시간 모든 적들을 느려지게 하는 효과를 지니고 있다.";
        //        _this.Japan = "ゲームの進行中、ビックリマークで 現れるアイテムの一つ。\n一定時間、すべての敵を遅くする効果がある。";
        //        _this.English = "One of the items that appears on the exclamation point during the game.\nIt has the effect of slowing down all enemies for a certain period of time.";
        //        _this.image2.gameObject.SetActive(true);
        //        spriteset(_this.sprite[2], _this.korea);

        //        //sprite3 = (Sprite)EditorGUILayout.ObjectField(sprite3, typeof(Sprite), true);
        //        //_this.img1 = sprite3;
        //        break;
        //    case tipNumber.지우개:
        //        _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n현재 화면에 나타나있는 모든 적들을 지워버리는\n효과를 지니고 있다";
        //        _this.Japan = "ゲームの進行中、ビックリマークで現れるアイテムの一つ。\n現在画面に現れている敵全体を消す効果を持っている。";
        //        _this.English = "One of the items that appears on the exclamation point during the game.\nIt currently has the effect of erasing all enemies on the screen.";
        //        _this.image2.gameObject.SetActive(true);
        //        spriteset(_this.sprite[3], _this.korea);

        //        //sprite4 = (Sprite)EditorGUILayout.ObjectField(sprite4, typeof(Sprite), true);
        //        //_this.img1 = sprite4;
        //        break;
        //    case tipNumber.오팔:
        //        _this.korea = "오팔은 캐릭터를 살 수 있는 귀한 재화야.\n게임 진행 중에 아이템을 대신해서 나타나기도 하니까\n사라지기 전에 꼭 얻어두라구!";
        //        _this.Japan = "オパールはキャラクターを買うことができる貴重な財貨だ。\nゲーム中にアイテムの代わりに現れたりもするので消える前に必ず貰っておけよ！";
        //        _this.English = "Opal is a precious commodity for buying characters.\nIt appears on behalf of items during the game, so make sure to get it before it disappears!";
        //        _this.image2.gameObject.SetActive(false);
        //        spriteset(_this.sprite[4], _this.korea);

        //        //sprite5 = (Sprite)EditorGUILayout.ObjectField(sprite5, typeof(Sprite), true);
        //        //_this.img1 = sprite5;
        //        break;
        //}
    }
}

/*
public class CustomTongs : EditorWindow
{
    public tongs tong;
    public Ease ease;

    private PlayerCtrl player;
    private smooseMapChange mapchange;
    private string s;

    [MenuItem("CustomEditor/tongsWindow")]
    public static void showWindow()
    {
        EditorWindow.GetWindow(typeof(CustomTongs));
    }

    private void OnGUI()
    {
        GUILayout.Label("Player Collider On Off", EditorStyles.boldLabel);
        GUILayout.Space(5);

        //  player = (PlayerCtrl)EditorGUILayout.ObjectField(player, typeof(PlayerCtrl), true);
        if (GUILayout.Button("player collider 끄기"))
        {
            player = FindObjectOfType<PlayerCtrl>();
            player.coll.enabled = false;
        }
        // mapchange = (smooseMapChange)EditorGUILayout.ObjectField(mapchange, typeof(PlayerCtrl), true);
        GUILayout.Space(10);

        if (GUILayout.Button("stageChageTimer 10초"))
        {
            mapchange = FindObjectOfType<smooseMapChange>();
            mapchange.MapChangeTimer = 10f;
        }

        //GUILayout.Label("[ 게 발Script ]", EditorStyles.boldLabel);
        ////=====================================//
        //GUILayout.Space(5);
        ////=====================================//
        //tong = (tongs)EditorGUILayout.ObjectField(tong, typeof(tongs), true);

        //if (GUILayout.Button("tong 연결"))
        //{
        //    tong = FindObjectOfType<tongs>();
        //}
        ////=====================================//
        //GUILayout.Space(20);
        ////=====================================//
        //GUILayout.Label("[ Ease ]", EditorStyles.boldLabel);
        ////=====================================//
        //GUILayout.Space(5);
        ////=====================================//
        //ease = (Ease)EditorGUILayout.EnumPopup(ease);
        //tong.ease = ease;
        ////tong.ease = ease;
        ////=====================================//
        //GUILayout.Space(10);
        ////=====================================//
    }
}
*/