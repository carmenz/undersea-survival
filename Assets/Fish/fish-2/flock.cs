using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class flock : MonoBehaviour {

	public float speed = 0.1f;
	float rotationalSpeed = 0.4f;
	float neighborDistance = 2.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	bool turning = false;

	private float gazeTime = 0.3f;
	private float timer;
	private bool gazeAt;
	private GameController gameController;
	public GameObject explosion;

	bool kill = false;

	private AudioSource source;
	public AudioClip sound;


	void Start () {
		speed = Random.Range (2f, 5f);

		//set up link with script:
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("can't find GameController!");
		}
	}

	void Update () {	//check if this fish is being focused:
		if (gazeAt) {
			timer += Time.deltaTime;
			if (timer >= gazeTime) {
				//click the button via gaze input
				PointerClick ();
				timer = 0f;
				//play sound
			}
		} else {	//update the behaviour:
			if (Vector3.Distance (transform.position, Vector3.zero) >= fishglobal.tankSize) {
				turning = true;
			} else {
				turning = false;
			}
			if (turning) {
				Vector3 direction = Vector3.zero - transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationalSpeed);
				
				speed = Random.Range (2f, 4f);
			}

			if (Random.Range (0, 5) < 1) {
				ApplyRules ();
			}
			transform.Translate (0, 0, Time.deltaTime * speed);
		}
	}

	void ApplyRules() {
		GameObject[] gos;
		gos = fishglobal.allFish;

		//GameObject[] gos = GameObject.FindGameObjectsWithTag("Fish_separate");
		float gSpeed = 1f;

		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;

		float dist;

		Vector3 goalPos = fishglobal.goalPos;
		int groupSize = 0;

		foreach(GameObject go in gos) {
			if (go != gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighborDistance) {
					vcenter += go.transform.position;
					groupSize++;

					if (dist < 1.0f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					flock anotherFlock = go.GetComponent<flock>();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
//			if (groupSize > 0) {
//				vcenter = vcenter / groupSize + (goalPos - gameObject.transform.position);
//				speed = gSpeed / groupSize;
//
//				Vector3 direction = (vcenter + vavoid) - transform.position;
//
//				if (direction != Vector3.zero) {
//					transform.rotation = Quaternion.Slerp (transform.rotation,
//						Quaternion.LookRotation (direction),
//						rotationalSpeed);
//				}
//			}
		}
	}

	public void SetGazedAt(bool gazedAt){
		if (gazedAt) {
			GetComponent<Renderer>().material.color = Color.blue;
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
		gameController.AddOne ();
		//add animation:
		Instantiate(explosion, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint (sound, transform.position);
		gameObject.SetActive (false);

	}

}
