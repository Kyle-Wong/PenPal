using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour {
	public TextMesh[] fillIns;

	// Use this for initialization
	void Start () {
		InitializeFillIns();
	}
	
	// Update is called once per frame
	void Update () {
		
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
				break;
			case 1:
				return "You were interested in what they had to say.";
				break;
			case 2:
				return "You supported their successes.";
				break;
			case 3:
				return "You showed that you trusted them.";
				break;
			case 4:
				return "You chose friendship over yourself.";
				break;
			case 5: 
				return "You acknowledged their successes in light of your own.";
				break;
			case 6:
				return "You forgave and empathized with their loss.";
				break;
			case 7:
				return "You reached back out to them as a friend.";
				break;
			default:
				return "";
		}
	}
	string GetNextNegative(int i) {
		switch(i) {
			case 0:
				return "You were reluctant off the bat.";
				break;
			case 1:
				return "You talked about yourself.";
				break;
			case 2:
				return "You wanted them to share their success with you";
				break;
			case 3:
				return "You talked about your own plans.";
				break;
			case 4:
				return "You weren't inspired to perservere.";
				break;
			case 5: 
				return "You chose to talk about the great things in your life.";
				break;
			case 6:
				return "You were not there for them when they opened up to you.";
				break;
			case 7:
				return "Your last words to them were bitter and unforgiving.";
				break;
			default:
				return "";
		}
	}
	string GetNextNeutral(int i) {
		switch(i) {
			case 0:
				return "You were reserved off the bat";
				break;
			case 1:
				return "You acted uninterested in their life.";
				break;
			case 2:
				return "You compared yourself to them.";
				break;
			case 3:
				return "You relied on yourself instead of their help.";
				break;
			case 4:
				return "You didn't want to admit what they meant to you.";
				break;
			case 5: 
				return "You were reserved in sharing your own successes.";
				break;
			case 6:
				return "You were shy to fully empahize with their loss.";
				break;
			case 7:
				return "You reached back out to them as a pen pal.";
				break;
			default:
				return "";
		}
	}
}
