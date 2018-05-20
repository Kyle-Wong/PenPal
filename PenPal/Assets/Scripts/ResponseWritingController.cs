using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResponseWritingController : MonoBehaviour, ILetterController {
    public TextMesh header;
    public TextMeshWrapper textWrapper;
    public TextMesh closing;
    Queue<LetterEvent> letterQueue;
    LetterEvent currentLE;
	public GraphicColorLerp fadeToBlack;
    public GraphicColorLerp fadeToTransparent;

    void Start () {
		Debug.Log(GameManager.playerChoiceHistory);
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
		foreach(LetterEvent e in result) {
			foreach (int j in GameManager.playerChoiceHistory) {
				if (e.relatedToID == j) {
					return e.text;
				}
			}
		}
        return "BAD CHOICE MADE";
	}
	
	public void pressContinueButton() {
		string nextScene = "";
		if (currentLE.goToNext == LetterEvent.Speaker.PLAYER) 
			nextScene = "LetterWritingScene";
		else if (currentLE.goToNext == LetterEvent.Speaker.MADLIBS) 
			nextScene = "MadLibsScene";
		else if (currentLE.goToNext == LetterEvent.Speaker.NARRATIVE)
			nextScene = "CutScene";
		StartCoroutine(loadAfterDelay(nextScene,fadeToBlack.duration));
	}

  	public IEnumerator loadAfterDelay(string sceneName, float delay)
    {
        fadeToBlack.startColorChange();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
	public LetterWritingController.State getState() {
		return LetterWritingController.State.LetterDone;
	}
}
