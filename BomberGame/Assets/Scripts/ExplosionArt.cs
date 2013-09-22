using UnityEngine;
using System.Collections;

public class ExplosionArt : MonoBehaviour {
	AnimationController _sprite;
	
	// Use this for initialization
	void Start () {
		_sprite = gameObject.GetComponent(typeof(AnimationController)) as AnimationController;
	}
	
	// Update is called once per frame
	void Update () {
		if (_sprite.GetLoopCount() >= 1) {
			Destroy(gameObject);
		}
	}
}
