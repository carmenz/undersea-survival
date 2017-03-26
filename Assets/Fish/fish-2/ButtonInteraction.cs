using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour {

	private float gazeTime = 1f;
	private float timer;
	private bool gazeAt;
	public bool startGame;

	public void Start() {
		gazeAt = false;
		timer = 0f;
		startGame = false;
	}

	void Update() {
		if (gazeAt) {

			timer += Time.deltaTime;
			if (timer >= gazeTime) {
				//click the button via gaze input
//				ExecuteEvents.Execute (gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerClickHandler);
				//play sound
				timer = 0f;
				startGame = true;
			}
		}
	}

	public void PointerEnter() {
		gazeAt = true;
	}

	public void PointerExit() {
		gazeAt = false;
		timer = 0f;
	}

	public void PointerClick() {
		Debug.Log ("pointer click");
		//start game:
		startGame = true;
	}
}
