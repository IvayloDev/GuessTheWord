using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

	public static bool newGameBool = false;
	public static bool resumeGameBool = false;
	public GameObject ContinueButton,newGameButton,NewGamePanel;
	private int indexForFirstPlay = 0;
	public Text LevelText;


	void Update() {

		//Set text for current level
		LevelText.text = "" + AnswerManager.levelIndex;
	}

	void OnDestroy() {

		//Save int for if this is the players first game
		PlayerPrefs.SetInt("indexForFirstPlay", indexForFirstPlay);
		PlayerPrefs.Save();

	}


	void Start(){

		//Get values
		AnswerManager.levelIndex = PlayerPrefs.GetInt("LevelIndex");
		indexForFirstPlay = PlayerPrefs.GetInt("indexForFirstPlay",0);
		
		//Set to false by default
		NewGamePanel.SetActive(false);

		//If this is first game make button bigger and don't enable continue button
		if(indexForFirstPlay == 0){
			ContinueButton.SetActive(false);
			newGameButton.GetComponent<RectTransform>().sizeDelta = new Vector2(470,155);
		}else{

			//If this is NOT first play, enable continue and make new game button normal size
			ContinueButton.SetActive(true);
			newGameButton.GetComponent<RectTransform>().sizeDelta = new Vector2(360,118);
			
		}

	}


	public void OnNewGameClick(){
		if(indexForFirstPlay == 0) {
			//Set to be 1 so we know this is now not a first game
			indexForFirstPlay = 1;
			//Set to true so in answerManager we can shuffle new words
			newGameBool = true;
			//Set level index to 1
			AnswerManager.levelIndex = 1;
			//Load game
			Application.LoadLevel("Game");
		}else{
			//If this is NOT first game set newGamePanel to true so player can choose if he wants to begin new level or not
		NewGamePanel.SetActive(true);
		}
	}

	public void OnYesClick() {
		//Deactivate panel
		NewGamePanel.SetActive(false);
		//Set index to 1(just in case)
		indexForFirstPlay = 1;
		//Shuffle new words
		newGameBool = true;
		//Set level to 1
		AnswerManager.levelIndex = 1;
		//Load Game
		Application.LoadLevel("Game");
	}
	
	public void OnNoClick() {
		//Deactivate panel if player clicks "No"
		NewGamePanel.SetActive(false);
	}

	public void OnResumeClick(){
		resumeGameBool = true;
		Application.LoadLevel("Game");
	}


	
}
