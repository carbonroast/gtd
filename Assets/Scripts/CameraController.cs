using System.Collections;
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
	private Vector3 position;

	void Awake(){
		rotation = transform.rotation;
		position = new Vector3 (5, 12, -7);

	}

	void LateUpdate(){
		transform.rotation = rotation;
		transform.position = position;



		if (Input.GetKey (KeyCode.UpArrow) /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
			position.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow) /*|| Input.mousePosition.y <= panBorderThickness*/) {
			position.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow) /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
			position.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow) /*|| Input.mousePosition.y <= panBorderThickness*/) {
			position.x -= panSpeed * Time.deltaTime;
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		position.y += scroll * scrollSpeed * 100f * Time.deltaTime;


		/*//keeping camera in bounds
		newposition.x = Mathf.Clamp (newposition.x, -panLimit.x, panLimit.x);
		newposition.y = Mathf.Clamp (newposition.y, minY, maxY);
		newposition.z = Mathf.Clamp (newposition.z, -panLimit.y, panLimit.y);
		*/



	}
	// Update is called once per frame

	void FixedUpdate () {

//		Vector3 pos = transform.localPosition;
//

//
	}
}

