using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Paddle : MonoBehaviour {

	[SerializeField]
	[Range(1, 2)]
	int _playerNumber;
	[SerializeField]
	float _speed;
	[SerializeField]
	AiDifficulity _aiDifficulity;


	CharacterController _charController;

	Puck _puck;
	Vector3 _lookDirection;
	Vector3 _startPosition;

	//for very hard ai
	Vector3 _positionOffset = Vector3.zero;
	bool _newPositionOffset;

	void Awake() {
		_charController = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		_puck = GameObject.FindGameObjectWithTag("Puck").GetComponent<Puck>();
		_lookDirection = transform.TransformDirection(transform.forward);
		_startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (_aiDifficulity == AiDifficulity.None) {
			float x = Input.GetAxis("Horizontal_P" + _playerNumber);
			float y = Input.GetAxis("Vertical_P" + _playerNumber);
			Move(x, y);
		} else {
			MoveAi();
		}
	}
	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, _lookDirection);
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

	private void Move(float x, float y) {
		x *= _speed * Time.deltaTime;
		y *= _speed * Time.deltaTime;
		_charController.Move(new Vector3(x, y, 0));
	}

	private void MoveAi() {
		Vector3 targetPosition;
		//Pomeni da gre žoga stran od ploščka --> go back to start
		if (Mathf.Sign(_lookDirection.z) == Mathf.Sign(_puck.Velocity.z)) {
			targetPosition = _startPosition;
			if (_aiDifficulity == AiDifficulity.Hard && !_newPositionOffset) { //hard ai nas poskuša zajebat
				_positionOffset = new Vector3(Random.Range(-.4f,.4f), Random.Range(-.4f,.4f));
				Debug.Log("[Paddle - MoveAi] Position offset is " + _positionOffset);
				_newPositionOffset = true;
			}
		} else {
			targetPosition = _puck.transform.position + _positionOffset;
			_newPositionOffset = false;
		}

		float deltaX = targetPosition.x - transform.position.x;
		float deltaY = targetPosition.y - transform.position.y;

		float modifier = AiDifficulityExtension.SpeedModifier(_aiDifficulity);

		float x = 0,
			y = 0;
		//if (Mathf.Abs(deltaX) > .5f)
			x = Mathf.Clamp(deltaX, -1, 1) * modifier;
		//if (Mathf.Abs(deltaY) > .5f)
			y = Mathf.Clamp(deltaY, -1, 1) * modifier;
		
		Move(x, y);
	}
}
