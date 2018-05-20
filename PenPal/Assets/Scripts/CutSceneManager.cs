using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {

	// Use this for initialization
    public Image background;
    //public Text cutSceneTitle;
    public Text narrativePrompt;
    public Text narPrompt2;
    public float timer;
    public float duration;
    public int switchPrompt;      
    
    private GraphicColorLerp flavorColorLerp;
    private GraphicColorLerp flavorColorLerp2;
    Queue<LetterEvent> cutSceneQueue;
    LetterEvent curIndex;

    public Text prompt1;
    public Text prompt2;
    private GraphicColorLerp colorLerp1;
    private GraphicColorLerp colorLerp2;
    public float holdDuration;
    public float gapBetweenSentences;
    private float[] timers;
	void Start () {
        cutSceneQueue = new Queue<LetterEvent>(GameManager.narrativeQueue);
        colorLerp1 = prompt1.gameObject.GetComponent<GraphicColorLerp>();
        colorLerp2 = prompt2.gameObject.GetComponent<GraphicColorLerp>();
        timers = new float[] { 0, 0 };
        StartCoroutine(textLoop(prompt1, colorLerp1, 0,timers,0));
        StartCoroutine(textLoop(prompt2, colorLerp2, holdDuration,timers,1));
        //cutSceneTitle.text = "Next Chapter";
        /*
        switchPrompt = 1;
        
        narrativePrompt.text = " ";
        narPrompt2.text = " ";

        //NarLength = " ";
        if (cutSceneQueue.Count > 0)
        {
            curIndex = cutSceneQueue.Dequeue();
        }

        narrativePrompt.text = curIndex.text;
        
        flavorColorLerp = narrativePrompt.GetComponent<GraphicColorLerp>();         
        
        //flavorColorLerp.startColorChange();
        flavorColorLerp.duration = 1;

        flavorColorLerp2 = narPrompt2.GetComponent<GraphicColorLerp>();

        //flavorColorLerp2.startColorChange(); uncomment this to get second text fade in
        flavorColorLerp2.initialDelay = 3;
        flavorColorLerp2.duration = 1;

        if (cutSceneQueue.Count > 0)
        {
            curIndex = cutSceneQueue.Dequeue();
        }

        StartCoroutine(Example());
        */


	}

    //comment this coroutine to get second text fade in
    IEnumerator Example()
    {
        flavorColorLerp.startColorChange();
        yield return new WaitForSeconds(3);
        flavorColorLerp2.startColorChange();
        yield return new WaitForSeconds(3);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        timer += Time.deltaTime;

        //every three seconds stop to dequeue
        
        if (timer >= duration || Input.GetButtonDown("Submit"))
        //use multiple buttons to skip text or wait three seconds
        {
            timer = 0; //reset timer to allow every three duartion
            //cutSceneTitle.text = " ";

            //narrativePrompt.text = "Ready for battle!";
            //cutSceneTitle.text = " ";                  
            if (curIndex.type == LetterEvent.Type.SENTENCE)
            {
                //alternate font object for fade in with even and odd
                if (switchPrompt % 2 != 0) //when index is odd
                {
                    narrativePrompt.text = " ";
                    narPrompt2.text = curIndex.text;                    
                    switchPrompt++;                    
                }
                else //index is even
                {
                    narPrompt2.text = " ";
                    narrativePrompt.text = curIndex.text;                    
                    switchPrompt++;
                }
                //show current index message
            }

            //cutscene is ended at EOL and this scene is ended
            else if (curIndex.type == LetterEvent.Type.EOL) 
            {
                

            }
            //move to next update as next frame occurs
            curIndex = cutSceneQueue.Dequeue();


        }       
        */
	}
    private void loadNextScene()
    {
        GameManager.narrativeQueue = cutSceneQueue;
        switch (curIndex.goToNext)
        {
            case LetterEvent.Speaker.PLAYER:
                SceneManager.LoadScene("LetterWritingScene");
                break;
            case LetterEvent.Speaker.PAL:
                SceneManager.LoadScene("LetterReceivingScene");
                break;
            case LetterEvent.Speaker.MADLIBS:
                SceneManager.LoadScene("MadLibsScene");
                break;
            case LetterEvent.Speaker.END:
                SceneManager.LoadScene("EndGameScene");
                break;
        }
    }
    private IEnumerator textLoop(Text textObject, GraphicColorLerp colorLerp, float initialDelay, float[] timers, int index)
    {
        while (initialDelay > 0)
        {
            if (Input.GetButtonDown("Submit"))
            {
                timers[0] = .1f;
                timers[1] = .1f;
                break;

            }
            initialDelay -= Time.deltaTime;
            yield return null;
        }
        Color start = colorLerp.startColor;
        Color end = colorLerp.endColor;
        while (true)
        {
            curIndex = cutSceneQueue.Dequeue();
            switch (curIndex.type)
            {
                case LetterEvent.Type.SENTENCE:
                    //continue
                    break;
                case LetterEvent.Type.EOL:
                    loadNextScene();    //break out of scene
                    yield break;
            }
            textObject.text = curIndex.text;
            colorLerp.startColor = start;
            colorLerp.endColor = end;
            colorLerp.startColorChange();
            yield return null;
            timers[index] = holdDuration - gapBetweenSentences;
            while(timers[index] > 0)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    timers[0] = .1f;
                    timers[1] = .1f;
                }
                timers[index] -= Time.deltaTime;
                yield return null;
            }
            colorLerp.startColor = end;
            colorLerp.endColor = start;
            colorLerp.startColorChange();
            yield return null;
            timers[index] = holdDuration + gapBetweenSentences;
            while (timers[index] > 0)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    timers[0] = .1f;
                    timers[1] = .1f;
                }
                timers[index] -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
