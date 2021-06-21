using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sound
{
    [Serializable]
    public class Sound
    {
        public AudioClip clip;
        public SoundPlayType soundPlayType;
        public string soundclip_Link = string.Empty;
    }

    public class SoundManager : MonoBehaviour
    {
        public AudioSource[] source;

        public Sound[] sound;

        public static bool BMG_Sound_Onoff = true;
        public static bool SFX_Sound_Onoff = true;
        
        

        public void MainBGM()
        {   
            if(BMG_Sound_Onoff)
            OnClickClip(sound[1]);
        }

        public void GameBGM()
        {
            if(BMG_Sound_Onoff)
            OnClickClip(sound[0]);
        }

        public void ButtonSound()
        {   
            if(SFX_Sound_Onoff)
            OnClickClip(sound[2]);
        }

        public void LoadingSound()
        {
            if(SFX_Sound_Onoff)
            OnClickClip(sound[3]);
        }


        public void Bgm_Off()
        {
            source[0].Stop();
        }
        public void OnClickClip(Sound sound)
        {
            switch (sound.soundPlayType)
            {
                case SoundPlayType.BGM:
                    source[0].clip = sound.clip;
                    source[0].loop = true;
                    source[0].Play();
                    break;

                case SoundPlayType.EFFECT:
                    break;

                case SoundPlayType.UI:
                    source[1].clip = sound.clip;
                    source[1].loop = false;
                    source[1].PlayOneShot(sound.clip);
                    break;

                case SoundPlayType.Loading:
                    source[1].clip = sound.clip;
                    source[1].loop = false;
                    source[1].PlayOneShot(sound.clip);
                    break;
            }
        }
    }
}