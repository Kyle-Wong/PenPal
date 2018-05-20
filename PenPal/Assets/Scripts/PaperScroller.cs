using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperScroller : MonoBehaviour {

    // Use this for initialization
    public float scrollSpeed;
    private ILetterController gameController;
    public float minY;
    public float maxY;
    private float mouseDiff;
    private Camera mainCam;
    public TextMesh textMesh;
    private float oldHeight;
    private float newHeight;
    public bool autoScroll = false;
    public float autoScrollingStartY;
    private bool firstScroll = true;
    private float scrollHeight = 0;
    public Transform closing;
    private void Awake()
    {
        
    }
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LetterWritingController>();
        if (gameController == null) //i assume if this fails, i need to find ResponseWritingController
                gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ResponseWritingController>();
        if (gameController == null)
                gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MadlibsController>();
        mainCam = Camera.main;
        if (autoScroll)
        {
            newHeight = textMesh.GetComponent<Renderer>().bounds.extents.y;
            oldHeight = newHeight;
        } else
        {
            if(textMesh != null)
                closing.position = new Vector3(closing.position.x,closing.position.y+(textMesh.GetComponent<Renderer>().bounds.extents.y-textMesh.transform.position.y),closing.position.z);
        }

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
                transform.position -= Vector3.up * scrollSpeed * Time.deltaTime;
                
            } else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
                
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
      
        if (autoScroll)
        {
            newHeight = textMesh.GetComponent<Renderer>().bounds.extents.y;
            
            if (newHeight > autoScrollingStartY && oldHeight < newHeight)
            {
                if (firstScroll)
                {
                    scrollHeight = (newHeight - oldHeight);
                    firstScroll = false;
                }
                transform.position += Vector3.up * scrollHeight;
                closing.position += Vector3.down * scrollHeight;
            }
            oldHeight = newHeight;
        }
        else
        {
            //if (textMesh != null)
                //closing.position = new Vector3(closing.position.x, textMesh.transform.position.y-textMesh.GetComponent<Renderer>().bounds.extents.y*2.5f-.8f, closing.position.z);
        }
    }
    
    

}
