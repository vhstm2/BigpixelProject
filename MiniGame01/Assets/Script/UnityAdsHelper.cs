using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class UnityAdsHelper : MonoBehaviour
{
    // Start is called before the first frame update
    private const string android_game_id = "3384979";

    private const string ios_game_id = "3384978";

    private const string rewarded_video_id = "rewardedVideo";

    public UnityEvent successfully;
    public UnityEvent skipped;
    public UnityEvent failed;

    public int min_Money = 1;
    public int max_Money = 5;

    private int value_Money = 0;

    private void OnEnable()
    {
        Initialize();

        value_Money = Random.Range(min_Money, max_Money);
    }

    public void UserDataSetMoney()
    {
        UserDataManager.instance.opalmoney += value_Money;
        value_Money = Random.Range(min_Money, max_Money);
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#else
        Advertisement.Initialize(android_game_id);
#endif
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };

            Advertisement.Show(rewarded_video_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");

                    // to do ...
                    // 광고 시청이 완료되었을 때 처리
                    successfully?.Invoke();

                    //광고시청 완료 카운트
                    UserDataManager.instance.Player_Eqip.AdsConut++;

                    if (UserDataManager.instance.Player_Eqip.AdsConut >= 20)
                    {
                        //7번
                        UserDataManager.instance.Achiement_Secces(7);
                    }
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");

                    // to do ...
                    // 광고가 스킵되었을 때 처리
                    skipped?.Invoke();
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");

                    // to do ...
                    // 광고 시청에 실패했을 때 처리
                    failed?.Invoke();
                    break;
                }
        }
    }
}