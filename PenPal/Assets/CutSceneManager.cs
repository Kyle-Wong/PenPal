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

        duration = 3;
        titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        titleColorLerp.duration = duration;
        titleColorLerp.startColorChange();
        flavorColorLerp = narrativePrompt.GetComponent<GraphicColorLerp>();
        flavorColorLerp.initialDelay = duration;
        flavorColorLerp.duration = duration*2;
        flavorColorLerp.startColorChange();
        while (queueCount > 0)
        {
            futureNP += " ";
            futureNP += curIndex.text;
            queueCount--;
            curIndex = cutSceneQueue.Dequeue();
        }

        duration = futureNP.Length / 3;
 
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= duration)
        {
            //cutSceneTitle.text = " ";                  
            narrativePrompt.text = futureNP;
        }
        
	}
}
