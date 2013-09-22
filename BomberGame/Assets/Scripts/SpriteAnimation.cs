using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	public Texture[] texture;
	public bool Loop = true;
	
	private int _frame = 0;
	private float _frameRate = 1f / 24f;
	private float _timer = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Reset() {
		_frame = 0;
		_timer = 0;
	}
	
	public void UpdateAnimation () {
		_timer += Time.deltaTime;
		
		if (_timer >= _frameRate) {
			_timer = 0;
			
			_frame++;
			if (_frame >= texture.Length) {
				if (Loop) {
					_frame = 0;
				} else {
					_frame--;
				}
			}
			
			renderer.material.SetTexture("_MainTex", texture[_frame]);
		}
	}
}
