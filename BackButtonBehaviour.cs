using UnityEngine;
using System.Collections;

public class BackButtonBehaviour : MonoBehaviour {

	public static bool disableShopBool;

	void Update () {

		Debug.LogError(disableShopBool);
		Debug.LogError(ShopManager.isShopOn);

		if(Input.GetKeyDown(KeyCode.Escape)){

			if(ShopManager.isShopOn){
				disableShopBool = true;
			}

			if(Application.loadedLevelName == "Game" && !ShopManager.isShopOn){
				Application.LoadLevel("MainMenu");
			}
			if(Application.loadedLevelName == "MainMenu"){
			Application.Quit();
			}

		}

	}
}
