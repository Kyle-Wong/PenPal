using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuController : MonoBehaviour {

    // Use this for initialization
    public string startGameScene;
    public string creditsScene;
    public EventSystem eventSystem;
    public GraphicColorLerp fadeToBlack;
    public bool allowInput = true;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    public void startGame()
    {
        if (!allowInput)
            return;
        allowInput = false;
        eventSystem.SetSelectedGameObject(null);
        StartCoroutine(loadAfterDelay(startGameScene, fadeToBlack.duration));
    }
    public void goToCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public IEnumerator loadAfterDelay(string sceneName, float delay)
    {
        fadeToBlack.startColorChange();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
