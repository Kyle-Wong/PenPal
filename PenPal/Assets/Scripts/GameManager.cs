using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Queue<LetterEvent> playerQueue;
	public static Queue<LetterEvent> narrativeQueue;
	public static Queue<LetterEvent> palQueue;
	void Start () {
		DontDestroyOnLoad(this);
       	playerQueue = new Queue<LetterEvent>();
       	narrativeQueue = new Queue<LetterEvent>();
       	palQueue = new Queue<LetterEvent>();
	}
	
	void Update () {

	}
}
