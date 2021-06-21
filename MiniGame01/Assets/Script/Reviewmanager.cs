using Google.Play.Review;
using System.Collections;
using UnityEngine;

public class Reviewmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private ReviewManager _reviewManager;

    private PlayReviewInfo _playReviewInfo;

    private void Start()
    {
    }

    public void ReviewStart()
    {
        StartCoroutine(ReviewIEnumerater());
    }

    private IEnumerator ReviewIEnumerater()
    {
        yield return null;
        if (Application.platform == RuntimePlatform.Android)
        {
            _reviewManager = new ReviewManager();

            var playReviewInfoAsyncOperation = _reviewManager.RequestReviewFlow();

            playReviewInfoAsyncOperation.Completed += playReviewInfoAsync =>
            {
                if (playReviewInfoAsync.Error != ReviewErrorCode.NoError)
                {
                    var playReviewInfo = playReviewInfoAsync.GetResult();
                    _reviewManager.LaunchReviewFlow(playReviewInfo);
                }
            };
        }
    }
}