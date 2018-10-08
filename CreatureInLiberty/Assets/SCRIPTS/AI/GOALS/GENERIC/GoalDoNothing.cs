using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDoNothing : Goal
{

	public override void awakeInit() {
		
	}

    public override float computeDesirability() {
        return 0.3f;
    }

    public override bool follow() {
		
		agent.enqueueNextState("doNothing");
		return agent.doNextState();
    }

    public override void setupState(StateBehaviour state)
    {
        
    }
}
