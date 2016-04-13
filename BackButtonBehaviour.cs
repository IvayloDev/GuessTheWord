using UnityEngine;
using System.Collections;

public class BackButtonBehaviour : MonoBehaviour {

	public static bool disableShopBool;

	void Update () {

		//On back button click
		if(Input.GetKeyDown(KeyCode.Escape)){

			//Check if shop is currently open and set bool to true
			if(ShopManager.isShopOn){
				disableShopBool = true;
			}

			//If player is in game scene and shop is closed, go to MainMenu
			if(Application.loadedLevelName == "Game" && !ShopManager.isShopOn){
				Application.LoadLevel("MainMenu");
			}

			//If player is in mainMenu and back is pressed, quit game
			if(Application.loadedLevelName == "MainMenu"){
			Application.Quit();
			}

		}

	}
}
