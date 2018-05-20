using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabNavigator : MonoBehaviour {
	//Code snippets from unitydevtips.wordpress.com
	public bool findFirstSelectable = true;
	private int shiftWindow;
	private int shiftWindowReset = 2;
	// Use this for initialization
	void Start () {
		shiftWindow = shiftWindowReset;
	}
	
	// Update is called once per frame
	void Update () {
		if (shiftWindow < shiftWindowReset) {
			shiftWindow--;
			if (shiftWindow <= 0)
				shiftWindow = shiftWindowReset;
		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab)) {
			if (EventSystem.current != null && shiftWindow == shiftWindowReset) {
				GameObject selected = EventSystem.current.currentSelectedGameObject;
				if (selected == null && findFirstSelectable) {
					Selectable found = (Selectable.allSelectables.Count > 0) ? Selectable.allSelectables[0] : null;
					if (found != null) {
						selected = found.gameObject;
					}
				}
				if (selected != null) {
					Selectable current = (Selectable) selected.GetComponent("Selectable");
					if (current != null) {
						Selectable nextUp = current.FindSelectableOnUp();
						if (nextUp != null) {
							nextUp.Select();
							shiftWindow--;
						}
					}
				}
			}
		} else if (Input.GetKeyDown(KeyCode.Tab)) {
			if (EventSystem.current != null) {
				GameObject selected = EventSystem.current.currentSelectedGameObject;
				if (selected == null && findFirstSelectable) {
					Selectable found = (Selectable.allSelectables.Count > 0) ? Selectable.allSelectables[0] : null;
					if (found != null) {
						selected = found.gameObject;
					}
				}
				if (selected != null) {
					Selectable current = (Selectable) selected.GetComponent("Selectable");
					if (current != null) {
						Selectable nextDown = current.FindSelectableOnDown();
						if (nextDown != null) 
							nextDown.Select();
					}
				}
			}
		}
	}
}
