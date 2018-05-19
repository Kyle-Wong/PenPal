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
        textInOneLine = removeNewLines(textMesh.text);
        words = textInOneLine.Split(' ');
    }
    void Start () {
        
        textMesh.text = "";
        addNewLines();
	}
	
	// Update is called once per frame
	void Update () {
        textInOneLine = removeNewLines(textMesh.text);
        words = textInOneLine.Split(' ');
        textMesh.text = "";
        addNewLines();
    }
    private string removeNewLines(string str)
    {
        return str.Replace("\n", " ");
    }
    private void addNewLines()
    {
        if (words.Length == 0)
            return;
        textMesh.text = words[0];
        for(var i = 1; i < words.Length; i++)
        {
            int len = textMesh.text.Length;
            textMesh.text += ' ' + words[i];
            if(textMesh.GetComponent<Renderer>().bounds.extents.x > lineLength)
            {
                textMesh.text = textMesh.text.Substring(0, len) + '\n' + words[i];
            }
        }
    }
}
