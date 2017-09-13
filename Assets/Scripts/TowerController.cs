using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {


	public GameObject towerone;
	public GameObject towertwo;
	public LayerMask BuildBlock;
	private GameObject selectedTower;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		//SelectTower ();
		Click ();
	}
	void SelectTower(){
		if (Input.GetKey(KeyCode.A)) {
			selectedTower = towerone;
			print ("1");
		} 
		else if(Input.GetKey(KeyCode.S)) {
			selectedTower = towertwo;
			print ("2");
		}

	}
	void Click(){
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log ("down");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f, BuildBlock.value)) {
				//Debug.Log (hit.transform.gameObject.name);
				bool canBuild = hit.transform.GetComponent<CanBuild>().canBuild;
				if (canBuild) {
					GameObject g = (GameObject)Instantiate (towerone);
					//GameObject g = (GameObject)Instantiate (selectedTower);
					g.transform.position = hit.transform.position + Vector3.up;
					hit.transform.GetComponent<CanBuild>().canBuild = false;
				}

			}
		}

	}



	}

