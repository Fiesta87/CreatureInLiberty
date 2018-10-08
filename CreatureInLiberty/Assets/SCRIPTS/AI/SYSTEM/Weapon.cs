using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public List<GameObject> touchedEntitiesList;

	private List<GameObject> detectedEntitiesList;
	private bool attackInProgress;

	void Awake() {
		this.detectedEntitiesList = new List<GameObject>();
		this.touchedEntitiesList = new List<GameObject>();
		this.attackInProgress = false;
	}

	void OnTriggerEnter(Collider other) {
        this.detectedEntitiesList.Add(other.gameObject);
		if(attackInProgress){
			this.touchedEntitiesList.Add(other.gameObject);
		}
    }

	void OnTriggerExit(Collider other) {
        this.detectedEntitiesList.Remove(other.gameObject);
    }
	
	public void startAttack() {
		this.attackInProgress = true;
		this.touchedEntitiesList = new List<GameObject>(detectedEntitiesList);
	}
}
