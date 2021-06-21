using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.Networking;

public class Localizing
{
    public string Korean;
    public string English;
    public string Japanese;
}

public enum localizer_Enum
{
    Korean =0, English,  Japanese
}

public class Localizer : MonoBehaviour
{
    public static Localizer instance;

    public localizer_Enum localizer = localizer_Enum.Korean;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private List<Dictionary<string, object>> locallizerDic;

    private void Start()
    {
        locallizerDic = CSVReader.Read("file");

        for (int i = 0; i < locallizerDic.Count; i++)
        {
            Localizing lo = new Localizing();
            lo.Korean = locallizerDic[i]["Korean"].ToString();
            lo.English = locallizerDic[i]["English"].ToString();
            lo.Japanese = locallizerDic[i]["Japanese"].ToString();

            UserDataManager.instance.locallizingDic.Add(locallizerDic[i]["KEY"].ToString(), lo);
        }

        if (Application.systemLanguage == SystemLanguage.Korean)
            localizer = localizer_Enum.Korean;

        else if (Application.systemLanguage == SystemLanguage.English)
            localizer = localizer_Enum.English;
        
        else
            localizer = localizer_Enum.Japanese;




    }

    // Update is called once per frame
    private void Lan_Key(object[] ob, string key)
    {
        Debug.Log(UserDataManager.instance.locallizingDic[key].Korean);
        Debug.Log(UserDataManager.instance.locallizingDic[key].Japanese);
        Debug.Log(UserDataManager.instance.locallizingDic[key].English);
    }
}