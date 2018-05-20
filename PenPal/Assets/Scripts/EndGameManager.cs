using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {
	public TextMesh[] fillIns;
	public GameObject posHolder;
	public GameObject negHolder;
	public Button continueButton;
	public GraphicColorLerp fadeToBlack;



	// Use this for initialization
	void Start () {
		InitializeFillIns();
		InitializeBarSummary();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeBarSummary() {
		float sum = GameManager.totalPlayerPositiveScore + GameManager.totalPlayerNegativeScore;
		float normalPos = GameManager.totalPlayerPositiveScore / sum;
		posHolder.transform.localScale = new Vector3(normalPos, 1, 1);
	}

	void InitializeFillIns() {
		for (int i = 0; i < fillIns.Length; ++i) {
			List<float> currentHistory = GameManager.playerScoreHistory[i];
			if (currentHistory[0] > currentHistory[1])
				fillIns[i].text = GetNextPositive(i); 
			else if (currentHistory[0] < currentHistory[1]) 
				fillIns[i].text = GetNextNegative(i);
			else 
				fillIns[i].text = GetNextNeutral(i);
		}	
	}
	string GetNextPositive(int i) {
		switch(i) {
			case 0:
				return "You were friendly off the bat.";
			case 1:
				return "You were interested in what they had to say.";
			case 2:
				return "You supported their successes.";
			case 3:
				return "You showed that you trusted them.";
			case 4:
				return "You chose friendship over yourself.";
			case 5: 
				return "You validated their successes instead of your own.";
			case 6:
				return "You forgave and empathized with their loss.";
			case 7:
				return "You reached back out to them as a best friend.";
			default:
				return "";
		}
	}
	string GetNextNegative(int i) {
		switch(i) {
			case 0:
				return "You were reluctant off the bat.";
			case 1:
				return "You talked about yourself.";
			case 2:
				return "You wanted them to share their success with you.";
			case 3:
				return "You talked about your own plans.";
			case 4:
				return "You weren't inspired to perservere.";
			case 5: 
				return "You talked about the great things in your life.";
			case 6:
				return "You ignored them when they opened up to you.";
			case 7:
				return "Your last words were bitter and unforgiving.";
			default:
				return "";
		}
	}
	string GetNextNeutral(int i) {
		switch(i) {
			case 0:
				return "You were reserved off the bat";
			case 1:
				return "You acted uninterested in their life.";
			case 2:
				return "You compared yourself to them.";
			case 3:
				return "You relied on yourself instead of their help.";
			case 4:
				return "You didn't want to admit what they meant to you.";
			case 5: 
				return "You were reserved in sharing your own successes.";
			case 6:
				return "You were shy to fully empahize with their loss.";
			case 7:
				return "You reached back out to them as a pen pal.";
			default:
				return "";
		}
	}
	public void pressContinueForPostgame() {
		StartCoroutine(loadAfterDelay("PostGameScene",fadeToBlack.duration));
	}

	private IEnumerator loadAfterDelay(string nextScene, float delay) {
		fadeToBlack.startColorChange();
        continueButton.interactable = false;
        yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(nextScene);
	}
}
