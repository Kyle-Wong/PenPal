using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialText : MonoBehaviour {

    // Use this for initialization
    public float charDelay;
    private TextMesh textMesh;
    private string allText;
    private float charTimer = 0;
    private int charIndex = 0;
    private bool isPlaying = false;
    private void Awake()
    {
        
    }
    void Start () {
        isPlaying = true;
        textMesh = GetComponent<TextMesh>();
        allText = textMesh.text;
        textMesh.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            charTimer += Time.deltaTime;
            if (charTimer > charDelay)
            {
                if (charIndex >= allText.Length)
                    return;
                charTimer = 0;
                while (allText[charIndex] == '\n' || allText[charIndex] == ' ')
                {
                    if (charIndex >= allText.Length)
                        break;
                    textMesh.text += allText[charIndex];
                    charIndex++;
                }
                textMesh.text += allText[charIndex];
                charIndex++;
            }
        }
	}
    public void begin()
    {
        charTimer = 0;
        isPlaying = true;
    }
    public void end()
    {
        isPlaying = false;
    }
    public void resetText()
    {
        textMesh.text = "";
        isPlaying = false;
    }
    public bool playing()
    {
        return isPlaying;
    }
}
