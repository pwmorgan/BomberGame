﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	//
	public bool Alive = true;
	public float PersonalSpace = 2;
	public float RunAwaySpeed = 10;
	
	public AudioClip[] BloodSounds = new AudioClip[0];
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject player = GameObject.FindGameObjectsWithTag( "Player" )[0];
		float distance = Vector3.Distance(player.transform.position, transform.position);
		
		if (distance < PersonalSpace) {
			Vector3 runAway = transform.position - player.transform.position;
			runAway.Normalize();
			runAway *= RunAwaySpeed * (PersonalSpace - distance);
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
	
	public void Splatter() {
		
		// pick sound to play
		AudioClip BloodSound = BloodSounds[ Random.Range( 0, BloodSounds.Length ) ];
		audio.clip = BloodSound;
		
		ulong delay = (ulong) Mathf.RoundToInt( (44100 * Random.value) );
		delay += 22050;
		audio.Play( delay );
	}
}
