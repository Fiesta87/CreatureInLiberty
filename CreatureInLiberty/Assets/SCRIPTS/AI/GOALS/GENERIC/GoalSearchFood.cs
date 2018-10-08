using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSearchFood : Goal
{
	// private Dictionary<string,Vector3> memoryFoodLocations;
	[HideInInspector]
	public Food currentFood;
	public Food.Types foodType;
	private System.Type realFoodType;

	public override void awakeInit() {
		// this.memoryFoodLocations = new Dictionary<string,Vector3>();
		realFoodType = Food.getFoodTypeFromEnum(foodType);
	}

    public override float computeDesirability() {
		List<Component> foods = agent.perception.getAllPerceptedEntitiesComponentsOfType(realFoodType);

		if(foods.Count > 0) {
			return 0.0f;
		} else {
			return agent.hungriness * agent.gluttony;
		}
    }

    public override bool follow() {
		
		// if(memoryFoodLocations.Count > 0) {
		
		// } else {

			// List<Component> foods = agent.perception.getAllPerceptedEntityComponentOfType(realFoodType);

			// if(foods.Count > 0) {

			// 	currentFood = (Food)foods[0];
			// 	Debug.Log("food founded : " + currentFood);

			// 	if(!agent.isAtPosition(currentFood.transform.position)){
			// 		agent.walkTo(currentFood.transform.position);
			// 	}

			// 	agent.enqueueNextState("eat");

			// } else {

				agent.wander(20.0f, 30.0f);
			// }
			return agent.doNextState();
		// }
    }

	public override void setupState(StateBehaviour state){

		// if(state is StateEat){

		// 	StateEat stateEat = (StateEat)state;

		// 	stateEat.food = currentFood;
		// } else {
		// 	throw new System.Exception("Can't setup the state : " + state.ToString());
		// }
	}
}
