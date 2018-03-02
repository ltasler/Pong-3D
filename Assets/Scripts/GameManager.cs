using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	Text _textP1;
	Text _textP2;

	string _scoreP1 = "0";
	string _scoreP2 = "0";

	const string SCENE_MAIN = "Main";

	void OnEnable() {
		Puck.OnPlayerScored += PlayerScored;
	}

	void OnDisable() {
		Puck.OnPlayerScored -= PlayerScored;
	}

	void Awake() {
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {

	}

	void OnLevelWasLoaded() {
		LoadScoreUi();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnGUI is only supposed to be used when in game
	/// </summary>
	void OnGUI() {
		if (_textP1 != null)
			_textP1.text = _scoreP1.ToString();
		if (_textP2 != null)
			_textP2.text = _scoreP2.ToString();
	}

	#region Menu Scene button clicks

	public void OnePlayer() {
		//Todo: Implement it!
	}

	public void TwoPlayers() {
		SceneManager.LoadScene(SCENE_MAIN);
	}

	public void Exit() {
#if UNITY_EDITOR 
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

#endregion

	private void PlayerScored(int player) {
		if (player == 1)
			_scoreP1 = (int.Parse(_scoreP1) + 1).ToString();
		else if (player == 2)
			_scoreP2 = (int.Parse(_scoreP2) + 1).ToString();
		else
			Debug.LogWarning("[Game Manager - PlayerScored] Uknown player number: " + player);

	}

	private void LoadScoreUi() {
		GameObject scoreUi = GameObject.FindGameObjectWithTag("ScoreUI");
		if (scoreUi == null)
			return;

		Text[] texts = scoreUi.GetComponentsInChildren<Text>();
		foreach(Text t in texts) {
			if (t.gameObject.name.Contains("Red"))
				_textP1 = t;
			else
				_textP2 = t;
		}

	}

}
