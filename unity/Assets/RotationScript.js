#pragma strict

function Start () {

}

function Update () {
	var ROTATION_SPEED : float = 1.0 / 10.0; // speed of rotation in degrees per second
	transform.Rotate(Vector3.up * Time.deltaTime * ROTATION_SPEED);
}