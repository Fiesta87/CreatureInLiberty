using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisplayableAgent : DisplayableEntity {

    private Agent agent;

    // Use this for initialization
    void Awake () {
		this.agent = GetComponent<Agent>();
	}

    public override List<string> getInfos() {
        
        List<string> result = new List<string>();

        if(agent.isAlive()) {

            result.Add("Current goal\t\t: " + agent.getCurrentGoal().ToString().Split(new Char [] {'(', ')' })[1].Substring(4));

            result.Add("Health\t\t\t\t: " + agent.health);

            result.Add("Hungriness\t\t: " + agent.hungriness);

        } else {

            result.Add("Dead");

        }

        

        return result;
    }
}
