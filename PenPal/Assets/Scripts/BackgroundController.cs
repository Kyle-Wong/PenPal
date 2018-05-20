using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

    // Use this for initialization
    public Sprite[] woods;
    private int group;
	void Start () {
        group = GameManager.playerQueue.Peek().group;
        GetComponent<SpriteRenderer>().sprite = woods[(group - 1) % woods.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
