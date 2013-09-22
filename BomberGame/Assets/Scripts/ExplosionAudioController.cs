using UnityEngine;
using System.Collections;

public class ExplosionAudioController : MonoBehaviour {
	
	public AudioClip[] ExplosionSounds = new AudioClip[0];
	
	// Use this for initialization
	void Start () {
		
		AudioClip ExplosionSound = ExplosionSounds[ Random.Range( 0, ExplosionSounds.Length ) ];
		audio.clip = ExplosionSound;
		
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
