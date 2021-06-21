using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager Instance;

    public static StageManager instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = (StageManager)FindObjectOfType(typeof(StageManager));
                if (Instance == null)
                {
                    Debug.Log("There's no active StageManager object");
                }
            }
            return Instance;
        }
    }


    private void Awake()
    {
        if (Instance == null)
            DontDestroyOnLoad(this);
        if (Instance != null && Instance != this)
            DestroyImmediate(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Sprite[] StageMapSprites;







}
