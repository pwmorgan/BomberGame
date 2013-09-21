﻿using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	// inspector properties
	public string[] EnemyTags = new string[1];
	public int LevelDuration = 30;
	public GUIStyle RemainingTimeGUIStyle = new GUIStyle();
	public GUIStyle EndSequenceGUIStyle = new GUIStyle();
	
	// Enemies game object array
	private GameObject[] Enemies = new GameObject[0];
	
	// Slow Motion Time tracking
	private float SlowTimeStart;
	private float SlowTimeDuration;
	private float SlowTimeElapsed = 0.0f;
	private bool IsTimeSlowed = false;
	
	// Level Duration tracking
	private float LevelStartTime;
	private float RemainingTime;
	private Rect RemainingTimeRect;
	private float RemainingTimeWidth = 100.0f;
	private float RemainingTimeHeight = 50.0f;
	private string DisplayTime;
	
	// End of Level tracking
	private bool IsLevelActive = true;
	private Rect EndSequenceRect;
	private float EndSequenceWidth = 600.0f;
	private float EndSequenceHeight = 300.0f;
	private float EndSequenceSpacer = 10.0f;
	
	
	// Use this for initialization
	void Start () {
		Debug.Log( "GameStateController Initialized-------" );
		
		// setup GUI rect's
		RemainingTimeRect = new Rect( Screen.width / 2 - (RemainingTimeWidth/2), 0, RemainingTimeWidth, RemainingTimeHeight ); 
		EndSequenceRect = new Rect( Screen.width / 2 - (EndSequenceWidth/2), Screen.height/2 - (EndSequenceHeight/2), EndSequenceWidth, EndSequenceHeight );
		
		// assign remaining time
		RemainingTime = LevelDuration;
		LevelStartTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Mouse click, for debuggin only
		/*
		if (Input.GetMouseButtonDown(0)) {
			SlowTime( );	
		}*/
		
		// is time slowed down?
		// this is for bullet time duration
		if (IsTimeSlowed) {
			SlowTimeElapsed = Time.time - SlowTimeStart;
			
			Debug.Log( "SlowTimeElapsed: " + SlowTimeElapsed );
			if (SlowTimeElapsed >= SlowTimeDuration) {
				ResumeTime();
			}
		}
		
		// update remaining time
		if (RemainingTime >= 0) {
			RemainingTime = LevelDuration - ( Time.time - LevelStartTime );
			DisplayTime = "" + Mathf.Round(RemainingTime);
		} else {
			// end the level
			TimeExpired();
		}
		
	}
	
	
	// draw gui
	void OnGUI() {
		
		if ( IsLevelActive ) {
		
			// draw remaining time
			GUI.Label( RemainingTimeRect, DisplayTime, RemainingTimeGUIStyle );
			
		} else {
			GUI.BeginGroup( EndSequenceRect );
				GUI.Label( new Rect( 0, 0, EndSequenceWidth, 25 ), "LEVEL COMPLETE", EndSequenceGUIStyle );
			GUI.EndGroup();
			
		}
		
	}
	
	// Method: CheckEnemies, sees who is alive or dead in the scene
	public void CheckEnemies() {
		Debug.Log( "CheckEnemies" );
		
		// identify enemies
		// TODO: loop through enemy tags array for applicable objects
		Enemies = GameObject.FindGameObjectsWithTag( EnemyTags[0] );
		Debug.Log( "Total Enemies: " + Enemies.Length );
		
		// see if enemy is dead
		foreach (GameObject enemy in Enemies) {
			EnemyController ECScript = enemy.GetComponent<EnemyController>();
			
		}
		
	}
	
	// Method: SlowTime, slows timescale for explosion
	public void SlowTime( float dur=0.2f, float min=0.1f  ) {
		Debug.Log( "SlowTime" );
		
		// assign time scale
		// TODO: Tween this value
		Time.timeScale = min;
		
		// Track duration
		SlowTimeStart = Time.time;
		SlowTimeDuration = dur;
		IsTimeSlowed = true;
		
	}
	
	// Method: ResumeTime, speeds time back up to default
	private void ResumeTime( ) {
		Debug.Log( "ResumeTime" );
		
		// assign time scale
		// TODO: Tween this value
		Time.timeScale = 1.0f;
		
		// Tracking
		IsTimeSlowed = false;
	}
	
	// Method: TimeExpired, fired when timer reaches 0
	private void TimeExpired() {
		Debug.Log( "TimeExpired");
		
		IsLevelActive = false;
	}
	
	// Method: EndLevel, ends the level and provides the user options
	private void EndLevel() {
		Debug.Log( "EndLevel" );
		
		IsLevelActive = false;
	}
	
	// Method: NextLevel, load the next level
	private void NextLevel() {
		Debug.Log( "NextLevel" );
	}
	
	// Method: RestartLevel, reloads the current level
	private void RestartLevel() {
		Debug.Log( "RestartLevel" );
	}
}
