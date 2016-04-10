using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {

	public Animator shopAnimator;

	private AnswerManager answerManagerScript;

	public GameObject[] guessBoxGO;
	public Text[] guessBoxText;

	public static bool purchasedOnce;
	private int[] array;
	private int count;

	public static bool isShopOn;

	private List<Text> GoList = new List<Text>();

	void Start () {
		shopAnimator.SetBool("shopIn",false);
		shopAnimator.SetBool("shopOut",false);
		isShopOn = false;

		foreach(GameObject go in guessBoxGO){
			go.SetActive(true);
		}


	}


	public void OnRemoveLettersClick() {

		if(CoinManagement.coins >= 40){
			
			count = 0;

				foreach(Text txt in guessBoxText){

				if(txt.tag == "letterFromWord"){
					continue;	
				}

				if(txt.tag != "letterFromWord"){
					GoList.Add(txt);
			}
		}

		while(count < 3){

			GoList[count].GetComponentInParent<Image>().enabled = false;
			GoList[count].GetComponentInParent<BoxCollider>().enabled = false;
			GoList[count].text = "";
			

			count++;

		}
			
		CoinManagement.coins -= 40;

		ExitShop();

		}

	}
		

	
	public void OpenNextLevel() {

		if(CoinManagement.coins >= 80){


			AnswerManager.buyNewLevel = true;

			CoinManagement.coins -= 80;

			ExitShop();
		}
		
	}

	public void OnQuestionMarkClick() {

		isShopOn = true;
		shopAnimator.SetBool("shopIn",true);
		shopAnimator.SetBool("shopOut",false);

	}

	public void OnExitClick(){

		ExitShop();

	}

	void ExitShop(){
		shopAnimator.SetBool("shopOut",true);
		shopAnimator.SetBool("shopIn",false);
		isShopOn = false;
		BackButtonBehaviour.disableShopBool = false;
	}

	IEnumerator ActivateButtons() {

		yield return new WaitForSeconds(0.4f);

		foreach(Text thx in guessBoxText){
			thx.GetComponentInParent<Image>().enabled = true;
			thx.GetComponentInParent<BoxCollider>().enabled = true;
			GoList.Clear();

		}
	}

	void Update() {

		if(AnswerManager.nextLevelActivated){


			StartCoroutine(ActivateButtons());
	
		}

		if(AnswerManager.buyNewLevel){
			shopAnimator.SetBool("shopOut",true);
			shopAnimator.SetBool("shopIn",false);
		}

		if(BackButtonBehaviour.disableShopBool){
			ExitShop();
		}

	}




}
