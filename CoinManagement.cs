using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinManagement : MonoBehaviour {

	public Text coinsText;
	public static int coins;

	void OnDestroy() {
		PlayerPrefs.SetInt("coins",coins);
	}

	void Start () {
	
		coins = PlayerPrefs.GetInt("coins",40);

	}


	void Update () {
		coinsText.text = "" + coins;
	}
}
