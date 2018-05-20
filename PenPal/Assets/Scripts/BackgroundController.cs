using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer myImage;
    public Sprite[] woods;
    private int group;
	void Start () {
        group = GameManager.playerQueue.Peek().group;
        myImage.sprite = woods[group];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
