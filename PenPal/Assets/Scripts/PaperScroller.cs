using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScroller : MonoBehaviour {

    // Use this for initialization
    public float scrollSpeed;
    private LetterWritingController gameController;
    public float minY;
    public float maxY;
    private float mouseDiff;
    private Camera mainCam;
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LetterWritingController>();
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDiff = transform.position.y - mainCam.ScreenToWorldPoint(Input.mousePosition).y;
        }
        if (gameController.getState() == LetterWritingController.State.LetterDone)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
                
            } else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.position -= Vector3.up * scrollSpeed * Time.deltaTime;
                
            } else if (Input.GetMouseButton(0))
            {
                transform.position = new Vector3(transform.position.x,(mainCam.ScreenToWorldPoint(Input.mousePosition).y+mouseDiff),transform.position.z);
            }
           
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }
    
    

}
