using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	//
	public string[] EnemyTags = new string[1];
	
	// Enemies game object array
	private GameObject[] Enemies = new GameObject[0];
	
	// Time tracking
	private float SlowTimeStart;
	private float SlowTimeDuration;
	private float SlowTimeElapsed = 0.0f;
	private bool IsTimeSlowed = false;
	
	
	// Use this for initialization
	void Start () {
		Debug.Log( "GameStateController Initialized-------" );
		
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
		
	}
	
	// Method: CheckEnemies, sees who is alive or dead in the scene
	public void CheckEnemies() {
		Debug.Log( "CheckEnemies" );
		
		// identify enemies
		Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log( "Total Enemies: " + Enemies.Length );
		
		// kill applicable enemies
		foreach (GameObject enemy in Enemies) {
			EnemyController ECScript = enemy.GetComponent<EnemyController>();
			
			// for now, kill the enemy
			// TODO: logic for whether or not kill the enemy 
			ECScript.Kill();
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
}
