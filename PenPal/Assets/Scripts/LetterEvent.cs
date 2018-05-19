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

	public Speaker speaker;
	public int group;
	public Type type;
	public string text;
}
