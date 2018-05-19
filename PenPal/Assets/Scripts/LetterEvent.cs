using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public class LetterEvent {
	public enum Speaker {
		PLAYER,
		PAL,
		NARRATIVE
	}
	public enum Type {
		INTRO,
		SENTENCE,
		CHOICE, 
		CLOSING,
		EOP,
		EOL, 
		ERROR
	}

	//Speaker of the message, mostly for queue bookkeeping. See above enum.
	public Speaker speaker;
	public int group;
	//Type of the message. Use these to know what to do in code. See above enum.
	public Type type;
	//The text of the message
	public string text;
	//Abbrevited prompt for CHOICE types
	public string prompt;
	//For the player, how many "positive points" this choice will add to your positive score.
	public int positive = 0;
	//For the player, how many "negative points" this choice will add to your negative score.
	public int negative = 0;
}
