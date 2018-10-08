using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : MonoBehaviour {

	public float quantity = 1.0f;

	protected float initialQuantity;

	void Awake() {
		initialQuantity = quantity;
	}

	public virtual bool eat(float quantityEatted){
		this.quantity -= quantityEatted;
		if(quantity <= 0.0f) {
			this.gameObject.SetActive(false);
			return true;
		}
		return false;
	}

	public static System.Type getFoodTypeFromEnum(Food.Types type){

		switch(type){
			case Food.Types.Rock: return typeof(RockFood);
			case Food.Types.Plant: return typeof(PlantFood);
			case Food.Types.Golem: return typeof(GolemFood);
			case Food.Types.MagicEssence: return typeof(MagicEssenceFood);
		}
		return null;
	}

	public enum Types {
		Rock,
		Plant,
		Golem,
		MagicEssence
	}
}
