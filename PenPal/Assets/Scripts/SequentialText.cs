using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialText : MonoBehaviour {

    // Use this for initialization
    public float charDelay;
    private TextMesh textMesh;
    [HideInInspector]
    public string allText = "";
    private float charTimer = 0;
    private int charIndex = 0;
    private bool isPlaying = false;
    private void Awake()
    {
        
    }
    void Start () {
        isPlaying = true;
        textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            charTimer += Time.deltaTime;
            if (charTimer > charDelay)
            {
                if (charIndex >= allText.Length)
                {
                    isPlaying = false;
                    return;
                }
                charTimer = 0;
                while (allText[charIndex] == '\n' || allText[charIndex] == ' ')
                {
                    
                    textMesh.text += allText[charIndex];
                    charIndex++;
                    if (charIndex >= allText.Length)
                    {
                        isPlaying = false;
                        break;
                    }
                }
                if (charIndex >= allText.Length)
                {
                    isPlaying = false;
                    return;
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
    public void setText(string s)
    {
        charIndex = 0;
        allText = textMesh.GetComponent<TextMeshWrapper>().getWrappedString(s);
    }
    public void addText(string s)
    {
        allText = textMesh.GetComponent<TextMeshWrapper>().getWrappedString(allText + s);
    }
}
