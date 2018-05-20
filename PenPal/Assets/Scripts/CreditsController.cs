using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

	// Use this for initialization
    public UIRevealer[] nameRevealer;
    public UIRevealer[] roleRevealer;
    public float initialDelay;
    public float repeatDelay;
	void Start () {
        StartCoroutine(revealCredits());
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private IEnumerator revealCredits()
    {
        yield return new WaitForSeconds(initialDelay);
        for(int i = 0; i < nameRevealer.Length; i++)
        {
            nameRevealer[i].revealUI();
            roleRevealer[i].revealUI();
            yield return new WaitForSeconds(repeatDelay);
        }
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
