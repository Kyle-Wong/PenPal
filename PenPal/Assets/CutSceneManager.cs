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
    private GraphicColorLerp titleColorLerp;
    private GraphicColorLerp flavorColorLerp;

	void Start () {
        cutSceneTitle.text = "This is my text";
        flavorText.text = " ";
        //timer = 5;
        titleColorLerp = cutSceneTitle.GetComponent<GraphicColorLerp>();
        titleColorLerp.duration = 5;
        titleColorLerp.startColorChange();
        flavorColorLerp = flavorText.GetComponent<GraphicColorLerp>();
        flavorColorLerp.initialDelay = 5;
        flavorColorLerp.duration = 5;
        flavorColorLerp.startColorChange();
	}
	
	// Update is called once per frame
	void Update () {
        
        timer += Time.deltaTime;
        if (timer >= 5.0)
        {
            //cutSceneTitle.text = " ";
            flavorText.text = "This is a story.";
        }
        if (timer >= 10.0)
        {
            //flavorText.text = " ";
            

        }
	}
}
