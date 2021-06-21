using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCounting : MonoBehaviour
{
    // Start is called before the first frame update

    public StageChange stageChange;
    public GameObject[] MapInfo;

    public Button LeftButton;
    public Button RightButton;

    private void Awake()
    {
        if (UserDataManager.instance.Player_Eqip.StageNumber == 0)
        {
            LeftButton.interactable = false;
        }
        else
        {
            LeftButton.interactable = true;
        }

        if (UserDataManager.instance.Player_Eqip.StageNumber == stageChange.MapsImages.Length - 1)
        {
            RightButton.interactable = false;
        }
        else
        {
            RightButton.interactable = true;
        }
        MapControll();
    }

    public void LeftButtonClick()
    {
        UserDataManager.instance.Player_Eqip.StageNumber--;
        if (UserDataManager.instance.Player_Eqip.StageNumber >= 0)
        {
            //stageChange.Change();
            MapControll();
        }
        else
        {
            UserDataManager.instance.Player_Eqip.StageNumber++;
        }

        DirButtonOnOff();
    }

    public void RightButtonClick()
    {
        UserDataManager.instance.Player_Eqip.StageNumber++;
        if (UserDataManager.instance.Player_Eqip.StageNumber <= stageChange.MapsImages.Length - 1)
        {
            //stageChange.Change();
            MapControll();
        }
        else
        {
            UserDataManager.instance.Player_Eqip.StageNumber--;
        }

        DirButtonOnOff();
    }

    private void DirButtonOnOff()
    {
        if (UserDataManager.instance.Player_Eqip.StageNumber == 0)
        {
            LeftButton.interactable = false;
            RightButton.interactable = true;
        }
        else if (UserDataManager.instance.Player_Eqip.StageNumber + 1 == stageChange.MapsImages.Length)
        {
            RightButton.interactable = false;
            LeftButton.interactable = true;
        }
        else
        {
            RightButton.interactable = true;
            LeftButton.interactable = true;
        }
    }

    public void MapControll()
    {
        MapInfo[UserDataManager.instance.Player_Eqip.StageNumber].SetActive(true);

        for (int i = 0; i < MapInfo.Length; i++)
        {
            if (i == UserDataManager.instance.Player_Eqip.StageNumber)
                continue;
            else if (MapInfo[i].activeSelf)
                MapInfo[i].SetActive(false);
        }
    }
}