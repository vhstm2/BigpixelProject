using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserDataContent : MonoBehaviour
{
    public UserData[] userDatas;

    public Text characterExplanation_Text2;

    public int checkIndex = 0;

    public Slider grafSlider_small;

    public Image star;
    public Image CharacterImage2;
    public CanvasGroup center2;
    public GameObject Yes_purchase_panel;
    public GameObject No_purchase_panel;

    public TextMeshProUGUI P_Money;
    public TextMeshProUGUI P_OPAL;
    public TextMeshProUGUI SliderText;
    public TextMeshProUGUI timetext;

    public Text name;
    public Text abillity;

    private float currend_Time = 1800f;

    public gametimeReward reward_value;

    public Character_item[] character_Items;

    public Button RewordButton;

    public Button SceneExit;
    public PurchaseButton purchaseButton;

    public characterPurchase_Effect purchase_Effect;

    private void OnEnable()
    {
        //     UserDataManager.instance.Player_Eqip.idx = -1;
        checkIndex = UserDataManager.instance.Player_Eqip.idx;
        userDatas[UserDataManager.instance.Player_Eqip.idx].BlackImage.gameObject.SetActive(true);
        userDatas[UserDataManager.instance.Player_Eqip.idx].CheckImage.gameObject.SetActive(true);

        character_setting();
    }

    public void Click(string n)
    {
        SceneExit.interactable = false;

        IndexActive(checkIndex, false);
        IndexActive(UserDataManager.instance.Player_Eqip.idx, true);

        SpriteSet();

        center2.gameObject.SetActive(true);

        purchaseButton.targetProductId = userDatas[checkIndex].characterName;

        //var fadeob = UserDataManager.instance.stateMachine.fadeob;
        //fadeob.raycastTarget = true;

        //DOTween.To(() => fadeob.color,
        //x => fadeob.color = x,
        //new Color(0, 0, 0, 0.8f),
        //0.5f).OnComplete(() => { });
    }

    private void IndexActive(int idx, bool flag)
    {
        userDatas[idx].BlackImage.gameObject.SetActive(flag);
        userDatas[idx].CheckImage.gameObject.SetActive(flag);
    }

    //float sliderMaxValue = 3600f;
    private void Update()
    {
        #region 시간타이머

        {
            int minute = (int)UserDataManager.instance.timer / 60;

            int second = (int)UserDataManager.instance.timer - (minute * 60);

            int second1 = second / 10;

            int second2 = second % 10;

            timetext.text =
                (minute.ToString() + " : " + second1.ToString() + second2.ToString());
        }

        #endregion 시간타이머

        // ex) 1700 % 1800
        float value = UserDataManager.instance.timer / currend_Time;
        // Debug.Log($" 타임계산 =  {value} , Timer = {UserDataManager.instance.timer }");
        float slidervalue = 1 - value;
        grafSlider_small.value = slidervalue;
        SliderText.text = (slidervalue * 100f).ToString("0") + "%";

        if (UserDataManager.instance.timer <= 0f)
        {
            //버튼 비활성화
            RewordButton.gameObject.SetActive(true);
        }
        else
        {
            //버튼 활성화
            RewordButton.gameObject.SetActive(false);
        }
    }

    public void CenterExit2()
    {
        center2.gameObject.SetActive(false);
        SceneExit.interactable = true;
        //var fadeob = UserDataManager.instance.stateMachine.fadeob;
        //fadeob.raycastTarget = false;

        //DOTween.To(() => fadeob.color,
        //x => fadeob.color = x,
        //new Color(0, 0, 0, 0.0f),
        //0.5f).OnComplete(() => { });
    }

    /// <summary>
    /// center 내용 세팅
    /// </summary>
    private void SpriteSet()
    {
        checkIndex = UserDataManager.instance.Player_Eqip.idx;

        if (checkIndex < 9)
            UserDataManager.instance.character = Character.NORMAL;
        else
            UserDataManager.instance.character = Character.RARE;

        var character = UserDataManager.instance.Player_Eqip.user_Characters[checkIndex];

        if (character.is_purchase)
        {
            name.text = " 케릭터이름 : " + character.name;
            abillity.text = " 어빌리티 : " + character.Abillity;

            Yes_purchase_panel.SetActive(true);
            No_purchase_panel.SetActive(false);

            userDatas[character.charcterindx].isPurchase = true;
        }
        else
        {
            Yes_purchase_panel.SetActive(false);
            No_purchase_panel.SetActive(true);
        }
        if (UserDataManager.instance.character == Character.RARE)
        {
            star.gameObject.SetActive(true);
            star.sprite = userDatas[checkIndex].star.sprite;
        }
        else
        {
            star.gameObject.SetActive(false);
        }

        spriteset_character(CharacterImage2);
    }

    /// <summary>
    /// image/text 세팅
    /// </summary>
    /// <param name="Characterimage"></param>
    /// <param name="E_character"></param>
    private void spriteset_character(Image Characterimage)
    {
        Characterimage.sprite = userDatas[checkIndex].ItemImage.sprite;
        var character = Characterimage.GetComponent<RectTransform>();
        var user = userDatas[checkIndex].ItemImage.GetComponent<RectTransform>();
        character.anchorMin = new Vector2(user.anchorMin.x, user.anchorMin.y);
        character.anchorMax = new Vector2(user.anchorMax.x, user.anchorMax.y);
        var rect = character.rect;
        rect.yMax = 0;
        rect.yMin = 0;
        rect.xMin = 0;
        rect.xMax = 0;

        var text = UserDataManager.instance.Player_Eqip.user_Characters[checkIndex];
        characterExplanation_Text2.text = text.text;
    }

    public void Submit2()
    {
        var character = UserDataManager.instance.Player_Eqip.user_Characters[checkIndex];
        UserDataManager.instance.Player_Eqip.select_Character = character;

        center2.gameObject.SetActive(false);
        SceneExit.interactable = true;

     


    }

    public void Character_Purchase(int opal_price)
    {
        if (UserDataManager.instance.opalmoney >= opal_price)
        {
            UserDataManager.instance.Achiement_Secces(4);

            int money = UserDataManager.instance.opalmoney;
            money -= opal_price;
            if (money < 0)
                money = 0;

            UserDataManager.instance.opalmoney = money;
            print("구매성공");
            MessagePopManager.instance.ShowPop("구매성공!!!");
            StartCoroutine(purchase_Effect.purchase_effect_play());

            ////////////////////////////////////
            UserDataManager.instance.Player_Eqip.user_Characters[checkIndex].is_purchase = true;
            userDatas[checkIndex].isPurchase = true;
            Click("RARE");

            UserDataManager.instance.ch_number++;

            if (UserDataManager.instance.ch_number >= 3)
                UserDataManager.instance.Achiement_Secces(6);

        }
        else
        {
            print("구매실패(오팔부족)");
            MessagePopManager.instance.ShowPop("케릭터 구매에 실패했습니다.");
        }
    }

    public void character_setting()
    {
        var character = UserDataManager.instance.Player_Eqip.select_Character;

        List<charcter_infomation> list = new List<charcter_infomation>(UserDataManager.instance.Player_Eqip.user_Characters);

        var listAll = list.FindAll(x => x.is_purchase == true && x.E_charcter == Character.RARE);

        foreach (var item in listAll)
        {
            Debug.Log(item.charcterindx);
            userDatas[item.charcterindx].isPurchase = true;
        }
    }
}