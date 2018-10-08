using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehaviour : StateMachineBehaviour {

    protected Agent agent;
    protected bool safeUpdate;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.agent = animator.gameObject.GetComponent<Agent>();
		this.agent.getCurrentGoal().setupState(this);
        EnterState(animator, stateInfo, layerIndex);
        this.safeUpdate = true;
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(safeUpdate) {
			UpdateState(animator, stateInfo, layerIndex);
		}
	}
    
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ExitState(animator, stateInfo, layerIndex);
	}
    /*
	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}*/

    abstract public void EnterState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    abstract public void UpdateState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    abstract public void ExitState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    protected void doNextState() {
        safeUpdate = agent.doNextState();
    }
}
