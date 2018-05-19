using System.Collections;
using System.Collections.Generic;

//Can be called from anywhere- does not need to be on a GameObject.
public static class GameManager {
	//PlayerQueue for writing letters
	public static Queue<LetterEvent> playerQueue = new Queue<LetterEvent>();
	//NarrativeQueue for cutscenes
	public static Queue<LetterEvent> narrativeQueue = new Queue<LetterEvent>();
	//PalQueue for the response letters you get back
	public static Queue<LetterEvent> palQueue = new Queue<LetterEvent>();
}
