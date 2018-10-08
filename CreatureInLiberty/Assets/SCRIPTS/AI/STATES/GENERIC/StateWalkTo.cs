using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalkTo : StateBehaviour {

	
	override public void EnterState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void UpdateState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(agent.isAtDestination()){
			doNextState();
		}
	}

	override public void ExitState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
