using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Paddle : MonoBehaviour {

	[SerializeField]
	[Range(1, 2)]
	int playerNumber;
	[SerializeField]
	float speed;

	CharacterController charController;


	void Awake() {
		charController = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal_P" + playerNumber) * speed * Time.deltaTime;
		float y = Input.GetAxis("Vertical_P" + playerNumber) * speed * Time.deltaTime;
		Vector3 motion = new Vector3(x, y, 0);

		charController.Move(motion);
	}

	public Vector3 GetBounceDirection(Collision collision) {
		Vector3 collisionLocation = Vector3.zero;
		foreach (ContactPoint c in collision.contacts)
			collisionLocation += c.point;
		collisionLocation /= collision.contacts.Length;
		collisionLocation = transform.InverseTransformPoint(collisionLocation);

		Vector3 newDirection = new Vector3(collisionLocation.x, .5f, collisionLocation.z).normalized;

		Debug.DrawRay(transform.TransformPoint(collisionLocation), transform.TransformDirection(transform.forward), Color.red, 10f);
		Debug.DrawRay(transform.TransformPoint(collisionLocation), transform.TransformDirection(newDirection), Color.blue, 10f);

		return transform.TransformDirection(newDirection);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, transform.TransformDirection(transform.forward));
	}
}
