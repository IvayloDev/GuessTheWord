using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

	public Animator coinAnimator,bgAnimator,ribbonAnimator,textAnimator;


	void start() {

		//Set all animations to default(false)

		coinAnimator.SetBool("nextLevel",false);
		bgAnimator.SetBool("nextLevel",false); 
		ribbonAnimator.SetBool("nextLevel",false); 
		textAnimator.SetBool("nextLevel",false); 

		coinAnimator.SetBool("removeLevel",false);
		bgAnimator.SetBool("removeLevel",false); 
		ribbonAnimator.SetBool("removeLevel",false); 
		textAnimator.SetBool("removeLevel",false); 
	}


	void Update () {


		if(AnswerManager.nextLevelActivated){

			//Deactivate bool and set animations for ribbon and coins
			AnswerManager.nextLevelActivated = false;

			coinAnimator.SetBool("nextLevel",true);
			bgAnimator.SetBool("nextLevel",true); 
			ribbonAnimator.SetBool("nextLevel",true); 
			textAnimator.SetBool("nextLevel",true); 

			//Start coroutine for stopping animations
			StartCoroutine(StopAnimations());

		
		}

	}

		IEnumerator StopAnimations() {

		
		//Wait 1.1 so the player can see the next level panel
			yield return new WaitForSeconds(1.1f);


		//Reset to default
		coinAnimator.SetBool("nextLevel",false);
		bgAnimator.SetBool("nextLevel",false); 
		ribbonAnimator.SetBool("nextLevel",false); 
		textAnimator.SetBool("nextLevel",false); 

		//Panel fadeOut animations
		coinAnimator.SetBool("removeLevel",true);
		bgAnimator.SetBool("removeLevel",true); 
		ribbonAnimator.SetBool("removeLevel",true); 
		textAnimator.SetBool("removeLevel",true); 


		yield return new WaitForSeconds(1f);


		//After all the animations are done, set bools to false so they are ready for next level activation
		coinAnimator.SetBool("removeLevel",false);
		bgAnimator.SetBool("removeLevel",false); 
		ribbonAnimator.SetBool("removeLevel",false); 
		textAnimator.SetBool("removeLevel",false); 


	}

}
