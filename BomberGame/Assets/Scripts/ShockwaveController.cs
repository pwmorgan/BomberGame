using UnityEngine;
using System.Collections;

public class ShockwaveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		iTween.FadeTo( gameObject, iTween.Hash( "alpha", 0.2f, "time", 0.2f, "easetype", iTween.EaseType.easeInCubic ) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
