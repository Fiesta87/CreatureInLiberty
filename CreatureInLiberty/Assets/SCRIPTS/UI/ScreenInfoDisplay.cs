using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenInfoDisplay : MonoBehaviour {

	public static ScreenInfoDisplay Instance;

	private Text text;
	private DisplayableEntity displayable;
	private bool entityClicked;

	void Awake () {
		this.text = GetComponent<Text>();
		ScreenInfoDisplay.Instance = this;
		this.entityClicked = false;
	}

	void Update() {
		if(Input.GetMouseButtonDown(0) && !this.entityClicked) {
			clearInfos();
		}
	}

	void LateUpdate() {
		if(ScreenInfoDisplay.Instance.displayable != null) {
			showInfos();
			this.entityClicked = false;
		}
	}

	public void setInfos(DisplayableEntity displayable) {

		ScreenInfoDisplay.Instance.displayable = displayable;
		this.entityClicked = true;
	}

	public void clearInfos() {
		
		ScreenInfoDisplay.Instance.displayable = null;
		ScreenInfoDisplay.Instance.text.text = "";
	}

	private void showInfos() {
		ScreenInfoDisplay.Instance.text.text = displayable.gameObject.name;

		List<string> list = displayable.getInfos();
		
		foreach(string s in list) {
			ScreenInfoDisplay.Instance.text.text += "\n" + s;
		}
	}
}
