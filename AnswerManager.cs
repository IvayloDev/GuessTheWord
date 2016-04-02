using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnswerManager : MonoBehaviour {


	public TextAsset WordsXMLFile;
	private AnswerData answerData;
	private static Word currentWord;
	private static int r,i,int2,h,WordLength;
	private Word tmp;
	private char RandomLetter;
	private string BGAlphabet ="АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЮЯ";
	private string userInput = "";

	public GameObject[] PossibleLetters;
	public GameObject[] guessBoxGO;
	public Text[] PossibleTexts;
	public Image currentImage;
	public RectTransform[] answerTransforms;
	public RectTransform[] guessBoxTransforms;	

	private int index,stringIndex;

	private Vector3 currentPos; 

	private Vector3 initPos0,initPos1,initPos2,initPos3,initPos4,initPos5,initPos6,initPos7,initPos8,initPos9,initPos10,initPos11,initPos12,initPos13;

	
	void OnDestroy(){
		PlayerPrefs.SetInt("R",r);
	}

	public void NextLevel(){

		index = 0;
		stringIndex = 0;
		userInput = "";

		foreach(GameObject GO in guessBoxGO){
			GO.tag = "Untagged";
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
		guessBoxTransforms[13].transform.position = initPos13;
		//Debug.Log(r);
		Debug.LogError(currentWord.wordText);
	}

	void Start () {

		r = PlayerPrefs.GetInt("R",0);


		answerData = AnswerData.LoadFromText(WordsXMLFile.text);

		if(r >= answerData.words.Count){
				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    //	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
		}
			}

		currentWord = answerData.words[r++];


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
		initPos13 = guessBoxTransforms[13].transform.position;
		
		

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

	public void LettersToChooseFrom (string alphabet){

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


		foreach(char charaaa in currentWord.wordText){

				PossibleTexts[h++].text = "" + charaaa;

			}

	} 


	void Update() {

		Debug.Log(r);


		if(index == WordLength){

			if(userInput == currentWord.wordText){
				NextLevel();
				return;
			}
		}

		if( Input.GetMouseButtonDown(0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			
			if(Physics.Raycast(ray,out hit,100)) {

				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1"){
					hit.transform.position = initPos0;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (1)"){
					hit.transform.position = initPos1;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (2)"){
					hit.transform.position = initPos2;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (3)"){
					hit.transform.position = initPos3;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (4)"){
					hit.transform.position = initPos4;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (5)"){
					hit.transform.position = initPos5;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (6)"){
					hit.transform.position = initPos6;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (7)"){
					hit.transform.position = initPos7;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (8)"){
					hit.transform.position = initPos8;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (9)"){
					hit.transform.position = initPos9;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (10)"){
					hit.transform.position = initPos10;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (11)"){
					hit.transform.position = initPos11;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (12)"){
					hit.transform.position = initPos12;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}
				if(hit.transform.tag == "isTop" && hit.transform.name == "BlankSpace1 (13)"){
					hit.transform.position = initPos13;
					hit.transform.tag = "Untagged";
					index--;
					userInput = userInput.Replace(hit.transform.GetComponentInChildren<Text>().text, string.Empty);
					stringIndex--;
					return;
				}

			

				if(index != WordLength && hit.transform.tag != "isTop"){
					hit.transform.position = answerTransforms[index++].transform.position;
					hit.transform.tag = "isTop";

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
