using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChange : MonoBehaviour
{
    public Text BGMText;
    public Text SFXText;
    public Text GOOGLEText;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Localizer.instance.localizer == localizer_Enum.Korean)
        {
            GOOGLEText.text = UserDataManager.instance.locallizingDic["SETTING_Google"].Korean;
            BGMText.text = UserDataManager.instance.locallizingDic["SETTING_BGM"].Korean;
            SFXText.text = UserDataManager.instance.locallizingDic["SETTING_SFX"].Korean;
        }
        if (Localizer.instance.localizer == localizer_Enum.English)
        {
            GOOGLEText.text = UserDataManager.instance.locallizingDic["SETTING_Google"].English;
            BGMText.text = UserDataManager.instance.locallizingDic["SETTING_BGM"].English;
            SFXText.text = UserDataManager.instance.locallizingDic["SETTING_SFX"].English;
        }
       
        if (Localizer.instance.localizer == localizer_Enum.Japanese)
        {
            GOOGLEText.text = UserDataManager.instance.locallizingDic["SETTING_Google"].Japanese;
            BGMText.text = UserDataManager.instance.locallizingDic["SETTING_BGM"].Japanese;
            SFXText.text = UserDataManager.instance.locallizingDic["SETTING_SFX"].Japanese;
        }
    }
}