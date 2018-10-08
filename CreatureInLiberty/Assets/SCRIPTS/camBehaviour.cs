using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camBehaviour : MonoBehaviour {

	public Transform target;
	public float speed = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		speed += Input.GetAxis("Mouse ScrollWheel") * 3.0f;

		speed = Mathf.Max(speed, 1.0f);

		if(Input.GetKey(KeyCode.Z)){
			this.transform.position += this.transform.forward * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			this.transform.position -= this.transform.forward * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q)){
			this.transform.position -= this.transform.right * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			this.transform.position += this.transform.right * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Space)){
			this.transform.position += this.transform.up * speed * Time.deltaTime;
		}
		this.transform.LookAt(target);
	}
}
