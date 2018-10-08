using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisplayableEntity : MonoBehaviour {

	public abstract List<string> getInfos();

	void OnMouseDown(){
		ScreenInfoDisplay.Instance.setInfos(this);
	}
}
