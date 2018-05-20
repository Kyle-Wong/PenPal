using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {

	// Use this for initialization
    public Image background;
    public Text cutSceneTitle;
    public Text narrativePrompt;
    public float timer;
    public int duration;
    //string futureNP;
    string NarLength;
    private GraphicColorLerp titleColorLerp;
    private GraphicColorLerp flavorColorLerp;
    Queue<LetterEvent> cutSceneQueue;
    LetterEvent curIndex;
    
	void Start () {
        cutSceneQueue = GameManager.narrativeQueue;

        cutSceneTitle.text = "Next Chapter";
        
        narrativePrompt.text = " ";

        NarLength = " ";
        curIndex = cutSceneQueue.Dequeue();

        //futureNP = " ";

        while (cutSceneQueue.Count > 0)
        {
            NarLength += curIndex.text;
            curIndex = cutSceneQueue.Dequeue();
            NarLength += " ";
        }

        titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        
        flavorColorLerp = narrativePrompt.GetComponent<GraphicColorLerp>();

        //titleColorLerp.duration = 5; //set the next chapter duration to five

        
        //flavorColorLerp.duration = 5;       

        
        titleColorLerp.startColorChange();        
        
        flavorColorLerp.startColorChange();

        flavorColorLerp.initialDelay = 5;

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;


        if (timer >= 5.0)
        {
            cutSceneTitle.text = " ";

            //narrativePrompt.text = "Ready for battle!";
            //cutSceneTitle.text = " ";                  
            narrativePrompt.text = NarLength;            

        }

        if (timer >= 10.0)
        {
            narrativePrompt.text = " ";
        }
        
	}
}
