using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public int speed;
	
	private Vector3 _velocity;
	private CharacterController _controller;
	
	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	
	void Update () {
		// Get input
		float xVel = Input.GetAxis("Horizontal");
		float yVel = Input.GetAxis("Vertical");
		
		// Horizontal Movement
		_velocity.x = xVel * speed * Time.fixedDeltaTime;
		
		// Vertical Movement
		_velocity.z = yVel * speed * Time.fixedDeltaTime;
		
		// Update position
		_controller.Move(_velocity);
		
		// Space Bar Explodes
			// Tell Game manager an explosion is starting.
			// Create an explosion at player location.
	}
}
