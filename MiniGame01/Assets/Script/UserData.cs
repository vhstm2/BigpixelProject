using UnityEngine;
using UnityEngine.UI;

public enum Character
{
    NULL,
    NORMAL,
    RARE
}

public class UserData : MonoBehaviour
{
    public string characterName;

    public Image BlackImage;
    public Image ItemImage;
    public Image CheckImage;
    public Image star = null;
    public int ArrayIdx;

    public Character character = Character.NORMAL;

    private bool Ispurchase;

    public bool isPurchase
    {
        get { return Ispurchase; }
        set
        {
            Ispurchase = value;
            if (character == Character.RARE)
            {
                ItemImage.sprite = Ispurchase ? yes_purchase_sprite : no_purchase_sprite;
                star.sprite = Ispurchase ? Color_Star : Gray_Star;
            }
        }
    }

    public Sprite no_purchase_sprite;
    public Sprite yes_purchase_sprite;

    public Sprite Color_Star = null;
    public Sprite Gray_Star = null;

    private void OnEnable()
    {
        //if (ItemImage != null && ArrayIdx < UserDataManager.instance.UserSprites.Length)
        //  ItemImage.sprite = UserDataManager.instance.UserSprites[ArrayIdx];

        var purchase = UserDataManager.instance.Player_Eqip.user_Characters[ArrayIdx].is_purchase;
        isPurchase = purchase;
    }

    public void UserEqip()
    {
        // if (UserDataManager.instance.Player_Eqip.user_Characters[ArrayIdx].is_purchase)
        UserDataManager.instance.Player_Eqip.idx = ArrayIdx;
    }
}