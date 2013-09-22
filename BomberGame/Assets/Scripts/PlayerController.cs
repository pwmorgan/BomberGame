using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public int speed;
	public Transform explosion;
	public Transform animatedPlane;
	public GameObject ScorchMark;
	
	private enum State {
		ALIVE,
		DEAD
	}
	private Vector3 _velocity;
	private CharacterController _controller;
	private GameStateController _gameController;
	private State _state;
	private AnimationController _sprite;
	
	
	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController>();
		GameObject gameControllerObj = GameObject.FindGameObjectsWithTag( "GameController" )[0];
		
		_gameController = gameControllerObj.GetComponent(typeof(GameStateController)) as GameStateController;
		_state = State.ALIVE;
		_sprite = animatedPlane.GetComponent(typeof(AnimationController)) as AnimationController;
	}
	
	// Update is called once per frame
	
	void Update () {
		// TODO: If timer runs out, disable input
		
		
		switch (_state) {
			case State.ALIVE:
				// Get input
				float xVel = Input.GetAxis("Horizontal");
				float yVel = Input.GetAxis("Vertical");
				
				// Horizontal Movement
				_velocity.x = xVel * speed * Time.fixedDeltaTime;
			
				// Vertical Movement
				_velocity.z = yVel * speed * Time.fixedDeltaTime;
				
			
			
				// Z is up, X is right
				// Up Left Right Down
				if (_velocity.x > 0) {
					if (_velocity.z > _velocity.x) {
						// Set to Up Anim
						_sprite.SetAnimation(0);
					} else {
						// Set to Right Anim
						_sprite.SetAnimation(2);
					}
				} else if (_velocity.x < 0){	
					if (_velocity.z < _velocity.x) {
						// Set to Down Anim
						_sprite.SetAnimation(3);
					} else {
						// Set to Left Anim
						_sprite.SetAnimation(1);
					}
				} else {
					if (_velocity.z < 0) {
						// Set to Down Anim
						_sprite.SetAnimation(3);
					
					} else if (_velocity.z > 0) {
						// Set to Up Anim
						_sprite.SetAnimation(0);
					
					} else {
						// Set Idle Animation
					}
				}
			
				// Update position
				_controller.Move(_velocity);
				
				// Create Explosion
				if (Input.GetButtonDown("Detonate")) {
				
					_gameController.CancelLevelTimer();
					Explode();
					
					
				}
			
				break;
			
			case State.DEAD:
				
				break;
			
			default:
			
				// Something is amiss
				break;
			
		}
	}
	
	public void Explode() {
	
		if (_state == State.ALIVE) {
			// Create an explosion at player location.
			Instantiate(explosion, transform.position, transform.rotation);
			
			_state = State.DEAD;
			Destroy( animatedPlane.gameObject );
		}
	}
}
