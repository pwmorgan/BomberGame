using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	// inspector properties
	public Camera LevelCamera;
	public string[] EnemyTags = new string[1];
	public int LevelDuration = 30;
	public string NextLevelName = "MainMenu";
	public GUIStyle RemainingTimeGUIStyle = new GUIStyle();
	public GUIStyle EndSequenceGUIStyle = new GUIStyle();
	
	// Entitiies game object arrays
	private GameObject[] Enemies = new GameObject[0];
	private GameObject[] Explosions = new GameObject[0];
	
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
	private float RemainingTimeWidth = 100.0f;
	private float RemainingTimeHeight = 50.0f;
	private string DisplayTime;
	private bool IsRemainingTimeDisplayed = true;
	
	// End of Level tracking
	private bool IsLevelActive = true;
	private Rect EndSequenceRect;
	private float EndSequenceWidth = 600.0f;
	private float EndSequenceHeight = 300.0f;
	private float EndSequenceSpacer = 10.0f;
	private float EndSequenceButtonHeight = 25.0f;
	
	// Score tracking
	private int TotalKills = 0;
	
	
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
			CameraController CCScript = LevelCamera.GetComponent<CameraController>();
			CCScript.Shake();
		}
		*/
		
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
				DisplayTime = "" + Mathf.Round(RemainingTime);
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
			GUI.Label( RemainingTimeRect, DisplayTime, RemainingTimeGUIStyle );
			
		}
		
		if (!IsLevelActive) {
			GUI.BeginGroup( EndSequenceRect );
				GUI.Label( new Rect( 0, 0, EndSequenceWidth, EndSequenceButtonHeight ), "TOTAL KILLS: " + TotalKills, EndSequenceGUIStyle );
			
				if (GUI.Button( new Rect( 0, EndSequenceButtonHeight + EndSequenceSpacer, EndSequenceWidth, 25 ), "REPLAY" ) ) {
					RestartLevel();
				}
				if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + EndSequenceSpacer) * 2, EndSequenceWidth, 25 ), "NEXT LEVEL" ) ) {
					NextLevel();
				}
				if (GUI.Button( new Rect( 0, (EndSequenceButtonHeight + EndSequenceSpacer) * 3, EndSequenceWidth, 25 ), "QUIT GAME" ) ) {
					QuitToMenu();
				}
			GUI.EndGroup();
			
		}
		
	}
	
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
	}
	
	// Method: TimeExpired, fired when timer reaches 0
	private void TimeExpired() {
		Debug.Log( "TimeExpired");
		
		CheckEnemies();
		
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
		
		Application.LoadLevel( "MainMenu" );
	}
	
	// Method: RestartLevel, reloads the current level
	private void RestartLevel() {
		Debug.Log( "RestartLevel" );
		
		Application.LoadLevel( Application.loadedLevel );
	}
	
	// Method: QuitToMenu, quits the level and takes the user to the main menu scene
	private void QuitToMenu() {
		Debug.Log( "QuitToMenu" );	
		
		Application.LoadLevel( "MainMenu" );
	}
}
