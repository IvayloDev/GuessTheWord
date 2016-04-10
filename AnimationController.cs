using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

	public Animator coinAnimator,bgAnimator,ribbonAnimator,textAnimator;


	void start() {

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


			AnswerManager.nextLevelActivated = false;

			coinAnimator.SetBool("nextLevel",true);
			bgAnimator.SetBool("nextLevel",true); 
			ribbonAnimator.SetBool("nextLevel",true); 
			textAnimator.SetBool("nextLevel",true); 

			StartCoroutine(StopAnimations());

		
		}

	}

		IEnumerator StopAnimations() {

		

			yield return new WaitForSeconds(1.1f);

		coinAnimator.SetBool("nextLevel",false);
		bgAnimator.SetBool("nextLevel",false); 
		ribbonAnimator.SetBool("nextLevel",false); 
		textAnimator.SetBool("nextLevel",false); 

		coinAnimator.SetBool("removeLevel",true);
		bgAnimator.SetBool("removeLevel",true); 
		ribbonAnimator.SetBool("removeLevel",true); 
		textAnimator.SetBool("removeLevel",true); 


		yield return new WaitForSeconds(1f);

		coinAnimator.SetBool("removeLevel",false);
		bgAnimator.SetBool("removeLevel",false); 
		ribbonAnimator.SetBool("removeLevel",false); 
		textAnimator.SetBool("removeLevel",false); 


	}

}
