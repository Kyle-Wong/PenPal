using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterWritingController : MonoBehaviour, ILetterController {

    // Use this for initialization
    public enum State
    {
        LetterWriting,
        LetterDone,
    }
    private State state = State.LetterWriting;
    public TextMesh[] buttonList;
    private TextMeshWrapper[] wrappers;
    public SequentialText headerRevealer;
    public SequentialText bodyRevealer;
    public SequentialText closingRevealer;

    public float charDelay;
    public TextMesh header;
    public TextMesh body;
    public TextMesh closing;
    Queue<LetterEvent> letterQueue;
    LetterEvent currentLE;
    LetterEvent[] choices;
    LetterEvent chosenLE;
    private int lineCount;
    private bool waitingForInput;
    public string cutScene;
    private void Awake()
    {
        headerRevealer.charDelay = charDelay;
        bodyRevealer.charDelay = charDelay;
        closingRevealer.charDelay = charDelay;
        
    }
    void Start () {
        letterQueue = GameManager.playerQueue;
        currentLE = letterQueue.Dequeue();
        wrappers = new TextMeshWrapper[buttonList.Length];
        for(int i = 0; i < buttonList.Length; i++)
        {
            wrappers[i] = buttonList[i].GetComponent<TextMeshWrapper>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (state == State.LetterWriting)
        {
            switch (currentLE.type)
            {
                case (LetterEvent.Type.INTRO):
                    headerRevealer.begin();
                    headerRevealer.setText(buildHeader());
                    break;
                case (LetterEvent.Type.SENTENCE):
                    if (headerRevealer.playing() || waitingForInput)
                        break;
                    bodyRevealer.begin();
                    string paragraph = buildParagraph();
                    bodyRevealer.addText(paragraph);
                    break;
                case (LetterEvent.Type.CLOSING):
                    if (bodyRevealer.playing() || waitingForInput)
                        break;
                    closingRevealer.begin();
                    closingRevealer.setText(buildClosing());
                    break;
                case (LetterEvent.Type.CHOICE):
                    if (choices == null)
                    {
                        choices = getChoices();
                    }
                    break;
                case LetterEvent.Type.EOP:
                    headerRevealer.allText += '\n';
                    currentLE = letterQueue.Dequeue();
                    break;
                case LetterEvent.Type.EOL:
                    state = State.LetterDone;
                    break;
                case LetterEvent.Type.ERROR:
                    Debug.Log("ERROR");
                    break;
            }
        }
	}
    public string buildHeader()
    {
        string result = "";
        while(letterQueue.Count > 0 && currentLE.type == LetterEvent.Type.INTRO)
        {
            result += currentLE.text;
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
    public string buildParagraph()
    {
        string result = "";
        if (letterQueue.Count == 0)
            return "";
        while(letterQueue.Count > 0)
        {
            bool breakLoop = false;
            switch (currentLE.type)
            {
                case LetterEvent.Type.SENTENCE:
                    result += currentLE.text;
                    break;
                case LetterEvent.Type.CHOICE:
                    breakLoop = true;
                    break;
                case LetterEvent.Type.EOP:
                    result += "\n";
                    breakLoop = true;
                    break;
                case LetterEvent.Type.EOL:
                    breakLoop = true;
                    break;
                default:
                    breakLoop = true;
                    break;
            }
            if (breakLoop)
                break;
            currentLE = letterQueue.Dequeue();
        }
        return result;
    }
    private LetterEvent[] getChoices()
    {
        List<LetterEvent> result = new List<LetterEvent>();
        if (currentLE.type != LetterEvent.Type.CHOICE)
            return null;
        while(currentLE.type == LetterEvent.Type.CHOICE)
        {
            result.Add(currentLE);
            currentLE = letterQueue.Dequeue();
        }
        
        for(int i = 0; i < result.Count; i++)
        {
            buttonList[i].text = wrappers[i].getWrappedString(result[i].text);
        }
        waitingForInput = true;
        return result.ToArray();

    }
    public void selectChoice(int buttonNum)
    {
        if (!waitingForInput)
            return;
        if(buttonNum < choices.Length)
        {
            chosenLE = choices[buttonNum];
            waitingForInput = false;
            bodyRevealer.setPlaying(true);
            bodyRevealer.addText(" " + chosenLE.text);
            GameManager.playerPositiveScore += chosenLE.positive;
            GameManager.playerNegativeScore += chosenLE.negative;
            GameManager.playerChoiceHistory.Add(chosenLE.eventID);
        }

    }
    public State getState()
    {
        return state;
    }
    public void goToCutscene()
    {
        SceneManager.LoadScene(cutScene);
    }
}
