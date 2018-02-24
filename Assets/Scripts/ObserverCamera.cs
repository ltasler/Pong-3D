using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCamera : MonoBehaviour {

	GameObject _puck;

	[SerializeField]
	float _rotateSpeed;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		_puck = GameObject.FindGameObjectWithTag("Puck");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetDir = _puck.transform.position - transform.position;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * _rotateSpeed);
	}
}
