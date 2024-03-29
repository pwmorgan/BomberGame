﻿using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	// inspector properties
	public Camera LevelCamera;
	public string[] EnemyTags = new string[1];
	public int LevelDuration = 30;
	public int KillsRequired = 0;
	public string NextLevelName = "MainMenu";
	public GameObject ScorchMark;
	public GameObject BloodSplatter;
	public GUIStyle RemainingTimeGUIStyle = new GUIStyle();
	public GUIStyle EndSequenceGUIStyle = new GUIStyle();
	public GUIStyle EndSequenceButtonStyle = new GUIStyle();
	
	// Entitiies game object arrays
	private GameObject[] Enemies = new GameObject[0];
	private GameObject[] Explosions = new GameObject[0];
	
	private Transform PlayerDeathTransform;
	
	// Slow Motion Time tracking
	private float CurrentTimeScale;
	private float SlowTimeStart;
	private float SlowTimeDuration;
	private float SlowTimeElapsed = 0.0f;
	private bool IsTimeSlowed = false;
	
	// Level Duration tracking
	private float LevelStartTime;
	private float RemainingTime;
	private Rect RemainingTimeRect;
	private Rect MinKillsRect;
	private float RemainingTimeWidth = 300.0f;
	private float RemainingTimeHeight = 25.0f;
	private string DisplayTime;
	private bool IsRemainingTimeDisplayed = true;
	
	// End of Level tracking
	private bool IsLevelActive = true;
	private Rect EndSequenceRect;
	private float EndSequenceWidth = 600.0f;
	private float EndSequenceHeight = 300.0f;
	private float EndSequenceSpacer = 10.0f;
	private float EndSequenceButtonHeight = 55.0f;
	private float EndSequenceDelay = 2.0f;
	private float EndSequencePauseDuration = 0.0f;
	private float EndSequenceStartTime;
	
	// Score tracking
	private int TotalKills = 0;
	
	
	// Use this for initialization
	void Start () {
		Debug.Log( "GameStateController Initialized-------" );
		
		// setup GUI rect's
		MinKillsRect = new Rect( Screen.width/2 - (RemainingTimeWidth/2), 10, RemainingTimeWidth, RemainingTimeHeight );
		RemainingTimeRect = new Rect( Screen.width/2 - (RemainingTimeWidth/2), 12+RemainingTimeHeight, RemainingTimeWidth, RemainingTimeHeight ); 
		EndSequenceRect = new Rect( Screen.width / 2 - (EndSequenceWidth/2), Screen.height/2 - (EndSequenceHeight/2), EndSequenceWidth, EndSequenceHeight );
		
		// assign remaining time
		RemainingTime = LevelDuration;
		LevelStartTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// 'r' to reset
		if (Input.GetButtonDown("Reset")) {
			RestartLevel();
		}
		
		// is time slowed down?
		// this is for bullet time duration
		if (IsTimeSlowed) {
			SlowTimeElapsed = Time.time - SlowTimeStart;
			
			//Debug.Log( "SlowTimeElapsed: " + SlowTimeElapsed );
			if (SlowTimeElapsed >= SlowTimeDuration) {
				ResumeTime();
			}
		}
		
		// update remaining time, if it's being displayed
		if (IsRemainingTimeDisplayed) {
			if (RemainingTime >= 0) {
				RemainingTime = LevelDuration - ( Time.time - LevelStartTime );
				DisplayTime = "TIME: " + Mathf.Round(RemainingTime);
			} else {
				if (IsLevelActive) {
					// end the level
					TimeExpired();
				}
			}
		}
	}
	
	
	// draw gui
	void OnGUI() {
		
		if ( IsRemainingTimeDisplayed ) {
			
			// draw remaining time
			//GUI.Box( RemainingTimeRect, "TEST", FloatingBoxGUIStyle );
			GUI.Label( RemainingTimeRect, DisplayTime, RemainingTimeGUIStyle );
			
			// draw kills threshold
			GUI.Label( MinKillsRect, "KILLS NEEDED: " + KillsRequired, RemainingTimeGUIStyle );
			
		}
		
		if (!IsLevelActive) {
			EndSequencePauseDuration = Time.time - EndSequenceStartTime;
			if (EndSequencePauseDuration >= EndSequenceDelay ) {
				// LEVEL PASSED
				if (TotalKills >= KillsRequired ) {
					GUI.BeginGroup( EndSequenceRect );
						GUI.Label( new Rect( 0, 0, EndSequenceWidth, 30 ), "LEVEL PASSED!", EndSequenceGUIStyle );
						GUI.Label( new Rect( 0, 32, EndSequenceWidth, 30 ), "KILLS: " + TotalKills, EndSequenceGUIStyle );
					
						if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + (EndSequenceSpacer*2) ), EndSequenceWidth, EndSequenceButtonHeight ), "REPLAY", EndSequenceButtonStyle ) ) {
							RestartLevel();
						}
						if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + (EndSequenceSpacer*2)) * 2, EndSequenceWidth, EndSequenceButtonHeight ), "NEXT LEVEL", EndSequenceButtonStyle ) ) {
							NextLevel();
						}
						if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + (EndSequenceSpacer*2)) * 3, EndSequenceWidth, EndSequenceButtonHeight ), "MAIN MENU", EndSequenceButtonStyle ) ) {
							QuitToMenu();
						}
					GUI.EndGroup();
					
				// LEVEL FAILED
				} else {
					GUI.BeginGroup( EndSequenceRect );
						GUI.Label( new Rect( 0, 0, EndSequenceWidth, 30 ), "LEVEL FAILED!", EndSequenceGUIStyle );
						GUI.Label( new Rect( 0, 32, EndSequenceWidth, 30 ), "KILLS: " + TotalKills + " / " + KillsRequired, EndSequenceGUIStyle );
					
						if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + (EndSequenceSpacer*2)), EndSequenceWidth, EndSequenceButtonHeight ), "REPLAY", EndSequenceButtonStyle ) ) {
							RestartLevel();
						}
						if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + (EndSequenceSpacer*2)) * 2, EndSequenceWidth, EndSequenceButtonHeight ), "MAIN MENU", EndSequenceButtonStyle ) ) {
							QuitToMenu();
						}
					GUI.EndGroup();
				}
			}
			
		}
		
	}
	
	// Method: CancelLevelTimer, removes the timer from the level
	public void CancelLevelTimer() {
		Debug.Log( "CancelLevelTimer" );
		
		IsRemainingTimeDisplayed = false;
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
			
			if ( !ECScript.Alive ) {
				TotalKills ++;
			}
		}
		Debug.Log( "Total Kills: " + TotalKills );
		
		// end sequence
		EndSequenceStartTime = Time.time;
		
	}
	
	// Method: CheckExplosions, see is the explosions are finished
	public void CheckExplosions() {
		Debug.Log( "CheckExplosions" );
		
		int ExplosionsAlive = 0;
		
		// identify explosions
		Explosions = GameObject.FindGameObjectsWithTag( "Explosion" );
		Debug.Log( "Total Explosions: " + Explosions.Length );
		
		// see if explosion is done
		foreach (GameObject explosion in Explosions) {
			ExplosionController ECScript = explosion.GetComponent<ExplosionController>();
			
			if ( ECScript.Alive ) {
				ExplosionsAlive ++;
			}
		}
		
		if (ExplosionsAlive == 0) {
			IsLevelActive = false;
			CheckEnemies();
		}
		
	}
	
	// Method: SlowTime, slows timescale for explosion
	public void SlowTime( float dur=0.2f, float min=0.1f  ) {
		Debug.Log( "SlowTime" );
		
		// assign time scale
		// TODO: Tween this value
		//Time.timeScale = min;
		iTween.ValueTo( gameObject, iTween.Hash("from", Time.timeScale, "to", min, "time", 0.25f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "OnUpdateSlowTimeTween", "oncomplete", "OnSlowTimeComplete" ) );
		
		// Track duration
		SlowTimeStart = Time.time;
		SlowTimeDuration = dur;
		//IsTimeSlowed = true;
		
	}
	
	private void OnUpdateSlowTimeTween( float ts ) {
		
		Time.timeScale = ts;
	}
	
	private void OnSlowTimeComplete( ) {
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
		
		// camera shake
		CameraController CCScript = LevelCamera.GetComponent<CameraController>();
		CCScript.Shake();
		
		
		// player ref
		GameObject Player = GameObject.FindGameObjectWithTag( "Player" );
		
		// explosion sfx
		//Explosion.audio.Play();
		
		// scorch mark
		PlayerDeathTransform = Player.transform;
		Instantiate(ScorchMark, PlayerDeathTransform.position, transform.rotation);

		
	}
	
	// Method: TimeExpired, fired when timer reaches 0
	private void TimeExpired() {
		Debug.Log( "TimeExpired");
		
		//IsLevelActive = false;
		
		// Explode the player
		GameObject Player = GameObject.FindGameObjectWithTag( "Player" );
		PlayerController PCScript = Player.GetComponent<PlayerController>();
		PCScript.Explode();
		
	}
	
	// Method: EndLevel, ends the level and provides the user options
	private void EndLevel() {
		Debug.Log( "EndLevel" );
		
		IsLevelActive = false;
	}
	
	// Method: NextLevel, load the next level
	private void NextLevel() {
		Debug.Log( "NextLevel" );
		
		if ( NextLevelName == "" ) {
			Application.LoadLevel( Application.loadedLevel );
		} else {
			Application.LoadLevel( NextLevelName );
		}
		
		
	}
	
	// Method: RestartLevel, reloads the current level
	public void RestartLevel() {
		Debug.Log( "RestartLevel" );
		
		Application.LoadLevel( Application.loadedLevel );
	}
	
	// Method: QuitToMenu, quits the level and takes the user to the main menu scene
	private void QuitToMenu() {
		Debug.Log( "QuitToMenu" );	
		
		Application.LoadLevel( "MainMenu" );
	}
}
