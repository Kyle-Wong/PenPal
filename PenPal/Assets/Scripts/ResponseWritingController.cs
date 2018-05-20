using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResponseWritingController : MonoBehaviour, ILetterController {
    public TextMesh header;
    public TextMeshWrapper textWrapper;
    public TextMesh closing;
    Queue<LetterEvent> letterQueue;
    LetterEvent currentLE;
	public GraphicColorLerp fadeToBlack;
    public GraphicColorLerp fadeToTransparent;
    public Button continueButton;
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
        return currentLE.text.Replace("[player]", GameManager.playerName);
    }
    public string buildClosing()
    {
		//Todo: parse out penpal name and replace
        string result = "";
        result += currentLE.text;
        return result;
    }
    public string buildLetter()
    {
        string result = "";
        if (letterQueue.Count == 0)
            return "";
        while(currentLE.type != LetterEvent.Type.EOL)
        {
            switch (currentLE.type)
            {
				case LetterEvent.Type.INTRO:
					header.text = buildHeader();
					break;
                case LetterEvent.Type.SENTENCE:
                    result += currentLE.text.Replace("[player]", GameManager.playerName);
                    break;
				case LetterEvent.Type.CLOSING:
					result += checkForMadLibs();
					closing.text = buildClosing();
					break;
                case LetterEvent.Type.CHOICE:
                    result += resolveChoice().Replace("[player]", GameManager.playerName);
                    break;
                case LetterEvent.Type.EOP:
                    result += "\n\n ";
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
        List<LetterEvent> result = new List<LetterEvent>(){currentLE};
        while(letterQueue.Peek().type == LetterEvent.Type.CHOICE)
        {
            currentLE = letterQueue.Dequeue();
			result.Add(currentLE);
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

	private string checkForMadLibs() {
		string ret = "";
		if (GameManager.madLibsResults.Count > 0) {
			ret += "\n\nP.S. Here's the madlibs results:\n";
			ret += GameManager.madLibsResults.Dequeue();
		}
		return ret;
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
        continueButton.interactable = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
	public LetterWritingController.State getState() {
		return LetterWritingController.State.LetterDone;
	}
}
