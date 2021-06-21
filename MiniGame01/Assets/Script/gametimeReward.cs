using TMPro;
using UnityEngine;

public class gametimeReward : MonoBehaviour
{
    public rewardvalue[] rewards;

    public UnityEngine.UI.Image image;
    public TextMeshProUGUI rewardText;

    public rewardvalue reValue;

    private void OnEnable()
    {
        rewardEnter();
    }

    public void rewardSetting()
    {
        //  int n = Random.Range(0, rewards.Length);    //보상종류
        //  int m = Random.Range(rewards[n].reward, rewards[n].reward + 20);        //보상수치
        rewards[0].reward = UserDataManager.instance.Player_Eqip.rewardIndx;
        reValue = rewards[0];
        //reValue = rewards[UserDataManager.instance.Player_Eqip.rewardIndx];
        image.sprite = reValue.S_reward;
        rewardText.text = reValue.reward.ToString();
    }

    public void rewrad()
    {
        UserDataManager.instance.opalmoney += reValue.reward;
    }

    public void rewardIndex()
    {
        int n = Random.Range(1, 6);    //보상종류
        UserDataManager.instance.Player_Eqip.rewardIndx = n;
        reValue.reward = n;
    }

    public void rewardEnter()
    {
        rewardSetting();
    }

    public void Enters()
    {
        rewrad();         //게이지 보상줌
        rewardIndex();    //다음게이지보상 고르기
        rewardEnter();    //다음게이지보상 결정

        UserDataManager.instance.timer = 1800f;
    }
}

[System.Serializable]
public class rewardvalue
{
    public string reward_name;
    public Sprite S_reward;
    public int reward;
}