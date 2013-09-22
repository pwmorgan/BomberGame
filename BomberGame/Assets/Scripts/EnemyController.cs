using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	//
	public bool Alive = true;
	public float PersonalSpace = 2;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject player = GameObject.FindGameObjectsWithTag( "Player" )[0];
		float distance = Vector3.Distance(player.transform.position, transform.position);
		
		if (distance < PersonalSpace) {
			Debug.Log("Invaded personal space!");
			Vector3 runAway = transform.position - player.transform.position;
			runAway *= 10;
			rigidbody.AddForce(runAway);
		}
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
