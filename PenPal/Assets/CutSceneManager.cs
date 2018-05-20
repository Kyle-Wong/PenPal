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
    string futureNP;
    string NarLength;
    private GraphicColorLerp titleColorLerp;
    private GraphicColorLerp flavorColorLerp;
    Queue<LetterEvent> cutSceneQueue;
    LetterEvent curIndex;
    
	void Start () {
        cutSceneQueue = GameManager.narrativeQueue;

        int queueCount = cutSceneQueue.Count; 
        
        curIndex = cutSceneQueue.Dequeue();
        cutSceneTitle.text = "Next Chapter";
        narrativePrompt.text = " ";
        futureNP = " ";       
    

        //timer = 5;


        while (queueCount > 0)
        {
            
            NarLength += curIndex.text;
            queueCount--;
            curIndex = cutSceneQueue.Dequeue();
            NarLength += " ";
        }


        titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        flavorColorLerp = narrativePrompt.GetComponent<GraphicColorLerp>();
        duration = NarLength.Length / 3;
        titleColorLerp.duration = 3;
         
        titleColorLerp.startColorChange();        
        flavorColorLerp.startColorChange();

        flavorColorLerp.duration = duration;
        futureNP = NarLength;
 
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;


        if (timer >= 3.0)
        {
            //cutSceneTitle.text = " ";                  
            narrativePrompt.text = futureNP;
        }
        

	}
}
