using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Achiement_ButtonControll : MonoBehaviour
{

    

    public GameObject[] OnOffObject;

    public Button LeftButton;
    public Button RightButton;

    private int nextInt =0;

    private void Awake() 
    {
       OnoffObjetControll(nextInt);
    }


    private void OnoffObjetControll(int n)
    {
        if(n == 0)
        {
            LeftButton.interactable = false;
            RightButton.interactable = true;
            OnOffObject[0].SetActive(true);
            OnOffObject[1].SetActive(false);
        }
        else
        {
            LeftButton.interactable = true;
            RightButton.interactable = false;
            OnOffObject[0].SetActive(false);
            OnOffObject[1].SetActive(true);
        }
    }


    public void ButtonEventClick(bool ButtonDir)
    {
        
        if(ButtonDir == true)nextInt--;
        else nextInt++;
        
        IntizerControll();
        OnoffObjetControll(nextInt);
    }

  

    private void IntizerControll()
    {
        if(nextInt <=0) nextInt = 0;
        else if(nextInt >=1) nextInt = 1;
    }




}
