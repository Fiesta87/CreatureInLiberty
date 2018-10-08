using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHuntPrey : Goal
{
	[HideInInspector]
	public Agent currentPrey;
	public Food.Types preyType;
	public float attackDistance;
	public int damage = 100;
	private System.Type realFoodType;

	public override void awakeInit() {
		realFoodType = Food.getFoodTypeFromEnum(preyType);
	}

    public override float computeDesirability() {

        List<Component> foods = agent.perception.getAllPerceptedEntitiesComponentsOfType(realFoodType);

		if(foods.Count > 0) {
			
			currentPrey = foods[0].GetComponent<Agent>();
        	return agent.hungriness * agent.gluttony * agent.gluttony;

		} else {

			return 0.0f;
		}
    }

    public override bool follow() {

		// List<Component> foods = agent.perception.getAllPerceptedEntitiesComponentsOfType(realFoodType);

		// if(foods.Count > 0) {

			// currentFood = (Food)foods[0];
			// Debug.Log("food founded : " + currentFood);

			if(agent.distanceTo(currentPrey.transform.position) > attackDistance){
			// if(agent.canCatchPreyIn(currentPrey, 0.5f)){
				agent.hunt(currentPrey);
			}
			
			agent.enqueueNextState("attack");
			
			
			

		// } else {

		// 	agent.wander(10.0f, 15.0f);
		// }
		return agent.doNextState();
    }

	public override void setupState(StateBehaviour state){
		
		if(state is StateHunt){
			StateHunt stateEat = (StateHunt)state;

			stateEat.prey = this.currentPrey;
			stateEat.attackDistance = this.attackDistance;

		} else if(state is StateAttack) {
			StateAttack stateAttack = (StateAttack)state;

			stateAttack.damage = this.damage;
		} else {
			throw new System.Exception("Can't setup the state : " + state.ToString());
		}
	}
}
