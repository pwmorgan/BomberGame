using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	// Inspector variables
	public float ShakeIntensity = 1.0f;
	public float ShakeDuration = 0f;
	
	//
	private Transform OriginalTransform;
	
	
	// Use this for initialization
	void Start () {
		
		// store original transform - we need this for post-shake resetting
		OriginalTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Shake() {
		OriginalTransform = gameObject.transform;
		iTween.ShakePosition( gameObject, iTween.Hash ( "x", ShakeIntensity * 0.1f, "z", ShakeIntensity * 0.1f, "y", 0, "duration", ShakeDuration, "oncomplete", "OnShakeComplete", "oncompletetarget", gameObject) );
	}
	
	private void OnShakeComplete( ) {
		Debug.Log( "OnShakeComplete" );
		gameObject.transform.position = new Vector3( OriginalTransform.position.x, OriginalTransform.position.y, OriginalTransform.position.z );
	}
			
}
