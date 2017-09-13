using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RedCapsule : Enemy {

	public override void Type(){
		gameObject.layer = LayerMask.NameToLayer ("Ground");
	}

	public override void Movement(){
		base.enemy.speed = speed;
		base.enemy.acceleration = 2;

	}
}