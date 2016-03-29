using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnswerManager : MonoBehaviour {


	public TextAsset WordsXMLFile;
	private AnswerData answerData;
	private static Word currentWord;
	private int r,i,int2,h,WordLength;
	private Word tmp;
	private char RandomLetter;
	private string BGAlphabet ="абвгдежзийклмнопрстуфхцчшщъьюя";
	public GameObject[] PossibleLetters;
	public Text[] PossibleTexts;

	public RectTransform[] answerTransforms;

	private int index;

	public void OnClick(){

		//TODO ////////////////////////////currentWord.wordText = GetComponent<SpriteRenderer>.tag ...
	
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
					h++;
		currentWord = answerData.words[r++];
		
		Debug.LogError(currentWord.wordText);
	}

	void Start () {

		answerData = AnswerData.LoadFromText(WordsXMLFile.text);


				for (i = answerData.words.Count - 1; i > 0; i--) {  			// get the count of the array an shuffle all the elements   begin from end to start of the array 
					r = Random.Range(0,i);		    //	get a random number from 0 to array count
					tmp = answerData.words[i];							 				    //	swap the random place (eg: 3) and assign it to tmp 
					answerData.words[i] = answerData.words[r]; 		    // swap the i(current number) with tmp
					answerData.words[r] = tmp;                          				    // swap the tmp with the value of i
		}

		currentWord = answerData.words[r++]; 

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



		//transformIndex[0] = answerTransforms[0];

		//answerTransforms[0] = 1;

		//Debug.Log(answerTransforms.GetValue(1));

//		transformIndex[0] = (int)answerTransforms.GetValue(0);
//		transformIndex[1] = (int)answerTransforms.GetValue(1);
//		transformIndex[2] = (int)answerTransforms.GetValue(2);
//		transformIndex[3] = (int)answerTransforms.GetValue(3);
//		transformIndex[4] = (int)answerTransforms.GetValue(4);
//		transformIndex[5] = (int)answerTransforms.GetValue(5);
//		transformIndex[6] = (int)answerTransforms.GetValue(6);
//		transformIndex[7] = (int)answerTransforms.GetValue(7);
//		transformIndex[8] = (int)answerTransforms.GetValue(8);
//		transformIndex[9] = (int)answerTransforms.GetValue(9);
//		transformIndex[10] = (int)answerTransforms.GetValue(10);
//		transformIndex[11] = (int)answerTransforms.GetValue(11);
//		
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


	public void OnButtonClick(){

		//var currentTransform = 
			answerTransforms[index++].gameObject.SetActive(false);
		//answerTransforms[transformIndex++];

//		Vector3 pos = this.transform.position;
//		//this.gameObject.transform.position = answerTransforms[index++].transform.position;
//		this.transform.position = pos;

		//Debug.Log(currentTransform);
	}

	
	void Update () {

		//transformIndex = answerTransforms[hah];

	}
}
