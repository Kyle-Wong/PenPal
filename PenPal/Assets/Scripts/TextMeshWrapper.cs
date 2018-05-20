using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshWrapper : MonoBehaviour {

    // Use this for initialization
    private TextMesh textMesh;
    public float lineLength;
    private string textInOneLine;
    private string[] words;
    private void Awake()
    {
        
        textMesh = GetComponent<TextMesh>();
    }
    void Start () {
        textMesh.text = getWrappedString(textMesh.text);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
    }
    private string removeNewLines(string str)
    {
        return str.Replace("\n", " ");
    }
    private void addNewLines()
    {
        if (words.Length == 0)
            return;

        for(var i = 0; i < words.Length; i++)
        {
            int len = textMesh.text.Length;
            textMesh.text += words[i] + ' ';
            if(textMesh.GetComponent<Renderer>().bounds.extents.x > lineLength)
            {
                textMesh.text = textMesh.text.Substring(0, len) + '\n' + words[i] + ' ';
            }
        }
        textMesh.text = textMesh.text.Replace("\n ", "\n");
    }
    public string getWrappedString(string s)
    {
        words = s.Split(' ');
        textMesh.text = "";
        addNewLines();
        string result = textMesh.text;
        textMesh.text = "";
        return result;
    }
    public void addAndWrapText(string s)
    {
        textMesh.text = getWrappedString(textMesh.text + s);
    }
    public string addText(string s)
    {
        return getWrappedString(textMesh.text + s);
    }
}
