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
    public GameObject mainMenuButtons;
    public Canvas inputCanvas;
    public InputField nameBox;
    public GameObject playButton;
    public enum State
    {
        MainMenu,
        AtInputField,
    }
    private State state = State.MainMenu;
	void Start () {
        inputCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.MainMenu:
                break;
            case State.AtInputField:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("MainMenu");
                    //lazy way to go back
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    startGame();
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    eventSystem.SetSelectedGameObject(playButton);
                }
                break;
        }
	}




    public void startGame()
    {
        if (!allowInput)
            return;
        if (nameBox.text.Trim() == "")
            return;
        GameManager.playerName = nameBox.text;
        allowInput = false;
        eventSystem.SetSelectedGameObject(null);
        StartCoroutine(loadAfterDelay(startGameScene, fadeToBlack.duration));
    }
    public void goToInputField()
    {

        mainMenuButtons.SetActive(false);
        inputCanvas.enabled = true;
        eventSystem.SetSelectedGameObject(nameBox.gameObject);
       
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
