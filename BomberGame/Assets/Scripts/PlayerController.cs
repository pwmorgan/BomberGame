using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public int speed;
	
	private Vector3 _velocity;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	
	void Update () {
		// Horizontal Movement
		float xVel = Input.GetAxis("Horizontal");
		float yVel = Input.GetAxis("Vertical");
		
		_velocity.x = xVel * speed * Time.fixedDeltaTime;
		
		// Vertical Movement
		_velocity.z = yVel * speed * Time.fixedDeltaTime;
		
		
		// Update position
		transform.Translate(_velocity);
		
		// Space Bar Explodes
			// Tell Game manager an explosion is starting.
			// Create an explosion at player location.
	}
}
