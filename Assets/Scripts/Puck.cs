using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Puck : MonoBehaviour {

	public delegate void PlayerScored(int player);
	public static event PlayerScored OnPlayerScored;

	[SerializeField]
	float initialSpeed = 10f;
	[SerializeField]
	float speedupOnHit = .5f;

	Rigidbody rigidBody;

	//it's required for bouncing
	public Vector3 Velocity {get;set;}

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(Respawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		Paddle paddle = collision.gameObject.GetComponent<Paddle>();
		Vector3 newDirection;
		float speed = Velocity.magnitude;
		if (paddle) {
			newDirection = paddle.GetBounceDirection(collision);
			speed += speedupOnHit;
		} else {
			Vector3 collisonNormal = Vector3.zero;
			foreach (ContactPoint c in collision.contacts)
				collisonNormal += c.normal;
			collisonNormal /= collision.contacts.Length;

			newDirection = Vector3.Reflect(Velocity.normalized, collisonNormal);
		}
		rigidBody.velocity = Velocity = newDirection * speed;
	}

	void OnTriggerEnter(Collider other) {
		BroadcastPlayerScored(other);
		StartCoroutine(Respawn());
	}

	private IEnumerator Respawn() {
		transform.position = new Vector3(0, 0, 0);
		rigidBody.velocity = Velocity = Vector3.zero;
		yield return new WaitForSeconds(1);
		InitBall();

	}

	private void BroadcastPlayerScored(Collider other) {
		if (OnPlayerScored == null)
			return;
		int player = 0;
		string otherName = other.gameObject.name;
		if (otherName.Contains("P1"))
			player = 1;
		else if (otherName.Contains("P2"))
			player = 2;
		OnPlayerScored(player);
	}

	private void InitBall() {
		Vector3 direction = Vector3.zero;
		direction.x = Random.Range(-.8f, .8f);
		direction.y = Random.Range(-.8f, .8f);
		direction.z = (int)Random.Range(0, 2) * 2 - 1;
		direction.Normalize();
		Debug.Log("Ball direction: " + direction);

		rigidBody.velocity = Velocity = direction * initialSpeed;

	}
}
