using System;
using UnityEngine;
using UnityEngine.UI;

public enum tipNumber
{
    팁, 투명화, 슬로우, 지우개, 오팔
}

[Serializable]
public struct lang
{
    public tipNumber tip;
}

[Serializable]
public class tipClass : MonoBehaviour
{
    public CanvasGroup tip_background;

    public Text text;

    public Image image1;
    public Image image2;

    public string korea;
    public string English;
    public string Japan;

    public Sprite img1;
    //public Sprite img2;

    //[ArrayElementTitle("tip")]
    public lang lang_tip;

    public Sprite[] sprite;

    private  string localizerText = string.Empty;

    public void enumChange(int maxNumber)
    {
        int n = UnityEngine.Random.Range(0, maxNumber + 1);

        lang_tip.tip = (tipNumber)n;
    }

    public void spriteset(tipClass _this, Sprite sprite ,GameObject img2 , tipNumber tipnumber )
    {
        //sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), true);
        _this.img1 = sprite;

        _this.image1.sprite = _this.img1;

        if (Localizer.instance.localizer == localizer_Enum.Korean)
        {
            text.fontSize = 40;
            localizerText = _this.korea;

            if (tipnumber == tipNumber.팁 || tipnumber == tipNumber.오팔)      img2.gameObject.SetActive(false);
            else                 img2.gameObject.SetActive(true);

        }
        else if (Localizer.instance.localizer == localizer_Enum.English)
        {
            text.fontSize = 32;
            localizerText = _this.English;
            img2.gameObject.SetActive(false);
        }
        else
        {
            text.fontSize = 32;
            localizerText = _this.Japan;
            img2.gameObject.SetActive(false);
        }

        _this.text.text = localizerText.Replace("\\n", "\n");

       
    }

    public void OnInspector(tipClass _this)
    {
        switch (_this.lang_tip.tip)
        {
            case tipNumber.팁:
                _this.korea = "15초마다 나타나는 이 느낌표는 적이 아냐!\n아이템이 떨어지는 위치라구!\n어디서 나타날지 모르니까 눈 똑바로 뜨고 봐야 할걸?";
                _this.Japan = "15秒ごとに現れる、このビックリマークは敵じゃない！\nアイテムが落ちる位置なんだ！\nどこから現われるか分からないから\n目をしっかり開けて見なければならないよ。";
                _this.English = "This exclamation mark that appears every 15 seconds is not an enemy!\nIt's where the items fall!\nYou don't know where it's coming from\nso you'll have to keep your eyes open.";
                spriteset(_this, _this.sprite[0], _this.image2.gameObject , tipNumber.팁);
                _this.image1.SetNativeSize();
                break;

            case tipNumber.투명화:
                _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n짧은시간 투명화되어 모든 충돌을 회피할 수있는 효과를 지니고 있다.";
                _this.Japan = "ゲームの進行中! ビックリマークで現れるアイテムの一つ。\n短時間、透明化され、あらゆる衝突を回避できる効果を持っている。";
                _this.English = "One of the items that appears on the exclamation point during the game.\nIt has the effect of being transparent\nfor a short time to avoid all conflicts.";
                spriteset(_this, _this.sprite[1] , _this.image2.gameObject,tipNumber.투명화);
                _this.image1.SetNativeSize();
                break;

            case tipNumber.슬로우:
                _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n일정 시간 모든 적들을 느려지게 하는 효과를 지니고 있다.";
                _this.Japan = "ゲームの進行中! ビックリマークで現れるアイテムの一つ。\n一定時間、すべての敵を遅くする効果がある。";
                _this.English = "One of the items that appears on the exclamation point during the game.\nIt has the effect of slowing down all enemies\nfor a certain period of time.";
                spriteset(_this, _this.sprite[2], _this.image2.gameObject, tipNumber.슬로우);
                _this.image1.SetNativeSize();
                break;

            case tipNumber.지우개:
                _this.korea = "게임 진행 중   느낌표에서 나타나는 아이템들 중 하나.\n현재 화면에 나타나있는 모든 적들을 지워버리는 효과를 지니고 있다";
                _this.Japan = "ゲームの進行中! ビックリマークで現れるアイテムの一つ。\n現在画面に現れている敵全体を消す効果を持っている。";
                _this.English = "One of the items that appears on the exclamation point during the game.\nIt currently has the effect of erasing\nall enemies on the screen.";
                spriteset(_this, _this.sprite[3], _this.image2.gameObject, tipNumber.지우개);
                _this.image1.SetNativeSize();
                break;

            case tipNumber.오팔:
                _this.korea = "오팔은 캐릭터를 살 수 있는 귀한 재화야.\n게임 진행 중에 아이템을 대신해서 나타나기도 하니까\n사라지기 전에 꼭 얻어두라구!";
                _this.Japan = "オパールはキャラクターを買うことができる貴重な財貨だ。\nゲーム中にアイテムの代わりに現れたりもするので\n消える前に必ず貰っておけよ！";
                _this.English = "Opal is a precious commodity for buying characters.\nIt appears on behalf of items during the game,\nso make sure to get it before it disappears!";
                spriteset(_this, _this.sprite[4], _this.image2.gameObject, tipNumber.오팔);
                _this.image1.SetNativeSize();
                break;
        }



    }
}