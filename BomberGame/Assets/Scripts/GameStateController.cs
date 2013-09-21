using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	//
	public string[] EnemyTags = new string[1];
	
	//
	private GameObject[] Enemies = new GameObject[0];
	
	
	// Use this for initialization
	void Start () {
		Debug.Log( "GameStateController Initialized-------" );
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButtonDown(0)) {
			CheckEnemies();	
		}
		*/
	}
	
	// Method: CheckEnemies, sees who is alive or dead in the scene
	public void CheckEnemies() {
		Debug.Log( "CheckEnemies" );
		
		// loop through scene, identify enemies
		GameObject[] enemyObjects;
		enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log( "Total Enemies: " + enemyObjects.Length );
		
		/*
		// loop through and kill, to test
		for ( int i=0; i<enemyObjects.Length; i++) {
			Debug.Log( i );
			EnemyController ECScript =  enemyObjects[ i ].GetComponent<EnemyController>();
			
		}
		*/
		
		
	}
	
	// Method: SlowTime, slows timescale for explosion
	public void SlowTime( float t=0.1f ) {
		Debug.Log( "SlowTime" );
		
		// assign time scale
		// TODO: Tween this value
		Time.timeScale = t;
	}
	
	// Method: ResumeTime, speeds time back up to default
	public void ResumeTime( ) {
		Debug.Log( "ResumeTime" );
		
		// assign time scale
		// TODO: Tween this value
		Time.timeScale = 1.0f;
	}
}
