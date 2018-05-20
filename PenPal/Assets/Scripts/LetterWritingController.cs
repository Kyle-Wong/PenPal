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

    private float charDelay;
    public float regularDelay;
    public float shortDelay;
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
    public GameObject sendButton;
    public GameObject sendText;
    public GraphicColorLerp fadeToBlack;
    public GraphicColorLerp fadeToTransparent;
    public float initialDelayDuration;
    private float initialDelayTimer = 0;
    public bool inFastForward;
    private void Awake()
    {
        headerRevealer.charDelay = charDelay;
        bodyRevealer.charDelay = charDelay;
        closingRevealer.charDelay = charDelay;
        
    }
    void Start () {
        letterQueue = new Queue<LetterEvent>(GameManager.playerQueue);
        currentLE = letterQueue.Dequeue();
        wrappers = new TextMeshWrapper[buttonList.Length];
        for(int i = 0; i < buttonList.Length; i++)
        {
            wrappers[i] = buttonList[i].GetComponent<TextMeshWrapper>();
        }
        sendButton.SetActive(false);
        sendText.SetActive(false);
        if (inFastForward)
        {
            headerRevealer.charDelay = shortDelay;
            bodyRevealer.charDelay = shortDelay;
            closingRevealer.charDelay = shortDelay;
        }
        else
        {
            headerRevealer.charDelay = regularDelay;
            bodyRevealer.charDelay = regularDelay;
            closingRevealer.charDelay = regularDelay;
        }
    }

    // Update is called once per frame
    void Update () {
        if (initialDelayTimer < initialDelayDuration)
        {
            initialDelayTimer += Time.deltaTime;
        }
        else
        {
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
                        if (bodyRevealer.playing())
                            break;
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
            else if (state == State.LetterDone)
            {
                sendButton.SetActive(true);
                sendText.SetActive(true);
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
            for(int i = 0; i < buttonList.Length; i++)
            {
                buttonList[i].text = "";
            }
            choices = null;
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
        GameManager.playerQueue = letterQueue;
        GameManager.storeScores();
        StartCoroutine(loadAfterDelay(cutScene,fadeToBlack.duration));
    }
    public IEnumerator loadAfterDelay(string sceneName, float delay)
    {
        fadeToBlack.startColorChange();
        sendButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void toggleFastForward()
    {
        inFastForward = !inFastForward;
        if (inFastForward)
        {
            headerRevealer.charDelay = shortDelay;
            bodyRevealer.charDelay = shortDelay;
            closingRevealer.charDelay = shortDelay;
        } else
        {
            headerRevealer.charDelay = regularDelay;
            bodyRevealer.charDelay = regularDelay;
            closingRevealer.charDelay = regularDelay;
        }
    }

}
