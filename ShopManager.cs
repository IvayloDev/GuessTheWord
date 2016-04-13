using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {

	public Animator shopAnimator;

	private AnswerManager answerManagerScript;

	public GameObject[] guessBoxGO;
	public Text[] guessBoxText;

	private int[] array;
	private int count;

	public static bool isShopOn;
	public static bool purchasedOnce;

	//create a list holding the text objects
	private List<Text> GoList = new List<Text>();

	void Start () {

		//reset all animations to default
		shopAnimator.SetBool("shopIn",false);
		shopAnimator.SetBool("shopOut",false);
		isShopOn = false;
		purchasedOnce = false;

		//foreach text, enable image and collider
		foreach(Text thx in guessBoxText){
			thx.GetComponentInParent<Image>().enabled = true;
			thx.GetComponentInParent<BoxCollider>().enabled = true;
			GoList.Clear();
			
		}

		//activate all guess boxes (just to be sure)
		foreach(GameObject go in guessBoxGO){
			go.SetActive(true);
		}


	}


	//called if player has clicked "remove letter"
	public void OnRemoveLettersClick() {


		//if player has enough money to buy and this is his first purchase
		if(CoinManagement.coins >= 40 && purchasedOnce == false){

			//exit the shop
			ExitShop();                       

			//reset  count
			count = 0;
			//set purchased bool to true
			purchasedOnce = true;

			//check for every text component if it is marked with tag "letterFromWord" and if yes continue to find the chars in the texts which are not from the word 
				foreach(Text txt in guessBoxText){

				if(txt.tag == "letterFromWord"){
					continue;	
				}

				if(txt.tag != "letterFromWord"){
					//add the found letter
					GoList.Add(txt);
			}
		}

			// remove three letters
		while(count < 3){

				//doesn't work with setActive(false), so insted I disable the image and box collider which is a lot like disabling the gameObject
			GoList[count].GetComponentInParent<Image>().enabled = false;
			GoList[count].GetComponentInParent<BoxCollider>().enabled = false;
			GoList[count].text = "";
			

			//remove if you want infinite loop 
			count++;

		}
			//subtract 40 coins
		CoinManagement.coins -= 40;


		}

	}
		

	//if player clicks on "open next level"
	public void OpenNextLevel() {

		//check if player has 80 coins
		if(CoinManagement.coins >= 80){

			//set buy new level to new
			AnswerManager.buyNewLevel = true;

			//enable all components in the text objects
			foreach(Text thx in guessBoxText){
				thx.GetComponentInParent<Image>().enabled = true;
				thx.GetComponentInParent<BoxCollider>().enabled = true;

				//clear list
				GoList.Clear();
				
			}


			//subtract 80 points
			CoinManagement.coins -= 80;


			//get out of shop
			ExitShop();
		}
		
	}


	public void OnQuestionMarkClick() {

		//activate shop and animations for it
		isShopOn = true;
		shopAnimator.SetBool("shopIn",true);
		shopAnimator.SetBool("shopOut",false);

	}

	public void OnExitClick(){

		//on exit button click
		ExitShop();

	}

	void ExitShop(){
		//trigger animations for shop fadeOut
		shopAnimator.SetBool("shopOut",true);
		shopAnimator.SetBool("shopIn",false);
		isShopOn = false;

		//bool used to check if the player is in shop. If true the back button will fadeOut the shop, if not go to main screen
		BackButtonBehaviour.disableShopBool = false;
	}

	IEnumerator ActivateButtons() {

		//wait 0.4 seconds and make changes while player is in new level panel
		yield return new WaitForSeconds(0.4f);

		//activate all texts 
		foreach(Text thx in guessBoxText){
			thx.GetComponentInParent<Image>().enabled = true;
			thx.GetComponentInParent<BoxCollider>().enabled = true;
			GoList.Clear();

		}
	}

	void Update() {

		if(AnswerManager.nextLevelActivated){

			//check if next level is enabled
			StartCoroutine(ActivateButtons());
	
		}

		if(AnswerManager.buyNewLevel){
			//fadeOut shop
			shopAnimator.SetBool("shopOut",true);
			shopAnimator.SetBool("shopIn",false);
		}

		if(BackButtonBehaviour.disableShopBool){
			//if back is pressed, exit shop
			ExitShop();
		}

	}




}
