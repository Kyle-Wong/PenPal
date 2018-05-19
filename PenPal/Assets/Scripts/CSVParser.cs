using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
public class CSVParser : MonoBehaviour {
	// Use this for initialization
	void Start () {
		ReadCSV();		
	}
	private void ReadCSV() {
		StreamReader file = new System.IO.StreamReader("Assets/Resources/Narrative/script.csv");	
		string buf;
		file.ReadLine(); //eat the header 
		for (int i = 0; (buf= file.ReadLine()) != null; ++i) {
			string[] entry = buf.Trim().Split(',');
			CreateLetterEvent(entry, i);
		}
		//maybe run a unit test on sizes of queue vs. sizes expected lol
		Debug.Log("Loaded all queues...");
		while (GameManager.playerQueue.Count != 0) {
			LetterEvent e = GameManager.playerQueue.Dequeue();
		}
	}
	
	private void CreateLetterEvent(string[] entry, int i) {
		LetterEvent e = new LetterEvent();
		if (entry[0].Equals("player")) {
			e.speaker = LetterEvent.Speaker.PLAYER;
			e.group = Convert.ToInt32(entry[1]);
			e.type = ResolveLetterType(entry[2], i);
			e.text = entry[3];
			GameManager.playerQueue.Enqueue(e);
		} else if (entry[0].Equals("narrative")) {
			e.speaker = LetterEvent.Speaker.NARRATIVE;
			e.group = Convert.ToInt32(entry[1]);
			e.type = ResolveLetterType(entry[2], i);
			e.text = entry[3];
			GameManager.narrativeQueue.Enqueue(e);
		} else if (entry[0].Equals("pal")) {
			e.speaker = LetterEvent.Speaker.PAL;
			e.group = Convert.ToInt32(entry[1]);
			e.type = ResolveLetterType(entry[2], i);
			e.text = entry[3];
		} else {
			Debug.Log("Error: cannot resolve speaker for entry " + i.ToString());
		}
	}
	private LetterEvent.Type ResolveLetterType(string type, int i) {
		if (type.Equals("intro")) {
			return LetterEvent.Type.INTRO;
		} else if (type.Equals("sentence")) {
			return LetterEvent.Type.SENTENCE;
		} else if (type.Equals("choice")) {
			return LetterEvent.Type.CHOICE;
		} else if (type.Equals("closing")) {
			return LetterEvent.Type.CLOSING;
		} else if (type.Equals("EOP")) {
			return LetterEvent.Type.EOP;
		} else if (type.Equals("EOL")) {
			return LetterEvent.Type.EOL;
		} else {
			Debug.Log("Error: could not resolve type for entry " + i.ToString());
			return LetterEvent.Type.ERROR;
		}
	}
}
