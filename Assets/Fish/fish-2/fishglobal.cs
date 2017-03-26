using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishglobal : MonoBehaviour {

	public GameObject fish1;
	public GameObject fish2;
	public GameObject fish3;

	public bool startCreateFish = false;
	private bool restarted = false;
	static int numFish = 7;
	public static int tankSize = 5;
	public static GameObject[] allFish = new GameObject[numFish * 3];
	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (startCreateFish) {
			for (int i = 0; i < numFish * 3; i++) {
				Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize),
					Random.Range (-tankSize, tankSize),
					Random.Range (-tankSize, tankSize));
				if (i < numFish * 3 && i > numFish * 2) {
					allFish [i] = (GameObject)Instantiate (fish1, pos, Quaternion.identity);
				} else if (i < 2 * numFish && i > numFish) {
					allFish [i] = (GameObject)Instantiate (fish2, pos, Quaternion.identity);
				} else {
					allFish [i] = (GameObject)Instantiate (fish3, pos, Quaternion.identity);
				}
			}

			startCreateFish = false;
		}

		if (restarted) {
			Start ();
			restarted = false;
		}

		if (Random.Range(1,10000) < 50) {
			goalPos = new Vector3(Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize));
		}
	}


	public void Reset(){
		for (int i = 0; i < numFish*3; i++) {
			Debug.Log ("destory this one");
			Destroy(allFish [i]);
		}
		allFish = new GameObject[numFish*3];
		goalPos = Vector3.zero;
		restarted = true;
	}
}
