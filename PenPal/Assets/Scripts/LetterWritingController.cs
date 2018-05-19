using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterWritingController : MonoBehaviour {

    // Use this for initialization
    
    private int state = 0;
    public SequentialText textRevealer;
    public float charDelay;
    public TextMesh header;
    public TextMesh body;
    public TextMesh closing;
    Queue<LetterEvent> letterQueue;
    LetterEvent currentLE;
    LetterEvent[] choices;
    private int lineCount;

    private void Awake()
    {
        textRevealer.charDelay = charDelay;
    }
    void Start () {
        letterQueue = GameManager.playerQueue;
        currentLE = letterQueue.Dequeue();
    }

    // Update is called once per frame
    void Update () {
        switch (currentLE.type)
        {
            case (LetterEvent.Type.INTRO):
                header.text = buildHeader();
                break;
            case (LetterEvent.Type.SENTENCE):
                string paragraph = buildParagraph();
                Debug.Log(paragraph);
                textRevealer.addText(paragraph);
                break;
            case (LetterEvent.Type.CLOSING):
                closing.text = buildClosing();
                break;
            case (LetterEvent.Type.CHOICE):
                if(choices == null)
                {
                    choices = getChoices();
                }
                break;
            case LetterEvent.Type.EOP:
                textRevealer.allText += '\n';
                currentLE = letterQueue.Dequeue();
                break;
            case LetterEvent.Type.EOL:
                //do nothing
                break;
            case LetterEvent.Type.ERROR:
                Debug.Log("ERROR");
                break;
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
        return result.ToArray();
    }
    public void selectChoice(int buttonNum)
    {


    }
}
