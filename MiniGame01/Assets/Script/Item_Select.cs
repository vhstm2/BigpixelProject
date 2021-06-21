using UnityEngine;

public class Item_Select : MonoBehaviour
{
    public static int ItemSelect_idx;

    public Character_item[] character_Items;

    private int prevIdx;

    private void OnEnable()
    {
        //Itembox();
    }

    public void SelectItemClick()
    {
        character_Items[prevIdx].Check.gameObject.SetActive(false);

        prevIdx = ItemSelect_idx;

        character_Items[prevIdx].Check.gameObject.SetActive(true);

        // Itembox();
        // 인게임에 아이템 들고가기
    }

    private void Itembox()
    {
        if (UserDataManager.instance.Player_Eqip.Slow == 0) character_Items[0].ItemButton.interactable = false;
        else character_Items[0].ItemButton.interactable = true;

        if (UserDataManager.instance.Player_Eqip.Eraser == 0) character_Items[2].ItemButton.interactable = false;
        else character_Items[2].ItemButton.interactable = true;

        if (UserDataManager.instance.Player_Eqip.Transparenc == 0) character_Items[3].ItemButton.interactable = false;
        else character_Items[3].ItemButton.interactable = true;
    }
}