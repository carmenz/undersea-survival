using System.Collections;
using UnityEngine;

public class Focus : MonoBehaviour {

	public Transform targetPoint;
	private Transform startPoint;
	//public Vector3 targetPoint;
	//private Vector3 startPoint;
	public float speed = 0.1f;
	private float startTime;
	private float length;

	void Start() {
		//targetPoint = FishHit.myPosition;
		startPoint = targetPoint;

		startTime = Time.time;
		//length = Vector3.Distance (startPoint, targetPoint);
		length = Vector3.Distance (startPoint.position, targetPoint.position);
	
	}

	void Update () {

		float dist = (Time.time - startTime) * speed;
		float frac = dist / length;
		transform.position = Vector3.Lerp (startPoint.position, targetPoint.position, frac);

		//transform.LookAt (target);
	}
}
