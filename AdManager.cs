using UnityEngine;
using System.Collections;
using GoogleMobileAds; 

public class AdManager : MonoBehaviour {

	
	private AdMobPlugin admob;
	private const string Interstitial_ID = "ca-app-pub-4847787002677683/8446856652";
	private const string Banner_ID = "ID";
	public static int adCount = 0;
    private bool showAd;
	
	void Awake () {
        //set up
        showAd = false;
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(Banner_ID,AdMobPlugin.AdSize.SMART_BANNER,true, Interstitial_ID);
	}
	void Update () {
		
        //check if adcount is 2, request ad and reset count
		if(adCount == 2){
			admob.RequestInterstitial();
			adCount = 0;
            showAd = true;
		}

		HandleInterstitialLoaded();
	}

	void OnEnable(){
		AdMobPlugin.InterstitialLoaded += HandleInterstitialLoaded;
	}
	
	void OnDisable() {
		
		AdMobPlugin.InterstitialLoaded -= HandleInterstitialLoaded;
		
	}
	
	public void HandleInterstitialLoaded() {

		if(AnswerManager.nextLevelActivated && showAd){
			//when next level is activated, show ad
			admob.ShowInterstitial();
            showAd = false;
		}
	}
}

