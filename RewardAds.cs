using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class RewardAds : MonoBehaviour
{

    public GameObject RewardButtonGO;

    public void OnShowRewardAdClick() {

        ShowRewardedAd();

    }

    void Update() {

        if (AnswerManager.nextLevelActivated) {

            RewardButtonGO.SetActive(true);

        }

    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else {
            RewardButtonGO.SetActive(false);
        }
    }
    

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Code to reward the player
                CoinManagement.coins += 20;
                PlayerPrefs.SetInt("coins",CoinManagement.coins);
                RewardButtonGO.SetActive(false);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
