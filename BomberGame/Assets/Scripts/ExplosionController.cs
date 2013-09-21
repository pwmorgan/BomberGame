using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {
	
	public int explosionRate;
	public int maxScale;
	
	private float _scale = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_scale += explosionRate * Time.deltaTime;
		transform.localScale = new Vector3(_scale, _scale, _scale);
		
		if (_scale > maxScale) {
			Destroy(gameObject);
		}
	}
}
