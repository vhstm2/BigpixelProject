using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Eqip_Shop_YesAndNo : MonoBehaviour
{
    public enum purchaseEnum { 결제하기, 게임머니구입, 아이템구입 };

    public purchaseEnum PurchaseEnum;

    //돌아갈 자신의 위치
    public Transform parent;

    //커질떄 부모
    public Transform pr_front;

    //작아질떄 부모
    public Transform pr_back;

    public Shop_EQIP EQIP;

    //구입 (결제)
    [Header("결제")]
    public PurchaseButton purchase;

    //구입 게임머니 / 아이템구입
    [Header("게임머니/아이템")]
    public shop_purchase purchase2;

    //자신의 이미지 속성
    public Image ClickObject_image;

    private void Awake()
    {
        //캔버스그룹 본인 클릭불가
        //EQIP.canvasGroup.blocksRaycasts = false;
    }

    public void Click()
    {
        EQIP.YesButton.onClick.AddListener(ButtonYes);
        EQIP.NoButton.onClick.AddListener(ButtonNo);

        transform.SetParent(pr_back.transform);
        transform.DOLocalMove((Vector3.zero), 0.5f);
        transform.DOScale(Vector3.one * 1.5f, 0.5f);
        // .OnComplete(() =>
        {
            DOTween.To(() =>
            EQIP.canvasGroup.alpha,
            x => EQIP.canvasGroup.alpha = x,
            1, 0.5f).
            OnComplete(() =>
            {
                ClickObject_image.raycastTarget = false;
            });

            //var ClickObject_image = ClickObject.GetComponent<UnityEngine.UI.Image>();

            EQIP.canvasGroup.blocksRaycasts = true;

            var fadeob = UserDataManager.instance.stateMachine.fadeob;
            fadeob.raycastTarget = true;

            DOTween.To(() => fadeob.color,
            x => fadeob.color = x,
            new Color(0, 0, 0, 0.8f),
            0.5f).OnComplete(() => { });
        }//);
    }

    public void ButtonYes()
    {
        EQIP.soundManager.ButtonSound();
        if (PurchaseEnum == purchaseEnum.결제하기)
        {
            purchase.HandleClick();
        }
        if (PurchaseEnum == purchaseEnum.게임머니구입)
        {
            purchase2.GameMoney_purchase();
        }
        if (PurchaseEnum == purchaseEnum.아이템구입)
        {
            purchase2.ITEM_purchase();
        }
        ButtonNo();
    }

    public void ButtonNo()
    {
        EQIP.soundManager.ButtonSound();
        // EQIP.canvasGroup.blocksRaycasts = true;
        ClickObject_image.raycastTarget = true;
        transform.SetParent(pr_front.transform);
        transform.DOMove(parent.position, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        {
            DOTween.To(() =>
               EQIP.canvasGroup.alpha,
               x => EQIP.canvasGroup.alpha = x,
               0, 0.5f).
               OnComplete(() => { });

            // EQIP.canvasGroup.blocksRaycasts = false;

            var fadeob = UserDataManager.instance.stateMachine.fadeob;
            fadeob.raycastTarget = false;

            DOTween.To(() => fadeob.color,
                x => fadeob.color = x,
                new Color(0, 0, 0, 0.0f),
                0.5f).OnComplete(() =>
                {
                    //ClickObject = null;
                });
        }

        EQIP.YesButton.onClick.RemoveListener(ButtonYes);
        EQIP.NoButton.onClick.RemoveListener(ButtonNo);

        //ClickObject = null;
    }

    public void voidEnter()
    {
    }
}