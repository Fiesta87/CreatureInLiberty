using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDeath : Death {

	public Transform drop;

    public override void die(Agent agent) {
        
		Instantiate(drop, this.transform.position + Vector3.up * 3.0f, Quaternion.identity);

		this.gameObject.SetActive(false);
    }
}
