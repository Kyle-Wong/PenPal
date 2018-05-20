using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
public class CSVParser : MonoBehaviour {
	public static CSVParser instance;
	// Use this for initialization
	void Awake () {
		if (!instance) {
			instance = this;
			DontDestroyOnLoad(this);
			ReadCSV();	
		} else {
			GameObject.Destroy(this.gameObject);
		}	
	}
	private void ReadCSV() {
		StreamReader file = new System.IO.StreamReader("Assets/Resources/Narrative/script.tsv");	
		string buf;
		file.ReadLine(); //eat the header 
		for (int i = 2; (buf= file.ReadLine()) != null; ++i) {
			string[] entry = buf.Trim().Split('\t');
			Debug.Log("Loading queues at tsv line " + i.ToString());
			CreateLetterEvent(entry, i);
		}
		//maybe run a unit test on sizes of queue vs. sizes expected lol
		Debug.Log("Loaded all queues.");
		//Now, we'll load the madlibs...
		file = new System.IO.StreamReader("Assets/Resources/Narrative/madlibs.tsv");	
		file.ReadLine(); //eat the header 
		for (int i = 2; (buf= file.ReadLine()) != null; ++i) {
			string[] entry = buf.Trim().Split('\t');
			Debug.Log("Loading madlibs at tsv line " + i.ToString());
			CreateMadlibsEvent(entry, i);
		}
		//maybe run a unit test on sizes of queue vs. sizes expected lol
		Debug.Log("Loaded all madlibs.");
	}

	private void CreateLetterEvent(string[] entry, int i) {
		LetterEvent e = new LetterEvent();
		e.eventID = Convert.ToInt32(entry[0]);
		if (entry[1].Equals("player")) {
			e.speaker = LetterEvent.Speaker.PLAYER;
			e.group = Convert.ToInt32(entry[2]);
			e.type = ResolveLetterType(entry[3], i);
			e.text = entry[4];
			e.prompt = entry[5];
			if (!entry[6].Equals(""))
				e.positive = Convert.ToInt32(entry[6]);
			if (!entry[7].Equals(""))
				e.negative = Convert.ToInt32(entry[7]);
			if (!entry[8].Equals(""))
				e.relatedToID = Convert.ToInt32(entry[8]);
			e.goToNext = ResolveSpeakerType(entry[9]);
			GameManager.playerQueue.Enqueue(e);
		} else if (entry[1].Equals("narrative")) {
			e.speaker = LetterEvent.Speaker.NARRATIVE;
			e.group = Convert.ToInt32(entry[2]);
			e.type = ResolveLetterType(entry[3], i);
			e.text = entry[4];
			e.prompt = entry[5];
			if (!entry[6].Equals(""))
				e.positive = Convert.ToInt32(entry[7]);
			if (!entry[7].Equals(""))
				e.negative = Convert.ToInt32(entry[7]);
			if (!entry[8].Equals(""))
				e.relatedToID = Convert.ToInt32(entry[8]);
			e.goToNext = ResolveSpeakerType(entry[9]);
			GameManager.narrativeQueue.Enqueue(e);
		} else if (entry[1].Equals("pal")) {
			e.speaker = LetterEvent.Speaker.PAL;
			e.group = Convert.ToInt32(entry[2]);
			e.type = ResolveLetterType(entry[3], i);
			e.text = entry[4];
			e.prompt = entry[5];
			if (!entry[6].Equals(""))
				e.positive = Convert.ToInt32(entry[6]);
			if (!entry[7].Equals(""))
				e.negative = Convert.ToInt32(entry[7]);
			if (!entry[8].Equals(""))
				e.relatedToID = Convert.ToInt32(entry[8]);
			e.goToNext = ResolveSpeakerType(entry[9]);
			GameManager.palQueue.Enqueue(e);
		} else {
			Debug.Log("Error: cannot resolve speaker for entry " + entry[1] + i.ToString());
		}
	}
	private LetterEvent.Type ResolveLetterType(string type, int i) {
		if (type.Equals("greeting")) {
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

	private LetterEvent.Speaker ResolveSpeakerType(string type) {
		if (type.Equals("player")) {
			return LetterEvent.Speaker.PLAYER;
		} else if (type.Equals("narrative")) {
			return LetterEvent.Speaker.NARRATIVE;
		} else if (type.Equals("pal")) {
			return LetterEvent.Speaker.PAL;
		} else if (type.Equals("madlibs")) {
			return LetterEvent.Speaker.MADLIBS;
		} else {
			return LetterEvent.Speaker.NONE;
		}
	}

	private void CreateMadlibsEvent(string[] entry, int i) {
		MadlibsEvent e = new MadlibsEvent();
		e.groupID = Convert.ToInt32(entry[0]);
		e.text = entry[1];
		e.prompt = entry[2];
		GameManager.madLibsQueue.Enqueue(e);
	}
}
