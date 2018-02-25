using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Puck : MonoBehaviour {

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
		InitBall();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitBall() {
		Vector3 direction = Vector3.zero;
		direction.x = Random.Range(-.8f, .8f);
		direction.y = Random.Range(-.8f, .8f);
		direction.z = (int)Random.Range(0, 2) * 2 - 1;
		Debug.Log("Ball direction: " + direction);
		direction.Normalize();

		rigidBody.velocity = Velocity = direction * initialSpeed;

	}

	void OnCollisionEnter(Collision collision) {
		Paddle paddle = collision.gameObject.GetComponent<Paddle>();
		Vector3 newDirection;
		if (paddle)
			newDirection = paddle.GetBounceDirection(collision);
		else {
			Vector3 collisonNormal = Vector3.zero;
			foreach (ContactPoint c in collision.contacts)
				collisonNormal += c.normal;
			collisonNormal /= collision.contacts.Length;

			newDirection = Vector3.Reflect(Velocity.normalized, collisonNormal);
		}

		float speed = Velocity.magnitude;
		speed += speedupOnHit;
		rigidBody.velocity = Velocity = newDirection * speed;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject);
		//TODO: keep track of score
		//reset d pozition.
		transform.position = new Vector3(0, 0, 0);
		InitBall();
	}
}
