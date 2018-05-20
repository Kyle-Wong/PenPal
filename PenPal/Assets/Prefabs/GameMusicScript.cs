using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour {

    public AudioClip[] sounds;
    public AudioSource music;
    Queue<LetterEvent> letterQueue;
    LetterEvent curIndex;
    int currentGroup = 1;
	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
        int groupIndex = GameManager.playerQueue.Peek().group;
		if(currentGroup != groupIndex && currentGroup <= 8 && currentGroup >= 1){
            music.Stop();
            music.clip = sounds[groupIndex-1];
            music.Play();
        }
        currentGroup = groupIndex;
	}

    private void playsound(int index)
    {
        music.clip = sounds[index];
        music.Play();
    }
}
