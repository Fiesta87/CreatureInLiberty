using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEssenceFood : Food {

    public override bool eat(float quantityEatted){
		this.quantity -= quantityEatted;
        this.transform.localScale = Vector3.one * Mathf.Max(0.01f, this.quantity / this.initialQuantity);
		if(quantity <= 0.0f) {
			this.gameObject.SetActive(false);
			return true;
		}
		return false;
	}
}
