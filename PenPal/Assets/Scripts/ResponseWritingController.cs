using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseWritingController : MonoBehaviour {
    public TextMesh header;
    public TextMeshWrapper textWrapper;
    public TextMesh closing;
    Queue<LetterEvent> letterQueue;
    LetterEvent currentLE;

    void Start () {
        letterQueue = GameManager.palQueue;
        currentLE = letterQueue.Dequeue();
		textWrapper.addAndWrapText(buildLetter());
    }

    // Update is called once per frame
    void Update () {
    
	}
    public string buildHeader()
    {
        string result = "";
        while(letterQueue.Count > 0 && currentLE.type == LetterEvent.Type.INTRO)
        {
            result += currentLE.text;
			result = result.Replace("[player]", GameManager.playerName);
            currentLE = letterQueue.Dequeue();
        }
        return result;
    }
    public string buildClosing()
    {
        string result = "";
        while(letterQueue.Count > 0 && currentLE.type == LetterEvent.Type.CLOSING)
        {
            result += currentLE.text;
            currentLE = letterQueue.Dequeue();
        }
        return result;
    }
    public string buildLetter()
    {
        string result = "";
        if (letterQueue.Count == 0)
            return "";
        while(letterQueue.Count > 0)
        {
            switch (currentLE.type)
            {
				case LetterEvent.Type.INTRO:
					header.text = buildHeader();
					break;
                case LetterEvent.Type.SENTENCE:
                    result += currentLE.text;
                    break;
				case LetterEvent.Type.CLOSING:
					closing.text = buildClosing();
					break;
                case LetterEvent.Type.CHOICE:
                    result += resolveChoice();
                    break;
                case LetterEvent.Type.EOP:
                    result += "\n";
                    break;
                case LetterEvent.Type.EOL:
                default:
                    break;
            }
            currentLE = letterQueue.Dequeue();
        }
		result = result.Replace("[player]", GameManager.playerName);
        return result;
    }
    private string resolveChoice()
    {
        List<LetterEvent> result = new List<LetterEvent>();
        if (currentLE.type != LetterEvent.Type.CHOICE)
            return null;
        while(currentLE.type == LetterEvent.Type.CHOICE)
        {
            result.Add(currentLE);
            currentLE = letterQueue.Dequeue();
        }
		//TODO: REMOVE HARDCODED 8 WHEN PLAYER IS MAKING CHOICES
		GameManager.playerChoiceHistory.Add(8);
		foreach(LetterEvent e in result) {
			foreach (int j in GameManager.playerChoiceHistory) {
				if (e.relatedToID == j) {
					return e.text;
				}
			}
		}
        return "BAD CHOICE MADE";
	}
}
