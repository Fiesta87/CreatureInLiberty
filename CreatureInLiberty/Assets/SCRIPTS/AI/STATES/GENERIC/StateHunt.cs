using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHunt : StateBehaviour {

	[HideInInspector]
	public Agent prey;
	[HideInInspector]
	public float attackDistance;
	override public void EnterState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void UpdateState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		

		// if(agent.distanceTo(prey.transform.position) <= attackDistance) {
		if(agent.canCatchPreyIn(prey, attackDistance, 1.0f)) {
			agent.stopSteeringBehaviour();
			doNextState();
		} else if(Vector3.Distance(agent.getDestination(), prey.transform.position) >= 1.5f) {
			agent.setDestination(prey.transform.position);
		}
	}

	override public void ExitState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
