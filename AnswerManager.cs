using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnswerManager : MonoBehaviour {


	public TextAsset WordsXMLFile;
	private AnswerData answerData;
	public static Word currentWord;
	public static int r,i,int2,h,WordLength;
	private Word tmp;
	private char RandomLetter;
	private string BGAlphabet ="АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЮЯ";
	private string userInput = "";
	public Text LevelText;
	public GameObject[] PossibleLetters;
	public GameObject[] guessBoxGO;
	public Text[] PossibleTexts;
	public Image currentImage;
	public RectTransform[] answerTransforms;
	public RectTransform[] guessBoxTransforms;	
	public AudioClip[] audioClip;
	public static bool buyNewLevel;

	private int index,stringIndex;

	public static int levelIndex;

	public static bool nextLevelActivated;

	private Vector3 currentPos; 

	private Vector3 initPos0,initPos1,initPos2,initPos3,initPos4,initPos5,initPos6,initPos7,initPos8,initPos9,initPos10,initPos11,initPos12;


		public void OnBackArrowClick(){

		//handling of back button press
			Application.LoadLevel("MainMenu");
		}

	void OnDestroy(){

		//saving
		PlayerPrefs.SetInt("R",r);
		PlayerPrefs.SetInt("I",i);
		PlayerPrefs.SetString("currentPicString",currentWord.wordText);
		PlayerPrefs.SetInt("LevelIndex",levelIndex);
		PlayerPrefs.Save();
	}

	
	public void PlaySound(int clip){

		//Sounds 
		GetComponent<AudioSource>().clip = audioClip[clip];
		GetComponent<AudioSource>().Play();
	}


	//called when a level is completed
	public void NextLevel(){
	

		//add point to the level count
		levelIndex++;

		//reset buy new level bool from shopManager +
		buyNewLevel = false;


		//reset the size and set default tag of every guess word 
		foreach(GameObject GO in guessBoxGO){
			GO.tag = "Untagged";
			GO.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
		}


		//if the previous level contained the last picture. Shuffle the array again
		if(r >= answerData.words.Count) {
			for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
				r = Random.Range(0,i);		    //	get a random number from 0 to array count
				tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
				answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
				answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
			}
		}

		//set the current word to index++
		currentWord = answerData.words[r++];
		
		//reset all letters
		foreach (GameObject GO in PossibleLetters) {
			GO.SetActive(false);
		}


		//holds the words' length
		WordLength = currentWord.wordText.ToString().Length;
	


		//activate blank spots,  however letters the word contains 
		for (int u = 0; u <= WordLength - 1; u++) {
			PossibleLetters[u].SetActive(true);
		}

		//call LettersToChooseFrom
		LettersToChooseFrom(BGAlphabet);


	//get the current word and set the picture for it
		currentImage.sprite = Resources.Load(currentWord.wordText,typeof(Sprite)) as Sprite;
		
		
		//preset all guess boxes transfroms to vectors
		guessBoxTransforms[0].transform.position = initPos0;
		guessBoxTransforms[1].transform.position = initPos1;
		guessBoxTransforms[2].transform.position = initPos2;
		guessBoxTransforms[3].transform.position = initPos3;
		guessBoxTransforms[4].transform.position = initPos4;
		guessBoxTransforms[5].transform.position = initPos5;
		guessBoxTransforms[6].transform.position = initPos6;
		guessBoxTransforms[7].transform.position = initPos7;
		guessBoxTransforms[8].transform.position = initPos8;
		guessBoxTransforms[9].transform.position = initPos9;
		guessBoxTransforms[10].transform.position = initPos10;
		guessBoxTransforms[11].transform.position = initPos11;
		guessBoxTransforms[12].transform.position = initPos12;


			//Save all the things so if the player force quits the game while in game screen, next time when the game is loaded it will get the last saved values
				PlayerPrefs.SetInt("LevelIndex",levelIndex);
				PlayerPrefs.SetString("currentPicString",currentWord.wordText);
				PlayerPrefs.SetInt("coins",CoinManagement.coins);
				PlayerPrefs.SetInt("R",r);
				PlayerPrefs.SetInt("I",i);

		//add 1 to adCount. if == 4 request an add
		AdManager.adCount++;

		//this is set to false so on new level the player can purchase "remove letters" bonus. This keeps track of if the user has used money to buy "remove letters" and if yes > he cannot buy anymore
		ShopManager.purchasedOnce = false;

		//activate each gameObject(if the player had used "remove Letters" on the prev level)
		foreach(GameObject go in guessBoxGO){
			go.SetActive(true);
		}

	}



	void Start () {

		//get values from playerPrefs
		r = PlayerPrefs.GetInt("R",0);
		i = PlayerPrefs.GetInt("I",0);
		CoinManagement.coins = PlayerPrefs.GetInt("coins",40);


		//Load words from xml file
		answerData = AnswerData.LoadFromText(WordsXMLFile.text);

		//if this is a new game  
		if(MainMenuButtons.newGameBool){
			MainMenuButtons.newGameBool = false;

				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    											//	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
			}


			currentWord = answerData.words[r++];
		

		} 

			//if the player continues from where he left off
		if(MainMenuButtons.resumeGameBool){
			MainMenuButtons.resumeGameBool = false;

			//get the word player is currently on. This is so you dont get a new picture everytime you continue you level 
				currentWord.wordText = PlayerPrefs.GetString("currentPicString");

			//This is to check if somehow there isnt a word loaded or the level is 0
			if(currentWord.wordText == null || levelIndex == 0){
				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    //	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
				}
				currentWord = answerData.words[r++];
			}
		}


		currentImage.sprite = Resources.Load(currentWord.wordText,typeof(Sprite)) as Sprite;
		


		//set guess transforms to initial position
		initPos0 = guessBoxTransforms[0].transform.position;
		initPos1 = guessBoxTransforms[1].transform.position;
		initPos2 = guessBoxTransforms[2].transform.position;
		initPos3 = guessBoxTransforms[3].transform.position;
		initPos4 = guessBoxTransforms[4].transform.position;
		initPos5 = guessBoxTransforms[5].transform.position;
		initPos6 = guessBoxTransforms[6].transform.position;
		initPos7 = guessBoxTransforms[7].transform.position;
		initPos8 = guessBoxTransforms[8].transform.position;
		initPos9 = guessBoxTransforms[9].transform.position;
		initPos10 = guessBoxTransforms[10].transform.position;
		initPos11 = guessBoxTransforms[11].transform.position;
		initPos12 = guessBoxTransforms[12].transform.position;
		
		


		//same stuff as the on next level method

		foreach (GameObject GO in PossibleLetters) {
			GO.SetActive(false);
		}

		WordLength = currentWord.wordText.ToString().Length;

		for (int u = 0; u <= WordLength - 1; u++) {
			PossibleLetters[u].SetActive(true);
			
		}

		LettersToChooseFrom(BGAlphabet);


		//END OF START
	}




	public void LettersToChooseFrom(string alphabet){

		//convert the alphabet to characters
		alphabet.ToCharArray();


		
//set random char in each guess box text box
		foreach(Text txt in PossibleTexts){

			txt.text = "" + alphabet[Random.Range(0,alphabet.Length)];
	
		}
		
		//choose non repeating places for the word's letters to be held in
		for (int2 = PossibleTexts.Length - 1; int2 > 0; int2--) {  // get the count of the array an shuffle all the elements   begin from end to start of the array 
			h = Random.Range(0, int2);												   //	get a random number from 0 to array count
			Text tmp2 = PossibleTexts[int2];							   //	swap the random place (eg: 3) and assign it to tmp 
			PossibleTexts[int2] = PossibleTexts[h];  // swap the i(current number) with tmp
			PossibleTexts[h] = tmp2;                            // swap the tmp with the value of i
			}

		//set all texts to default tag
		foreach(Text txt in PossibleTexts){
			txt.tag = "Untagged";
		}


		//fill in the word's chars in the random places chosen above
		foreach(char chars in currentWord.wordText){
			
			PossibleTexts[h].GetComponent<Text>().tag = "letterFromWord";
				PossibleTexts[h++].text = "" + chars;
			}
	} 


	//new level coroutine.
	IEnumerator NewLevelCor () {

		//activate bool
		nextLevelActivated = true;

		//play next level sound
		PlaySound(0);

		//wait 1 secound (mainly for animation purposes)
		yield return new WaitForSeconds(1);

		//activate next level
		NextLevel();
	}



	void Update() {
	
		//if the user "bought" a new level reset things
		if(buyNewLevel){
				NextLevel();
				userInput = "";
				index = 0;
				stringIndex = 0;
		}


		//set the text above the picture to current level index
		LevelText.text = "" + levelIndex;


		//if user has filled the blank spaces 
		if(index == WordLength){

			//if the user has guess the word 
			if(userInput == currentWord.wordText){

				//start newlevel coroutine
				StartCoroutine(NewLevelCor());

				//reset all of these
				userInput = "";
				index = 0;
				stringIndex = 0;

				//add 3 coins
				CoinManagement.coins += 3;
				return;
			}
		}


		//On touch 
		if( Input.GetMouseButtonDown(0)) {


			RaycastHit hit;

			//get coordinates of clicked space
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			//use raycast to "see" where the user tapped
			if(Physics.Raycast(ray,out hit,100)) {


				//all of these check if the user clicked a guess box which is currently in the answers(top boxes) and return to its init position. Also remove the letter from the string which hold the guessed word,reset size,tag,remove 1 index and play sound 
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1"){
					hit.transform.position = initPos0;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (1)"){
					hit.transform.position = initPos1;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (2)"){
					hit.transform.position = initPos2;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (3)"){
					hit.transform.position = initPos3;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (4)"){
					hit.transform.position = initPos4;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (5)"){
					hit.transform.position = initPos5;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (6)"){
					hit.transform.position = initPos6;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (7)"){
					hit.transform.position = initPos7;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (8)"){
					hit.transform.position = initPos8;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (9)"){
					hit.transform.position = initPos9;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (10)"){
					hit.transform.position = initPos10;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (11)"){
					hit.transform.position = initPos11;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (12)"){
					hit.transform.position = initPos12;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					PlaySound(2);
					return;
				}
			

				//if the the clicked box is not in top boxes, put it there, add tag, index++ make it smaller in size and play sound
				if(index != WordLength && hit.transform.tag != "isTop"){
					hit.transform.position = answerTransforms[index++].transform.position;
					hit.transform.tag = "isTop";
					hit.transform.GetComponent<RectTransform>().sizeDelta = answerTransforms[0].transform.GetComponent<RectTransform>().sizeDelta / 1.1f;
					//add picked letter to string
					if(userInput.Length != WordLength){
					userInput = userInput.Insert(stringIndex++,hit.transform.GetComponentInChildren<Text>().text);
					}
						PlaySound(1);					
				}
					

				//this makes sure you cannot exceed the current word lenght
				if(index >= WordLength){
					
					index = WordLength;
				
				}
			}	
		}
	}
}
