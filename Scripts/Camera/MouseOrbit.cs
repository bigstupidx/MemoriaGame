//
// Author: 
// 	  Veli V
//  Modified by:
//    Luis Alejandro Vieira lavz24@gmail.com
//
//  Copyright (c) 2014
using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour {

	public Transform target;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distance = 25;

	float x = 0.0f;
	float y = 0.0f;

	public string AxisX = "Mouse X";
	public string AxisY = "Mouse Y";

	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		if (rigidbody)
			rigidbody.freezeRotation = true;
	}

	void LateUpdate () {
		if (target) {
			x += Input.GetAxis(AxisX) * xSpeed * Time.deltaTime;
			y -= Input.GetAxis(AxisY) * ySpeed * Time.deltaTime;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);


			transform.rotation = rotation;
			transform.position = rotation*(new Vector3(0,0,-distance)) + target.position;

		}

	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}


}
