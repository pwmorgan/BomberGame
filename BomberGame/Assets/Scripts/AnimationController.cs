using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	
	public SpriteAnimation[] animations;
	private int _id;
	
	// Use this for initialization
	void Start () {
	}
	
	public void SetAnimation(int id) {
		_id = id;
		if (id != _id) {
			animations[_id].Reset();
		}
	}
	
	// Update is called once per frame
	void Update () {
		animations[_id].UpdateAnimation();
	}
}
