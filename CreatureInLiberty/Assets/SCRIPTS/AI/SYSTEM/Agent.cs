using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {

	public float goalUpdateDeltaTime = 2.0f;
	public Queue<string> statesToDo;
	public Goal[] goalsList;
	public float stubbornness = 1.0f;
	public float gluttony = 1.0f;
	public bool randomizeStartingMetabolismParameters = false;
	public int health = 100;
	public float hungriness = 0.0f;

	private Animator animator;
	private NavMeshAgent navAgent;
	private Goal currentGoal;
	private float nextGoalUpdateTimeStamp;
	public string currentState;
	private bool buzzy;
	[HideInInspector]
	public Perception perception;
	[HideInInspector]
	public Weapon weapon;
	[HideInInspector]
	public bool stopThinking = false;
	private Death death;
	private Vector3 previousPosition;

	void Awake () {
		this.animator = GetComponent<Animator>();
		this.navAgent = GetComponent<NavMeshAgent>();
		this.statesToDo = new Queue<string>();
		this.buzzy = false;
		this.perception = GetComponentInChildren<Perception>();
		this.weapon = GetComponentInChildren<Weapon>();
		this.currentState = "";
		this.death = GetComponent<Death>();
		if(randomizeStartingMetabolismParameters) {
			this.hungriness = Random.Range(0.0f, 1.0f);
		}
	}

	void Start () {
		previousPosition = this.transform.position;
	}

	void Update(){
		
		updateMetabolism();

		if(navAgent.velocity.magnitude > navAgent.speed / 1.5f && Vector3.Distance(previousPosition, transform.position) < 0.01f) {
			Debug.Log(gameObject.name + " IS STUCK : " + navAgent.velocity.magnitude + " " + Vector3.Distance(previousPosition, transform.position));
			// this.buzzy = false;
			// determineNextGoal();
			navAgent.velocity = Vector3.zero;
			walkTo(this.transform.position - this.transform.forward * 2.0f);
		}

		previousPosition = transform.position;

		if(Time.time >= nextGoalUpdateTimeStamp && !stopThinking) {
			nextGoalUpdateTimeStamp = Time.time + goalUpdateDeltaTime;
			determineNextGoal();
		}
	}

	private bool determineNextGoal() {

		if(currentGoal == null){
			this.buzzy = true;
			currentGoal = goalsList[goalsList.Length-1];
			Debug.Log(this.gameObject.name + " initial goal : " + currentGoal.ToString());
			return currentGoal.follow();
		}

		float desirability;
		float maxDesirability = 0.0f;
		Goal nextGoal = goalsList[0];

		foreach(Goal g in goalsList){

			desirability = g.computeDesirability();

			if(g.Equals(currentGoal)){
				desirability = /*Mathf.Min(*/desirability * stubbornness/*, 1.0f)*/;
			}

			if(desirability > maxDesirability){

				maxDesirability = desirability;
				nextGoal = g;
			}
		}

		if(!nextGoal.Equals(currentGoal) || !this.buzzy){
			this.buzzy = true;
			currentGoal = nextGoal;
			Debug.Log(this.gameObject.name + " follow a new Goal : " + currentGoal.ToString());
			return currentGoal.follow();
		}
		return true;
	}

	public bool doNextState() {
		// Debug.Log(this.gameObject.name + " : doNextState");
		if(statesToDo.Count > 0) {

			string state = statesToDo.Dequeue();

			if(currentState.Equals(state)){
				// Debug.Log(this.gameObject.name + " : same state : " + state);
				return true;
			} else {
				// Debug.Log(this.gameObject.name + " : new state : " + state);
				currentState = state;
				animator.SetTrigger(state);
				return false;
			}
		} else {
			// Debug.Log(this.gameObject.name + " : no more state to do");
			this.stopThinking = false;
			this.buzzy = false;
			return determineNextGoal();
		}
	}

	public Goal getCurrentGoal(){
		return currentGoal;
	}

	public void walkTo(Vector3 destination) {
		this.statesToDo.Clear();

		this.statesToDo.Enqueue("walkTo");

		navAgent.SetDestination(destination);
	}

	public bool isAtPosition(Vector3 position) {
		return Vector3.Distance(position, transform.position) <= navAgent.stoppingDistance;
	}

	public bool isAtDestination() {
		return (navAgent.destination - transform.position).magnitude <= navAgent.stoppingDistance;
	}

	public Vector3 getDestination() {
		return navAgent.destination;
	}

	public float distanceTo(Vector3 position) {
		return Vector3.Distance(this.transform.position, position);
	}

	public bool canCatchPreyIn(Agent prey, float attackDistance, float time) {

		// float currentDistance = Vector3.Distance(this.transform.position, prey.transform.position);

		// float relativeSpeed = (this.navAgent.velocity - prey.navAgent.velocity).magnitude;

		// Debug.Log("currentDistance : " + currentDistance);
		// Debug.Log("relativeSpeed : " + relativeSpeed);

		// return (currentDistance - attackDistance / time) / relativeSpeed <= time;

		float currentDistance = Vector3.Distance(this.transform.position, prey.transform.position);

		return currentDistance + prey.navAgent.velocity.magnitude * time < attackDistance;
	}

	public void wander(float minDistance, float maxDistance) {

		Vector3 randomDestination = 
			this.transform.position + 
			Quaternion.AngleAxis(45.0f * Random.Range(-1.0f, 1.0f), this.transform.up) * 
			this.transform.forward  * Random.Range(minDistance, maxDistance);

		 walkTo(randomDestination);
	}

	public void hunt(Agent prey) {
		this.statesToDo.Clear();
		
		this.statesToDo.Enqueue("hunt");

		navAgent.SetDestination(prey.transform.position);
	}

	public void stopSteeringBehaviour() {
		navAgent.isStopped = true;
		navAgent.ResetPath();
	}

	public void resumeSteeringBehaviour() {
		navAgent.isStopped = false;
	}

	public void setDestination(Vector3 destination) {
		navAgent.SetDestination(destination);
	}

	public void enqueueNextState(string state) {
		this.statesToDo.Enqueue(state);
	}

	public void takeDamage(int damage) {
		this.health -= damage;

		if(this.health <= 0) {
			this.health = 0;
			this.death.die(this);
		}
	}

	public bool isAlive() {
		return this.health > 0;
	}

	private void updateMetabolism() {
		this.hungriness = Mathf.Clamp(this.hungriness + 0.008f * Time.deltaTime, 0.0f, 1.0f);
	}
}
