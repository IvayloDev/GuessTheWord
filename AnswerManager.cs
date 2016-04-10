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

	public static bool buyNewLevel;

	private int index,stringIndex;

	public static int levelIndex;

	public static bool nextLevelActivated;

	private Vector3 currentPos; 

	private Vector3 initPos0,initPos1,initPos2,initPos3,initPos4,initPos5,initPos6,initPos7,initPos8,initPos9,initPos10,initPos11,initPos12;


		public void OnBackArrowClick(){
			Application.LoadLevel("MainMenu");
		}

	void OnDestroy(){
		PlayerPrefs.SetInt("R",r);
		PlayerPrefs.SetInt("I",i);
		PlayerPrefs.SetString("currentPicString",currentWord.wordText);
		PlayerPrefs.SetInt("LevelIndex",levelIndex);
		PlayerPrefs.Save();
	}

	public void NextLevel(){

		levelIndex++;

		buyNewLevel = false;

		foreach(GameObject GO in guessBoxGO){
			GO.tag = "Untagged";
			GO.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
		}


		//on Next Level
		if(r >= answerData.words.Count) {
			for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
				r = Random.Range(0,i);		    //	get a random number from 0 to array count
				tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
				answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
				answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
				Debug.LogError("New shuffle");
			}
			
		}
		currentWord = answerData.words[r++];
		

		foreach (GameObject GO in PossibleLetters) {
			GO.SetActive(false);
		}
		
		WordLength = currentWord.wordText.ToString().Length;
	

		
		for (int u = 0; u <= WordLength - 1; u++) {
			PossibleLetters[u].SetActive(true);
			Debug.Log(currentWord.wordText);
			
		}
		
		LettersToChooseFrom(BGAlphabet);


	
		currentImage.sprite = Resources.Load(currentWord.wordText,typeof(Sprite)) as Sprite;
		
		
		
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


		Debug.LogError(currentWord.wordText);
	
				PlayerPrefs.SetInt("LevelIndex",levelIndex);
				PlayerPrefs.SetString("currentPicString",currentWord.wordText);
				PlayerPrefs.SetInt("coins",CoinManagement.coins);
				PlayerPrefs.SetInt("R",r);
				PlayerPrefs.SetInt("I",i);

		AdManager.adCount++;

	}



	void Start () {

		r = PlayerPrefs.GetInt("R",0);
		i = PlayerPrefs.GetInt("I",0);

		answerData = AnswerData.LoadFromText(WordsXMLFile.text);


		if(MainMenuButtons.newGameBool){
			MainMenuButtons.newGameBool = false;

				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    //	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
			}


			currentWord = answerData.words[r++];
		

		} 
				//currentWord.wordText = PlayerPrefs.GetString("currentPicString");
		
		if(MainMenuButtons.resumeGameBool){
			MainMenuButtons.resumeGameBool = false;

				currentWord.wordText = PlayerPrefs.GetString("currentPicString");

			if(currentWord.wordText == null || levelIndex == 0){
				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    //	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
					//}
				}
				currentWord = answerData.words[r++];
			}
		}

		currentImage.sprite = Resources.Load(currentWord.wordText,typeof(Sprite)) as Sprite;
		



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
		
		

		Debug.LogError(currentWord.wordText);


		foreach (GameObject GO in PossibleLetters) {
			GO.SetActive(false);
		}

		WordLength = currentWord.wordText.ToString().Length;

		for (int u = 0; u <= WordLength - 1; u++) {
			PossibleLetters[u].SetActive(true);
			Debug.Log(currentWord.wordText);
			
		}

		LettersToChooseFrom(BGAlphabet);


		//END OF START
	}

	public void LettersToChooseFrom(string alphabet){

		alphabet.ToCharArray();


		
		
		foreach(Text txt in PossibleTexts){

			txt.text = "" + alphabet[Random.Range(0,alphabet.Length)];
	
		}
		
		
		for (int2 = PossibleTexts.Length - 1; int2 > 0; int2--) {  // get the count of the array an shuffle all the elements   begin from end to start of the array 
			h = Random.Range(0, int2);												   //	get a random number from 0 to array count
			Text tmp2 = PossibleTexts[int2];							   //	swap the random place (eg: 3) and assign it to tmp 
			PossibleTexts[int2] = PossibleTexts[h];  // swap the i(current number) with tmp
			PossibleTexts[h] = tmp2;                            // swap the tmp with the value of i
			}

		foreach(Text txt in PossibleTexts){
		
			txt.tag = "Untagged";
		}

		foreach(char chars in currentWord.wordText){
			
			PossibleTexts[h].GetComponent<Text>().tag = "letterFromWord";
				PossibleTexts[h++].text = "" + chars;
				

			}

	} 

	IEnumerator NewLevelCor () {

		nextLevelActivated = true;
		
		yield return new WaitForSeconds(1);

		NextLevel();

	}

	void Update() {
		
		if(buyNewLevel){
				NextLevel();
				userInput = "";
				index = 0;
				stringIndex = 0;
		}


		LevelText.text = "" + levelIndex;

		Debug.LogError(currentWord.wordText);



		if(index == WordLength){
			if(userInput == currentWord.wordText){
				StartCoroutine(NewLevelCor());
				userInput = "";
				index = 0;
				stringIndex = 0;
				CoinManagement.coins += 3;
				return;
			}
		}

		if( Input.GetMouseButtonDown(0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			
			if(Physics.Raycast(ray,out hit,100)) {

				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1"){
					hit.transform.position = initPos0;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (1)"){
					hit.transform.position = initPos1;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (2)"){
					hit.transform.position = initPos2;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (3)"){
					hit.transform.position = initPos3;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (4)"){
					hit.transform.position = initPos4;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (5)"){
					hit.transform.position = initPos5;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (6)"){
					hit.transform.position = initPos6;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (7)"){
					hit.transform.position = initPos7;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (8)"){
					hit.transform.position = initPos8;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (9)"){
					hit.transform.position = initPos9;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (10)"){
					hit.transform.position = initPos10;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (11)"){
					hit.transform.position = initPos11;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (12)"){
					hit.transform.position = initPos12;
					hit.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
			

				if(index != WordLength && hit.transform.tag != "isTop"){
					hit.transform.position = answerTransforms[index++].transform.position;
					hit.transform.tag = "isTop";
					hit.transform.GetComponent<RectTransform>().sizeDelta = answerTransforms[0].transform.GetComponent<RectTransform>().sizeDelta / 1.1f;
					//add picked letter to string
					userInput = userInput.Insert(stringIndex++,hit.transform.GetComponentInChildren<Text>().text);
					
				}
					

				if(index >= WordLength){
					
					index = WordLength;
				
				}
			}	
		}
	}
}
