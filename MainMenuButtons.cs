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
		LevelText.text = "" + AnswerManager.levelIndex;
		Debug.Log(AnswerManager.levelIndex);
	}

	void OnDestroy() {
		PlayerPrefs.SetInt("indexForFirstPlay", indexForFirstPlay);
		PlayerPrefs.Save();

	}


	void Start(){
		AnswerManager.levelIndex = PlayerPrefs.GetInt("LevelIndex");
		indexForFirstPlay = PlayerPrefs.GetInt("indexForFirstPlay",0);
		
		NewGamePanel.SetActive(false);

		
		if(indexForFirstPlay == 0){
			ContinueButton.SetActive(false);
			newGameButton.GetComponent<RectTransform>().sizeDelta = new Vector2(470,155);
		}else{
			ContinueButton.SetActive(true);
			newGameButton.GetComponent<RectTransform>().sizeDelta = new Vector2(360,118);
			
		}

	}

	public void OnNewGameClick(){
		if(indexForFirstPlay == 0) {
			indexForFirstPlay = 1;
			newGameBool = true;
			AnswerManager.levelIndex = 1;
			Application.LoadLevel("Game");
		}else{
		NewGamePanel.SetActive(true);
		}
	}

	public void OnYesClick() {
		NewGamePanel.SetActive(false);
		indexForFirstPlay = 1;
		newGameBool = true;
		AnswerManager.levelIndex = 1;
		Application.LoadLevel("Game");
	}
	
	public void OnNoClick() {
		NewGamePanel.SetActive(false);
	}

	public void OnResumeClick(){
		resumeGameBool = true;
		Application.LoadLevel("Game");
	}


	
}
