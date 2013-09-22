using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	//
	public bool Alive = true;
	public float PersonalSpace = 2;
	public float RunAwaySpeed = 10;
	public Transform animatedPlane;
	
	public GameObject BloodSplatterPrefab;
	
	private AnimationController _sprite;
	
	public AudioClip[] BloodSounds = new AudioClip[0];
	
	// Use this for initialization
	void Start () {
		_sprite = animatedPlane.GetComponent(typeof(AnimationController)) as AnimationController;
		transform.rotation = new Quaternion(0, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject player = GameObject.FindGameObjectsWithTag( "Player" )[0];
		float distance = Vector3.Distance(player.transform.position, transform.position);
		
		if (Alive) {
			if (distance < PersonalSpace) {
				Vector3 runAway = transform.position - player.transform.position;
				runAway.Normalize();
				runAway *= RunAwaySpeed * (PersonalSpace - distance);
				rigidbody.AddForce(runAway);
			}
		
			if (rigidbody.velocity.z != 0 || rigidbody.velocity.x != 0) {
				_sprite.SetAnimation(0);
				
				Vector3 localScale = animatedPlane.localScale;
				if (rigidbody.velocity.x < 0) {
					// Flip animation;
					localScale.x = -1 *  Mathf.Abs(localScale.x);
				} else if (rigidbody.velocity.x > 0) {
					// Unflip animation;
					localScale.x = Mathf.Abs(localScale.x);
				}
				animatedPlane.localScale = localScale;
			} else {
				_sprite.SetAnimation(2);
			}
		}
	}
	
	// Kill the enemy
	public void Kill(Vector3 explosion, int explosionForce) {
		Debug.Log( "Killed Enemy" );
		
		Alive = false;
		
		Vector3 distance = explosionForce * (transform.position - explosion) ;
		_sprite.SetAnimation(1);
		rigidbody.AddForce(distance);
		Splatter();
		//GameObject.Destroy( gameObject );
	}
	
	public void Splatter() {
		
		// pick sound to play
		AudioClip BloodSound = BloodSounds[ Random.Range( 0, BloodSounds.Length ) ];
		audio.clip = BloodSound;
		
		//ulong delay = (ulong) Mathf.RoundToInt( (44100 * Random.value) );
		//delay += 22050;
		audio.Play();
		
		// show blood
		Quaternion BloodRotation = transform.rotation;
		BloodRotation.y = Random.Range( 0, 360 );
		Instantiate( BloodSplatterPrefab, transform.position, BloodRotation );
	}
}
