using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RedCapsule : Enemy {


	public override void Movement(){
		base.enemy.speed = speed;
		base.enemy.acceleration = 2;
	}
}
