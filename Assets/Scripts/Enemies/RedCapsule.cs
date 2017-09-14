using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class RedCapsule : Enemy {

	public override void Type(){

		gameObject.layer = Mathf.RoundToInt(Mathf.Log(type.value, 2));

	}

	public override void Movement(){
		base.enemy.speed = speed;
		base.enemy.acceleration = 2;

	}
}