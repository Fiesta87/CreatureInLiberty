using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : StateBehaviour {

	[HideInInspector]
	public int damage;

	private bool damageDone;

	override public void EnterState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		agent.stopThinking = true;
		damageDone = false;
		agent.weapon.startAttack();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void UpdateState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		if(stateInfo.normalizedTime > 0.6f && !damageDone) {
			damageDone = true;
			foreach(GameObject go in agent.weapon.touchedEntitiesList) {
				Agent target = go.GetComponent<Agent>();

				if(target != null) {
					target.takeDamage(damage);
				}
			}
		}

		if(stateInfo.normalizedTime >= 1.0f) {
			agent.resumeSteeringBehaviour();
			agent.currentState = "";
			doNextState();
		}
	}

	override public void ExitState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
