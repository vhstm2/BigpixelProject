using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//루프 페이드 관련 속성 , 오디오클립 속성들
public class SoundClip : MonoBehaviour
{
    public SoundPlayType soundPlay = SoundPlayType.None;
    public string clipName = string.Empty;
    public string clipPath = string.Empty;
    public float maxVolume = 1.0f;
    public bool HasLoop = false;
    public float[] checkTime = new float[0];
    public float[] setTime = new float[0];
    public int realld = 0;

    private AudioClip clip = null;
    public int currentLoop = 0;
    public float pitch = 1.0f;
    public float dopplerLevel = 1.0f;

    //3d 사운드 설정 모드
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

    public float minDistance = 10000.0f;
    public float maxDistance = 10000.0f;
    public float sparialBlend = 1.0f;

    public float fadeTime1 = 0.0f;
    public float fadeTime2 = 0.0f;
    //패스
    // public Interpolate.Function interpolate_Func;

    public bool isFadeIn = false;
    public bool isFadeOut = false;

    public SoundClip()
    {
    }

    public SoundClip(string clipPath, string clipName)
    {
        this.clipPath = clipPath;
        this.clipName = clipName;
    }

    public void PreLoad()
    {
        if (this.clip == null)
        {
            string fullPath = this.clipPath + this.clipName;
            // this.clip = ResourceManager.Load(fullPath) as AudioClip;
        }
    }

    public void AddLoop()
    {
        //   this.checkTime = ArrayHelper
    }
}