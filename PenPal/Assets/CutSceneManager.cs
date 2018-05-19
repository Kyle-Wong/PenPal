using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {

	// Use this for initialization
    public Image background;
    public Text cutSceneTitle;
    public Text flavorText;
    public float timer;
    public float duration;
    private GraphicColorLerp titleColorLerp;
    private GraphicColorLerp flavorColorLerp;
    Queue<LetterEvent> cutSceneQueue;
    LetterEvent curIndex;
    
	void Start () {
        cutSceneQueue = GameManager.narrativeQueue;
        curIndex = cutSceneQueue.Dequeue();
        cutSceneTitle.text = "Chapter 1";
        flavorText.text = " ";
        //timer = 5;
        titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        titleColorLerp.duration = 5;
        titleColorLerp.startColorChange();
        flavorColorLerp = flavorText.GetComponent<GraphicColorLerp>();
        flavorColorLerp.initialDelay = 5;
        flavorColorLerp.duration = 5;
        flavorColorLerp.startColorChange();
        duration = 5; 
	}
	
	// Update is called once per frame
	void Update () {
       
       
        timer += Time.deltaTime;
        while (curIndex.type.text != "EOL")
            if (timer >= duration)
            {
                //cutSceneTitle.text = " ";                  
                flavorText.text = curIndex.text;
            }

	}
}
