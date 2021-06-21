using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using System;

[Serializable]
public class Setting : MonoBehaviour
{
    public SoundManager soundManager;
   

    public Toggle[] bgmToggle;
    public Toggle[] sfxToggle;
    public Toggle[] googleToggle;
    public Toggle[] LanguageToggle;


    private enum setting_state
    { 
        none, bgm,sfx,google,language
    }

    private void Awake()
    {

        language();



        //===================================================================================//
        //구글 로그인중일때
        if (GooglePlayGPGS.instance.Authenticated)
        {
            toggles_changer(googleToggle, true);
        }
        else
        {
            toggles_changer(googleToggle, false);
        }
        //===================================================================================//
        //BGM켜져있을때
        if (SoundManager.BMG_Sound_Onoff)
        {
            toggles_changer(bgmToggle, true);
        }
        //BGM꺼져있을때
        else
        {
            toggles_changer(bgmToggle, false);
         
        }
        //===================================================================================//
        //SFX켜져있을때
        if (SoundManager.SFX_Sound_Onoff)
        {
            toggles_changer(sfxToggle, true);
        
        }
        //SFX꺼져있을때
        else
        {
        
            toggles_changer(sfxToggle, false);
        }
        //===================================================================================//
    }


    private void toggles_changer(Toggle[] toggles , bool onoff)
    {
        if (onoff)
        {
            toggles[0].isOn = onoff;
            toggles[1].isOn = !onoff;

            toggles[0].interactable = false;
            toggles[1].interactable = true;
        }
        else
        {
            toggles[0].isOn = onoff;
            toggles[1].isOn = !onoff;

            toggles[0].interactable = true;
            toggles[1].interactable = false;
        }

    }

    public void BGM_ONOFF(int n)
    {
        if (n == 1)
        {
            if (bgmToggle[0].isOn)
            {
                SoundManager.BMG_Sound_Onoff = true;
                //sfx_play()
                soundManager.ButtonSound();
                //bgm_play()
                soundManager.MainBGM();

                bgmToggle[1].isOn = false;

                bgmToggle[0].interactable = false;
                bgmToggle[1].interactable = true;

            }
        }
        else
        {
            if (bgmToggle[1].isOn)
            {
                SoundManager.BMG_Sound_Onoff = false;
                soundManager.Bgm_Off();

                bgmToggle[0].isOn = false;

                bgmToggle[0].interactable = true;
                bgmToggle[1].interactable = false;
            }
        }
    }






    public void SFX_ON_OFF(int n)
    {
        if (n == 1)
        {
            if (sfxToggle[0].isOn)
            {
                SoundManager.SFX_Sound_Onoff = true;
                soundManager.ButtonSound();
                sfxToggle[1].isOn = false;
                sfxToggle[0].interactable = false;
                sfxToggle[1].interactable = true;
            }
        }
        else
        {
            if (sfxToggle[1].isOn)
            {
                SoundManager.SFX_Sound_Onoff = false;
                sfxToggle[0].isOn = false;
                sfxToggle[0].interactable = true;
                sfxToggle[1].interactable = false;
            }
        }
       
    }


    public void GOOGLE_ONOFF(int n)
    {
        if (n == 1)
        {
            if (googleToggle[0].isOn)
            {
                googleToggle[1].isOn = false;
                soundManager.ButtonSound();
                GooglePlayGPGS.instance.GPGSLogin();
                googleToggle[0].interactable = false;
                googleToggle[1].interactable = true;
            }
        }
        else
        {
            if (googleToggle[1].isOn)
            {
                googleToggle[0].isOn = false;
                soundManager.ButtonSound();
                GooglePlayGPGS.instance.GoogleLogOut();
                googleToggle[0].interactable = true;
                googleToggle[1].interactable = false;
            }
        }
    }
  

    ///========================================================================
    public void language()
    {
        if (Localizer.instance.localizer == localizer_Enum.Korean)
        {
            //Korean
            //Language_Button(0);
            LanguageToggle[0].isOn = true;
            LanguageToggle[1].isOn = false;
            LanguageToggle[2].isOn = false;

            LanguageToggle[0].interactable = false;
            LanguageToggle[1].interactable = true;
            LanguageToggle[2].interactable = true;

        }
        else if (Localizer.instance.localizer == localizer_Enum.Japanese)
        {
            //Japanese
            // Language_Button(2);
            LanguageToggle[0].isOn = false;
            LanguageToggle[1].isOn = false;
            LanguageToggle[2].isOn = true;


            LanguageToggle[0].interactable = true;
            LanguageToggle[1].interactable = true;
            LanguageToggle[2].interactable = false;
        }
        else
        {
            //English
            // Language_Button(1);
            LanguageToggle[0].isOn = false;
            LanguageToggle[1].isOn = true;
            LanguageToggle[2].isOn = false;

            LanguageToggle[0].interactable = true;
            LanguageToggle[1].interactable = false;
            LanguageToggle[2].interactable = true;
        }
    }

    ///========================================================================
    
    

    ///========================================================================
    public void Language_Button(int  n)
    {
        soundManager.ButtonSound();
        if (n == 0)
        {
            if (LanguageToggle[0].isOn == true)
            {
                Localizer.instance.localizer = localizer_Enum.Korean;
                LanguageToggle[1].isOn = false;
                LanguageToggle[2].isOn = false;

                LanguageToggle[0].interactable = false;
                LanguageToggle[1].interactable = true;
                LanguageToggle[2].interactable = true;
            }
        }
        if (n == 1)
        {
            if (LanguageToggle[1].isOn == true)
            {
                Localizer.instance.localizer = localizer_Enum.English;
                LanguageToggle[0].isOn = false;
                LanguageToggle[2].isOn = false;

                LanguageToggle[0].interactable = true;
                LanguageToggle[1].interactable = false;
                LanguageToggle[2].interactable = true;
            }
        }
        if (n == 2)
        {
            if (LanguageToggle[2].isOn == true)
            {
                Localizer.instance.localizer = localizer_Enum.Japanese;
                LanguageToggle[0].isOn = false;
                LanguageToggle[1].isOn = false;

                LanguageToggle[0].interactable = true;
                LanguageToggle[1].interactable = true;
                LanguageToggle[2].interactable = false;
            }
        }
    }
}