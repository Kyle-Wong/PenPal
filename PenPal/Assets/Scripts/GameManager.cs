using System.Collections;
using System.Collections.Generic;

public static class GameManager {
	public static float playerPositiveScore;
	public static float playerNegativeScore;
	public static Queue<LetterEvent> playerQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> narrativeQueue = new Queue<LetterEvent>();
	public static Queue<LetterEvent> palQueue = new Queue<LetterEvent>();
}
