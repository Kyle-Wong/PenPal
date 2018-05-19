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
    [HideInInspector]
    public int charIndex = 0;
    private bool isPlaying = false;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        allText = "";
    }
    void Start () {
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
                    
                    //textMesh.text += allText[charIndex];
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
                textMesh.text = allText.Substring(0, charIndex+1);
                charIndex++;
            }
        }
	}
    public void begin()
    {
        isPlaying = true;
    }
    public void end()
    {
        isPlaying = false;
    }
    public void resetText()
    {
        charIndex = 0;
        charTimer = 0;
        textMesh.text = "";
        isPlaying = false;
    }
    public bool playing()
    {
        return isPlaying;
    }
    public void setPlaying(bool x)
    {
        isPlaying = x;
    }
    public void setText(string s)
    {
        charTimer = 0;
        charIndex = 0;
        TextMeshWrapper wrapper = GetComponent<TextMeshWrapper>();
        if (wrapper != null)
            allText = GetComponent<TextMeshWrapper>().getWrappedString(s);
        else
            allText = s;
    }
    public void addText(string s)
    {
        TextMeshWrapper wrapper = GetComponent<TextMeshWrapper>();
        if (wrapper != null)
        {
            allText = GetComponent<TextMeshWrapper>().getWrappedString(allText + s);
        }
        else
            allText = allText + s;
    }
}
