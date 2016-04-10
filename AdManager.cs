using UnityEngine;
using System.Collections;
using GoogleMobileAds; 

public class AdManager : MonoBehaviour {

	
	private AdMobPlugin admob;
	private const string Interstitial_ID = "ID";
	private const string Banner_ID = "ID";
	public static int adCount = 0;
	
	void Awake () {
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(Banner_ID,AdMobPlugin.AdSize.SMART_BANNER,true, Interstitial_ID,true);
		admob.RequestAd();

	}
	void Update () {

		if(adCount == 2){
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
			admob.ShowInterstitial();
		}
	}
}

