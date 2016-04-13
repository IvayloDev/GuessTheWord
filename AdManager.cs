using UnityEngine;
using System.Collections;
using GoogleMobileAds; 

public class AdManager : MonoBehaviour {

	
	private AdMobPlugin admob;
	private const string Interstitial_ID = "ID";
	private const string Banner_ID = "ID";
	public static int adCount = 0;
	
	void Awake () {
		//set up a banner
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(Banner_ID,AdMobPlugin.AdSize.SMART_BANNER,true, Interstitial_ID);

	}
	void Update () {

		//check if adcount is 4, request ad and reset count
		if(adCount == 4){
			admob.RequestInterstitial();
			adCount = 0;
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

		if(AnswerManager.nextLevelActivated){
			//when next level is activated, shop ad
			admob.ShowInterstitial();
		}
	}
}

