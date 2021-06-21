using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger Instance;

    public static SceneChanger instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(SceneChanger)) as SceneChanger;

                if (Instance == null)
                {
                    Debug.LogError("There's no active SceneChanger object");
                }
            }

            return Instance;
        }
    }

    public UnityEngine.UI.Image FadeOut_Ob = null;

    public void SceneChange(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void SceneChangeSyne(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    private void Awake()
    {
        //if(Instance != null)
        //DontDestroyOnLoad(this);
    }
}