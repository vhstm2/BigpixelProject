using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_purchase : MonoBehaviour
{
    public int Money;
    public int opal;

    #region 게임머니 구입

    //게임머니 구입
    public void GameMoney_purchase()
    {
        if (UserDataManager.instance.opalmoney < opal)
        {
            Debug.Log("오팔이 부족합니다");
            return;
        }

        UserDataManager.instance.opalmoney -= opal;
        UserDataManager.instance.gameMoney += Money;

        Debug.Log("게임머니 구매");
    }

    #endregion 게임머니 구입

    #region 아이템 구입

    public string purchaseItem;

    //아이템 구입
    public void ITEM_purchase()
    {
        if (UserDataManager.instance.gameMoney < Money)
        {
            Debug.Log("돈이 부족합니다");
            return;
        }

        if (purchaseItem.Contains("투명화"))
        {
            //투명화 구입
            UserDataManager.instance.Item_Transparency += 1;

            Debug.Log("투명화 아이템 구매");
        }
        else if (purchaseItem.Contains("슬로우"))
        {
            //슬로우 구입
            UserDataManager.instance.Item_Slow += 1;

            Debug.Log("슬로우 아이템 구매");
        }
        else
        {
            //지우개 구입
            UserDataManager.instance.Item_Eraser += 1;

            Debug.Log("지우개 아이템 구매");
        }

        UserDataManager.instance.gameMoney -= Money;
    }

    #endregion 아이템 구입
}