using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public class LetterEvent {
	public enum Speaker {
		PLAYER,
		PAL,
		NARRATIVE,
		MADLIBS,
		END,
		NONE
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

	//EventID for this event.
	public int eventID;
	//Speaker of the message, mostly for queue bookkeeping. See above enum.
	public Speaker speaker;
	//Group of the message. Messages in the same group are part of the same scenario.
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
	//Sometimes, the pal's response will have a "related to ID" to indiciate to only write
	//with this choice if the player chose to respond with the event ID indicated. So lifelike!
	public int relatedToID = -1;
	//Which speaker type to go to
	public Speaker goToNext;
}
