using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinManagement : MonoBehaviour {

	public Text coinsText;
	public static int coins;

	void OnDestroy() {

		//save coins
		PlayerPrefs.SetInt("coins",coins);
	}

	void Start () {
	
		//get saved coins, if first game, coins = 40
		coins = PlayerPrefs.GetInt("coins",40);

	}


	void Update () {

		//set text to coins
		coinsText.text = "" + coins;
	}
}
