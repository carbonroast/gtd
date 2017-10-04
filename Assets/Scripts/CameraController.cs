﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour {


	public float panSpeed = 20f;
	public float panBorderThickness = 20f;
	public Vector2 panLimit;
	public float scrollSpeed =20f;
	public float minY = 20f;
	public float maxY = 120f;

	private Quaternion rotation;

	void Awake(){
		rotation = transform.rotation;
	}

	void LateUpdate(){
		transform.rotation = rotation;
	}
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 pos = transform.position;

		if (Input.GetKey (KeyCode.UpArrow) /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
			pos.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow) /*|| Input.mousePosition.y <= panBorderThickness*/) {
			pos.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow) /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow) /*|| Input.mousePosition.y <= panBorderThickness*/) {
			pos.x -= panSpeed * Time.deltaTime;
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		pos.y += scroll * scrollSpeed * 100f * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, -panLimit.x, panLimit.x);
		pos.y = Mathf.Clamp (pos.y, minY, maxY);
		pos.z = Mathf.Clamp (pos.z, -panLimit.y, panLimit.y);

		transform.position = pos;
	}
}

