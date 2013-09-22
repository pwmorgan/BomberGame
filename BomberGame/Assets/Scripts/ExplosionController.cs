using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {
	
	public Transform explosionPlane;
	public int explosionForce;
	public int explosionRate;
	public int maxScale;
	public float timeSlowDuration;
	public float timeSlowRate;
	public bool Alive = true;
	
	private GameStateController _gameController;
	
	private float _scale = 1;
	
	// Use this for initialization
	void Start () {
		GameObject gameControllerObj = GameObject.FindGameObjectsWithTag( "GameController" )[0];
		_gameController = gameControllerObj.GetComponent(typeof(GameStateController)) as GameStateController;
		_gameController.SlowTime(timeSlowDuration, timeSlowRate);
		CameraController camera = _gameController.LevelCamera.GetComponent(typeof(CameraController)) as CameraController;
		
		Vector3 planePosition = transform.position;
		planePosition.z += 3;
		planePosition.y += 1f;
		Instantiate(explosionPlane, planePosition, transform.rotation);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_scale > maxScale) {
			// Set this to die.
			Alive = false;
			
			// Tell GameController that an explosion died.
			_gameController.CheckExplosions();
			
			// Destroy object
			Destroy(gameObject);
		}
		
		_scale += explosionRate * Time.deltaTime;
		transform.localScale = new Vector3(_scale, 1f, _scale);
	}
	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			EnemyController enemy = collider.gameObject.GetComponent(typeof(EnemyController)) as EnemyController;
			
			if (enemy.Alive) {
				enemy.Kill(transform.position, explosionForce);
			}
		}
	}
}
