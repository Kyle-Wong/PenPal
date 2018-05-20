using System.Collections;
using System.Collections.Generic;

public static class GameManager {
	public static string playerName = "Default Playername";
	public static float playerPositiveScore;
	public static float playerNegativeScore;
	//When a player makes a choice, add it's entry ID to this list. 
	public static List<int> playerChoiceHistory = new List<int>();
	public static Queue<LetterEvent> playerQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> narrativeQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> palQueue = new Queue<LetterEvent>();
}
