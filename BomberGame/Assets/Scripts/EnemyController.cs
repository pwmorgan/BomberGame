using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	//
	public bool Alive = true;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Kill the enemy
	public void Kill() {
		Debug.Log( "Killed Enemy" );
		
		Alive = false;
		
		GameObject.Destroy( gameObject );
	}
}
