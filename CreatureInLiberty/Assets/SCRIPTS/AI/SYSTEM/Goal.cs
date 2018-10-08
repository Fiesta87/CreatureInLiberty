using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : MonoBehaviour {

	protected Agent agent;
	// protected Dictionary<string,string> memory;

	// Use this for initialization
	void Awake () {
		this.agent = GetComponent<Agent>();
		awakeInit();
	}

	public abstract void awakeInit();

	public abstract float computeDesirability();

	public abstract bool follow();

	public abstract void setupState(StateBehaviour state);
}
