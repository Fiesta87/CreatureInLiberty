using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {

	private List<GameObject> detectedEntitiesList;

	void Awake() {
		this.detectedEntitiesList = new List<GameObject>();
	}

	void OnTriggerEnter(Collider other) {
        this.detectedEntitiesList.Add(other.gameObject);
		// Debug.Log("added : " + other.gameObject.ToString());
    }

	void OnTriggerExit(Collider other) {
        this.detectedEntitiesList.Remove(other.gameObject);
		// Debug.Log("removed : " + other.gameObject.ToString());
    }

	public List<Component> getAllPerceptedEntitiesComponentsOfType(System.Type type){

		List<Component> result = new List<Component>();
		Component comp;

		List<GameObject> toRemove = new List<GameObject>();

		foreach(GameObject go in detectedEntitiesList) {
			
			if(go.activeInHierarchy){
				comp = go.GetComponent(type);
				
				if(comp != null){
					result.Add(comp);
				}
			} 
			else {
				toRemove.Add(go);
			}
		}

		foreach(GameObject go in toRemove) {
			detectedEntitiesList.Remove(go);
		}
		toRemove.Clear();

		return result;
	}

	public void cleanPerceptedInactivedEntity() {

		List<GameObject> toRemove = new List<GameObject>();

		foreach(GameObject go in detectedEntitiesList) {
			
			if(!go.activeInHierarchy){
				toRemove.Add(go);
			}
		}

		foreach(GameObject go in toRemove) {
			detectedEntitiesList.Remove(go);
		}
		toRemove.Clear();
	}
}
