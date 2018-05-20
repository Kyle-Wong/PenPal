using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PostGameManager : MonoBehaviour {

	// Use this for initialization
    public Image background;
    //public Text cutSceneTitle;
    public float timer;
    public float duration;
    public int switchPrompt;      
    
    private GraphicColorLerp flavorColorLerp;
    private GraphicColorLerp flavorColorLerp2;
    List<string> cutSceneQueue;

    public Text prompt1;
    public Text prompt2;
    private GraphicColorLerp colorLerp1;
    private GraphicColorLerp colorLerp2;
    public float holdDuration;
    public float gapBetweenSentences;
    private float[] timers;
	void Start () {
        cutSceneQueue = new List<string>(){"I got a letter in the mail today.", "I recognized the handwriting."};
        colorLerp1 = prompt1.gameObject.GetComponent<GraphicColorLerp>();
		colorLerp2 = prompt2.gameObject.GetComponent<GraphicColorLerp>();
		prompt1.text = cutSceneQueue[0];
		prompt2.text = cutSceneQueue[1];
		colorLerp1.duration = 5f;
		colorLerp2.duration = 5f;
		StartCoroutine(textLoop(colorLerp1, colorLerp2, 10f));
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
    private IEnumerator textLoop(GraphicColorLerp colorLerp1, GraphicColorLerp colorLerp2, float delay)
    {
        colorLerp1.startColorChange();
		yield return new WaitForSeconds(delay);
		//colorLerp2.startColorChange();
		//yield return new WaitForSeconds(delay);
    }
}
