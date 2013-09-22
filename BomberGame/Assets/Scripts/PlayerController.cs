using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public int speed;
	public Transform explosion;
	
	private enum State {
		ALIVE,
		DEAD
	}
	private Vector3 _velocity;
	private CharacterController _controller;
	private GameStateController _gameController;
	private State _state;
	
	
	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController>();
		GameObject gameControllerObj = GameObject.FindGameObjectsWithTag( "GameController" )[0];
		
		_gameController = gameControllerObj.GetComponent(typeof(GameStateController)) as GameStateController;
		_state = State.ALIVE;
	}
	
	// Update is called once per frame
	
	void Update () {
		
		switch (_state) {
			case State.ALIVE:
				// Get input
				float xVel = Input.GetAxis("Horizontal");
				float yVel = Input.GetAxis("Vertical");
				
				// Horizontal Movement
				_velocity.x = xVel * speed * Time.fixedDeltaTime;
				
				// Vertical Movement
				_velocity.z = yVel * speed * Time.fixedDeltaTime;
				
				// Update position
				_controller.Move(_velocity);
				
				// Create Explosion
				if (Input.GetButtonDown("Detonate")) {
					// Create an explosion at player location.
		            Instantiate(explosion, transform.position, transform.rotation);
					_gameController.SlowTime(0.2f, 0.2f);
					_state = State.DEAD;
				}
			
				break;
			
			case State.DEAD:
			
				break;
			
			default:
			
				// Something is amiss
				break;
			
		}
	}
}
