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
	public void Kill(Vector3 explosion, int explosionForce) {
		Debug.Log( "Killed Enemy" );
		
		Alive = false;
		
		Vector3 distance = explosionForce * (transform.position - explosion) ;
		
		rigidbody.AddForce(distance);
		
		//GameObject.Destroy( gameObject );
	}
}
