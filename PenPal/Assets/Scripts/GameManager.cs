using System.Collections;
using System.Collections.Generic;

public static class GameManager {
	public static float totalPlayerPositiveScore;
	public static float totalPlayerNegativeScore;
	public static string playerName = "Default Playername";
	public static float playerPositiveScore;
	public static float playerNegativeScore;
	//When a player makes a choice, add it's entry ID to this list. 
	public static List<int> playerChoiceHistory = new List<int>();
	public static List<List<float>> playerScoreHistory = new List<List<float>>();
	public static Queue<LetterEvent> playerQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> narrativeQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> palQueue = new Queue<LetterEvent>();
	public static Queue<MadlibsEvent> madLibsQueue = new Queue<MadlibsEvent>();
	public static Queue<string> madLibsResults = new Queue<string>();

	public static void storeScores() {
		playerScoreHistory.Add(new List<float>(){playerPositiveScore, playerNegativeScore});
		totalPlayerPositiveScore += playerPositiveScore;
		totalPlayerNegativeScore += playerNegativeScore;
		playerNegativeScore = 0;
		playerPositiveScore = 0;
	}
}
