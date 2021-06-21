using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] MapsImages;

   
    private void OnEnable()
    {
        GooglePlayGPGS.DeleEvent += Change;

        //if (SceneManager.GetActiveScene().name != "Main")

       
    }

    private void OnDisable()
    {
        GooglePlayGPGS.DeleEvent -= Change;
    }
    

    public void Change()
    {
        MapsImages[UserDataManager.instance.Player_Eqip.StageNumber].SetActive(true);

        for (int i = 0; i < MapsImages.Length; i++)
        {
            if (i == UserDataManager.instance.Player_Eqip.StageNumber)
                continue;
            else if (MapsImages[i].activeSelf)
                MapsImages[i].SetActive(false);
        }
    }
}