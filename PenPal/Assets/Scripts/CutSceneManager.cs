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
    public float timer;
    public float duration;
    //string futureNP;
    string NarLength;
    private GraphicColorLerp titleColorLerp;
    private GraphicColorLerp flavorColorLerp;
    Queue<LetterEvent> cutSceneQueue;
    LetterEvent curIndex;
    
	void Start () {
        cutSceneQueue = GameManager.narrativeQueue;

        //cutSceneTitle.text = "Next Chapter";
        
        narrativePrompt.text = " ";

        //NarLength = " ";
        curIndex = cutSceneQueue.Dequeue();

        //futureNP = " ";

        //while (cutSceneQueue.Count > 0)
        //{
        //    NarLength += curIndex.text;
        //    curIndex = cutSceneQueue.Dequeue();
        //    NarLength += " ";
        //}



       // titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        
        flavorColorLerp = narrativePrompt.GetComponent<GraphicColorLerp>();

        //titleColorLerp.duration = 5; //set the next chapter duration to five

        
        //flavorColorLerp.duration = 5;       

        
        
        //titleColorLerp.startColorChange();        
        
        flavorColorLerp.startColorChange();

        //flavorColorLerp.initialDelay = 5;

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        //every three seconds stop to dequeue
        if (timer >= duration)
        {
            timer = 0; //reset timer to allow every three duartion
            //cutSceneTitle.text = " ";

            //narrativePrompt.text = "Ready for battle!";
            //cutSceneTitle.text = " ";                  
            if (curIndex.type == LetterEvent.Type.SENTENCE)
            {
                narrativePrompt.text = curIndex.text;
                //show current index message
            }

            //cutscene is ended at EOL and this scene is ended
            else if (curIndex.type == LetterEvent.Type.EOL) 
            {
                SceneManager.LoadScene("LetterReceivingScene");
            }
            //move to next update as next frame occurs
            curIndex = cutSceneQueue.Dequeue();


        }       
        
	}
}
