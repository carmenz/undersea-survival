#pragma strict


public var currentMount : Transform;
public var speedFactor : float = 0.1;
public var zoomFactor = 1.0;
public var cameraComp : Camera;

private var lastPosition : Vector3;


function Start () {
	lastPosition = transform.position;
}

function Update () {
	transform.position = Vector3.Lerp(transform.position, currentMount.position,0.1);
	transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);

	var velocity = Vector3.Magnitude(transform.position - lastPosition);
	cameraComp.fieldOfView = 60 + velocity + zoomFactor;

	lastPosition = transform.position;
}

function setMount (newMount : Transform) {
	currentMount = newMount;
}
