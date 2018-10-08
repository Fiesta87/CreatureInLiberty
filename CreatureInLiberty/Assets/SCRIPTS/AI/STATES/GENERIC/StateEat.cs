using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEat : StateBehaviour {

	public float eatTimeStamp;
	public bool onlyOneEatPerAnimLoop = true;
	public float eattingValue = 0.3f;
	public bool eattingValueIsPercentageOfHungriness = true;

	[HideInInspector]
	public Food food;

	private float nextEatTimeStamp;
	
	override public void EnterState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		nextEatTimeStamp = eatTimeStamp;
		if(!onlyOneEatPerAnimLoop) {
			agent.stopThinking = true;
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void UpdateState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(stateInfo.normalizedTime > nextEatTimeStamp){
			if(onlyOneEatPerAnimLoop){
				nextEatTimeStamp += 1.0f;
			} else {
				nextEatTimeStamp += eatTimeStamp;
			}

			float eattedValue;

			if(eattingValueIsPercentageOfHungriness) {
				eattedValue = agent.hungriness * eattingValue;
			} else {
				eattedValue = eattingValue;
			}

			eattedValue = Mathf.Min(food.quantity, eattedValue);

			agent.hungriness = Mathf.Max(0.0f, agent.hungriness - eattedValue);

			// Debug.Log("eatted : " + eattedValue);

			bool foodExhausted = food.eat(eattedValue);
			if(foodExhausted){
				// Debug.Log("foodExhausted : " + food.ToString());
				doNextState();
			}
		}
		if(!onlyOneEatPerAnimLoop && stateInfo.normalizedTime >= 1.0f) {
			doNextState();
		}
	}

	override public void ExitState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
