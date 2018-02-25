using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	[SerializeField]
	Text _textP1;
	[SerializeField]
	Text _textP2;

	string _scoreP1 = "0";
	string _scoreP2 = "0";

	void OnEnable() {
		Puck.OnPlayerScored += PlayerScored;
	}

	void OnDisable() {
		Puck.OnPlayerScored += PlayerScored;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		if (_textP1 != null)
			_textP1.text = _scoreP1.ToString();
		else
			Debug.Log("[Game Manager - OnGUI] No text prefab for player 1");
		if (_textP2 != null)
			_textP2.text = _scoreP2.ToString();
		else
			Debug.Log("[Game Manager - OnGUI] No text prefab for player 2");
	}

	private void PlayerScored(int player) {
		int socreP2 = int.Parse(_scoreP2);
		if (player == 1)
			_scoreP1 = (int.Parse(_scoreP1) + 1).ToString();
		else if (player == 2)
			_scoreP2 = (int.Parse(_scoreP2) + 1).ToString();
		else
			Debug.LogWarning("[Game Manager - PlayerScored] Uknown player number: " + player);

	}

}
