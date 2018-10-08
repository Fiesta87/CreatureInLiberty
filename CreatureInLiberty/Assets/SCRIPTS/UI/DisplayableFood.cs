using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisplayableFood : DisplayableEntity {

    private Food food;

    // Use this for initialization
    void Awake () {
		this.food = GetComponent<Food>();
	}

    public override List<string> getInfos() {
        
        List<string> result = new List<string>();

        result.Add("Food type\t\t\t\t\t: " + food.ToString().Split(new Char [] {'(', ')' })[1]);

        result.Add("Remaning quantity\t: " + ((food.quantity > 0) ? ""+food.quantity : "Exhausted"));

        

        return result;
    }
}
